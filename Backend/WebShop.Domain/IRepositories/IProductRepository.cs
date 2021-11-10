using WebShop.Core.Models;

namespace WebShop.Domain.IRepositories
{
    public interface IProductRepository
    {
        FilteredList GetAll();
    }
}