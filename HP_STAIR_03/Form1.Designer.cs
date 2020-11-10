namespace HP_STAIR_03
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.dgv_dimTable = new System.Windows.Forms.DataGridView();
            this.dimLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lbl_1 = new System.Windows.Forms.Label();
            this.lbl_status = new System.Windows.Forms.Label();
            this.txb_preset = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_dimTable)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(50, 381);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(139, 35);
            this.button1.TabIndex = 0;
            this.button1.Text = "Make Dimensions";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dgv_dimTable
            // 
            this.dgv_dimTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_dimTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dimLength});
            this.dgv_dimTable.Location = new System.Drawing.Point(34, 11);
            this.dgv_dimTable.Margin = new System.Windows.Forms.Padding(2);
            this.dgv_dimTable.Name = "dgv_dimTable";
            this.dgv_dimTable.RowHeadersWidth = 51;
            this.dgv_dimTable.RowTemplate.Height = 24;
            this.dgv_dimTable.Size = new System.Drawing.Size(176, 321);
            this.dgv_dimTable.TabIndex = 1;
            // 
            // dimLength
            // 
            this.dimLength.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dimLength.FillWeight = 85F;
            this.dimLength.HeaderText = "Dimension length";
            this.dimLength.MinimumWidth = 6;
            this.dimLength.Name = "dimLength";
            this.dimLength.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dimLength.Width = 120;
            // 
            // lbl_1
            // 
            this.lbl_1.AutoSize = true;
            this.lbl_1.Location = new System.Drawing.Point(9, 484);
            this.lbl_1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_1.Name = "lbl_1";
            this.lbl_1.Size = new System.Drawing.Size(40, 13);
            this.lbl_1.TabIndex = 2;
            this.lbl_1.Text = "Status:";
            // 
            // lbl_status
            // 
            this.lbl_status.AutoSize = true;
            this.lbl_status.Location = new System.Drawing.Point(49, 484);
            this.lbl_status.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_status.Name = "lbl_status";
            this.lbl_status.Size = new System.Drawing.Size(10, 13);
            this.lbl_status.TabIndex = 3;
            this.lbl_status.Text = " ";
            // 
            // txb_preset
            // 
            this.txb_preset.Location = new System.Drawing.Point(34, 444);
            this.txb_preset.Name = "txb_preset";
            this.txb_preset.Size = new System.Drawing.Size(176, 20);
            this.txb_preset.TabIndex = 4;
            this.txb_preset.Text = "ATK_DIM_LINE";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 428);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Preset name:";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(34, 348);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(61, 17);
            this.checkBox1.TabIndex = 6;
            this.checkBox1.Text = "Vertical";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(137, 348);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(73, 17);
            this.checkBox2.TabIndex = 7;
            this.checkBox2.Text = "Horizontal";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(241, 506);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txb_preset);
            this.Controls.Add(this.lbl_status);
            this.Controls.Add(this.lbl_1);
            this.Controls.Add(this.dgv_dimTable);
            this.Controls.Add(this.button1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_dimTable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dgv_dimTable;
        private System.Windows.Forms.Label lbl_1;
        private System.Windows.Forms.Label lbl_status;
        private System.Windows.Forms.TextBox txb_preset;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dimLength;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
    }
}

