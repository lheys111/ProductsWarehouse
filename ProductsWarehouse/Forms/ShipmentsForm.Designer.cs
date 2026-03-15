namespace ProductsWarehouse.Forms
{
    partial class ShipmentsForm
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
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnView = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.dgvShipments = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShipments)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnRefresh.Location = new System.Drawing.Point(357, 332);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(90, 23);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.Text = "Обновить";
            this.btnRefresh.UseVisualStyleBackColor = false;
            // 
            // btnView
            // 
            this.btnView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnView.Location = new System.Drawing.Point(154, 332);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(81, 23);
            this.btnView.TabIndex = 1;
            this.btnView.Text = "Просмотр";
            this.btnView.UseVisualStyleBackColor = false;
            // 
            // btnNew
            // 
            this.btnNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnNew.Location = new System.Drawing.Point(42, 332);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(83, 23);
            this.btnNew.TabIndex = 2;
            this.btnNew.Text = "Новая отгрузка";
            this.btnNew.UseVisualStyleBackColor = false;
            // 
            // dgvShipments
            // 
            this.dgvShipments.AllowUserToAddRows = false;
            this.dgvShipments.AllowUserToDeleteRows = false;
            this.dgvShipments.BackgroundColor = System.Drawing.Color.White;
            this.dgvShipments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvShipments.Location = new System.Drawing.Point(42, 35);
            this.dgvShipments.Name = "dgvShipments";
            this.dgvShipments.ReadOnly = true;
            this.dgvShipments.RowHeadersWidth = 51;
            this.dgvShipments.RowTemplate.Height = 24;
            this.dgvShipments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvShipments.Size = new System.Drawing.Size(393, 276);
            this.dgvShipments.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Состав отгрузки";
            // 
            // ShipmentsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 384);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvShipments);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.btnRefresh);
            this.Name = "ShipmentsForm";
            this.Text = "Журнал отгрузок";
           // this.Load += new System.EventHandler(this.ShipmentsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvShipments)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.DataGridView dgvShipments;
        private System.Windows.Forms.Label label1;
    }
}