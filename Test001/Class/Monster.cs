using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace DqTool
{
    /// <summary>
    /// モンスターの管理
    /// </summary>
    public class Monster
    {
        public MonsterName name { get; }
        public int healPoint { get; }
        public int autoheal { get; }
        public FormHp formHp { get; }

        private int hp;
        private readonly int mhp;
        private Bitmap damageBmp;
        private Bitmap healBmp;
        private bool isDQ5;
        private bool canDamage;
        private bool canHeal;
        private bool canAutoHeal;

        private enum HM
        {
            HM,
            H,
            Nathing
        }

        public static readonly Dictionary<MonsterName, MonsterData> monsterData = new Dictionary<MonsterName, MonsterData>() {
            {MonsterName.Oyabun,   new MonsterData( 200,  0, new Bitmap("img\\01_oyabun1.PNG" ), new Bitmap("img\\01_oyabun2.PNG" ), null, 0, true)},
            {MonsterName.Zairu,    new MonsterData( 160,  0, new Bitmap("img\\02_zairu1.PNG"  ), new Bitmap("img\\02_zairu2.PNG"  ), new Bitmap("img\\02_zairu3.PNG"), 35, true)},
            {MonsterName.Joou,     new MonsterData( 600,  0, new Bitmap("img\\03_joou1.PNG"   ), new Bitmap("img\\03_joou2.PNG"   ), new Bitmap("img\\03_joou3.PNG" ), 35, true)},
            {MonsterName.Taikou,   new MonsterData( 350,  0, new Bitmap("img\\04_taikou1.PNG" ), new Bitmap("img\\04_taikou2.PNG" ), null, 0, true)},
            {MonsterName.GenjinA,  new MonsterData( 400,  0, new Bitmap("img\\05_genjin1.PNG" ), new Bitmap("img\\05_genjinA.PNG" ), null, 0, true)},
            {MonsterName.GenjinB,  new MonsterData( 400,  0, new Bitmap("img\\05_genjin1.PNG" ), new Bitmap("img\\05_genjinB.PNG" ), null, 0, true)},
            {MonsterName.GenjinC,  new MonsterData( 400,  0, new Bitmap("img\\05_genjin1.PNG" ), new Bitmap("img\\05_genjinC.PNG" ), null, 0, true)},
            {MonsterName.Kandata,  new MonsterData( 600,  0, new Bitmap("img\\06_kandata1.PNG"), new Bitmap("img\\06_kandata2.PNG"), new Bitmap("img\\06_kandata3.PNG" ), 85, true)},
            {MonsterName.Oku,      new MonsterData( 950,  0, new Bitmap("img\\07_oku1.PNG"    ), new Bitmap("img\\07_oku2.PNG"    ), null, 0, true)},
            {MonsterName.Kimera,   new MonsterData( 800,  0, new Bitmap("img\\08_kimera1.PNG" ), new Bitmap("img\\08_kimera2.PNG" ), new Bitmap("img\\08_kimera3.PNG"  ), 85, true)},
            {MonsterName.Jami,     new MonsterData( 820,  0, new Bitmap("img\\09_jami1.PNG"   ), new Bitmap("img\\09_jami2.PNG"   ), new Bitmap("img\\09_jami3.PNG"    ), 820, true)},
            {MonsterName.Kobun,    new MonsterData( 500,  0, new Bitmap("img\\10_kobun1.PNG"  ), new Bitmap("img\\10_kobun2.PNG"  ), null, 0, true)},
            {MonsterName.Gonzu,    new MonsterData(1700,  0, new Bitmap("img\\11_gonzu1.PNG"  ), new Bitmap("img\\11_gonzu2.PNG"  ), null, 0, true)},
            {MonsterName.Gema,     new MonsterData(4500,  0, new Bitmap("img\\12_gema1.PNG"   ), new Bitmap("img\\12_gema2.PNG"   ), null, 0, true)},
            {MonsterName.Ramada,   new MonsterData(2000,  0, new Bitmap("img\\13_ramada1.PNG" ), new Bitmap("img\\13_ramada2.PNG" ), null, 0, true)},
            {MonsterName.Iburu,    new MonsterData(4500,  0, new Bitmap("img\\14_iburu1.PNG"  ), new Bitmap("img\\14_iburu2.PNG"  ), null, 0, true)},
            {MonsterName.Buon,     new MonsterData(4500,  0, new Bitmap("img\\15_buon1.PNG"   ), new Bitmap("img\\15_buon2.PNG"   ), null, 0, true)},
            {MonsterName.BattlerA, new MonsterData( 450,  0, new Bitmap("img\\16_hell1.PNG"   ), new Bitmap("img\\16_hellA.PNG"   ), null, 0, true)},
            {MonsterName.BattlerB, new MonsterData( 450,  0, new Bitmap("img\\16_hell1.PNG"   ), new Bitmap("img\\16_hellB.PNG"   ), null, 0, true)},
            {MonsterName.Mirudo1,  new MonsterData(1600, 50, new Bitmap("img\\17_mirudo1a.PNG"), new Bitmap("img\\17_mirudo2.PNG" ), null, 0, true)},
            {MonsterName.Mirudo2,  new MonsterData(4500,  0, new Bitmap("img\\17_mirudo1b.PNG"), new Bitmap("img\\17_mirudo2.PNG" ), new Bitmap("img\\17_mirudo3.PNG"  ), 500, true)}
        };

        public Monster(MonsterName n)
        {
            var mdata = monsterData[n];

            hp = mdata.hp;
            mhp = mdata.hp;
            name = n;
            damageBmp = mdata.damageBmp;
            healBmp = mdata.healBmp;
            healPoint = mdata.healPoint;
            autoheal = mdata.autoheal;
            isDQ5 = mdata.isDq5;

            canDamage = true;
            canHeal = true;
            canAutoHeal = true;

            formHp = new FormHp(mhp, GetFormType(n));
            formHp.ReLocation();
            formHp.Show();
        }

        private FormType GetFormType(MonsterName n)
        {
            switch (n)
            {
                case MonsterName.GenjinA:
                case MonsterName.Kandata:
                case MonsterName.BattlerA:
                    return FormType.Left;
                case MonsterName.GenjinC:
                case MonsterName.BattlerB:
                    return FormType.Right;
                default:
                    return FormType.Center;
            }
        }

        public void Destroy()
        {
            if (!formHp.IsDisposed)
            {
                formHp.Close();
                formHp.Dispose();
            }
        }

        /// <summary>
        /// ダメージを与える
        /// 死んだらtrue返す
        /// </summary>
        public bool Damage(int d)
        {
            if (formHp.IsDisposed) return true;
            if (d == -1)
            {
                canDamage = true;
                return false;
            }
            if (!canDamage) return false;
            if (isDQ5 && hp == 2047) return false;
            hp = Math.Max(hp - d, 0);
            canDamage = false;
            formHp.SetHp(hp);
            if (hp == 0)
            {
                return true;
            }
            else if (hp == 2047 && isDQ5)
            {
                formHp.ProgressRed();
            }
            return false;
        }

        /// <summary>
        /// 回復する
        /// </summary>
        public void Heal(bool h)
        {
            if (!h)
            {
                canHeal = true;
                return;
            }
            if (!canHeal) return;
            hp = Math.Min(hp + healPoint, mhp);
            canHeal = false;
            formHp.SetHp(hp);
        }

        /// <summary>
        /// 自動回復
        /// </summary>
        public void AutoHeal(Point scanPos)
        {
            var isHm = Calc.IsHM(Calc.Scan(new Rectangle(scanPos, new Size(16, 48))));
            var isH = Calc.IsH(Calc.Scan(new Rectangle(scanPos, new Size(16, 16))));

            if (isHm)
            {
                if (!canAutoHeal) return;
                hp = Math.Min(hp + autoheal, mhp);
                canAutoHeal = false;
                formHp.SetHp(hp);
                return;
            }

            if (isH)
            {
                canAutoHeal = true;
                return;
            }
        }

        /// <summary>
        /// ダメージ値を取得する
        /// ダメージ値を１つも検出できなかった場合は-1を返す
        /// </summary>
        /// <param name="scanPos">最上位桁目の数値の座標</param>
        /// <returns></returns>
        public int GetDamage(Point scanPos)
        {
            var rect = new Rectangle(scanPos, new Size(damageBmp.Width, damageBmp.Height + 32));
            var monsterName = Calc.Scan(rect);

            rect.Location = new Point(0, 0);
            rect.Height -= 32;
            if (!Calc.IsMatch(damageBmp, monsterName.Clone(rect, monsterName.PixelFormat)))
            {
                scanPos.Y += 32;
                rect.Y += 32;
                if (!Calc.IsMatch(damageBmp, monsterName.Clone(rect, monsterName.PixelFormat))) return -1;
            }

            scanPos.X += damageBmp.Width + 16;
            scanPos.Y += 16;

            var numBmp = Calc.Scan(new Rectangle(scanPos, new Size(16 * 4, 16)));
            var scanSize = new Rectangle(0, 0, 16, 16);

            var num = Calc.GetNum(numBmp.Clone(scanSize, numBmp.PixelFormat));
            if (num == -1) return num;

            for (int i = 0; i < 3; i++)
            {
                scanSize.X += 16;
                var tempNum = Calc.GetNum(numBmp.Clone(scanSize, numBmp.PixelFormat));
                if (tempNum == -1) return num;
                num *= 10;
                num += tempNum;
            }
            return num;
        }

        /// <summary>
        /// 回復行動をしているかどうかを取得する
        /// </summary>
        /// <param name="scanPos">テキスト欄の0,0の位置</param>
        /// <returns></returns>
        public bool IsHeal(Point scanPos)
        {
            return Calc.IsMatch(healBmp, Calc.Scan(new Rectangle(scanPos, healBmp.Size)));
        }

        public class MonsterData
        {
            public int hp { get; }
            /// <summary>
            /// 自動回復量
            /// </summary>
            public int autoheal { get; }
            public Bitmap nameBmp { get; }
            public Bitmap damageBmp { get; }
            public Bitmap healBmp { get; }
            public int healPoint { get; }
            public bool isDq5 { get; }

            public MonsterData(int h, int a, Bitmap nb, Bitmap db, Bitmap hb, int ht, bool dq5)
            {
                hp = h;
                autoheal = a;
                nameBmp = nb;
                damageBmp = db;
                healBmp = hb;
                healPoint = ht;
                isDq5 = dq5;
            }
        }
    }
}
