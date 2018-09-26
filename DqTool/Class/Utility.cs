using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DqTool
{
    public class ProgressBarEx : ProgressBar
    {
        private int PBS_SMOOTHREVERSE = 0x0010;

        protected override CreateParams CreateParams
        {
            [SecurityPermission(SecurityAction.Demand,
                Flags = SecurityPermissionFlag.UnmanagedCode)]
            get
            {
                CreateParams cps = base.CreateParams;
                //コントロールのスタイルにPBS_SMOOTHREVERSEを追加する
                cps.Style |= PBS_SMOOTHREVERSE;
                return cps;
            }
        }
    }
}
