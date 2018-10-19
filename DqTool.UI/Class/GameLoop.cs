using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using DqTool.Core;

namespace DqTool.UI.Class
{
    public static class GameLoop
    {
        private static readonly Battle _battle = new Battle();

        //public static async Task<int> LoopAsync(int wait)
        //{
        //    var time = Timer.Elapsed(() =>
        //    {
        //        _battle.Update(GetStatus());
        //    });

        //    await Task.Delay(wait);
        //    return time;
        //}

        //private static GameStatus GetStatus()
        //{
        //    var status = new GameStatus();
        //    status.name = ScanMonsterName();
        //    status.IsBattleStart = CanCreateMonster(status.name);
        //    status.Point = xxx;

        //    return status;
        //}

        ///// <summary>
        ///// ミルドラースの場合はスキャン位置が違う
        ///// </summary>
        ///// <returns></returns>
        //private static MonsterName ScanMonsterName()
        //{
        //    var name = new Rectangle(scan.Name, scan.NameSize).ToBitmap();
        //    foreach (var monster in monsterData)
        //    {
        //        if (name.Equal(monster.Value.NameBmp)) return monster.Key;
        //    }
        //    name = new Rectangle(scan.Name.X + 32, scan.Name.Y - 16 * 8, 64, 32).ToBitmap();
        //    if (name.Equal(monsterData[MonsterName.Mirudo1].NameBmp)) return MonsterName.Mirudo1;
        //    if (name.Equal(monsterData[MonsterName.Mirudo2].NameBmp)) return MonsterName.Mirudo2;

        //    return MonsterName.Unknown;
        //}





        //private static bool CanCreateMonster(MonsterName name)
        //{
        //    if (name == MonsterName.Unknown) return false;
        //    if (_monsters.Any(x => x.Name == name)) return false;

        //    if (name == MonsterName.Mirudo1 || name == MonsterName.Mirudo2)
        //    {
        //        if (mirudoCounter != 0)
        //        {
        //            mirudoCounter--;
        //            return false;
        //        }
        //    }
        //    return true;
        //}
    }
}
