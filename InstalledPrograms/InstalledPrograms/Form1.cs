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
        RegistryKey? regKey = Registry.LocalMachine?.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall");
        if (regKey == null)
        {
            Program.PanicBox("Could not load program list", "Registry key CurrentVersion\\Uninstall not found...?");
            throw new Exception();
        }
        string[]? keys = regKey.GetSubKeyNames();
        if (keys == null)
        {
            Program.PanicBox("Program list empty", "Uninstall.GetSubKeyNames() == null");
            throw new Exception();
        }
        foreach (string key in keys)
        {
            Program.programs.Add(new ProgramItem(key, dataGridView1, Program.programs.Count));
        }

        Parallel.ForEach(Program.programs, program =>
        //foreach (ProgramItem program in programs)
        {
            program.Process(regKey);
            //Log($"Loaded {program.key}\n- Name: {program.displayName}\n- Uninstall: {program.uninstallPath}");
        });
    }

    public void LoadPrograms()
    {
        Program.programs.Clear();
        dataGridView1.Rows.Clear();
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
