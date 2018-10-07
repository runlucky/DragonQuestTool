using DqTool.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DqTool.UI.Class
{
    public static class ResouceManager
    {
        public static Point GetLocation(MonsterName name)
        {
            switch (name)
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

        public static void SaveLocation(MonsterName name, Point location)
        {
            switch (name)
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
    }
}
