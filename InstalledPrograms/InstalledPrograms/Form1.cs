using Microsoft.Win32;
using System.Diagnostics;

namespace InstalledPrograms;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
    }

    private void InternalLog(string what)
    {
        richTextBox1.AppendText($"{what}\n");
        richTextBox1.Update();
    }

    public void Log(string what)
    {
        if (IsDisposed) return;
        Invoke(() => InternalLog(what));
    }

    private void LoadInitialKeys()
    {
        RegistryKey? regKey1 = Registry.LocalMachine?.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall");
        RegistryKey? regKey2 = Registry.CurrentUser?.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Uninstall");
        if (regKey1 == null || regKey2 == null)
        {
            Program.PanicBox("Could not load program list", "(Either LOCAL or USER) Registry key CurrentVersion\\Uninstall not found...?");
            throw new Exception();
        }
        string[]? keys = regKey1.GetSubKeyNames();
        if (keys == null)
        {
            Program.PanicBox("Machine rogram list empty", "Uninstall.GetSubKeyNames() == null");
            throw new Exception();
        }
        foreach (string key in keys)
        {
            Program.programs.Add(new ProgramItem(key, dataGridView1, Program.programs.Count, false));
        }
        keys = regKey2.GetSubKeyNames();
        if (keys != null)
        {
            foreach (string key in keys)
            {
                Program.programs.Add(new ProgramItem(key, dataGridView1, Program.programs.Count, true));
            }
        }

        SetStatusCount();
        
        Parallel.ForEach(Program.programs, program =>
        //foreach (ProgramItem program in programs)
        {
            try
            {
                program.Process(program.userlocal ? regKey2 : regKey1);
            }
            catch (Exception e)
            {
                Program.PanicBox(program.key, e.Message);
                throw new Exception();
            }
            //Log($"Loaded {program.key}\n- Name: {program.displayName}\n- Uninstall: {program.uninstallPath}");
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

    public void LoadPrograms()
    {
        Program.programs.Clear();
        dataGridView1.Rows.Clear();
        SetStatusCount();
        SetStatusSize();
        new Thread(LoadInitialKeys).Start();
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
}
