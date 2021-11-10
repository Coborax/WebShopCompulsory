namespace WebShop.Core.Services
{
    public interface IModelConverter<TModel, TEntity>
    {
        TModel ToModel(TEntity entity);
        TEntity ToEntity(TModel model);
    }
}