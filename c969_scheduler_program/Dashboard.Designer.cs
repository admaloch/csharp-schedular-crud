namespace c969_scheduler_program
{
    partial class Dashboard
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
            this.label1 = new System.Windows.Forms.Label();
            this.usernameLbl = new System.Windows.Forms.Label();
            this.timeZoneLbl = new System.Windows.Forms.Label();
            this.regionLbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(209, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(392, 55);
            this.label1.TabIndex = 0;
            this.label1.Text = "Main Dashboard";
            // 
            // usernameLbl
            // 
            this.usernameLbl.AutoSize = true;
            this.usernameLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usernameLbl.Location = new System.Drawing.Point(267, 98);
            this.usernameLbl.Name = "usernameLbl";
            this.usernameLbl.Size = new System.Drawing.Size(262, 29);
            this.usernameLbl.TabIndex = 1;
            this.usernameLbl.Text = "Current user: admaloch";
            // 
            // timeZoneLbl
            // 
            this.timeZoneLbl.AutoSize = true;
            this.timeZoneLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeZoneLbl.Location = new System.Drawing.Point(280, 147);
            this.timeZoneLbl.Name = "timeZoneLbl";
            this.timeZoneLbl.Size = new System.Drawing.Size(247, 20);
            this.timeZoneLbl.TabIndex = 11;
            this.timeZoneLbl.Text = "TimeZone: Central Standard Time";
            // 
            // regionLbl
            // 
            this.regionLbl.AutoSize = true;
            this.regionLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.regionLbl.Location = new System.Drawing.Point(312, 127);
            this.regionLbl.Name = "regionLbl";
            this.regionLbl.Size = new System.Drawing.Size(166, 20);
            this.regionLbl.TabIndex = 10;
            this.regionLbl.Text = "Region: United States";
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.timeZoneLbl);
            this.Controls.Add(this.regionLbl);
            this.Controls.Add(this.usernameLbl);
            this.Controls.Add(this.label1);
            this.Name = "Dashboard";
            this.Text = "Dashboard";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label usernameLbl;
        private System.Windows.Forms.Label timeZoneLbl;
        private System.Windows.Forms.Label regionLbl;
    }
}