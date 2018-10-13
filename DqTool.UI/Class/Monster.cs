using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

using DqTool.Core;
using DqTool.Core.Extensions;
using DqTool.UI.Resouces;

namespace DqTool.UI.Class
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
        private readonly HitPoint _hitPoint;
        public bool IsDead => _hitPoint.Now == 0;

        public readonly Bitmap damageBmp;
        private readonly Bitmap healBmp;
        private readonly bool isDQ5;
        private bool canDamage;
        private bool canHeal;
        private bool canAutoHeal;


        private static readonly IEnumerable<MonsterData> MonsterList = Serializer<List<MonsterData>>.Deserialize("monsters.xml");
        public static Monster Create(MonsterName name)
        {
            return null; //new Monster();
        }

        public Monster(MonsterData mdata)
        {
            Name = mdata.Name;
            damageBmp = mdata.DamageBmp;
            healBmp = mdata.HealBmp;
            HealPoint = mdata.Heal;
            Autoheal = mdata.Autoheal;
            isDQ5 = mdata.Title == GameTitle.DQ5;

            canDamage = true;
            canHeal = true;
            canAutoHeal = true;

            _hitPoint = new HitPoint(mdata.Hp);
            _hpGauge = new HpGauge(_hitPoint, ResouceManager.LoadLocation(Name));
            _hpGauge.Show();
        }

        public void Dispose()
        {
            if (!_hpGauge.IsDisposed)
            {
                _hpGauge.Close();
                ResouceManager.SaveLocation(Name, _hpGauge.Location);
                _hpGauge.Dispose();
            }
        }

        /// <summary>
        /// ダメージを与える
        /// </summary>
        public void Damage(int d)
        {
            if (_hpGauge.IsDisposed) return;
            if (d == -1)
            {
                canDamage = true;
                return;
            }
            if (!canDamage) return;
            if (isDQ5 && _hitPoint.Now == 2047) return;
            canDamage = false;
            _hpGauge.Damage(d);
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
            canHeal = false;
            _hpGauge.Heal(HealPoint);
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
                    canAutoHeal = false;
                    _hpGauge.Heal(Autoheal);
                    break;
            }
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
    }
}
