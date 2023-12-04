using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get()
        {
            var categories = await _categoryService.GetCategoriesAsync();
            if (categories == null)
            {
                return NotFound("Categories not found");
            }
            return Ok(categories);
        }

        [HttpGet("{id:int}", Name = "GetCategory")]
        public async Task<ActionResult<CategoryDTO>> GetById(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound("Category Not found");
            }
            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CategoryDTO categoryDto)
        {
            if (categoryDto == null)
            {
                return BadRequest("invalid data");
            }
            await _categoryService.AddCategoryAsync(categoryDto);
            return new CreatedAtRouteResult("GetCategory", new {id = categoryDto.Id}, categoryDto);
        }
    }
}
