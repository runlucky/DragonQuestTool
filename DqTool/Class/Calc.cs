using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

using DqLibrary.Extensions;

namespace DqTool
{
    /// <summary>
    /// 画像処理
    /// </summary>
    public static class Calc
    {
        /// <summary>
        /// 0-9の数値を格納
        /// </summary>
        private static readonly Bitmap[] nums = new Bitmap[10];

        private static readonly Bitmap hm1 = Properties.Resources.HM1;
        private static readonly Bitmap hm2 = Properties.Resources.HM2;
        private static readonly Bitmap hm3 = Properties.Resources.HM3;
        private static readonly Bitmap h1 = Properties.Resources.H1;
        private static readonly Bitmap h2 = Properties.Resources.H2;
        private static readonly Bitmap h3 = Properties.Resources.H3;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        static Calc()
        {
            var bmp = Properties.Resources.number;
            for (int i = 0; i < 10; i++)
            {
                nums[i] = bmp.Clone(new Rectangle(i * 16, 0, 16, 16), bmp.PixelFormat);
            }
        }

        public static bool IsCommandPhase(Bitmap bmp)
        {
            if (bmp.Equal(hm1)) return true;
            if (bmp.Equal(hm2)) return true;
            if (bmp.Equal(hm3)) return true;
            return false;
        }

        public static bool IsBattlePhase(Bitmap bmp)
        {
            if (bmp.Equal(h1)) return true;
            if (bmp.Equal(h2)) return true;
            if (bmp.Equal(h3)) return true;
            return false;
        }

        public static int ToInt(Bitmap num)
        {
            for (int i = 0; i < 10; i++)
            {
                if (num.Equal(nums[i])) return i;
            }
            return -1;
        }
    }
}
