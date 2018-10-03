using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Security.Cryptography;

using DqTool.Core.Extensions;
using DqTool.Core;
using System.Diagnostics;
using DqTool.UI.Class;

namespace DqTool.UI
{
    public partial class Dashboard : Form
    {
        private List<Monster> mst = new List<Monster>();
        private bool isAnalyzing = false;
        private bool shouldScanStop = false;
        private Scanner scanner = new Scanner();

        public Dashboard()
        {
            InitializeComponent();

            var data = Properties.Settings.Default;
            Location = data.MainPos;
            tbWait.Text = data.Wait;
            scanPosX.Value = data.ScanPos.X;
            scanPosY.Value = data.ScanPos.Y;

            ReScanImage();
        }

        private void OnScanButtonClick(object sender, EventArgs e)
        {
            button.Text = isAnalyzing ? "計測開始" : "計測終了";

            if (!isAnalyzing)
            {
                scanner.Init(new Point(Decimal.ToInt32(scanPosX.Value), Decimal.ToInt32(scanPosY.Value)));
                Scan();
            }
            shouldScanStop = isAnalyzing;
            isAnalyzing = isAnalyzing.Toggle();
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            mst.ForEach(x => x.Destroy());

            Properties.Settings.Default.MainPos = Location;
            Properties.Settings.Default.Wait = tbWait.Text;
            Properties.Settings.Default.ScanPos = new Point((int)scanPosX.Value, (int)scanPosY.Value);

            Properties.Settings.Default.Save();
        }

        private async void Scan()
        {
            var sw = Stopwatch.StartNew();

            while (true)
            {
                sw.Restart();

                if (shouldScanStop)
                {
                    EndBattle();
                    return;
                }

                CreateMonster();
                Damage();
                Heal();
                AutoHeal();

                labelMs.Text = $"負荷 {sw.ElapsedMilliseconds}ms";
                await Task.Delay(tbWait.Text.ToInt(100));
            }
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

            mst.ForEach(x => x.Destroy());
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
        /// ダメージ処理を行う
        /// モンスターがいない場合は何もしない
        /// </summary>
        private void Damage()
        {
            foreach (var v in mst)
            {
                if (v.Damage(v.GetDamage(scanner.Damage)))
                {
                    if (v.Name == MonsterName.Mirudo1 || v.Name == MonsterName.Mirudo2)
                    {
                        mirudoCounter = 25;
                    }
                    v.Destroy();
                }
            }

            mst.RemoveAll(x => x.HpGauge == null);
        }

        /// <summary>
        /// 回復処理を行う
        /// モンスターがいない場合は何もしない
        /// 回復行動を取らないモンスターの場合も何もしない
        /// </summary>
        private void Heal()
        {
            foreach (var v in mst.Where(x => x.HealPoint != 0)) v.Heal(v.IsHeal(scanner.Heal));
        }

        private void EndBattle()
        {
            mst.ForEach(x => x.Destroy());
            mst.Clear();
            shouldScanStop = false;
        }

        /// <summary>
        /// 自動回復処理
        /// </summary>
        private void AutoHeal()
        {
            foreach (var v in mst.Where(x => x.HasAutoHeal)) v.AutoHeal(scanner.AutoHeal);
        }

        /// <summary>
        /// ミルドラースの場合はスキャン位置が違う
        /// </summary>
        /// <returns></returns>
        private MonsterName ScanMonsterName()
        {
            var name = new Rectangle(scanner.Name, scanner.NameSize).ToBitmap();
            foreach (var v in Monster.monsterData)
            {
                if (name.Equal(v.Value.NameBmp)) return v.Key;
            }
            name = new Rectangle(scanner.Name.X + 32, scanner.Name.Y - 16 * 8, 64, 32).ToBitmap();
            if (name.Equal(Monster.monsterData[MonsterName.Mirudo1].NameBmp)) return MonsterName.Mirudo1;
            if (name.Equal(Monster.monsterData[MonsterName.Mirudo2].NameBmp)) return MonsterName.Mirudo2;

            return MonsterName.Unknown;
        }

        private void ScanPosX_ValueChanged(object sender, EventArgs e)
        {
            ReScanImage();
        }

        private void ScanPosY_ValueChanged(object sender, EventArgs e)
        {
            ReScanImage();
        }

        private void ReScanImage()
        {
            pictureBox1.Image = new Rectangle((int)scanPosX.Value, (int)scanPosY.Value, 144, 32).ToBitmap();
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            ReScanImage();
        }
    }
}
