namespace c969_scheduler_program
{
    partial class ApptTypesForm
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
            this.apptMonthDgv = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.apptMonthDgv)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(153, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(516, 42);
            this.label1.TabIndex = 0;
            this.label1.Text = "Appointment types by month";
            // 
            // apptMonthDgv
            // 
            this.apptMonthDgv.AllowUserToAddRows = false;
            this.apptMonthDgv.AllowUserToDeleteRows = false;
            this.apptMonthDgv.AllowUserToOrderColumns = true;
            this.apptMonthDgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.apptMonthDgv.Location = new System.Drawing.Point(143, 105);
            this.apptMonthDgv.Name = "apptMonthDgv";
            this.apptMonthDgv.Size = new System.Drawing.Size(541, 284);
            this.apptMonthDgv.TabIndex = 1;
            // 
            // ApptTypesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.apptMonthDgv);
            this.Controls.Add(this.label1);
            this.Name = "ApptTypesForm";
            this.Text = "ApptTypesForm";
            ((System.ComponentModel.ISupportInitialize)(this.apptMonthDgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView apptMonthDgv;
    }
}