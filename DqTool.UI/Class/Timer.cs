using System;
using System.Diagnostics;

namespace DqTool.UI.Class
{
    public static class Timer
    {
        private static readonly Stopwatch _stopWatch = new Stopwatch();

        public static int Elapsed(Action method)
        {
            _stopWatch.Restart();
            method();
            return (int)_stopWatch.ElapsedMilliseconds;
        }
    }
}
