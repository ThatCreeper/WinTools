using Microsoft.Win32;
using System.Diagnostics;

namespace InstalledPrograms;

public partial class Form1 : Form
{
    bool currentlyWinget = false;

    public Form1()
    {
        InitializeComponent();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
        Program.Log = Log;
        LoadPrograms();
        dataGridView1.CellContentClick += (sender, args) =>
        {
            if (args.RowIndex < 0) return;
            ProgramItem item = Program.programs[(int)(dataGridView1.Rows[args.RowIndex].Cells[0].Value)];
            if (args.ColumnIndex == 4)
            {
                item.Uninstall();
                LoadPrograms();
            }
            else if (args.ColumnIndex == 5)
            {
                item.OpenInRegEdit();
            }
        };
    }

    public void LoadPrograms()
    {
        Program.programs.Clear();
        dataGridView1.Rows.Clear();
        SetStatusCount();
        SetStatusSize();
        new Thread(LoadKeys).Start();
    }

    private void LoadKeys()
    {
        if (currentlyWinget)
            LoadWingetKeys();
        else
            LoadRegistryKeys();
        Invoke(() => filterBox_TextChanged(this, null));
    }

    private void LoadWingetKeys()
    {
        ProcessStartInfo startInfo = new ProcessStartInfo()
        {
            FileName = $"C:\\Users\\{Environment.UserName}\\AppData\\Local\\Microsoft\\WindowsApps\\winget.exe",
            Arguments = "list",
            RedirectStandardOutput = true,
            StandardOutputEncoding = System.Text.Encoding.UTF8,
            CreateNoWindow = true
        };
        Process proc = Process.Start(startInfo)!;
        proc.OutputDataReceived += (sender, data) => Log(data.Data ?? "");
        string? s;
        int idxId = 0;
        int idxVer = 0;
        while ((s = proc.StandardOutput.ReadLine()) != null)
        {
            if (!s.StartsWith("Name"))
                continue;
            idxId = s.IndexOf('I');
            idxVer = s.IndexOf('V');
            break;
        }
        proc.StandardOutput.ReadLine();
        List<List<string>> progs = proc.StandardOutput.ReadToEnd()
            .Split('\n')
            .AsParallel()
            .Where(s => s != "")
            .Select(s => new List<string>([
                s.Substring(0, idxId).Trim(),
                s.Substring(idxId, idxVer - idxId).Trim(),
                s.Substring(idxVer).Split(' ')[0]]))
            .ToList();
        foreach (List<string> line in progs)
        {
            Program.programs.Add(new WingetProgramItem(
                key: line[1],
                dataGridView1,
                Program.programs.Count,
                name: line[0],
                version: line[2]));
        }
        SetStatusCount();
    }

    private void LoadRegistryKeys()
    {
        RegistryKey? systemKey = Registry.LocalMachine?.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall");
        RegistryKey? userKey = Registry.CurrentUser?.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Uninstall");
        if (systemKey == null || userKey == null)
        {
            Program.PanicBox("Could not load program list", "(Either LOCAL or USER) Registry key CurrentVersion\\Uninstall not found...?");
            throw new Exception();
        }
        LoadInitialKeys(systemKey, false);
        LoadInitialKeys(userKey, true);
        ProcessKeys(systemKey, userKey);
    }

    private void LoadInitialKeys(RegistryKey regKey, bool isUserLocal)
    {
        string[]? keys = regKey.GetSubKeyNames();
        if (keys == null) return;
        foreach (string key in keys)
        {
            Program.programs.Add(new RegistryProgramItem(key, dataGridView1, Program.programs.Count, isUserLocal));
        }
    }

    private void ProcessKeys(RegistryKey systemKey, RegistryKey userKey)
    {
        SetStatusCount();

        Parallel.ForEach(Program.programs, program =>
        {
            try
            {
                program.Process(systemKey, userKey);
            }
            catch (Exception e)
            {
                Program.PanicBox(program.key, e.Message);
                throw new Exception();
            }
        });

        SetStatusSize();
    }

    private void SetStatusCount()
    {
        statusProgramsFound.Text = $"{Program.programs.Count} Programs Found";
    }

    private void SetStatusSize()
    {
        long totalInstallSize = 0;
        foreach (ProgramItem program in Program.programs)
        {
            totalInstallSize += program.installSizeBytes;
        }
        statusProgramSize.Text = Program.FormatSize(totalInstallSize);
    }

    public void Log(string what)
    {
        if (IsDisposed) return;
        Invoke(() => InternalLog(what));
    }

    private void InternalLog(string what)
    {
        richTextBox1.AppendText($"{what}\n");
        richTextBox1.Update();
    }

    private void sourceChoice_SelectedIndexChanged(object sender, EventArgs e)
    {
        currentlyWinget = sourceChoice.SelectedIndex == 1;
        LoadPrograms();
    }

    private void filterBox_TextChanged(object sender, EventArgs? e)
    {
        string filter = filterBox.Text.ToLower();
        foreach (DataGridViewRow row in dataGridView1.Rows)
        {
            row.Visible = filter == ""
                ? true
                : Program.programs[(int)row.Cells[0].Value].Fzf(filter);
        }
    }
}
