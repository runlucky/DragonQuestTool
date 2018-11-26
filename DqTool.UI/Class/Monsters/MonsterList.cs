using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using DqTool.Core;
using DqTool.Core.Extensions;
using DqTool.UI.Properties;

namespace DqTool.UI.Class.Monsters
{
    public static class MonsterList
    {
        private static readonly ScanPosition _dq5Position = new ScanPosition
        {
            Name = new Point(16 * 9, -16 * 3),
            Damage = new Point(0, 0),
            Heal = new Point(0, -32),
            AutoHeal = new Point(0, -16 * 18),
            NameSize = new Size(128, 32)
        };

        private static MonsterBreed DQ5(MonsterName name, int hp, int autoHeal, Bitmap nameImage, Bitmap damage, int heal = 0, Bitmap healImage = null)
        {
            return new MonsterBreed(GameTitle.DQ5, name, hp, autoHeal, heal, nameImage, damage, healImage, _dq5Position);
        }

        private static readonly IEnumerable<MonsterBreed> _breeds = new List<MonsterBreed> {
            DQ5(MonsterName.Oyabun  ,  200,  0, Resources._01_oyabun1 , Resources._01_oyabun2),
            DQ5(MonsterName.Taikou  ,  350,  0, Resources._04_taikou1 , Resources._04_taikou2),
            DQ5(MonsterName.GenjinA ,  400,  0, Resources._05_genjin1 , Resources._05_genjinA),
            DQ5(MonsterName.GenjinB ,  400,  0, Resources._05_genjin1 , Resources._05_genjinB),
            DQ5(MonsterName.GenjinC ,  400,  0, Resources._05_genjin1 , Resources._05_genjinC),
            DQ5(MonsterName.Oku     ,  950,  0, Resources._07_oku1    , Resources._07_oku2   ),
            DQ5(MonsterName.Kobun   ,  500,  0, Resources._10_kobun1  , Resources._10_kobun2 ),
            DQ5(MonsterName.Gonzu   , 1700,  0, Resources._11_gonzu1  , Resources._11_gonzu2 ),
            DQ5(MonsterName.Gema    , 4500,  0, Resources._12_gema1   , Resources._12_gema2  ),
            DQ5(MonsterName.Ramada  , 2000,  0, Resources._13_ramada1 , Resources._13_ramada2),
            DQ5(MonsterName.Iburu   , 4500,  0, Resources._14_iburu1  , Resources._14_iburu2 ),
            DQ5(MonsterName.Buon    , 4500,  0, Resources._15_buon1   , Resources._15_buon2  ),
            DQ5(MonsterName.BattlerA,  450,  0, Resources._16_hell1   , Resources._16_hellA  ),
            DQ5(MonsterName.BattlerB,  450,  0, Resources._16_hell1   , Resources._16_hellB  ),
            DQ5(MonsterName.Mirudo1 , 1600, 50, Resources._17_mirudo1a, Resources._17_mirudo2),
            DQ5(MonsterName.Zairu   ,  160,  0, Resources._02_zairu1  , Resources._02_zairu2  ,  35, Resources._02_zairu3  ),
            DQ5(MonsterName.Joou    ,  600,  0, Resources._03_joou1   , Resources._03_joou2   ,  35, Resources._03_joou3   ),
            DQ5(MonsterName.Kandata ,  600,  0, Resources._06_kandata1, Resources._06_kandata2,  85, Resources._06_kandata3),
            DQ5(MonsterName.Kimera  ,  800,  0, Resources._08_kimera1 , Resources._08_kimera2 ,  85, Resources._08_kimera3 ),
            DQ5(MonsterName.Jami    ,  820,  0, Resources._09_jami1   , Resources._09_jami2   , 820, Resources._09_jami3   ),
            DQ5(MonsterName.Mirudo2 , 4500,  0, Resources._17_mirudo1b, Resources._17_mirudo2 , 500, Resources._17_mirudo3 )
        };
        public static MonsterBreed GetBreed(MonsterName name) => _breeds.FirstOrDefault(x => x.Name == name);

        public static MonsterName ScanMonsterName(Point basePoint)
        {

            var breed = _breeds.FirstOrDefault(x =>
            {
                return true;
            });
            return breed.Name;
        }
    }
}
