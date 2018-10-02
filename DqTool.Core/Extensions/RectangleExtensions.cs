using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace DqTool.Core.Extensions
{
    public static class RectangleExtensions
    {
        /// <summary>
        /// 指定した矩形のキャプチャ画像を返す
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public static Bitmap ToBitmap(this Rectangle r)
        {
            var bitmap = new Bitmap(r.Width, r.Height);
            using (var graphic = Graphics.FromImage(bitmap))
            {
                graphic.CopyFromScreen(
                    new Point(r.X, r.Y),
                    new Point(0, 0),
                    bitmap.Size
                );
            }

            return bitmap;
        }
    }
}
