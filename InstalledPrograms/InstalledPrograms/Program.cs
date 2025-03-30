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
        ApplicationConfiguration.Initialize();
        Application.Run(new Form1());
    }

    public static void PanicBox(string title, string content)
    {
        MessageBox.Show(content, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    public static string FormatSize(long size)
    {
        double totalInstallSizeMB = size / (1024.0 * 1024.0);
        if (totalInstallSizeMB >= 1024)
        {
            totalInstallSizeMB /= 1024;
            return totalInstallSizeMB.ToString("0.00 GB");
        }
        else
        {
            return totalInstallSizeMB.ToString("0.00 MB");
        }
    }
}