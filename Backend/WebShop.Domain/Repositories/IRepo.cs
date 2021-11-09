using System.Collections.Generic;

namespace WebShop.Domain.Repositories
{
    public interface IRepo<T>
    {
        T Create(T toCreate);
        IEnumerable<T> GetAll();
        T Find(int id);
        T Update(T toUpdate);
        void Delete(T toDelete);
    }
}