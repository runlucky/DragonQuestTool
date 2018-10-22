using System;
using System.Drawing;

namespace DqTool.UI.Class
{
    public interface IScanner
    {
        void Damage();
        int GetDamage(Point scanPos, Bitmap damageBmp);
        Bitmap ReScanImage(Point location);
    }
}
