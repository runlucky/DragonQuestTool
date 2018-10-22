using System;
using System.Drawing;
using System.Windows.Forms;
using DqTool.Core;
using DqTool.UI.Class;
using MicroResolver;

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
            DIContainer.Init();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Dashboard());
        }
    }

    public static class DIContainer
    {
        internal static void Init()
        {
            Resolver = ObjectResolver.Create();
            Resolver.Register<IScanner, Scanner>();
        }

        public static ObjectResolver Resolver { get; private set; }
    }
}
