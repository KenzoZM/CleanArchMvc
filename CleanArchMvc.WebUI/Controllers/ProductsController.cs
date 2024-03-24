using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Domain.Entitites;
using CleanArchMvc.Infra.IoC;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace CleanArchMvc.WebUI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _environment;
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductsController(IProductService productAppService,
            ICategoryService categoryService, IWebHostEnvironment environment, 
            IHttpClientFactory httpClientFactory)
        {
            _productService = productAppService;
            _categoryService = categoryService;
            _environment = environment;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> API()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FetchData(string method, string id)
        {
            ModelState.Remove("Category");
            string apiUrl = ApiSettings.ApiBaseUrl + "api/Products";

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
                    var product = JsonConvert.DeserializeObject<ProductDTO>(jsonContent, new JsonSerializerSettings
                    {
                        ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver
                        {
                            NamingStrategy = new Newtonsoft.Json.Serialization.CamelCaseNamingStrategy()
                        }
                    });
                    return View("API", new List<ProductDTO> { product }); // Retornar uma lista com um único produto
                }
                else // Caso contrário, desserialize a lista de produtos
                {
                    var products = JsonConvert.DeserializeObject<IEnumerable<ProductDTO>>(jsonContent, new JsonSerializerSettings
                    {
                        ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver
                        {
                            NamingStrategy = new Newtonsoft.Json.Serialization.CamelCaseNamingStrategy()
                        }
                    });
                    return View("API", products);
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
            var products = await _productService.GetProductsAsync();
            return View(products);
        }

        // GET: ProductsController/Create/5
        [Authorize(Roles = "Admin")]
        [HttpGet()]
        public async Task<IActionResult> Create()
        {
            ViewBag.CategoryId =
            new SelectList(await _categoryService.GetCategoriesAsync(), "Id", "Name");

            return View();
        }

        // POST: ProductsController/Create/5
        [HttpPost]
        public async Task<IActionResult> Create(ProductDTO productDto)
        {
            ModelState.Remove("Category");
            if (ModelState.IsValid)
            {
                await _productService.CreateProductAsync(productDto);
                return RedirectToAction(nameof(Index));
            }

            var categories = await _categoryService.GetCategoriesAsync();
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name");
            return View(productDto);
        }

        public async Task<IActionResult> Export()
        {
            var dados = await GetData();

            using (XLWorkbook workBook = new XLWorkbook())
            {
                workBook.AddWorksheet(dados, "Data Products");

                using (MemoryStream ms = new MemoryStream())
                {
                    workBook.SaveAs(ms);
                    return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spredsheetml.sheet", "Products.xls");
                }
            }
        }

        private async Task<DataTable> GetData()
        {
            DataTable dataTable = new DataTable();

            dataTable.TableName = "Data Products";

            dataTable.Columns.Add("Id", typeof(int));
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("Description", typeof(string));
            dataTable.Columns.Add("Price", typeof(decimal));
            dataTable.Columns.Add("Stock", typeof(int));
            dataTable.Columns.Add("Image", typeof(string));
            dataTable.Columns.Add("Category", typeof(string));
            dataTable.Columns.Add("CategoryId", typeof(int));

            var dados = await _productService.GetProductsAsync();
            if (dados.Any())
            {
                var categoryDictionary = (await _categoryService.GetCategoriesAsync()).ToDictionary(c => c.Id, c => c.Name);
                foreach (var product in dados) 
                {

                    var categoryName = product.CategoryId.HasValue ? categoryDictionary.GetValueOrDefault(product.CategoryId.Value, "N/A") : "N/A";
                    dataTable.Rows.Add(product.Id, product.Name, product.Description, product.Price, product.Stock, product.Image, categoryName, product.CategoryId);
                }
            }

            return dataTable;
        }


        // GET: ProductsController/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpGet()]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var productDto = await _productService.GetProductByIdAsync(id);

            if (productDto == null) return NotFound();

            var categories = await _categoryService.GetCategoriesAsync();
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name", productDto.CategoryId);

            return View(productDto);
        }

        // POST: ProductsController/Edit/5
        [HttpPost()]
        public async Task<IActionResult> Edit(ProductDTO productDto)
        {
            ModelState.Remove("Category");
            if (ModelState.IsValid)
            {
                await _productService.UpdateProductAsync(productDto);
                return RedirectToAction(nameof(Index));
            }
            var categories = await _categoryService.GetCategoriesAsync();
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name");
            return View(productDto);
        }

        // GET: ProductsController/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpGet()]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var productDto = await _productService.GetProductByIdAsync(id);

            if (productDto == null) return NotFound();

            var categoryName = productDto.Category?.Name; // Obtém o nome da categoria
            ViewData["CategoryName"] = categoryName; // Passa o nome da categoria para a view

            return View(productDto);
        }

        // POST: ProductsController/Delete/5
        [HttpPost(), ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productService.DeleteProductAsync(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var productDto = await _productService.GetProductByIdAsync(id);

            if (productDto == null) return NotFound();
            var wwwroot = _environment.WebRootPath;
            var image = Path.Combine(wwwroot, "images\\" + productDto.Image);
            var exists = System.IO.File.Exists(image);
            ViewBag.ImageExist = exists;

            var categoryName = productDto.Category?.Name;
            ViewData["CategoryName"] = categoryName;

            return View(productDto);
        }
    }
}
