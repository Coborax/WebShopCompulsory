using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebShop.Core.Services;
using WebShop.Domain;
using WebShop.Domain.IRepositories;

namespace Webshop.Infrastructure.DB.EFCore.Repositories
{
    public class EFCoreRepo<T, TEntity> : IRepo<T> where TEntity : class
    {
        protected readonly WebShopContext _ctx;
        private readonly IModelConverter<T, TEntity> _converter;

        public EFCoreRepo(WebShopContext ctx, IModelConverter<T, TEntity> converter)
        {
            _ctx = ctx;
            _converter = converter;
        }

        public T Create(T toCreate)
        {
            return _converter.ToModel(_ctx.Set<TEntity>().Add(_converter.ToEntity(toCreate)).Entity);
        }

        public IEnumerable<T> GetAll()
        {
            return _ctx.Set<TEntity>().Select(_converter.ToModel);
        }

        public T Find(int id)
        {
            var entity = _ctx.Set<TEntity>().Find(id);
            _ctx.Entry(entity).State = EntityState.Detached;
            return _converter.ToModel(entity);
        }

        public T Update(T toUpdate)
        {
            return _converter.ToModel(_ctx.Set<TEntity>().Update(_converter.ToEntity(toUpdate)).Entity);
        }

        public void Delete(T toDelete)
        {
            _ctx.Set<TEntity>().Remove(_converter.ToEntity(toDelete));
        }
    }
}
