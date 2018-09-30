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

namespace DqLibrary
{
    /// <summary>
    /// 画像処理
    /// </summary>
    public class NumberConverter
    {
        /// <summary>
        /// 0-9の数値を格納
        /// </summary>
        private readonly Bitmap[] nums = new Bitmap[10];

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public NumberConverter(Bitmap numbers)
        {
            for (int i = 0; i < 10; i++)
            {
                nums[i] = numbers.Clone(new Rectangle(i * 16, 0, 16, 16), numbers.PixelFormat);
            }
        }

        public int? ToInt(Bitmap num)
        {
            for (int i = 0; i < 10; i++)
            {
                if (num.Equal(nums[i])) return i;
            }
            return null;
        }
    }
}
