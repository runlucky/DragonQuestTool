using System;
using System.Windows.Forms;

namespace DqTool.UI.Extensions
{
    public interface EventArgsExtensions
    {
    }

    public static class MouseEventArgsExtensions
    {
        public static bool IsLeftClicking(this MouseEventArgs mouse) => mouse.Button == MouseButtons.Left;
    }
}
