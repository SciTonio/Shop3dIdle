using System;
using System.Collections.Generic;
using System.Linq;

public static class ListExtensions
{
    public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
    {
        foreach (T item in enumerable) action.Invoke(item);
    }

    public static void ForEach<T>(this List<T> input, Action<T, int> action)
    {
        for (int i = 0; i < input.Count; i++)
        {
            action(input[i], i);
        }
    }

    public static void ForEach<T>(this IEnumerable<T> input, Action<T, int> action)
    {
        for (int i = 0; i < input.Count(); i++)
        {
            action(input.ElementAt(i), i);
        }
    }
}
