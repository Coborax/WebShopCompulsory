using System.Collections.Generic;
using WebShop.Core.Models;

namespace WebShop.Domain.IRepositories
{
    public interface IRepo<T>
    {
        T Create(T toCreate);
        IEnumerable<T> GetAll();
        T Find(int id);
        T Update(T toUpdate);
        T Delete(T toDelete);
    }
}