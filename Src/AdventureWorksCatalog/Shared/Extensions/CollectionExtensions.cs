using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AdventureWorksCatalog.Extensions
{
    public static class CollectionExtensions
    {
        public static void AddRange<T>(this Collection<T> coll, IEnumerable<T> collAdd)
        {
            if (collAdd != null)
            {
                foreach (var item in collAdd)
                {
                    coll.Add(item);
                }
            }
        }
    }
}
