using System.Collections.Generic;

namespace Features.Extensions
{
    /// <summary>
    /// Abstract comparer with the ordering setting
    /// </summary>
    public abstract class AbstractComparer<T> : IComparer<T>
    {
        protected const int MORE_VALUE = 1;
        protected const int LESS_VALUE = -1;
        protected const int EQUAL_VALUE = 0;

        protected readonly bool acsending = false;

        public AbstractComparer(bool ascending) => acsending = ascending;

        public virtual int Compare(T data1, T data2) => acsending ? CompareDatas(data1, data2) : CompareDatas(data2, data1);

        protected abstract int CompareDatas(T data1, T data2);
    }
}
