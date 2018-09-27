namespace DqTool
{
    partial class FormHp
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
            this.labelHp = new System.Windows.Forms.Label();
            this.progress = new ProgressBarExtensions();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelHp
            // 
            this.labelHp.AutoSize = true;
            this.labelHp.BackColor = System.Drawing.Color.Black;
            this.labelHp.Font = new System.Drawing.Font("Consolas", 11F);
            this.labelHp.ForeColor = System.Drawing.Color.White;
            this.labelHp.Location = new System.Drawing.Point(0, -1);
            this.labelHp.Name = "labelHp";
            this.labelHp.Size = new System.Drawing.Size(40, 18);
            this.labelHp.TabIndex = 0;
            this.labelHp.Text = "4500";
            // 
            // progress
            // 
            this.progress.BackColor = System.Drawing.Color.Black;
            this.progress.ForeColor = System.Drawing.Color.White;
            this.progress.Location = new System.Drawing.Point(40, 0);
            this.progress.MarqueeAnimationSpeed = 10;
            this.progress.Name = "progress";
            this.progress.Size = new System.Drawing.Size(160, 16);
            this.progress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progress.TabIndex = 1;
            this.progress.Value = 30;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Controls.Add(this.labelHp);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 16);
            this.panel1.TabIndex = 2;
            // 
            // FormHp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Red;
            this.ClientSize = new System.Drawing.Size(200, 16);
            this.Controls.Add(this.progress);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormHp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FormHp";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormHp_FormClosing);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormHp_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormHp_MouseMove);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.FormHp_PreviewKeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelHp;
        private ProgressBarExtensions progress;
        private System.Windows.Forms.Panel panel1;
    }
}