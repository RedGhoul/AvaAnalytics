using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharpCounter.Dapper
{
    public interface IRepository<T>
    {
        Task Add(T item);
        void Remove(int id);
        void Update(T item);
        T FindByID(int id);
        Task<IEnumerable<T>> FindAll();
    }
}
