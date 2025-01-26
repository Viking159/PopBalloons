using System;
using System.Collections.Generic;

namespace Features.Extensions
{
    /// <summary>
    /// Serializable wrapper list of objects
    /// </summary>
    [Serializable]
    public class GenericListWrapper<T>
    {
        public List<T> Data = new List<T>();

        public GenericListWrapper(List<T> data) => Data = data;
    }
}