using System;
using System.Collections.Generic;
using System.Text;

namespace DqLibrary.Extensions
{
    public static class StringExtensions
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
}
