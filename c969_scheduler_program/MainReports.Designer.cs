namespace c969_scheduler_program
{
    partial class MainReports
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
            this.apptTypesBtn = new System.Windows.Forms.Button();
            this.userScheduleBtn = new System.Windows.Forms.Button();
            this.topCustomersBtn = new System.Windows.Forms.Button();
            this.exitBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(43, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(460, 73);
            this.label1.TabIndex = 0;
            this.label1.Text = "Create reports:";
            // 
            // apptTypesBtn
            // 
            this.apptTypesBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.apptTypesBtn.Location = new System.Drawing.Point(104, 130);
            this.apptTypesBtn.Name = "apptTypesBtn";
            this.apptTypesBtn.Size = new System.Drawing.Size(329, 38);
            this.apptTypesBtn.TabIndex = 1;
            this.apptTypesBtn.Text = "Number of appointment types per month";
            this.apptTypesBtn.UseVisualStyleBackColor = true;
            this.apptTypesBtn.Click += new System.EventHandler(this.apptTypesBtn_Click);
            // 
            // userScheduleBtn
            // 
            this.userScheduleBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userScheduleBtn.Location = new System.Drawing.Point(189, 174);
            this.userScheduleBtn.Name = "userScheduleBtn";
            this.userScheduleBtn.Size = new System.Drawing.Size(173, 38);
            this.userScheduleBtn.TabIndex = 2;
            this.userScheduleBtn.Text = "All users\' schedules";
            this.userScheduleBtn.UseVisualStyleBackColor = true;
            this.userScheduleBtn.Click += new System.EventHandler(this.userScheduleBtn_Click);
            // 
            // topCustomersBtn
            // 
            this.topCustomersBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.topCustomersBtn.Location = new System.Drawing.Point(175, 218);
            this.topCustomersBtn.Name = "topCustomersBtn";
            this.topCustomersBtn.Size = new System.Drawing.Size(202, 38);
            this.topCustomersBtn.TabIndex = 3;
            this.topCustomersBtn.Text = "Customer engagement";
            this.topCustomersBtn.UseVisualStyleBackColor = true;
            this.topCustomersBtn.Click += new System.EventHandler(this.topCustomersBtn_Click);
            // 
            // exitBtn
            // 
            this.exitBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exitBtn.Location = new System.Drawing.Point(228, 262);
            this.exitBtn.Name = "exitBtn";
            this.exitBtn.Size = new System.Drawing.Size(90, 38);
            this.exitBtn.TabIndex = 4;
            this.exitBtn.Text = "Exit";
            this.exitBtn.UseVisualStyleBackColor = true;
            this.exitBtn.Click += new System.EventHandler(this.exitBtn_Click);
            // 
            // MainReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 344);
            this.Controls.Add(this.exitBtn);
            this.Controls.Add(this.topCustomersBtn);
            this.Controls.Add(this.userScheduleBtn);
            this.Controls.Add(this.apptTypesBtn);
            this.Controls.Add(this.label1);
            this.Name = "MainReports";
            this.Text = "MainReports";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button apptTypesBtn;
        private System.Windows.Forms.Button userScheduleBtn;
        private System.Windows.Forms.Button topCustomersBtn;
        private System.Windows.Forms.Button exitBtn;
    }
}