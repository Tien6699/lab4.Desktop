namespace Lab_Basic_Command
{
    partial class BillCatalogForm
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
            this.lbxNgayLap = new System.Windows.Forms.ListBox();
            this.dgvBillDetails = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBillDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // lbxNgayLap
            // 
            this.lbxNgayLap.FormattingEnabled = true;
            this.lbxNgayLap.ItemHeight = 20;
            this.lbxNgayLap.Location = new System.Drawing.Point(33, 26);
            this.lbxNgayLap.Name = "lbxNgayLap";
            this.lbxNgayLap.Size = new System.Drawing.Size(217, 344);
            this.lbxNgayLap.TabIndex = 0;
            this.lbxNgayLap.SelectedIndexChanged += new System.EventHandler(this.lbxNgayLap_SelectedIndexChanged);
            // 
            // dgvBillDetails
            // 
            this.dgvBillDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBillDetails.Location = new System.Drawing.Point(264, 25);
            this.dgvBillDetails.Name = "dgvBillDetails";
            this.dgvBillDetails.RowHeadersWidth = 62;
            this.dgvBillDetails.RowTemplate.Height = 28;
            this.dgvBillDetails.Size = new System.Drawing.Size(406, 344);
            this.dgvBillDetails.TabIndex = 1;
            // 
            // BillCatalogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 395);
            this.Controls.Add(this.dgvBillDetails);
            this.Controls.Add(this.lbxNgayLap);
            this.Name = "BillCatalogForm";
            this.Text = "BillCatalogForm";
            ((System.ComponentModel.ISupportInitialize)(this.dgvBillDetails)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbxNgayLap;
        private System.Windows.Forms.DataGridView dgvBillDetails;
    }
}