using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Domain.Entitites;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace CleanArchMvc.WebUI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _environment;

        public ProductsController(IProductService productAppService,
            ICategoryService categoryService, IWebHostEnvironment environment)
        {
            _productService = productAppService;
            _categoryService = categoryService;
            _environment = environment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetProductsAsync();
            return View(products);
        }

        // GET: ProductsController/Create/5
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
            dataTable.Columns.Add("Category", typeof(CategoryDTO));
            dataTable.Columns.Add("CategoryId", typeof(int));

            var dados = await _productService.GetProductsAsync();
            if (dados.Any())
            {
                foreach (var product in dados) 
                {
                    dataTable.Rows.Add(product.Id, product.Name, product.Description, product.Price, product.Stock, product.Image, product.Category, product.CategoryId);
                }
            }

            return dataTable;
        }


        // GET: ProductsController/Edit/5
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
