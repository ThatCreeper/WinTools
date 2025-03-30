using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstalledPrograms;
internal class WingetProgramItem : ProgramItem
{
    public override bool IsUninstallable => false;

    public WingetProgramItem(string key, DataGridView dataGridView1, int listId, string name, string version)
        : base(key, dataGridView1, listId)
    {
        this.author = "";
        this.installSize = "";
        this.displayName = name;
        this.version = version;
        dataGridView1.Invoke(() =>
        {
            row = dataGridView1.Rows.Add([progListId, icon, displayName, key, "Disabled", "Disabled", installSize, author, version, ""]);
        });
    }

    public override void Uninstall()
    {
    }


    public override void OpenInRegEdit()
    {
        Program.PanicBox("Meaningless command", $"{displayName} cannot be opened in the Registry Editor.");
    }
}
