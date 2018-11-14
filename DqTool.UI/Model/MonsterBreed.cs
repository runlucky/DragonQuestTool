using System;
using System.Drawing;
using System.IO;
using DqTool.Core;
using DqTool.Core.Extensions;

namespace DqTool.UI.Class
{
    public struct MonsterBreed
    {
        public GameTitle Title { get; set; }
        public MonsterName Name { get; set; }

        public int Hp { get; set; }
        public int AutoHeal { get; set; }
        public int Heal { get; set; }

        public Bitmap NameImage { get; set; }
        public Bitmap DamageImage { get; set; }
        public Bitmap HealImage { get; set; }

        public ScanPosition ScanPosition { get; set; }

        public MonsterBreed(GameTitle title, MonsterName name, int hp, int autoHeal, int heal, Bitmap nameImage, Bitmap damage, Bitmap healImage, ScanPosition position)
        {
            Title = title;
            Name = name;

            Hp = hp;
            AutoHeal = autoHeal;
            Heal = heal;

            NameImage = nameImage;
            DamageImage = damage;
            HealImage = healImage;

            ScanPosition = position;
        }
    }
}
