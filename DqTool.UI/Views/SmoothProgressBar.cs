using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DqTool.UI
{
    /// <summary>
    /// プログレスバーのアニメーションをなめらかにするやつ
    /// </summary>
    public class SmoothProgressBar : ProgressBar
    {
        protected override CreateParams CreateParams
        {
            [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
            get
            {
                var cps = base.CreateParams;
                cps.Style |= 0x0010;
                return cps;
            }
        }
    }
}
