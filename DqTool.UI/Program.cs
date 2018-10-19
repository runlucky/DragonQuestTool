using System;
using System.Drawing;
using System.Windows.Forms;
using DqTool.UI.Class;

namespace DqTool.UI
{
    internal static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        private static void Main()
        {

            var data = new Bp
            {
                image = Properties.Resources._01_oyabun1
            };

            Serializer<Bp>.Serialize("bitmapdata.xml", data);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Dashboard());

        }

    }

    public class Bp
    {
        public Bitmap image
        {
            get; set;
        }
    }
}
