using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    public interface IDb<T, K>
    {
        void Create(T item);

        T Read(K key, bool useNavigationalProperties = false);

        IEnumerable<T> ReadAll(bool useNavigationalProperties = false);

        void Update(T item, bool useNavigationalProperties = false);

        void Delete(K key);

    }
}
