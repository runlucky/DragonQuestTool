namespace DqTool.UI
{
    partial class Dashboard
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Dashboard));
            this.button = new System.Windows.Forms.Button();
            this.labelMs = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.scanPosX = new System.Windows.Forms.NumericUpDown();
            this.scanPosY = new System.Windows.Forms.NumericUpDown();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbWait = new System.Windows.Forms.TextBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scanPosX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scanPosY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // button
            // 
            this.button.Location = new System.Drawing.Point(4, 4);
            this.button.Name = "button";
            this.button.Size = new System.Drawing.Size(80, 23);
            this.button.TabIndex = 0;
            this.button.Text = "計測開始";
            this.button.UseVisualStyleBackColor = true;
            this.button.Click += new System.EventHandler(this.OnScanButtonClick);
            // 
            // labelMs
            // 
            this.labelMs.AutoSize = true;
            this.labelMs.Location = new System.Drawing.Point(90, 9);
            this.labelMs.Name = "labelMs";
            this.labelMs.Size = new System.Drawing.Size(26, 12);
            this.labelMs.TabIndex = 5;
            this.labelMs.Text = "0ms";
            // 
            // pictureBox1
            // 
            this.pictureBox1.ImageLocation = "";
            this.pictureBox1.Location = new System.Drawing.Point(4, 86);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(144, 32);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // scanPosX
            // 
            this.scanPosX.Location = new System.Drawing.Point(79, 61);
            this.scanPosX.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.scanPosX.Name = "scanPosX";
            this.scanPosX.Size = new System.Drawing.Size(48, 19);
            this.scanPosX.TabIndex = 7;
            this.scanPosX.Value = new decimal(new int[] {
            49,
            0,
            0,
            0});
            this.scanPosX.ValueChanged += new System.EventHandler(this.ScanPosX_ValueChanged);
            // 
            // scanPosY
            // 
            this.scanPosY.Location = new System.Drawing.Point(133, 61);
            this.scanPosY.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.scanPosY.Name = "scanPosY";
            this.scanPosY.Size = new System.Drawing.Size(48, 19);
            this.scanPosY.TabIndex = 7;
            this.scanPosY.Value = new decimal(new int[] {
            417,
            0,
            0,
            0});
            this.scanPosY.ValueChanged += new System.EventHandler(this.ScanPosY_ValueChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "DQ5",
            "DQ6"});
            this.comboBox1.Location = new System.Drawing.Point(4, 33);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(80, 20);
            this.comboBox1.TabIndex = 10;
            this.comboBox1.Text = "DQ5";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 11;
            this.label1.Text = "スキャン座標";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(92, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 12);
            this.label2.TabIndex = 12;
            this.label2.Text = "Wait";
            // 
            // tbWait
            // 
            this.tbWait.Location = new System.Drawing.Point(124, 33);
            this.tbWait.Name = "tbWait";
            this.tbWait.Size = new System.Drawing.Size(57, 19);
            this.tbWait.TabIndex = 13;
            this.tbWait.Text = "0";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::DqTool.UI.Properties.Resources._01_oyabun2;
            this.pictureBox2.ImageLocation = "";
            this.pictureBox2.InitialImage = Properties.Resources._01_oyabun2;
            this.pictureBox2.Location = new System.Drawing.Point(4, 124);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(144, 32);
            this.pictureBox2.TabIndex = 6;
            this.pictureBox2.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(4, 162);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 14;
            this.button1.Text = "画像更新";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.OnRefreshImageButtonClick);
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(188, 190);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tbWait);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.scanPosY);
            this.Controls.Add(this.scanPosX);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.labelMs);
            this.Controls.Add(this.button);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Dashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "どらくえつーる";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.scanPosX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.scanPosY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button;
        private System.Windows.Forms.Label labelMs;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.NumericUpDown scanPosX;
        private System.Windows.Forms.NumericUpDown scanPosY;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbWait;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button button1;
    }
}

