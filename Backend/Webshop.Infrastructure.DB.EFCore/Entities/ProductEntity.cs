namespace Webshop.Infrastructure.DB.EFCore.Entities
{
    public class ProductEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public string Img { get; set; }
    }
}