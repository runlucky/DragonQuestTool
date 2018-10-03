using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using DqTool.Core;
using DqTool.UI.Extensions;

namespace DqTool.UI
{
    public partial class HpGauge : Form
    {
        public HpGauge(int maxHp, Point location)
        {
            InitializeComponent();

            progress.Maximum = maxHp;
            Hp = maxHp;
            Location = location;
        }

        public int Hp
        {
            set
            {
                labelHp.Text = value.ToString();
                progress.Value = value;
            }
        }

        private Point _mousePoint;
        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (!e.IsLeftClicking()) return;
            _mousePoint = e.Location;
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (!e.IsLeftClicking()) return;
            Left += e.X - _mousePoint.X;
            Top += e.Y - _mousePoint.Y;
        }

        private void OnPreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left: Left--; break;
                case Keys.Right: Left++; break;
                case Keys.Up: Top--; break;
                case Keys.Down: Top++; break;
            }
        }
    }
}
