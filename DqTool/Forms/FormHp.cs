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
using DqLibrary;

namespace DqTool
{
    public partial class FormHp : Form
    {
        private int mhp;
        private FormType formType;

        public FormHp(int mh, FormType ft = FormType.Center)
        {
            InitializeComponent();

            progress.MouseDown += new MouseEventHandler(FormHp_MouseDown);
            progress.MouseMove += new MouseEventHandler(FormHp_MouseMove);
            labelHp.MouseDown += new MouseEventHandler(FormHp_MouseDown);
            labelHp.MouseMove += new MouseEventHandler(FormHp_MouseMove);

            mhp = mh;
            TransparencyKey = Color.Red;
            labelHp.Text = mh.ToString();
            progress.Value = 100;
            formType = ft;
        }

        public void SetHp(int h)
        {
            labelHp.Text = h.ToString();
            var per = (int)((1.0 * h / mhp) * 100);
            if (per < 25) ProgressRed();
            else if (per < 100) ProgressYellow();
            progress.Value = per;
        }

        public void ReLocation()
        {
            switch (formType)
            {
                case FormType.Center:
                    Location = Properties.Settings.Default.HpPos;
                    break;

                case FormType.Left:
                    Location = Properties.Settings.Default.HpLPos;
                    break;

                case FormType.Right:
                    Location = Properties.Settings.Default.HpRPos;
                    break;
            }
        }

        private Point mousePoint;

        private void FormHp_MouseDown(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                //位置を記憶する
                mousePoint = new Point(e.X, e.Y);
            }
        }

        private void FormHp_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                this.Left += e.X - mousePoint.X;
                this.Top += e.Y - mousePoint.Y;
            }
        }

        /// <summary>
        /// プログレスバーの長さが正しく反映されないので使用禁止
        /// </summary>
        public void ProgressRed()
        {
            //SendMessage(new HandleRef(progress, progress.Handle),
            //    PBM_SETSTATE, PBST_ERROR, IntPtr.Zero);
        }

        /// <summary>
        /// プログレスバーの長さが正しく反映されないので使用禁止
        /// </summary>
        private void ProgressYellow()
        {
            //SendMessage(new HandleRef(progress, progress.Handle),
            //    PBM_SETSTATE, PBST_PAUSED, IntPtr.Zero);
        }

        private void FormHp_FormClosing(object sender, FormClosingEventArgs e)
        {
            switch (formType)
            {
                case FormType.Center:
                    Properties.Settings.Default.HpPos = Location;
                    break;

                case FormType.Left:
                    Properties.Settings.Default.HpLPos = Location;
                    break;

                case FormType.Right:
                    Properties.Settings.Default.HpRPos = Location;
                    break;
            }
            Properties.Settings.Default.Save();
        }

        private void FormHp_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    Left = Left - 1;
                    break;

                case Keys.Right:
                    Left = Left + 1;
                    break;

                case Keys.Up:
                    Top = Top - 1;
                    break;

                case Keys.Down:
                    Top = Top + 1;
                    break;

                default:
                    break;
            }
        }
    }
}
