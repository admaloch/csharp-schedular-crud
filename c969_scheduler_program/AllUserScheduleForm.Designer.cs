namespace c969_scheduler_program
{
    partial class AllUserScheduleForm
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
            this.schedulesDgv = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.schedulesDgv)).BeginInit();
            this.SuspendLayout();
            // 
            // schedulesDgv
            // 
            this.schedulesDgv.AllowUserToAddRows = false;
            this.schedulesDgv.AllowUserToDeleteRows = false;
            this.schedulesDgv.AllowUserToOrderColumns = true;
            this.schedulesDgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.schedulesDgv.Location = new System.Drawing.Point(130, 111);
            this.schedulesDgv.Name = "schedulesDgv";
            this.schedulesDgv.Size = new System.Drawing.Size(541, 284);
            this.schedulesDgv.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(107, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(595, 42);
            this.label1.TabIndex = 2;
            this.label1.Text = "All users\' appointment schedules";
            // 
            // AllUserScheduleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.schedulesDgv);
            this.Controls.Add(this.label1);
            this.Name = "AllUserScheduleForm";
            this.Text = "AllUserScheduleForm";
            ((System.ComponentModel.ISupportInitialize)(this.schedulesDgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView schedulesDgv;
        private System.Windows.Forms.Label label1;
    }
}