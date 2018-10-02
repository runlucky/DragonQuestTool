using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Linq;

namespace DqTool.Core.Extensions
{
    public static class BitmapExtensions
    {
        public static bool Equal(this Bitmap lhs, Bitmap rhs)
        {
            if (lhs.Width != rhs.Width) return false;
            if (lhs.Height != rhs.Height) return false;

            var bd1 = lhs.LockBits(new Rectangle(0, 0, lhs.Width, lhs.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            var bd2 = rhs.LockBits(new Rectangle(0, 0, rhs.Width, rhs.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

            if (bd1.Stride != bd2.Stride)
            {
                lhs.UnlockBits(bd1);
                rhs.UnlockBits(bd2);
                return false;
            }

            int bsize = bd1.Stride * lhs.Height;
            var bytes1 = new byte[bsize];
            var bytes2 = new byte[bsize];
            Marshal.Copy(bd1.Scan0, bytes1, 0, bsize);
            Marshal.Copy(bd2.Scan0, bytes2, 0, bsize);

            lhs.UnlockBits(bd1);
            rhs.UnlockBits(bd2);

            var md5 = new MD5CryptoServiceProvider();
            byte[] hash1 = md5.ComputeHash(bytes1);
            byte[] hash2 = md5.ComputeHash(bytes2);

            return hash1.SequenceEqual(hash2);
        }
    }
}
