namespace BTEAManual
{
    partial class Main
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
            this.dateRecordDate = new System.Windows.Forms.DateTimePicker();
            this.lblBusinessDate = new System.Windows.Forms.Label();
            this.btnGetSendData = new System.Windows.Forms.Button();
            this.pictureProgress = new System.Windows.Forms.PictureBox();
            this.lblProgress = new System.Windows.Forms.Label();
            this.line = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureProgress)).BeginInit();
            this.SuspendLayout();
            // 
            // dateRecordDate
            // 
            this.dateRecordDate.CustomFormat = " dd/MM/yyyy";
            this.dateRecordDate.Enabled = false;
            this.dateRecordDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateRecordDate.Location = new System.Drawing.Point(94, 19);
            this.dateRecordDate.Name = "dateRecordDate";
            this.dateRecordDate.Size = new System.Drawing.Size(80, 20);
            this.dateRecordDate.TabIndex = 0;
            // 
            // lblBusinessDate
            // 
            this.lblBusinessDate.AutoSize = true;
            this.lblBusinessDate.Location = new System.Drawing.Point(14, 21);
            this.lblBusinessDate.Name = "lblBusinessDate";
            this.lblBusinessDate.Size = new System.Drawing.Size(74, 13);
            this.lblBusinessDate.TabIndex = 1;
            this.lblBusinessDate.Text = "Business Date";
            // 
            // btnGetSendData
            // 
            this.btnGetSendData.Enabled = false;
            this.btnGetSendData.Location = new System.Drawing.Point(205, 16);
            this.btnGetSendData.Name = "btnGetSendData";
            this.btnGetSendData.Size = new System.Drawing.Size(112, 23);
            this.btnGetSendData.TabIndex = 2;
            this.btnGetSendData.Text = "Get && Send Data";
            this.btnGetSendData.UseVisualStyleBackColor = true;
            // 
            // pictureProgress
            // 
            this.pictureProgress.Location = new System.Drawing.Point(17, 61);
            this.pictureProgress.Name = "pictureProgress";
            this.pictureProgress.Size = new System.Drawing.Size(21, 21);
            this.pictureProgress.TabIndex = 12;
            this.pictureProgress.TabStop = false;
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Location = new System.Drawing.Point(41, 65);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(0, 13);
            this.lblProgress.TabIndex = 13;
            // 
            // line
            // 
            this.line.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.line.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.line.Location = new System.Drawing.Point(12, 49);
            this.line.Name = "line";
            this.line.Size = new System.Drawing.Size(305, 1);
            this.line.TabIndex = 14;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(329, 95);
            this.Controls.Add(this.line);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.pictureProgress);
            this.Controls.Add(this.btnGetSendData);
            this.Controls.Add(this.lblBusinessDate);
            this.Controls.Add(this.dateRecordDate);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Main";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BTEA Manual";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.pictureProgress)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateRecordDate;
        private System.Windows.Forms.Label lblBusinessDate;
        private System.Windows.Forms.Button btnGetSendData;
        private System.Windows.Forms.PictureBox pictureProgress;
        public System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Label line;
    }
}

