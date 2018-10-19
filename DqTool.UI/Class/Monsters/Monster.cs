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
        //public MonsterName Name { get; set; }
        //public GameTitle Title { get; set; }

        //public int Hp { get; set; }
        //public int AutoHeal { get; set; }
        //public int Heal { get; set; }

        //public string NamePath { get; set; }
        //public string DamagePath { get; set; }
        //public string HealPath { get; set; }

        //public ScanPosition ScanPosition { get; set; }
        public MonsterName Name { get; }
        private readonly int _heal;
        private readonly int _autoHeal;
        private bool HasAutoHeal => _autoHeal != 0;

        private readonly HpGauge _hpGauge;
        private readonly HitPoint _hitPoint;
        public bool IsDead => _hitPoint.Now == 0;

        public readonly Bitmap damageBmp;
        private readonly Bitmap healBmp;
        private bool canDamage = true;
        private bool canHeal = true;
        private bool canAutoHeal = true;

        private static readonly IEnumerable<MonsterBreed> MonsterList = Serializer<List<MonsterBreed>>.Deserialize("Resources\\Monsters.xml");

        public Monster(MonsterName name)
        {
            var aaa = new ScanPosition();
            aaa.AutoHeal = new Point();

            MonsterBreed breed = MonsterList.FirstOrDefault(x => x.Name == name);
            breed.
           Name = name;
            damageBmp = new Bitmap(breed.DamagePath);
            healBmp = new Bitmap(breed.HealPath);
            _heal = breed.Heal;
            _autoHeal = breed.AutoHeal;
            breed.ScanPosition;

            _hitPoint = new HitPoint(breed.Hp);
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
            _hpGauge.Heal(_heal);
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
                    _hpGauge.Heal(_autoHeal);
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
