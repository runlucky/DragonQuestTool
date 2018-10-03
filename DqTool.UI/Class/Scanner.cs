using System;
using System.Drawing;

namespace DqTool.UI.Class
{
    public class Scanner
    {
        private ScanPos pos = new ScanPos();

        public void Init(Point basePoint)
        {
            pos.Damage = new Point(basePoint.X, basePoint.Y);
            pos.Heal = new Point(pos.Damage.X, pos.Damage.Y - 32);
            pos.AutoHeal = new Point(pos.Damage.X, pos.Damage.Y - 16 * 18);
            pos.Name = new Point(pos.Damage.X + 16 * 9, pos.Damage.Y - 16 * 3);
            pos.NameSize = new Size(128, 32);
        }
    }
}
