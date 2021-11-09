using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShop.Domain;
using WebShop.RestAPI.DTOs.Products;

namespace WebShop.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ActionResult<List<ProductDto>> Get()
        {
            return _unitOfWork.Products.GetAll()
                .Select(p => new ProductDto { Name = p.Name, Desc = p.Desc, Img = p.Img })
                .ToList();
        }
    }
}