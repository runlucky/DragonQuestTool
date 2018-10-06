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
using DqTool.UI.Class;

namespace DqTool.UI
{
    public partial class HpGauge : Form
    {
        private HitPoint _hitPoint;

        public HpGauge(HitPoint hp, Point location)
        {
            InitializeComponent();

            _hitPoint = hp;

            progress.Maximum = _hitPoint.Now;
            Location = location;
        }

        private void Reflesh()
        {
            labelHp.Text = _hitPoint.Now.ToString();
            progress.Value = _hitPoint.Now;
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

        internal void Heal(int value)
        {
            _hitPoint.Heal(value);
            Refresh();
        }

        internal void Damage(int value)
        {
            _hitPoint.Damage(value);
            Refresh();
        }
    }
}
