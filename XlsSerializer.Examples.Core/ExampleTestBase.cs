using System;
using System.Collections.Generic;
using System.IO;

namespace XlsSerializer.Examples.Core
{
    public abstract class ExampleTestBase
    {
        protected abstract void Test(Stream target);

        public byte[] Run()
        {
            using (var strm = new MemoryStream())
            {
                Test(strm);

                return strm.GetBuffer();
            }
        }

        protected static IEqualityComparer<T> CompareItems<T>(Func<T, T, bool> comparer)
        {
            return new ItemComparer<T>(comparer);
        }

        private class ItemComparer<T>:IEqualityComparer<T>
        {
            private readonly Func<T, T, bool> m_equals;

            public ItemComparer(Func<T, T, bool> mEquals)
            {
                m_equals = mEquals;
            }

            public bool Equals(T x, T y)
            {
                return m_equals(x,y);
            }

            public int GetHashCode(T obj)
            {
                return obj.GetHashCode();
            }
        }
    }
}
