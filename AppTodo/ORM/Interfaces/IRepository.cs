using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Interfaces
{
    public interface IRepository <T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void Add(T obj);
        void Remove(T obj);
        void Update(T obj);

    }
}
