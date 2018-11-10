using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using DqTool.Core;
using DqTool.Core.Extensions;
using DqTool.UI.Class.Monsters;
using static DqTool.UI.Class.Monster;

namespace DqTool.UI.Class
{
    public class Scanner : IDisposable, IScanner
    {
        public bool shouldScanStop = false;
        private List<Monster> _monsters = new List<Monster>();
        private Point _basePoint;
        private readonly Stopwatch _stopWatch = new Stopwatch();
        private readonly BitmapConverter Converter = new BitmapConverter(Properties.Resources.number);

        public void Init(Point basePoint)
        {
            _basePoint = basePoint;
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
            var name = MonsterList.ScanMonsterName();
            if (!CanCreateMonster(name)) return;

            _monsters.ForEach(x => x.Dispose());
            _monsters.Clear();

            _monsters.Add(GetMonster(name));

            if (name == MonsterName.GenjinA)
            {
                _monsters.Add(GetMonster(MonsterName.GenjinB));
                _monsters.Add(GetMonster(MonsterName.GenjinC));
            }
            else if (name == MonsterName.BattlerA)
            {
                _monsters.Add(GetMonster(MonsterName.BattlerB));
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
            foreach (var v in _monsters.Where(x => x.HasHeal)) v.Heal();
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
            foreach (var monster in _monsters) monster.AutoHeal();
        }



        /// <summary>
        /// ダメージ処理を行う
        /// </summary>
        public void Damage()
        {
            foreach (var monster in _monsters)
            {
                monster.Damage(GetDamage(monster));
                if (monster.IsDead)
                {
                    if (monster.Name == MonsterName.Mirudo1 || monster.Name == MonsterName.Mirudo2)
                    {
                        mirudoCounter = 25;
                    }
                    monster.Dispose();
                }
            }
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
        public int GetDamage(Monster monster)
        {
            var damageBmp = monster.DamageImage;

            var rect = new Rectangle(_basePoint, new Size(damageBmp.Width, damageBmp.Height + 32));
            var monsterName = rect.ToBitmap();

            rect.Location = new Point(0, 0);
            rect.Height -= 32;
            if (!damageBmp.Equal(monsterName.Clone(rect, monsterName.PixelFormat)))
            {
                _basePoint.Y += 32;
                rect.Y += 32;
                if (!damageBmp.Equal(monsterName.Clone(rect, monsterName.PixelFormat))) return -1;
            }

            _basePoint.X += damageBmp.Width + 16;
            _basePoint.Y += 16;

            var numBmp = new Rectangle(_basePoint, new Size(16 * 4, 16)).ToBitmap();
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

        private Monster GetMonster(MonsterName name)
        {
            return new Monster(DIContainer.Resolver.Resolve<IScanner>(), name, _basePoint);
        }
    }
}
