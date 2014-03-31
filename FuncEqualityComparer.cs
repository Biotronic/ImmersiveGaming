using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmersiveGaming
{
    public class Comparer<T> : IEqualityComparer<T>
    {
        readonly Func<T, T, bool> _cmp;
        readonly Func<T, int> _hash;

        public Comparer(Func<T, T, bool> cmp)
            : this(cmp, t => 0)
        {
        }

        public Comparer(Func<T, T, bool> cmp, Func<T, int> hash)
        {
            _cmp = cmp;
            _hash = hash;
        }

        public bool Equals(T a, T b)
        {
            return _cmp(a, b);
        }

        public int GetHashCode(T obj)
        {
            return _hash(obj);
        }
    }
}
