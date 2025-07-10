namespace c969_scheduler_program
{
    partial class CustomerEngagementForm
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
            this.customerDgv = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.customerDgv)).BeginInit();
            this.SuspendLayout();
            // 
            // customerDgv
            // 
            this.customerDgv.AllowUserToAddRows = false;
            this.customerDgv.AllowUserToDeleteRows = false;
            this.customerDgv.AllowUserToOrderColumns = true;
            this.customerDgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.customerDgv.Location = new System.Drawing.Point(126, 111);
            this.customerDgv.Name = "customerDgv";
            this.customerDgv.Size = new System.Drawing.Size(541, 284);
            this.customerDgv.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(194, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(416, 42);
            this.label1.TabIndex = 4;
            this.label1.Text = "Customer engagement";
            // 
            // CustomerEngagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.customerDgv);
            this.Controls.Add(this.label1);
            this.Name = "CustomerEngagementForm";
            this.Text = "CustomerEngagementForm";
            ((System.ComponentModel.ISupportInitialize)(this.customerDgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView customerDgv;
        private System.Windows.Forms.Label label1;
    }
}