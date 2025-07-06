namespace c969_scheduler_program
{
    partial class MainAppointments
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
            this.monthCalendar = new System.Windows.Forms.MonthCalendar();
            this.addApptBtn = new System.Windows.Forms.Button();
            this.apptDgv = new System.Windows.Forms.DataGridView();
            this.modApptBtn = new System.Windows.Forms.Button();
            this.deleteApptBtn = new System.Windows.Forms.Button();
            this.exitBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.apptDgv)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(330, 55);
            this.label1.TabIndex = 1;
            this.label1.Text = "Appointments";
            // 
            // monthCalendar
            // 
            this.monthCalendar.Location = new System.Drawing.Point(58, 82);
            this.monthCalendar.Name = "monthCalendar";
            this.monthCalendar.TabIndex = 2;
            this.monthCalendar.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar_DateChanged);
            // 
            // addApptBtn
            // 
            this.addApptBtn.Location = new System.Drawing.Point(76, 266);
            this.addApptBtn.Name = "addApptBtn";
            this.addApptBtn.Size = new System.Drawing.Size(188, 32);
            this.addApptBtn.TabIndex = 17;
            this.addApptBtn.Text = "Add appointment";
            this.addApptBtn.UseVisualStyleBackColor = true;
            this.addApptBtn.Click += new System.EventHandler(this.addApptBtn_Click);
            // 
            // apptDgv
            // 
            this.apptDgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.apptDgv.Location = new System.Drawing.Point(348, 18);
            this.apptDgv.Name = "apptDgv";
            this.apptDgv.Size = new System.Drawing.Size(407, 420);
            this.apptDgv.TabIndex = 20;
            // 
            // modApptBtn
            // 
            this.modApptBtn.Location = new System.Drawing.Point(76, 304);
            this.modApptBtn.Name = "modApptBtn";
            this.modApptBtn.Size = new System.Drawing.Size(188, 32);
            this.modApptBtn.TabIndex = 21;
            this.modApptBtn.Text = "Modify selected appointment";
            this.modApptBtn.UseVisualStyleBackColor = true;
            this.modApptBtn.Click += new System.EventHandler(this.modApptBtn_Click);
            // 
            // deleteApptBtn
            // 
            this.deleteApptBtn.Location = new System.Drawing.Point(76, 342);
            this.deleteApptBtn.Name = "deleteApptBtn";
            this.deleteApptBtn.Size = new System.Drawing.Size(188, 32);
            this.deleteApptBtn.TabIndex = 22;
            this.deleteApptBtn.Text = "Delete selected appointment";
            this.deleteApptBtn.UseVisualStyleBackColor = true;
            // 
            // exitBtn
            // 
            this.exitBtn.Location = new System.Drawing.Point(76, 380);
            this.exitBtn.Name = "exitBtn";
            this.exitBtn.Size = new System.Drawing.Size(188, 32);
            this.exitBtn.TabIndex = 23;
            this.exitBtn.Text = "Exit";
            this.exitBtn.UseVisualStyleBackColor = true;
            // 
            // MainAppointments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(773, 454);
            this.Controls.Add(this.exitBtn);
            this.Controls.Add(this.deleteApptBtn);
            this.Controls.Add(this.modApptBtn);
            this.Controls.Add(this.apptDgv);
            this.Controls.Add(this.addApptBtn);
            this.Controls.Add(this.monthCalendar);
            this.Controls.Add(this.label1);
            this.Name = "MainAppointments";
            this.Text = "MainAppointments";
            ((System.ComponentModel.ISupportInitialize)(this.apptDgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MonthCalendar monthCalendar;
        private System.Windows.Forms.Button addApptBtn;
        private System.Windows.Forms.DataGridView apptDgv;
        private System.Windows.Forms.Button modApptBtn;
        private System.Windows.Forms.Button deleteApptBtn;
        private System.Windows.Forms.Button exitBtn;
    }
}