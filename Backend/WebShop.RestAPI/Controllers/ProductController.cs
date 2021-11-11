using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebShop.Core.IServices;
using WebShop.Core.Models;
using WebShop.RestAPI.DTOs.Products;

namespace WebShop.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public ActionResult<List<ProductDto>> Get()
        {
            return Ok(_productService.GetAll());
        }

        [HttpDelete]
        public ActionResult<Product> Delete(int id)
        {
            var productDeleted = _productService.Delete(id);
            return Ok(productDeleted);
        }
    }
}