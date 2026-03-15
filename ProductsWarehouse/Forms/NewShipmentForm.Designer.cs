namespace ProductsWarehouse.Forms
{
    partial class NewShipmentForm
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
          //  this.lblInfo = new System.Windows.Forms.Label();
            //this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblInfo
             /*
            this.lblInfo.AutoSize = true;
            this.lblInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblInfo.Location = new System.Drawing.Point(40, 37);
            this.lblInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(402, 25);
            this.lblInfo.TabIndex = 1;
            this.lblInfo.Text = "Форма создания отгрузки (в разработке)";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(200, 123);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(133, 37);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
           */ // 
            // NewShipmentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 246);
           // this.Controls.Add(this.btnClose);
           // this.Controls.Add(this.lblInfo);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "NewShipmentForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Новая отгрузка";
           // this.Load += new System.EventHandler(this.NewShipmentForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}