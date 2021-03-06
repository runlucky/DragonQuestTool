﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

using DqTool.Core;
using DqTool.Core.Extensions;
using DqTool.UI.Class.Monsters;
using DqTool.UI.Resouces;

namespace DqTool.UI.Class
{
    /// <summary>
    /// モンスターの管理
    /// </summary>
    public class Monster : IDisposable
    {
        private MonsterBreed _breed;
        private Point _basePoint;
        private bool HasAutoHeal => _breed.AutoHeal != 0;
        public bool HasHeal => _breed.Heal != 0;

        public MonsterName Name => _breed.Name;

        private readonly HpGauge _hpGauge;
        private readonly HitPoint _hitPoint;
        public bool IsDead => _hitPoint.Now == 0;

        public Bitmap DamageImage => _breed.DamageImage;
        private bool canDamage = true;
        private bool canHeal = true;
        private bool canAutoHeal = true;

        private IScanner _scanner;

        //private Bitmap ScanName => ScanPosition.NameImage;

        //public bool Exists() => ScanName.Equal(NameImage);


        public Monster(IScanner scanner, MonsterName name, Point basePoint)
        {
            _scanner = scanner;
            _breed = MonsterList.GetBreed(name);
            _basePoint = basePoint;

            _hitPoint = new HitPoint(_breed.Hp);
            _hpGauge = new HpGauge(_hitPoint, ResouceManager.LoadLocation(name));
            _hpGauge.Show();
        }

        public void Dispose()
        {
            if (!_hpGauge.IsDisposed)
            {
                _hpGauge.Close();
                ResouceManager.SaveLocation(_breed.Name, _hpGauge.Location);
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
            if (_breed.Title == GameTitle.DQ5 && _hitPoint.Now == 2047) return;
            canDamage = false;
            _hpGauge.Damage(d);
        }

        /// <summary>
        /// 回復する
        /// </summary>
        public void Heal()
        {
            if (!IsHeal())
            {
                canHeal = true;
                return;
            }
            if (!canHeal) return;
            canHeal = false;
            _hpGauge.Heal(_breed.Heal);
        }

        /// <summary>
        /// 回復行動をしているかどうかを取得する
        /// </summary>
        private bool IsHeal()
        {
            var scan = new Rectangle(_breed.ScanPosition.Heal, _breed.HealImage.Size).ToBitmap();
            return _breed.HealImage.Equal(scan);
        }

        /// <summary>
        /// 自動回復
        /// </summary>
        public void AutoHeal()
        {
            if (!HasAutoHeal) return;
            switch (Calc.GetPhase(new Rectangle(_breed.ScanPosition.AutoHeal, new Size(16, 48)).ToBitmap()))
            {
                case Phase.Battle:
                    canAutoHeal = true;
                    break;

                case Phase.Command:
                    if (!canAutoHeal) return;
                    canAutoHeal = false;
                    _hpGauge.Heal(_breed.AutoHeal);
                    break;
            }
        }
    }
}
