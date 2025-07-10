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
            this.appointmentsBtn = new System.Windows.Forms.Button();
            this.customersBtn = new System.Windows.Forms.Button();
            this.reportsBtn = new System.Windows.Forms.Button();
            this.logoutBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(127, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(392, 55);
            this.label1.TabIndex = 0;
            this.label1.Text = "Main Dashboard";
            // 
            // usernameLbl
            // 
            this.usernameLbl.AutoSize = true;
            this.usernameLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usernameLbl.Location = new System.Drawing.Point(187, 122);
            this.usernameLbl.Name = "usernameLbl";
            this.usernameLbl.Size = new System.Drawing.Size(262, 29);
            this.usernameLbl.TabIndex = 1;
            this.usernameLbl.Text = "Current user: admaloch";
            // 
            // timeZoneLbl
            // 
            this.timeZoneLbl.AutoSize = true;
            this.timeZoneLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeZoneLbl.Location = new System.Drawing.Point(202, 171);
            this.timeZoneLbl.Name = "timeZoneLbl";
            this.timeZoneLbl.Size = new System.Drawing.Size(247, 20);
            this.timeZoneLbl.TabIndex = 11;
            this.timeZoneLbl.Text = "TimeZone: Central Standard Time";
            // 
            // regionLbl
            // 
            this.regionLbl.AutoSize = true;
            this.regionLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.regionLbl.Location = new System.Drawing.Point(234, 151);
            this.regionLbl.Name = "regionLbl";
            this.regionLbl.Size = new System.Drawing.Size(166, 20);
            this.regionLbl.TabIndex = 10;
            this.regionLbl.Text = "Region: United States";
            // 
            // appointmentsBtn
            // 
            this.appointmentsBtn.Location = new System.Drawing.Point(324, 244);
            this.appointmentsBtn.Name = "appointmentsBtn";
            this.appointmentsBtn.Size = new System.Drawing.Size(95, 32);
            this.appointmentsBtn.TabIndex = 12;
            this.appointmentsBtn.Text = "Appointments";
            this.appointmentsBtn.UseVisualStyleBackColor = true;
            this.appointmentsBtn.Click += new System.EventHandler(this.appointmentsBtn_Click);
            // 
            // customersBtn
            // 
            this.customersBtn.Location = new System.Drawing.Point(223, 244);
            this.customersBtn.Name = "customersBtn";
            this.customersBtn.Size = new System.Drawing.Size(95, 32);
            this.customersBtn.TabIndex = 13;
            this.customersBtn.Text = "Customers";
            this.customersBtn.UseVisualStyleBackColor = true;
            this.customersBtn.Click += new System.EventHandler(this.customersBtn_Click);
            // 
            // reportsBtn
            // 
            this.reportsBtn.Location = new System.Drawing.Point(223, 282);
            this.reportsBtn.Name = "reportsBtn";
            this.reportsBtn.Size = new System.Drawing.Size(95, 32);
            this.reportsBtn.TabIndex = 14;
            this.reportsBtn.Text = "Reports";
            this.reportsBtn.UseVisualStyleBackColor = true;
            this.reportsBtn.Click += new System.EventHandler(this.reportsBtn_Click);
            // 
            // logoutBtn
            // 
            this.logoutBtn.Location = new System.Drawing.Point(324, 282);
            this.logoutBtn.Name = "logoutBtn";
            this.logoutBtn.Size = new System.Drawing.Size(95, 32);
            this.logoutBtn.TabIndex = 15;
            this.logoutBtn.Text = "Logout";
            this.logoutBtn.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(282, 206);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 20);
            this.label2.TabIndex = 16;
            this.label2.Text = "Navigation:";
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 393);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.logoutBtn);
            this.Controls.Add(this.reportsBtn);
            this.Controls.Add(this.customersBtn);
            this.Controls.Add(this.appointmentsBtn);
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
        private System.Windows.Forms.Button appointmentsBtn;
        private System.Windows.Forms.Button customersBtn;
        private System.Windows.Forms.Button reportsBtn;
        private System.Windows.Forms.Button logoutBtn;
        private System.Windows.Forms.Label label2;
    }
}