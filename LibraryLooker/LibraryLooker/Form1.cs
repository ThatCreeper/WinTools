using System.Diagnostics;

namespace LibraryLooker;

public partial class Form1 : Form, IDisposable
{
    BindingSource binding = new BindingSource();
    Thread t;

    public Form1()
    {
        InitializeComponent();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
        Thread t = new Thread(LoadThread);
        t.Start();
    }

    private void LoadThread()
    {
        Try(Dub);
        Try(Pip);
        Try(Nuget);
        Try(Cargo);
        Try(VCPkg);
        Try(HaxeLib);
        Try(Msys);

        statusLibraryCount.Text = $"{dataGridView1.Rows.Count} Libraries";
    }

    private void Msys()
    {
        ProcessStartInfo info = new ProcessStartInfo
        {
            FileName = "C:\\msys64\\usr\\bin\\pacman.exe",
            Arguments = "-Qe",
            RedirectStandardOutput = true,
            CreateNoWindow = true
        };
        Process process = Process.Start(info)!;
        string? line;
        while ((line = process.StandardOutput.ReadLine()) != null)
        {
            AddLibrary(line.Split(' ')[0], "msys");
        }
    }

    private void HaxeLib()
    {
        AddChildren("C:\\HaxeToolkit\\haxe\\lib", "haxelib");
    }

    private void VCPkg()
    {
        DirectoryInfo dir = new DirectoryInfo($"{AppData}\\Local\\vcpkg\\downloads");
        foreach (FileInfo file in dir.GetFiles())
        {
            AddLibrary(file.Name, "vcpkg");
        }
    }

    private void Cargo()
    {
        AddGrandchildren($"{Home}\\.cargo\\registry\\src", "cargo");
    }

    private void Nuget()
    {
        AddChildren($"{Home}\\.nuget\\packages", "nuget");
    }

    private void Dub()
    {
        AddChildren($"{AppData}\\Local\\dub\\packages", "dub");
    }

    private void Pip()
    {
        DirectoryInfo local = new DirectoryInfo(AppData + "\\Local\\Packages");
        foreach (DirectoryInfo sub in local.GetDirectories())
        {
            if (!sub.Name.StartsWith("PythonSoftwareFoundation"))
                continue;
            SubPip(sub);
        }
    }

    private void SubPip(DirectoryInfo dir)
    {
        DirectoryInfo local = new DirectoryInfo(dir.FullName + "\\LocalCache\\local-packages");
        foreach (DirectoryInfo sub in local.GetDirectories())
        {
            SubPip2(sub);
        }
    }

    private void SubPip2(DirectoryInfo dir)
    {
        AddChildren($"{dir.FullName}\\site-packages", "pip");
    }

    delegate void Loader();
    private void Try(Loader loader)
    {
        try
        {
            loader();
        }
        catch (Exception e)
        {
            return; // Noop for breakpoint
        }
    }

    private void AddChildren(DirectoryInfo dir, string source)
    {
        DirectoryInfo[] subs = dir.GetDirectories();
        foreach (DirectoryInfo sub in subs)
        {
            AddLibrary(sub.Name, source);
        }
    }

    private void AddChildren(string dir, string source)
    {
        AddChildren(new DirectoryInfo(dir), source);
    }

    private void AddGrandchildren(string dir, string source)
    {
        DirectoryInfo[] subs = new DirectoryInfo(dir).GetDirectories();
        foreach (DirectoryInfo sub in subs)
        {
            AddChildren(sub, source);
        }
    }

    private void AddLibrary(string name, string source)
    {
        Invoke(() => dataGridView1.Rows.Add([name, source]));
    }

    private string AppData => $"C:\\Users\\{Environment.UserName}\\AppData";
    private string Home => $"C:\\Users\\{Environment.UserName}";
}
