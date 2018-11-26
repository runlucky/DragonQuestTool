using System;
using System.Drawing;
using DqTool.Core.Extensions;

namespace DqTool.UI.Class
{
    public struct ScanPosition
    {
        public Point Name { get; set; }
        public Point Damage { get; set; }
        public Point Heal { get; set; }
        public Point AutoHeal { get; set; }
        public Size NameSize { get; set; }
        private Rectangle NameRectangle => new Rectangle(Name, NameSize);
        public Bitmap NameImage => NameRectangle.ToBitmap();
        ↑これは低レイヤーに依存しているので良くないよね
    }
}
