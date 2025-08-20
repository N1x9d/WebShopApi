using System.Diagnostics;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using WebShopApi.Models;

namespace WebShopApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HomeController: ControllerBase
{
    public async Task<HttpStatusCode> Index()
    {
        var bvm = new BaseViewModel();
        return HttpStatusCode.OK;
    }

    [HttpPost]
    public async Task<HttpStatusCode> AddProduct([FromBody]Product product)
    {
        product.AddDate = DateTimeOffset.Now;
        DAO.AddNewProduct(product);
        return  HttpStatusCode.OK;
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> Product(Guid ProductId)
    {
        return DAO.GetProduct(ProductId).Result;
    }
    
    [HttpGet("{type}")]
    public async Task<ActionResult<StoreModel>> Products(string Type)
    {
        var storeModel = new StoreModel();
        storeModel.Categories = DAO.GetTypesInfo().Result;
        ProductType type = (ProductType)Enum.Parse(typeof(ProductType), Type);
        storeModel.Products = DAO.GetProducts(type).Result;
        storeModel.Brands = new List<BrandModel>();
        var brandsName= storeModel.Products.Select(c => c.Brand).Distinct().ToList();

        foreach(var brandName in brandsName)
        {
            var count = storeModel.Products.Select(c => c.Brand == brandName).Count();
            storeModel.Brands.Add(new BrandModel { Count = count, Name = brandName });
        }

        return storeModel;
    }
}