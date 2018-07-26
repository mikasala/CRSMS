namespace CRSalesManagementSystem
{
    partial class SelectProducts
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dtSelProdList = new System.Windows.Forms.DataGridView();
            this.txtPSearch = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dtSelProdList)).BeginInit();
            this.SuspendLayout();
            // 
            // dtSelProdList
            // 
            this.dtSelProdList.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.dtSelProdList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dtSelProdList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtSelProdList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtSelProdList.BackgroundColor = System.Drawing.Color.Silver;
            this.dtSelProdList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtSelProdList.Location = new System.Drawing.Point(9, 51);
            this.dtSelProdList.Margin = new System.Windows.Forms.Padding(4);
            this.dtSelProdList.Name = "dtSelProdList";
            this.dtSelProdList.ReadOnly = true;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.dtSelProdList.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dtSelProdList.Size = new System.Drawing.Size(1024, 302);
            this.dtSelProdList.TabIndex = 104;
            this.dtSelProdList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtSelProdList_CellContentClick);
            // 
            // txtPSearch
            // 
            this.txtPSearch.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPSearch.Location = new System.Drawing.Point(156, 12);
            this.txtPSearch.Name = "txtPSearch";
            this.txtPSearch.Size = new System.Drawing.Size(178, 27);
            this.txtPSearch.TabIndex = 106;
            this.txtPSearch.TextChanged += new System.EventHandler(this.txtPSearch_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(11, 15);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(136, 18);
            this.label8.TabIndex = 105;
            this.label8.Text = "Search by Name:";
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(9, 360);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(1024, 27);
            this.btnSave.TabIndex = 107;
            this.btnSave.Text = "SAVE TRANSACTION";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // SelectProducts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MistyRose;
            this.ClientSize = new System.Drawing.Size(1040, 391);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtPSearch);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.dtSelProdList);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SelectProducts";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select Products";
            this.Load += new System.EventHandler(this.SelectProducts_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtSelProdList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dtSelProdList;
        internal System.Windows.Forms.TextBox txtPSearch;
        private System.Windows.Forms.Label label8;
        internal System.Windows.Forms.Button btnSave;
    }
}