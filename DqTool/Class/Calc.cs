using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

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
        private static Bitmap[] nums = new Bitmap[10];

        private static Bitmap hm1;
        private static Bitmap hm2;
        private static Bitmap hm3;
        private static Bitmap h1;
        private static Bitmap h2;
        private static Bitmap h3;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        static Calc()
        {
            var bmp = new Bitmap(@"img\number.bmp");
            for (int i = 0; i < 10; i++)
            {
                nums[i] = bmp.Clone(new Rectangle(i * 16, 0, 16, 16), bmp.PixelFormat);
            }

            hm1 = new Bitmap(@"img\HM1.PNG");
            hm2 = new Bitmap(@"img\HM2.PNG");
            hm3 = new Bitmap(@"img\HM3.PNG");
            h1 = new Bitmap(@"img\H1.PNG");
            h2 = new Bitmap(@"img\H2.PNG");
            h3 = new Bitmap(@"img\H3.PNG");
        }

        public static bool IsHM(Bitmap bmp)
        {
            if (IsMatch(bmp, hm1)) return true;
            if (IsMatch(bmp, hm2)) return true;
            if (IsMatch(bmp, hm3)) return true;
            return false;
        }

        public static bool IsH(Bitmap bmp)
        {
            if (IsMatch(bmp, h1)) return true;
            if (IsMatch(bmp, h2)) return true;
            if (IsMatch(bmp, h3)) return true;
            return false;
        }

        public static int GetNum(Bitmap num)
        {
            for (int i = 0; i < 10; i++)
            {
                if (IsMatch(num, nums[i])) return i;
            }
            return -1;
        }

        public static bool IsMatch(Bitmap bmp1, Bitmap bmp2)
        {
            if (bmp1.Width != bmp2.Width) return false;
            if (bmp1.Height != bmp2.Height) return false;

            var bd1 = bmp1.LockBits(new Rectangle(0, 0, bmp1.Width, bmp1.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            var bd2 = bmp2.LockBits(new Rectangle(0, 0, bmp2.Width, bmp2.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

            if (bd1.Stride != bd2.Stride)
            {
                bmp1.UnlockBits(bd1);
                bmp2.UnlockBits(bd2);
                return false;
            }

            int bsize = bd1.Stride * bmp1.Height;
            var bytes1 = new byte[bsize];
            var bytes2 = new byte[bsize];
            Marshal.Copy(bd1.Scan0, bytes1, 0, bsize);
            Marshal.Copy(bd2.Scan0, bytes2, 0, bsize);

            bmp1.UnlockBits(bd1);
            bmp2.UnlockBits(bd2);

            var md5 = new MD5CryptoServiceProvider();
            byte[] hash1 = md5.ComputeHash(bytes1);
            byte[] hash2 = md5.ComputeHash(bytes2);

            return hash1.SequenceEqual(hash2);
        }

        /// <summary>
        /// 指定した矩形のキャプチャ画像を返す
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public static Bitmap Scan(Rectangle r)
        {
            var scanImage = new Bitmap(r.Width, r.Height);
            var g = Graphics.FromImage(scanImage);
            g.CopyFromScreen(
                new Point(r.X, r.Y),
                new Point(0, 0),
                scanImage.Size
            );
            g.Dispose();

            return scanImage;
        }
    }
}
