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
        ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
        SuspendLayout();
        // 
        // richTextBox1
        // 
        richTextBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        richTextBox1.Location = new Point(12, 359);
        richTextBox1.Name = "richTextBox1";
        richTextBox1.Size = new Size(776, 79);
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
        dataGridView1.Location = new Point(12, 12);
        dataGridView1.Name = "dataGridView1";
        dataGridView1.ReadOnly = true;
        dataGridView1.Size = new Size(776, 341);
        dataGridView1.TabIndex = 1;
        // 
        // ProgamListID
        // 
        ProgamListID.HeaderText = "ID";
        ProgamListID.Name = "ProgamListID";
        ProgamListID.ReadOnly = true;
        ProgamListID.ToolTipText = "Not organized by Windows";
        ProgamListID.Visible = false;
        // 
        // IconCol
        // 
        IconCol.HeaderText = "Icon";
        IconCol.Name = "IconCol";
        IconCol.ReadOnly = true;
        IconCol.Width = 48;
        // 
        // ProgName
        // 
        ProgName.HeaderText = "Name";
        ProgName.Name = "ProgName";
        ProgName.ReadOnly = true;
        // 
        // RegKey
        // 
        RegKey.HeaderText = "Key";
        RegKey.Name = "RegKey";
        RegKey.ReadOnly = true;
        // 
        // UninstallBtn
        // 
        UninstallBtn.HeaderText = "Uninstall";
        UninstallBtn.Name = "UninstallBtn";
        UninstallBtn.ReadOnly = true;
        // 
        // OpenInRegEdit
        // 
        OpenInRegEdit.HeaderText = "RegEdit";
        OpenInRegEdit.Name = "OpenInRegEdit";
        OpenInRegEdit.ReadOnly = true;
        // 
        // ProgramSize
        // 
        ProgramSize.HeaderText = "Size";
        ProgramSize.Name = "ProgramSize";
        ProgramSize.ReadOnly = true;
        // 
        // AuthorText
        // 
        AuthorText.HeaderText = "Publisher";
        AuthorText.Name = "AuthorText";
        AuthorText.ReadOnly = true;
        // 
        // VersionText
        // 
        VersionText.HeaderText = "Version";
        VersionText.Name = "VersionText";
        VersionText.ReadOnly = true;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(dataGridView1);
        Controls.Add(richTextBox1);
        Name = "Form1";
        Text = "Program Listing";
        Load += Form1_Load;
        ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
        ResumeLayout(false);
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
}
