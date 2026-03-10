using Microsoft.AspNetCore.Mvc;
using System.Formats.Asn1;
using Web.Service.Categories;
using Web.Service.Categories.Create;
using Web.Service.Categories.Update;
using Web.Service.Products.Update;

namespace Web.API.Controllers
{
    [Route("api/category")]
    public class CategoryController(ICategoryService categoryService) : CustomBaseController
    {
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await categoryService.GetAllAsync();
            return CreateActionResult(result);
        }

        [HttpGet("getById/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await categoryService.GetByIdAsync(id);
            return CreateActionResult(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateCategoryRequest request)
        {
            var result = await categoryService.CreateAsync(request);
            return CreateActionResult(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateCategoryRequest request)
        {
            var result = await categoryService.UpdateAsync(request);
            return CreateActionResult(result);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await categoryService.DeleteAsync(id);
            return CreateActionResult(result);
        }
    }
}
