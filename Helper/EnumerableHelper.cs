using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Helper
{
    public static class EnumerableHelper
    {
        public static TItem MinItem<TItem>(this IEnumerable<TItem> items, Func<TItem, IComparable> selector)
            => MinItems(items, selector).FirstOrDefault();

        public static IEnumerable<TItem> MinItems<TItem>(this IEnumerable<TItem> items, Func<TItem, IComparable> selector)
            => items.Where(i => selector(i) == items.Min(o => selector(o)));
    }
}
