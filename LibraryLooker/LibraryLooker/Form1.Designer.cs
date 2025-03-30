namespace LibraryLooker;

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
        dataGridView1 = new DataGridView();
        LibName = new DataGridViewTextBoxColumn();
        LibSource = new DataGridViewTextBoxColumn();
        statusStrip1 = new StatusStrip();
        statusLibraryCount = new ToolStripStatusLabel();
        ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
        statusStrip1.SuspendLayout();
        SuspendLayout();
        // 
        // dataGridView1
        // 
        dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dataGridView1.Columns.AddRange(new DataGridViewColumn[] { LibName, LibSource });
        dataGridView1.Location = new Point(12, 12);
        dataGridView1.Name = "dataGridView1";
        dataGridView1.RowHeadersWidth = 62;
        dataGridView1.Size = new Size(364, 619);
        dataGridView1.TabIndex = 0;
        // 
        // LibName
        // 
        LibName.HeaderText = "Name";
        LibName.MinimumWidth = 8;
        LibName.Name = "LibName";
        LibName.ReadOnly = true;
        LibName.Width = 150;
        // 
        // LibSource
        // 
        LibSource.HeaderText = "Source";
        LibSource.MinimumWidth = 8;
        LibSource.Name = "LibSource";
        LibSource.ReadOnly = true;
        LibSource.Width = 150;
        // 
        // statusStrip1
        // 
        statusStrip1.ImageScalingSize = new Size(24, 24);
        statusStrip1.Items.AddRange(new ToolStripItem[] { statusLibraryCount });
        statusStrip1.Location = new Point(0, 611);
        statusStrip1.Name = "statusStrip1";
        statusStrip1.Size = new Size(388, 32);
        statusStrip1.TabIndex = 1;
        statusStrip1.Text = "statusStrip1";
        // 
        // statusLibraryCount
        // 
        statusLibraryCount.Name = "statusLibraryCount";
        statusLibraryCount.Size = new Size(92, 25);
        statusLibraryCount.Text = "0 Libraries";
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(388, 643);
        Controls.Add(statusStrip1);
        Controls.Add(dataGridView1);
        Name = "Form1";
        Text = "Library Looker";
        Load += Form1_Load;
        ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
        statusStrip1.ResumeLayout(false);
        statusStrip1.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private DataGridView dataGridView1;
    private DataGridViewTextBoxColumn LibName;
    private DataGridViewTextBoxColumn LibSource;
    private StatusStrip statusStrip1;
    private ToolStripStatusLabel statusLibraryCount;
}
