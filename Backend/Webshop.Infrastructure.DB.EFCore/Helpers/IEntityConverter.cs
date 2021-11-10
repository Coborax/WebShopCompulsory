namespace Webshop.Infrastructure.DB.EFCore.Helpers
{
    public interface IEntityConverter<TModel, TEntity>
    {
        TModel ToModel(TEntity entity);
        TEntity ToEntity(TModel model);
    }
}