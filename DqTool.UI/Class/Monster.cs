using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

using DqTool.Core;
using DqTool.Core.Extensions;

namespace DqTool.UI
{
    /// <summary>
    /// モンスターの管理
    /// </summary>
    public class Monster : IDisposable
    {
        public MonsterName Name { get; }
        public int HealPoint { get; }
        private int Autoheal { get; }
        private bool HasAutoHeal => Autoheal != 0;
        private readonly HpGauge _hpGauge;
        public bool IsDead => _hpGauge == null;

        private int hp;
        private readonly int mhp;
        private readonly Bitmap damageBmp;
        private readonly Bitmap healBmp;
        private readonly bool isDQ5;
        private bool canDamage;
        private bool canHeal;
        private bool canAutoHeal;

        private readonly BitmapConverter Converter = new BitmapConverter(Properties.Resources.number);

        private enum HM
        {
            HM,
            H,
            Nathing
        }

        public Monster(MonsterData mdata)
        {
            hp = mdata.Hp;
            mhp = mdata.Hp;
            Name = mdata.Name;
            damageBmp = mdata.DamageBmp;
            healBmp = mdata.HealBmp;
            HealPoint = mdata.HealPoint;
            Autoheal = mdata.Autoheal;
            isDQ5 = mdata.IsDq5;

            canDamage = true;
            canHeal = true;
            canAutoHeal = true;

            _hpGauge = new HpGauge(mhp, GetLocation());
            _hpGauge.Show();
        }

        private Point GetLocation()
        {
            switch (Name)
            {
                case MonsterName.GenjinA:
                case MonsterName.Kandata:
                case MonsterName.BattlerA:
                    return Properties.Settings.Default.HpLPos;

                case MonsterName.GenjinC:
                case MonsterName.BattlerB:
                    return Properties.Settings.Default.HpRPos;

                default:
                    return Properties.Settings.Default.HpPos;
            }
        }

        private void SaveLocation(Point location)
        {
            switch (Name)
            {
                case MonsterName.GenjinA:
                case MonsterName.Kandata:
                case MonsterName.BattlerA:
                    Properties.Settings.Default.HpLPos = location;
                    break;

                case MonsterName.GenjinC:
                case MonsterName.BattlerB:
                    Properties.Settings.Default.HpRPos = location;
                    break;

                default:
                    Properties.Settings.Default.HpPos = location;
                    break;
            }
            Properties.Settings.Default.Save();
        }

        public void Dispose()
        {
            if (!_hpGauge.IsDisposed)
            {
                _hpGauge.Close();
                SaveLocation(_hpGauge.Location);
                _hpGauge.Dispose();
            }
        }

        /// <summary>
        /// ダメージを与える
        /// 死んだらtrue返す
        /// </summary>
        public bool Damage(int d)
        {
            if (_hpGauge.IsDisposed) return true;
            if (d == -1)
            {
                canDamage = true;
                return false;
            }
            if (!canDamage) return false;
            if (isDQ5 && hp == 2047) return false;
            hp = Math.Max(hp - d, 0);
            canDamage = false;
            _hpGauge.Hp = hp;
            if (hp == 0)
            {
                return true;
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
            hp = Math.Min(hp + HealPoint, mhp);
            canHeal = false;
            _hpGauge.Hp = hp;
        }

        /// <summary>
        /// 自動回復
        /// </summary>
        public void AutoHeal(Point scanPos)
        {
            if (!HasAutoHeal) return;
            switch (Calc.GetPhase(new Rectangle(scanPos, new Size(16, 48)).ToBitmap()))
            {
                case Phase.Battle:
                    canAutoHeal = true;
                    break;

                case Phase.Command:
                    if (!canAutoHeal) return;
                    hp = Math.Min(hp + Autoheal, mhp);
                    canAutoHeal = false;
                    _hpGauge.Hp = hp;
                    break;
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
            var monsterName = rect.ToBitmap();

            rect.Location = new Point(0, 0);
            rect.Height -= 32;
            if (!damageBmp.Equal(monsterName.Clone(rect, monsterName.PixelFormat)))
            {
                scanPos.Y += 32;
                rect.Y += 32;
                if (!damageBmp.Equal(monsterName.Clone(rect, monsterName.PixelFormat))) return -1;
            }

            scanPos.X += damageBmp.Width + 16;
            scanPos.Y += 16;

            var numBmp = new Rectangle(scanPos, new Size(16 * 4, 16)).ToBitmap();
            var scanSize = new Rectangle(0, 0, 16, 16);

            var num = Converter.ToInt(numBmp.Clone(scanSize, numBmp.PixelFormat)) ?? -1;
            if (num == -1) return num;

            for (int i = 0; i < 3; i++)
            {
                scanSize.X += 16;
                var tempNum = Converter.ToInt(numBmp.Clone(scanSize, numBmp.PixelFormat)) ?? -1;
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
            return healBmp.Equal(new Rectangle(scanPos, healBmp.Size).ToBitmap());
        }

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
}
