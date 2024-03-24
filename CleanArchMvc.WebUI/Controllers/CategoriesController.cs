using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Infra.IoC;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CleanArchMvc.WebUI.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IHttpClientFactory _httpClientFactory;
        public CategoriesController(ICategoryService categoryService, IHttpClientFactory httpClientFactory)
        {
            _categoryService = categoryService;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> API()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FetchData(string method, string id)
        {
            string apiUrl = ApiSettings.ApiBaseUrl + "api/Categories";

            // Se o método selecionado for GetById e um ID válido for fornecido, ajuste a URL da API
            if (method == "GetById" && !string.IsNullOrEmpty(id))
            {
                apiUrl += "/" + id;
            }

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var jsonContent = await response.Content.ReadAsStringAsync();

                // Se o método selecionado for GetById, desserialize o produto individualmente
                if (method == "GetById")
                {
                    var category = JsonConvert.DeserializeObject<CategoryDTO>(jsonContent, new JsonSerializerSettings
                    {
                        ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver
                        {
                            NamingStrategy = new Newtonsoft.Json.Serialization.CamelCaseNamingStrategy()
                        }
                    });
                    return View("API", new List<CategoryDTO> { category }); // Retornar uma lista com um único produto
                }
                else // Caso contrário, desserialize a lista de produtos
                {
                    var categories = JsonConvert.DeserializeObject<IEnumerable<CategoryDTO>>(jsonContent, new JsonSerializerSettings
                    {
                        ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver
                        {
                            NamingStrategy = new Newtonsoft.Json.Serialization.CamelCaseNamingStrategy()
                        }
                    });
                    return View("API", categories);
                }
            }
            else
            {
                return View("API");
            }
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetCategoriesAsync();
            return View(categories);
        }

        // GET: CategoriesController/Create/5
        [Authorize(Roles = "Admin")]
        [HttpGet()]
        public IActionResult Create()
        {
            return View();
        }


        // POST: CategoriesController/Create/5
        [HttpPost]
        public async Task<IActionResult> Create(CategoryDTO category)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.AddCategoryAsync(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: CategoriesController/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpGet()]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var categoryDto = await _categoryService.GetCategoryByIdAsync(id);
            if (categoryDto == null) return NotFound();
            return View(categoryDto);
        }

        // POST: CategoriesController/Edit/5
        [HttpPost()]
        public async Task<IActionResult> Edit(CategoryDTO categoryDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _categoryService.UpdateCategoryAsync(categoryDto);
                }
                catch (Exception)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(categoryDto);
        }

        // GET: CategoriesController/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpGet()]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var categoryDto = await _categoryService.GetCategoryByIdAsync(id);

            if (categoryDto == null) return NotFound();

            return View(categoryDto);
        }

        // POST: CategoriesController/Delete/5
        [HttpPost(), ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _categoryService.RemoveCategoryAsync(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var categoryDto = await _categoryService.GetCategoryByIdAsync(id);

            if (categoryDto == null)
                return NotFound();

            return View(categoryDto);
        }
    }
}
