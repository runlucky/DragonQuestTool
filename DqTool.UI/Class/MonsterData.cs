using System;
using System.Drawing;
using DqTool.Core;

namespace DqTool.UI.Class
{
    public struct MonsterData
    {
        public MonsterName Name { get; }
        public GameTitle Title { get; }

        public int Hp { get; }
        public int Autoheal { get; }
        public int Heal { get; }

        public Bitmap NameBmp { get; }
        public Bitmap DamageBmp { get; }
        public Bitmap HealBmp { get; }

        public ScanPosition ScanPosition { get; }
    }
}
