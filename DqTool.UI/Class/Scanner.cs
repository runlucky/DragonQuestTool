using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using DqTool.Core;
using DqTool.Core.Extensions;
using static DqTool.UI.Class.Monster;

namespace DqTool.UI.Class
{
    public class Scanner : IDisposable
    {
        public bool shouldScanStop = false;
        private List<Monster> _monsters = new List<Monster>();
        private ScanPosition scan;
        private readonly Stopwatch _stopWatch = new Stopwatch();
        private readonly BitmapConverter Converter = new BitmapConverter(Properties.Resources.number);

        public static readonly Dictionary<MonsterName, MonsterData> monsterData = new Dictionary<MonsterName, MonsterData>() {
            {MonsterName.Oyabun,   new MonsterData(MonsterName.Oyabun,    200,  0, Properties.Resources._01_oyabun1  , Properties.Resources._01_oyabun2  , null, 0, true)},
            {MonsterName.Zairu,    new MonsterData(MonsterName.Zairu,     160,  0, Properties.Resources._02_zairu1   , Properties.Resources._02_zairu2   , Properties.Resources._02_zairu3  , 35, true)},
            {MonsterName.Joou,     new MonsterData(MonsterName.Joou,      600,  0, Properties.Resources._03_joou1    , Properties.Resources._03_joou2    , Properties.Resources._03_joou3   , 35, true)},
            {MonsterName.Taikou,   new MonsterData(MonsterName.Taikou,    350,  0, Properties.Resources._04_taikou1  , Properties.Resources._04_taikou2  , null, 0, true)},
            {MonsterName.GenjinA,  new MonsterData(MonsterName.GenjinA,   400,  0, Properties.Resources._05_genjin1  , Properties.Resources._05_genjinA  , null, 0, true)},
            {MonsterName.GenjinB,  new MonsterData(MonsterName.GenjinB,   400,  0, Properties.Resources._05_genjin1  , Properties.Resources._05_genjinB  , null, 0, true)},
            {MonsterName.GenjinC,  new MonsterData(MonsterName.GenjinC,   400,  0, Properties.Resources._05_genjin1  , Properties.Resources._05_genjinC  , null, 0, true)},
            {MonsterName.Kandata,  new MonsterData(MonsterName.Kandata,   600,  0, Properties.Resources._06_kandata1 , Properties.Resources._06_kandata2 , Properties.Resources._06_kandata3, 85, true)},
            {MonsterName.Oku,      new MonsterData(MonsterName.Oku,       950,  0, Properties.Resources._07_oku1     , Properties.Resources._07_oku2     , null, 0, true)},
            {MonsterName.Kimera,   new MonsterData(MonsterName.Kimera,    800,  0, Properties.Resources._08_kimera1  , Properties.Resources._08_kimera2  , Properties.Resources._08_kimera3 , 85, true)},
            {MonsterName.Jami,     new MonsterData(MonsterName.Jami,      820,  0, Properties.Resources._09_jami1    , Properties.Resources._09_jami2    , Properties.Resources._09_jami3   , 820, true)},
            {MonsterName.Kobun,    new MonsterData(MonsterName.Kobun,     500,  0, Properties.Resources._10_kobun1   , Properties.Resources._10_kobun2   , null, 0, true)},
            {MonsterName.Gonzu,    new MonsterData(MonsterName.Gonzu,    1700,  0, Properties.Resources._11_gonzu1   , Properties.Resources._11_gonzu2   , null, 0, true)},
            {MonsterName.Gema,     new MonsterData(MonsterName.Gema,     4500,  0, Properties.Resources._12_gema1    , Properties.Resources._12_gema2    , null, 0, true)},
            {MonsterName.Ramada,   new MonsterData(MonsterName.Ramada,   2000,  0, Properties.Resources._13_ramada1  , Properties.Resources._13_ramada2  , null, 0, true)},
            {MonsterName.Iburu,    new MonsterData(MonsterName.Iburu,    4500,  0, Properties.Resources._14_iburu1   , Properties.Resources._14_iburu2   , null, 0, true)},
            {MonsterName.Buon,     new MonsterData(MonsterName.Buon,     4500,  0, Properties.Resources._15_buon1    , Properties.Resources._15_buon2    , null, 0, true)},
            {MonsterName.BattlerA, new MonsterData(MonsterName.BattlerA,  450,  0, Properties.Resources._16_hell1    , Properties.Resources._16_hellA    , null, 0, true)},
            {MonsterName.BattlerB, new MonsterData(MonsterName.BattlerB,  450,  0, Properties.Resources._16_hell1    , Properties.Resources._16_hellB    , null, 0, true)},
            {MonsterName.Mirudo1,  new MonsterData(MonsterName.Mirudo1,  1600, 50, Properties.Resources._17_mirudo1a , Properties.Resources._17_mirudo2  , null, 0, true)},
            {MonsterName.Mirudo2,  new MonsterData(MonsterName.Mirudo2,  4500,  0, Properties.Resources._17_mirudo1b , Properties.Resources._17_mirudo2  , Properties.Resources._17_mirudo3 , 500, true)}
        };

        public void Init(Point basePoint)
        {
            scan = new ScanPosition
            {
                Damage = new Point(basePoint.X, basePoint.Y),
                Heal = new Point(scan.Damage.X, scan.Damage.Y - 32),
                AutoHeal = new Point(scan.Damage.X, scan.Damage.Y - 16 * 18),
                Name = new Point(scan.Damage.X + 16 * 9, scan.Damage.Y - 16 * 3),
                NameSize = new Size(128, 32)
            };
        }

        public async Task<int> ScanAsync(int wait)
        {
            _stopWatch.Restart();

            if (shouldScanStop)
            {
                EndBattle();
                return -1;
            }

            CreateMonster();
            Damage();
            Heal();
            AutoHeal();

            await Task.Delay(wait);
            return (int)_stopWatch.ElapsedMilliseconds;
        }

        private int mirudoCounter = 0;

        /// <summary>
        /// モンスター名をスキャンする
        /// スキャンできて、今のモンスターと違う場合は新しく
        /// モンスターを生成する
        /// </summary>
        private void CreateMonster()
        {
            var name = ScanMonsterName();
            if (!CanCreateMonster(name)) return;

            _monsters.ForEach(x => x.Dispose());
            _monsters.Clear();

            _monsters.Add(new Monster(monsterData[name]));

            if (name == MonsterName.GenjinA)
            {
                _monsters.Add(new Monster(monsterData[MonsterName.GenjinB]));
                _monsters.Add(new Monster(monsterData[MonsterName.GenjinC]));
            }
            else if (name == MonsterName.BattlerA)
            {
                _monsters.Add(new Monster(monsterData[MonsterName.BattlerB]));
            }
        }

        private bool CanCreateMonster(MonsterName name)
        {
            if (name == MonsterName.Unknown) return false;
            if (_monsters.Any(x => x.Name == name)) return false;

            if (name == MonsterName.Mirudo1 || name == MonsterName.Mirudo2)
            {
                if (mirudoCounter != 0)
                {
                    mirudoCounter--;
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 回復処理を行う
        /// モンスターがいない場合は何もしない
        /// 回復行動を取らないモンスターの場合も何もしない
        /// </summary>
        private void Heal()
        {
            foreach (var v in _monsters.Where(x => x.HealPoint != 0)) v.Heal(v.IsHeal(scan.Heal));
        }

        private void EndBattle()
        {
            _monsters.ForEach(x => x.Dispose());
            _monsters.Clear();
            shouldScanStop = false;
        }

        /// <summary>
        /// 自動回復処理
        /// </summary>
        private void AutoHeal()
        {
            foreach (var v in _monsters) v.AutoHeal(scan.AutoHeal);
        }

        /// <summary>
        /// ミルドラースの場合はスキャン位置が違う
        /// </summary>
        /// <returns></returns>
        private MonsterName ScanMonsterName()
        {
            var name = new Rectangle(scan.Name, scan.NameSize).ToBitmap();
            foreach (var v in monsterData)
            {
                if (name.Equal(v.Value.NameBmp)) return v.Key;
            }
            name = new Rectangle(scan.Name.X + 32, scan.Name.Y - 16 * 8, 64, 32).ToBitmap();
            if (name.Equal(monsterData[MonsterName.Mirudo1].NameBmp)) return MonsterName.Mirudo1;
            if (name.Equal(monsterData[MonsterName.Mirudo2].NameBmp)) return MonsterName.Mirudo2;

            return MonsterName.Unknown;
        }

        /// <summary>
        /// ダメージ処理を行う
        /// モンスターがいない場合は何もしない
        /// </summary>
        public void Damage()
        {
            foreach (var v in _monsters)
            {
                if (v.Damage(GetDamage(scan.Damage, v.damageBmp)))
                {
                    if (v.Name == MonsterName.Mirudo1 || v.Name == MonsterName.Mirudo2)
                    {
                        mirudoCounter = 25;
                    }
                    v.Dispose();
                }
            }

            //_monsters.RemoveAll(x => x.IsDead);
        }

        public Bitmap ReScanImage(Point location)
        {
            return new Rectangle(location.X, location.Y, 144, 32).ToBitmap();
        }

        public void Dispose()
        {
            _monsters.ForEach(x => x.Dispose());
        }

        /// <summary>
        /// ダメージ値を取得する
        /// ダメージ値を１つも検出できなかった場合は-1を返す
        /// </summary>
        /// <param name="scanPos">最上位桁目の数値の座標</param>
        /// <returns></returns>
        public int GetDamage(Point scanPos, Bitmap damageBmp)
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
    }
}
