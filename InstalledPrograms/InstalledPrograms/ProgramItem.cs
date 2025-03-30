using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InstalledPrograms;
internal class ProgramItem
{
    public bool userlocal;
    public string key;
    public string displayName = "[Loading]";
    public string? uninstallPath = null;
    public bool IsUninstallable => uninstallPath != null;
    public string installSize = "Loading";
    public long installSizeBytes = 0;
    public string? installDirectory = null;
    public int progListId;
    public string author = "Loading";
    public string version = "Loading";
    public Image? icon = null;

    private DataGridView dataGridView1;
    private int row;
    bool alreadyErrored = false;

    public ProgramItem(string key, DataGridView dataGridView1, int listId, bool userlocal)
    {
        this.key = key;
        this.dataGridView1 = dataGridView1;
        this.progListId = listId;
        this.userlocal = userlocal;
        dataGridView1.Invoke(() =>
        {
            row = dataGridView1.Rows.Add([progListId, icon, displayName, key, "Loading", "More Info", installSize, author, version, userlocal ? "User" : ""]);
        });
    }

    private RegistryKey? OpenPRKey(string pKey, RegistryKey regKey)
    {
        RegistryKey? prKey = regKey.OpenSubKey(pKey);
        if (prKey == null)
        {
            Program.PanicBox("Registry Key Missing", (userlocal ? "(Local) " : "") + $"Registry key {pKey} disappeared.");
        }
        return prKey;
    }

    private string FetchName(RegistryKey? prKey)
    {
        if (prKey == null) return "[Missing Entry]";
        return prKey.GetValue("DisplayName", key) as string ?? "[Invalid DisplayName]";
    }

    private string? FetchUninstallPath(RegistryKey? prKey)
    {
        return prKey?.GetValue("NoRemove", false)?.Equals(true) == true ? null : prKey?.GetValue("UninstallString") as string;
    }

    private string? FetchInstallDirectory(RegistryKey? prKey)
    {
        if (prKey == null) return null; // May be incorrect, only use for size information.
        string? s = prKey.GetValue("InstallLocation") as string;
        if (s == null)
        {
            s = prKey.GetValue("InstallSource") as string;
        }
        if (s == null || s == "") return null;
        return s.Trim('"');
    }

    private string FetchAuthor(RegistryKey? prKey)
    {
        return prKey?.GetValue("Publisher") as string ?? "";
    }

    private string FetchVersion(RegistryKey? prKey)
    {
        return prKey?.GetValue("DisplayVersion") as string ?? "";
    }

    private string? FetchIconPath(RegistryKey? prKey)
    {
        string? s = prKey?.GetValue("DisplayIcon") as string;
        if (s == null || s == "")
        {
            if (uninstallPath == null)
                return null;
            string match = Regex.Matches(uninstallPath, @"[\""].+?[\""]|[^ ]+")
                .Cast<Match>()
                .First().Value.Trim('"');
            if (Regex.IsMatch(match, @"^.:[\\\/]"))
                return match;
            match = "C:\\Windows\\System32\\" + match;
            return match;
        }
        return s;
    }

    private Image? FetchIcon(RegistryKey? prKey)
    {
        string? path = FetchIconPath(prKey);
        if (path == null) return null;
        try
        {
            return Icon.ExtractAssociatedIcon(path)?.ToBitmap();
        } catch (FileNotFoundException)
        {
            return null;
        }
    }

    private long DirSize(DirectoryInfo d)
    {
        long size = 0;
        // Add file sizes.
        try
        {
            FileInfo[] fis = d.GetFiles();
            foreach (FileInfo fi in fis)
            {
                size += fi.Length;
            }
            // Add subdirectory sizes.
            DirectoryInfo[] dis = d.GetDirectories();
            foreach (DirectoryInfo di in dis)
            {
                size += DirSize(di);
            }
        }
        catch (UnauthorizedAccessException) {
            if (alreadyErrored) return size;
            alreadyErrored = true;
            Program.Log($"Error reading size of {displayName}. Size may be inaccurate.");
        }
        catch (DirectoryNotFoundException)
        {
            if (alreadyErrored) return size;
            alreadyErrored = true;
            Program.Log($"Could not find directory for {displayName}. May be already uninstalled?");
        }
        return size;
    }

    private string GetInstallSize()
    {
        if (installDirectory == null)
            return "?";
        installSizeBytes = DirSize(new DirectoryInfo(installDirectory));
        return Program.FormatSize(installSizeBytes);
    }

    public void UpdateRow()
    {
        if (dataGridView1.IsDisposed) return;
        DataGridViewRow? frow = null;
        foreach (DataGridViewRow row in dataGridView1.Rows)
        {
            if (row.Cells[0].Value.Equals(progListId))
                frow = row;
        }
        if (frow == null)
            return;
        dataGridView1.Invoke(() =>
        {
            if (!IsUninstallable)
            {
                frow.DefaultCellStyle.BackColor = Color.DarkGray;
                frow.DefaultCellStyle.ForeColor = Color.White;
            }
            frow.Cells[1].Value = icon;
            frow.Cells[2].Value = displayName;
            frow.Cells[4].Value = IsUninstallable ? "Uninstall" : "Disabled";
            frow.Cells[6].Value = installSize;
            frow.Cells[7].Value = author;
            frow.Cells[8].Value = version;
            dataGridView1.Update();
        });
    }

    public void Process(RegistryKey regKey)
    {
        RegistryKey? prKey = OpenPRKey(key, regKey);
        displayName = FetchName(prKey);
        uninstallPath = FetchUninstallPath(prKey);
        installDirectory = FetchInstallDirectory(prKey);
        author = FetchAuthor(prKey);
        version = FetchVersion(prKey);
        icon = FetchIcon(prKey);

        UpdateRow();

        installSize = GetInstallSize();
        
        UpdateRow();
    }

    public void Uninstall()
    {
        if (MessageBox.Show($"Are you sure you want to uninstall {displayName}?", "Uninstall", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
        if (!IsUninstallable) return;
        Program.Log($"Executing command ${uninstallPath}");
        System.Diagnostics.Process.Start("cmd.exe", $"/C {uninstallPath}").WaitForExit();
    }

    public void OpenInRegEdit()
    {
        string machine = userlocal ? "HKEY_CURRENT_USER\\Software" : "HKEY_LOCAL_MACHINE\\SOFTWARE";
        Registry.SetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Applets\\Regedit", "Lastkey", $"Computer\\{machine}\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\{key}");
        System.Diagnostics.Process.Start("cmd.exe", "/C regedit.exe");
    }
}
