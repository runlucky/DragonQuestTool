using System;
using System.Drawing;

namespace DqTool.UI.Class
{
    public class HitPoint
    {
        public int Now { get; private set; }
        private readonly int _max;

        public HitPoint(int hp)
        {
            Now = hp;
            _max = hp;
        }

        internal void Heal(int value)
        {
            Now = Math.Min(Now + value, _max);
        }

        internal void Damage(int value)
        {
            Now = Math.Max(Now - value, 0);
        }

    }
}
