using System;
using System.Collections.Generic;

namespace Game
{
    internal static class Extensions
    {
        public static void Shuffle<T>(this IList<T> source)
        {
            var random = new Random();
            for (int index1 = source.Count - 1; index1 > 0; --index1)
            {
                int index2 = random.Next(index1 + 1);
                T obj = source[index1];
                source[index1] = source[index2];
                source[index2] = obj;
            }
        }

        public static void Each<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var item in collection)
            {
                action(item);
            }
        }

        public static void Each<T>(this IEnumerable<T> collection, Action<T, int> action)
        {
            var count = 0;
            foreach (var item in collection)
            {
                action(item, count++);
            }
        }
    }
}