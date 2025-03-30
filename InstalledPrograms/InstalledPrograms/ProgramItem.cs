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
internal abstract class ProgramItem
{
    public string key;
    public string displayName = "[Loading]";
    public abstract bool IsUninstallable { get; }
    public string installSize = "Loading";
    public long installSizeBytes = 0;
    public string? installDirectory = null;
    public int progListId;
    public string author = "Loading";
    public string version = "Loading";
    public Image? icon = null;

    protected DataGridView dataGridView1;
    protected int row;

    public ProgramItem(string key, DataGridView dataGridView1, int listId)
    {
        this.key = key;
        this.dataGridView1 = dataGridView1;
        this.progListId = listId;
    }

    public abstract void Uninstall();

    public abstract void OpenInRegEdit();

    public virtual void Process(RegistryKey systemRegKey, RegistryKey userRegKey) { }

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
}
