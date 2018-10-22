using System;
using System.Drawing;
using System.IO;
using DqTool.Core;

namespace DqTool.UI.Class
{
    public struct MonsterBreed
    {
        public MonsterName Name { get; set; }
        public GameTitle Title { get; set; }

        public int Hp { get; set; }
        public int AutoHeal { get; set; }
        public int Heal { get; set; }

        public string NamePath { get; set; }
        public string DamagePath { get; set; }
        public string HealPath { get; set; }

        public ScanPosition ScanPosition { get; set; }
    }
}
