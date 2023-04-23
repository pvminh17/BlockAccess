namespace BlockAccess
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.dgvLstHostEntry = new System.Windows.Forms.DataGridView();
            this.txtKeyword = new System.Windows.Forms.TextBox();
            this.btnAddNew = new System.Windows.Forms.Button();
            this.btnBlock = new System.Windows.Forms.Button();
            this.btnUnBlock = new System.Windows.Forms.Button();
            this.txtDomain = new System.Windows.Forms.TextBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ckbOnGroupAction = new System.Windows.Forms.CheckBox();
            this.cbbGroup = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLstHostEntry)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvLstHostEntry
            // 
            this.dgvLstHostEntry.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLstHostEntry.Location = new System.Drawing.Point(12, 120);
            this.dgvLstHostEntry.Name = "dgvLstHostEntry";
            this.dgvLstHostEntry.RowTemplate.Height = 25;
            this.dgvLstHostEntry.Size = new System.Drawing.Size(608, 595);
            this.dgvLstHostEntry.TabIndex = 0;
            this.dgvLstHostEntry.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvLstHostEntry_RowPostPaint);
            // 
            // txtKeyword
            // 
            this.txtKeyword.Location = new System.Drawing.Point(85, 12);
            this.txtKeyword.Name = "txtKeyword";
            this.txtKeyword.PlaceholderText = "Keyword";
            this.txtKeyword.Size = new System.Drawing.Size(471, 23);
            this.txtKeyword.TabIndex = 1;
            this.txtKeyword.TextChanged += new System.EventHandler(this.txtKeyword_TextChanged);
            // 
            // btnAddNew
            // 
            this.btnAddNew.Location = new System.Drawing.Point(481, 48);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(75, 23);
            this.btnAddNew.TabIndex = 2;
            this.btnAddNew.Text = "Add";
            this.btnAddNew.UseVisualStyleBackColor = true;
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // btnBlock
            // 
            this.btnBlock.Location = new System.Drawing.Point(432, 91);
            this.btnBlock.Name = "btnBlock";
            this.btnBlock.Size = new System.Drawing.Size(75, 23);
            this.btnBlock.TabIndex = 3;
            this.btnBlock.Text = "Block";
            this.btnBlock.UseVisualStyleBackColor = true;
            this.btnBlock.Click += new System.EventHandler(this.btnBlock_Click);
            // 
            // btnUnBlock
            // 
            this.btnUnBlock.Location = new System.Drawing.Point(519, 91);
            this.btnUnBlock.Name = "btnUnBlock";
            this.btnUnBlock.Size = new System.Drawing.Size(75, 23);
            this.btnUnBlock.TabIndex = 4;
            this.btnUnBlock.Text = "UnBlock";
            this.btnUnBlock.UseVisualStyleBackColor = true;
            this.btnUnBlock.Click += new System.EventHandler(this.btnUnBlock_Click);
            // 
            // txtDomain
            // 
            this.txtDomain.Location = new System.Drawing.Point(86, 48);
            this.txtDomain.Name = "txtDomain";
            this.txtDomain.PlaceholderText = "Domain/URL";
            this.txtDomain.Size = new System.Drawing.Size(389, 23);
            this.txtDomain.TabIndex = 5;
            this.txtDomain.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDomain_KeyPress);
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(75, 95);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(13, 15);
            this.lblTotal.TabIndex = 7;
            this.lblTotal.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "Amount:";
            // 
            // ckbOnGroupAction
            // 
            this.ckbOnGroupAction.AutoSize = true;
            this.ckbOnGroupAction.Location = new System.Drawing.Point(122, 95);
            this.ckbOnGroupAction.Name = "ckbOnGroupAction";
            this.ckbOnGroupAction.Size = new System.Drawing.Size(113, 19);
            this.ckbOnGroupAction.TabIndex = 8;
            this.ckbOnGroupAction.Text = "Action on group";
            this.ckbOnGroupAction.UseVisualStyleBackColor = true;
            this.ckbOnGroupAction.CheckedChanged += new System.EventHandler(this.ckbOnGroupAction_CheckedChanged);
            // 
            // cbbGroup
            // 
            this.cbbGroup.FormattingEnabled = true;
            this.cbbGroup.Location = new System.Drawing.Point(252, 91);
            this.cbbGroup.Name = "cbbGroup";
            this.cbbGroup.Size = new System.Drawing.Size(121, 23);
            this.cbbGroup.TabIndex = 9;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 727);
            this.Controls.Add(this.cbbGroup);
            this.Controls.Add(this.ckbOnGroupAction);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDomain);
            this.Controls.Add(this.btnUnBlock);
            this.Controls.Add(this.btnBlock);
            this.Controls.Add(this.btnAddNew);
            this.Controls.Add(this.txtKeyword);
            this.Controls.Add(this.dgvLstHostEntry);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Access Blocker";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLstHostEntry)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DataGridView dgvLstHostEntry;
        private TextBox txtKeyword;
        private Button btnAddNew;
        private Button btnBlock;
        private Button btnUnBlock;
        private TextBox txtDomain;
        private Label lblTotal;
        private Label label1;
        private CheckBox ckbOnGroupAction;
        private ComboBox cbbGroup;
    }
}