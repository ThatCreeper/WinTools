using System.Runtime.InteropServices;

namespace InstalledPrograms;

internal static class Program
{
    public static List<ProgramItem> programs = new List<ProgramItem>();
    public delegate void LogType(string s);

    public static LogType Log;

    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ComWrappers.RegisterForMarshalling(WinFormsComInterop.WinFormsComWrappers.Instance);
        ApplicationConfiguration.Initialize();
        Application.Run(new Form1());
    }

    public static void PanicBox(string title, string content)
    {
        MessageBox.Show(content, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}