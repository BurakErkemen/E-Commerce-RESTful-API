using Microsoft.AspNetCore.Mvc;
using Web.Service.Products;
using Web.Service.Products.Create;
using Web.Service.Products.Update;

namespace Web.API.Controllers
{
    [Route("api/product")]
    public class ProductController(IProductService productServices) : CustomBaseController
    {
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll() =>
            CreateActionResult(await productServices.GetAllListAsync());


        [HttpGet("pageList/{pageNumber:int}/{pageSize:int}")]
        public async Task<IActionResult> GetPagedAll(int pageNumber, int pageSize) =>
            CreateActionResult(await productServices.GetPagedAllListAsync(pageNumber, pageSize));


        [HttpGet("getById/{id:int}")]
        public async Task<IActionResult> GetById(int id) =>
            CreateActionResult(await productServices.GetByIdAsync(id));


        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateProductRequest request) =>
            CreateActionResult(await productServices.CreateAsync(request));


        [HttpPut("update")]
        public async Task<IActionResult> Update(UpdateProductRequest request) =>
            CreateActionResult(await productServices.UpdateAsync(request));


        [HttpPatch("updateStock")]
        public async Task<IActionResult> UpdateStock(UpdateProductStockRequest request) =>
            CreateActionResult(await productServices.UpdateStockAsync(request));


        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> Delete(int id) =>
            CreateActionResult(await productServices.DeleteAsync(id));
    }
}
