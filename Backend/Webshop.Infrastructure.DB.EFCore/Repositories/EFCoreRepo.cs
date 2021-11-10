using System.Collections.Generic;
using WebShop.Domain.IRepositories;

namespace Webshop.Infrastructure.DB.EFCore.Repositories
{
    public class EFCoreRepo<T> : IRepo<T> where T : class
    {
        protected readonly WebShopContext _ctx;

        public EFCoreRepo(WebShopContext ctx)
        {
            _ctx = ctx;
        }

        public T Create(T toCreate)
        {
            return _ctx.Set<T>().Add(toCreate).Entity;
        }

        public IEnumerable<T> GetAll()
        {
            return _ctx.Set<T>();
        }

        public T Find(int id)
        {
            return _ctx.Set<T>().Find(id);
        }

        public T Update(T toUpdate)
        {
            return _ctx.Set<T>().Update(toUpdate).Entity;
        }

        public void Delete(T toDelete)
        {
            _ctx.Set<T>().Remove(toDelete);
        }
    }
}