using System;
using System.Drawing;
using DqTool.Core;

namespace DqTool.UI.Class
{
    public class MonsterData
    {
        public int Hp { get; }

        /// <summary>
        /// 自動回復量
        /// </summary>
        public int Autoheal { get; }

        public Bitmap NameBmp { get; }
        public Bitmap DamageBmp { get; }
        public Bitmap HealBmp { get; }
        public int HealPoint { get; }
        public bool IsDq5 { get; }
        public MonsterName Name { get; }

        public MonsterData(MonsterName name, int h, int a, Bitmap nb, Bitmap db, Bitmap hb, int ht, bool dq5)
        {
            Name = name;
            Hp = h;
            Autoheal = a;
            NameBmp = nb;
            DamageBmp = db;
            HealBmp = hb;
            HealPoint = ht;
            IsDq5 = dq5;
        }
    }
}
