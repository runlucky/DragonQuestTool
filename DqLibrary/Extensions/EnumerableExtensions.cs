using System;
using System.Collections.Generic;
using System.Text;

namespace DqLibrary.Extensions
{
    public static class EnumerableExtentions
    {
        [Obsolete("foreachを使ったほうがいいらしいよ")]
        public static void ForEach<T>(this IEnumerable<T> source, Action<T, int> action)
        {
            int index = 0;
            foreach (var x in source)
                action(x, index++);
        }
    }
}
