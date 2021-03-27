using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace Utils
{
    public static class LinqExtensions
    {
        public static IEnumerable<float> Normalize<T>(this IEnumerable<T> source, Func<T, float> selector)
        {
            var enumerable = (source ?? new List<T>()).ToList();

            if (!enumerable.Any())
                throw new InvalidOperationException("Collection is null or empty");

            var length = Mathf.Sqrt(enumerable.Select(e => selector(e) * selector(e)).Sum());
            return enumerable.Select(e => selector(e) / length);
        }
    }
}