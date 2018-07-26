namespace CRSalesManagementSystem
{
    partial class ProductOrdered
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
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtPOWaist = new System.Windows.Forms.NumericUpDown();
            this.txtPOChest = new System.Windows.Forms.NumericUpDown();
            this.label20 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.btnPOSave = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtPOCloth = new System.Windows.Forms.TextBox();
            this.txtPODet = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPOLength = new System.Windows.Forms.NumericUpDown();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPOWaist)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPOChest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPOLength)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.MistyRose;
            this.groupBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox4.Controls.Add(this.txtPOLength);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.txtPOWaist);
            this.groupBox4.Controls.Add(this.txtPOChest);
            this.groupBox4.Controls.Add(this.label20);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.btnPOSave);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.txtPOCloth);
            this.groupBox4.Controls.Add(this.txtPODet);
            this.groupBox4.Font = new System.Drawing.Font("Century Gothic", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(4, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(379, 362);
            this.groupBox4.TabIndex = 94;
            this.groupBox4.TabStop = false;
            // 
            // txtPOWaist
            // 
            this.txtPOWaist.DecimalPlaces = 2;
            this.txtPOWaist.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPOWaist.Location = new System.Drawing.Point(120, 256);
            this.txtPOWaist.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.txtPOWaist.Name = "txtPOWaist";
            this.txtPOWaist.Size = new System.Drawing.Size(138, 27);
            this.txtPOWaist.TabIndex = 100;
            // 
            // txtPOChest
            // 
            this.txtPOChest.DecimalPlaces = 2;
            this.txtPOChest.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPOChest.Location = new System.Drawing.Point(120, 223);
            this.txtPOChest.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.txtPOChest.Name = "txtPOChest";
            this.txtPOChest.Size = new System.Drawing.Size(138, 27);
            this.txtPOChest.TabIndex = 99;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(29, 258);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(56, 19);
            this.label20.TabIndex = 88;
            this.label20.Text = "Waist :";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(29, 228);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 19);
            this.label10.TabIndex = 73;
            this.label10.Text = "Chest :";
            // 
            // btnPOSave
            // 
            this.btnPOSave.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPOSave.Location = new System.Drawing.Point(120, 319);
            this.btnPOSave.Name = "btnPOSave";
            this.btnPOSave.Size = new System.Drawing.Size(166, 29);
            this.btnPOSave.TabIndex = 67;
            this.btnPOSave.Text = "SAVE";
            this.btnPOSave.UseVisualStyleBackColor = true;
            this.btnPOSave.Click += new System.EventHandler(this.btnPOSave_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(30, 54);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(67, 19);
            this.label11.TabIndex = 72;
            this.label11.Text = "Details :";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(30, 18);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(57, 19);
            this.label13.TabIndex = 70;
            this.label13.Text = "Cloth :";
            // 
            // txtPOCloth
            // 
            this.txtPOCloth.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPOCloth.Location = new System.Drawing.Point(120, 15);
            this.txtPOCloth.Name = "txtPOCloth";
            this.txtPOCloth.Size = new System.Drawing.Size(165, 27);
            this.txtPOCloth.TabIndex = 75;
            // 
            // txtPODet
            // 
            this.txtPODet.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPODet.Location = new System.Drawing.Point(120, 52);
            this.txtPODet.Multiline = true;
            this.txtPODet.Name = "txtPODet";
            this.txtPODet.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtPODet.Size = new System.Drawing.Size(223, 167);
            this.txtPODet.TabIndex = 77;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(28, 288);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 19);
            this.label1.TabIndex = 101;
            this.label1.Text = "Length :";
            // 
            // txtPOLength
            // 
            this.txtPOLength.DecimalPlaces = 2;
            this.txtPOLength.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPOLength.Location = new System.Drawing.Point(120, 286);
            this.txtPOLength.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.txtPOLength.Name = "txtPOLength";
            this.txtPOLength.Size = new System.Drawing.Size(138, 27);
            this.txtPOLength.TabIndex = 102;
            // 
            // ProductOrdered
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(387, 369);
            this.Controls.Add(this.groupBox4);
            this.Name = "ProductOrdered";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Product Details";
            this.Load += new System.EventHandler(this.ProductOrdered_Load);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPOWaist)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPOChest)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPOLength)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.NumericUpDown txtPOWaist;
        private System.Windows.Forms.NumericUpDown txtPOChest;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label10;
        internal System.Windows.Forms.Button btnPOSave;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label13;
        internal System.Windows.Forms.TextBox txtPOCloth;
        internal System.Windows.Forms.TextBox txtPODet;
        private System.Windows.Forms.NumericUpDown txtPOLength;
        private System.Windows.Forms.Label label1;
    }
}