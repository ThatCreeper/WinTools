namespace InstalledPrograms;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        richTextBox1 = new RichTextBox();
        dataGridView1 = new DataGridView();
        ProgamListID = new DataGridViewTextBoxColumn();
        IconCol = new DataGridViewImageColumn();
        ProgName = new DataGridViewTextBoxColumn();
        RegKey = new DataGridViewTextBoxColumn();
        UninstallBtn = new DataGridViewButtonColumn();
        OpenInRegEdit = new DataGridViewButtonColumn();
        ProgramSize = new DataGridViewTextBoxColumn();
        AuthorText = new DataGridViewTextBoxColumn();
        VersionText = new DataGridViewTextBoxColumn();
        statusStrip1 = new StatusStrip();
        statusProgramsFound = new ToolStripStatusLabel();
        statusProgramSize = new ToolStripStatusLabel();
        ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
        statusStrip1.SuspendLayout();
        SuspendLayout();
        // 
        // richTextBox1
        // 
        richTextBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        richTextBox1.Location = new Point(17, 598);
        richTextBox1.Margin = new Padding(4, 5, 4, 5);
        richTextBox1.Name = "richTextBox1";
        richTextBox1.Size = new Size(1107, 129);
        richTextBox1.TabIndex = 0;
        richTextBox1.Text = "";
        // 
        // dataGridView1
        // 
        dataGridView1.AllowUserToAddRows = false;
        dataGridView1.AllowUserToDeleteRows = false;
        dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dataGridView1.Columns.AddRange(new DataGridViewColumn[] { ProgamListID, IconCol, ProgName, RegKey, UninstallBtn, OpenInRegEdit, ProgramSize, AuthorText, VersionText });
        dataGridView1.Location = new Point(17, 20);
        dataGridView1.Margin = new Padding(4, 5, 4, 5);
        dataGridView1.Name = "dataGridView1";
        dataGridView1.ReadOnly = true;
        dataGridView1.RowHeadersWidth = 62;
        dataGridView1.Size = new Size(1109, 568);
        dataGridView1.TabIndex = 1;
        // 
        // ProgamListID
        // 
        ProgamListID.HeaderText = "ID";
        ProgamListID.MinimumWidth = 8;
        ProgamListID.Name = "ProgamListID";
        ProgamListID.ReadOnly = true;
        ProgamListID.ToolTipText = "Not organized by Windows";
        ProgamListID.Visible = false;
        ProgamListID.Width = 150;
        // 
        // IconCol
        // 
        IconCol.HeaderText = "Icon";
        IconCol.MinimumWidth = 8;
        IconCol.Name = "IconCol";
        IconCol.ReadOnly = true;
        IconCol.Width = 48;
        // 
        // ProgName
        // 
        ProgName.HeaderText = "Name";
        ProgName.MinimumWidth = 8;
        ProgName.Name = "ProgName";
        ProgName.ReadOnly = true;
        ProgName.Width = 150;
        // 
        // RegKey
        // 
        RegKey.HeaderText = "Key";
        RegKey.MinimumWidth = 8;
        RegKey.Name = "RegKey";
        RegKey.ReadOnly = true;
        RegKey.Width = 150;
        // 
        // UninstallBtn
        // 
        UninstallBtn.HeaderText = "Uninstall";
        UninstallBtn.MinimumWidth = 8;
        UninstallBtn.Name = "UninstallBtn";
        UninstallBtn.ReadOnly = true;
        UninstallBtn.Width = 150;
        // 
        // OpenInRegEdit
        // 
        OpenInRegEdit.HeaderText = "RegEdit";
        OpenInRegEdit.MinimumWidth = 8;
        OpenInRegEdit.Name = "OpenInRegEdit";
        OpenInRegEdit.ReadOnly = true;
        OpenInRegEdit.Width = 150;
        // 
        // ProgramSize
        // 
        ProgramSize.HeaderText = "Size";
        ProgramSize.MinimumWidth = 8;
        ProgramSize.Name = "ProgramSize";
        ProgramSize.ReadOnly = true;
        ProgramSize.Width = 150;
        // 
        // AuthorText
        // 
        AuthorText.HeaderText = "Publisher";
        AuthorText.MinimumWidth = 8;
        AuthorText.Name = "AuthorText";
        AuthorText.ReadOnly = true;
        AuthorText.Width = 150;
        // 
        // VersionText
        // 
        VersionText.HeaderText = "Version";
        VersionText.MinimumWidth = 8;
        VersionText.Name = "VersionText";
        VersionText.ReadOnly = true;
        VersionText.Width = 150;
        // 
        // statusStrip1
        // 
        statusStrip1.ImageScalingSize = new Size(24, 24);
        statusStrip1.Items.AddRange(new ToolStripItem[] { statusProgramsFound, statusProgramSize });
        statusStrip1.Location = new Point(0, 718);
        statusStrip1.Name = "statusStrip1";
        statusStrip1.Size = new Size(1143, 32);
        statusStrip1.TabIndex = 2;
        statusStrip1.Text = "statusStrip1";
        // 
        // statusProgramsFound
        // 
        statusProgramsFound.Name = "statusProgramsFound";
        statusProgramsFound.Size = new Size(160, 25);
        statusProgramsFound.Text = "0 Programs Found";
        // 
        // statusProgramSize
        // 
        statusProgramSize.Name = "statusProgramSize";
        statusProgramSize.Size = new Size(77, 25);
        statusProgramSize.Text = "0.00 MB";
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1143, 750);
        Controls.Add(statusStrip1);
        Controls.Add(dataGridView1);
        Controls.Add(richTextBox1);
        Margin = new Padding(4, 5, 4, 5);
        Name = "Form1";
        Text = "Program Listing";
        Load += Form1_Load;
        ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
        statusStrip1.ResumeLayout(false);
        statusStrip1.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private RichTextBox richTextBox1;
    private DataGridView dataGridView1;
    private DataGridViewTextBoxColumn ProgamListID;
    private DataGridViewImageColumn IconCol;
    private DataGridViewTextBoxColumn ProgName;
    private DataGridViewTextBoxColumn RegKey;
    private DataGridViewButtonColumn UninstallBtn;
    private DataGridViewButtonColumn OpenInRegEdit;
    private DataGridViewTextBoxColumn ProgramSize;
    private DataGridViewTextBoxColumn AuthorText;
    private DataGridViewTextBoxColumn VersionText;
    private StatusStrip statusStrip1;
    private ToolStripStatusLabel statusProgramsFound;
    private ToolStripStatusLabel statusProgramSize;
}
