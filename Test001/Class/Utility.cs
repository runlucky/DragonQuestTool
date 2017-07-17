using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DqTool
{
    public static class Parse
    {
        /// <summary>
        /// int型に変換して返します
        /// </summary>
        /// <param name="text"></param>
        /// <param name="defaultValue">変換できなかった時の値</param>
        /// <returns></returns>
        public static int ToInt(this string text, int defaultValue = 0)
        {
            int r;
            if (int.TryParse(text, out r))
            {
                return r;
            }
            return defaultValue;
        }
    }

    public class ProgressBarEx : ProgressBar
    {
        private int PBS_SMOOTHREVERSE = 0x0010;

        protected override CreateParams CreateParams
        {
            [SecurityPermission(SecurityAction.Demand,
                Flags = SecurityPermissionFlag.UnmanagedCode)]
            get
            {
                CreateParams cps = base.CreateParams;
                //コントロールのスタイルにPBS_SMOOTHREVERSEを追加する
                cps.Style |= PBS_SMOOTHREVERSE;
                return cps;
            }
        }
    }

    public static class EnumerableExtentions
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T, int> action)
        {
            int index = 0;
            foreach (var x in source)
                action(x, index++);
        }
    }
}
