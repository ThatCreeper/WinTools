using System.Diagnostics;

namespace LibraryLooker;

public partial class Form1 : Form
{
    BindingSource binding = new BindingSource();

    public Form1()
    {
        InitializeComponent();
    }

    private void Form1_Load(object sender, EventArgs e)
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
            RedirectStandardOutput = true
        };
        Process process = Process.Start(info)!;
        process.WaitForExit();
        string[] libs = process.StandardOutput.ReadToEnd().Split('\n').Select(s => s.Split(' ')[0]).ToArray();
        foreach (string lib in libs)
        {
            AddLibrary(lib, "msys");
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
        dataGridView1.Rows.Add([name, source]);
    }

    private string AppData => $"C:\\Users\\{Environment.UserName}\\AppData";
    private string Home => $"C:\\Users\\{Environment.UserName}";
}
