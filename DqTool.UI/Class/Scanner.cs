using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using DqTool.Core;
using DqTool.Core.Extensions;

namespace DqTool.UI.Class
{
    public class Scanner : IDisposable
    {
        public bool shouldScanStop = false;
        private List<Monster> mst = new List<Monster>();
        private ScanPosition scan = new ScanPosition();
        private Stopwatch sw = new Stopwatch();


        public void Init(Point basePoint)
        {
            scan.Damage = new Point(basePoint.X, basePoint.Y);
            scan.Heal = new Point(scan.Damage.X, scan.Damage.Y - 32);
            scan.AutoHeal = new Point(scan.Damage.X, scan.Damage.Y - 16 * 18);
            scan.Name = new Point(scan.Damage.X + 16 * 9, scan.Damage.Y - 16 * 3);
            scan.NameSize = new Size(128, 32);
        }


        public async Task<int> ScanAsync(int wait)
        {
            sw.Restart();

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
            return (int)sw.ElapsedMilliseconds;
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

            mst.ForEach(x => x.Dispose());
            mst.Clear();

            mst.Add(new Monster(name));

            if (name == MonsterName.GenjinA)
            {
                mst.Add(new Monster(MonsterName.GenjinB));
                mst.Add(new Monster(MonsterName.GenjinC));
            }
            else if (name == MonsterName.BattlerA)
            {
                mst.Add(new Monster(MonsterName.BattlerB));
            }
        }

        private bool CanCreateMonster(MonsterName name)
        {
            if (name == MonsterName.Unknown) return false;
            if (mst.Any(x => x.Name == name)) return false;

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
            foreach (var v in mst.Where(x => x.HealPoint != 0)) v.Heal(v.IsHeal(scan.Heal));
        }

        private void EndBattle()
        {
            mst.ForEach(x => x.Dispose());
            mst.Clear();
            shouldScanStop = false;
        }

        /// <summary>
        /// 自動回復処理
        /// </summary>
        private void AutoHeal()
        {
            foreach (var v in mst.Where(x => x.HasAutoHeal)) v.AutoHeal(scan.AutoHeal);
        }

        /// <summary>
        /// ミルドラースの場合はスキャン位置が違う
        /// </summary>
        /// <returns></returns>
        private MonsterName ScanMonsterName()
        {
            var name = new Rectangle(scan.Name, scan.NameSize).ToBitmap();
            foreach (var v in Monster.monsterData)
            {
                if (name.Equal(v.Value.NameBmp)) return v.Key;
            }
            name = new Rectangle(scan.Name.X + 32, scan.Name.Y - 16 * 8, 64, 32).ToBitmap();
            if (name.Equal(Monster.monsterData[MonsterName.Mirudo1].NameBmp)) return MonsterName.Mirudo1;
            if (name.Equal(Monster.monsterData[MonsterName.Mirudo2].NameBmp)) return MonsterName.Mirudo2;

            return MonsterName.Unknown;
        }

        /// <summary>
        /// ダメージ処理を行う
        /// モンスターがいない場合は何もしない
        /// </summary>
        public void Damage()
        {
            foreach (var v in mst)
            {
                if (v.Damage(v.GetDamage(scan.Damage)))
                {
                    if (v.Name == MonsterName.Mirudo1 || v.Name == MonsterName.Mirudo2)
                    {
                        mirudoCounter = 25;
                    }
                    v.Dispose();
                }
            }

            mst.RemoveAll(x => x.HpGauge == null);
        }

        public Bitmap ReScanImage(Point location)
        {
            return new Rectangle(location.X, location.Y, 144, 32).ToBitmap();
        }

        public void Dispose()
        {
            mst.ForEach(x => x.Dispose());
        }
    }
}
