using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Security.Cryptography;

using DqTool.Core.Extensions;
using DqTool.Core;
using System.Diagnostics;
using DqTool.UI.Class;

namespace DqTool.UI
{
    public partial class Dashboard : Form
    {
        private bool isAnalyzing = false;
        private Scanner scanner = new Scanner();
        private Point ScanLocation => new Point((int)scanPosX.Value, (int)scanPosY.Value);

        public Dashboard()
        {
            InitializeComponent();

            var data = Properties.Settings.Default;
            Location = data.MainPos;
            tbWait.Text = data.Wait;
            scanPosX.Value = data.ScanPos.X;
            scanPosY.Value = data.ScanPos.Y;

            RefreshImage();
        }

        private async void OnScanButtonClick(object sender, EventArgs e)
        {
            if (isAnalyzing)
            {
                button.Text = "計測開始";
                scanner.shouldScanStop = true;
                isAnalyzing = false;
            }
            else
            {
                button.Text = "計測終了";
                scanner.shouldScanStop = false;
                isAnalyzing = true;

                scanner.Init(ScanLocation);

                while (true)
                {
                    var time = await scanner.ScanAsync(tbWait.Text.ToInt(100));
                    if (time == -1) break;
                    labelMs.Text = $"負荷 {time}ms";
                }
            }
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            scanner.Dispose();

            Properties.Settings.Default.MainPos = Location;
            Properties.Settings.Default.Wait = tbWait.Text;
            Properties.Settings.Default.ScanPos = ScanLocation;

            Properties.Settings.Default.Save();
        }

        private void RefreshImage() => pictureBox1.Image = scanner.ReScanImage(ScanLocation);

        private void ScanPosX_ValueChanged(object sender, EventArgs e) => RefreshImage();

        private void ScanPosY_ValueChanged(object sender, EventArgs e) => RefreshImage();

        private void Button1_Click_1(object sender, EventArgs e) => RefreshImage();
    }
}
