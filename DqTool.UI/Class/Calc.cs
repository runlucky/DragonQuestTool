using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

using DqTool.Core.Extensions;

namespace DqTool.UI
{
    public enum Phase
    {
        Battle,
        Command
    }

    /// <summary>
    /// 画像処理
    /// </summary>
    public static class Calc
    {
        private static readonly Bitmap hm1 = Properties.Resources.HM1;
        private static readonly Bitmap hm2 = Properties.Resources.HM2;
        private static readonly Bitmap hm3 = Properties.Resources.HM3;
        private static readonly Bitmap h1 = Properties.Resources.H1;
        private static readonly Bitmap h2 = Properties.Resources.H2;
        private static readonly Bitmap h3 = Properties.Resources.H3;

        private static bool IsCommandPhase(Bitmap bmp)
        {
            if (bmp.Equal(hm1)) return true;
            if (bmp.Equal(hm2)) return true;
            if (bmp.Equal(hm3)) return true;
            return false;
        }

        private static bool IsBattlePhase(Bitmap bmp)
        {
            if (bmp.Equal(h1)) return true;
            if (bmp.Equal(h2)) return true;
            if (bmp.Equal(h3)) return true;
            return false;
        }

        public static Phase GetPhase(Bitmap bmp)
        {
            if (IsCommandPhase(bmp)) return Phase.Command;
            if (IsBattlePhase(bmp)) return Phase.Battle;
            throw new MethodAccessException("戦闘していない or 画像がおかしいです");
        }
    }
}
