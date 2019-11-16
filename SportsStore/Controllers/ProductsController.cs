using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsStore.Models;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository repository;
        public int PageSize = 4;
        //private readonly ApplicationDbContext _contex = new ApplicationDbContext(ApplicationDbContext);
        public ProductsController(IProductRepository repo)
        {
            repository = repo;
        }

        // GET: api/Products/{category}/Page{productPage}
        [HttpGet("{category}/Page{productPage}")]
        public async Task<ActionResult<ProductsListViewModel>> GetProducts(string category, int productPage = 1)
        {
            return new ProductsListViewModel()
            {
                Products = repository.Products
                .Where(p=> p.Category == category || category == null)
                .OrderBy(p => p.ProductID)
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize),

                PagingInfo = new PagingInfo()
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.Products.Count(),
                },
                CurrentCategory = category
            };
        }

        // api/Products/{category}/Page{productPage}
        [HttpGet("/Page{productPage}")]
        public async Task<ActionResult<ProductsListViewModel>> GetProductsAll(string category, int productPage = 1)
        {
            return new ProductsListViewModel()
            {
                Products = repository.Products
                .OrderBy(p => p.ProductID)
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize),

                PagingInfo = new PagingInfo()
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.Products.Count(),
                },
                CurrentCategory = category
            };
        }
        //        // GET: api/Products/5
        //        [HttpGet("{id}")]
        //        public async Task<ActionResult<Product>> GetProduct(int id)
        //        {
        //            var product = await _context.Products.FindAsync(id);

        //            if (product == null)
        //            {
        //                return NotFound();
        //            }

        //            return product;
        //        }

        //        // PUT: api/Products/5
        //        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        //        // more details see https://aka.ms/RazorPagesCRUD.
        //        [HttpPut("{id}")]
        //        public async Task<IActionResult> PutProduct(int id, Product product)
        //        {
        //            if (id != product.ProductID)
        //            {
        //                return BadRequest();
        //            }

        //            _context.Entry(product).State = EntityState.Modified;

        //            try
        //            {
        //                await _context.SaveChangesAsync();
        //            }
        //            catch (DbUpdateConcurrencyException)
        //            {
        //                if (!ProductExists(id))
        //                {
        //                    return NotFound();
        //                }
        //                else
        //                {
        //                    throw;
        //                }
        //            }

        //            return NoContent();
        //        }

        //        // POST: api/Products
        //        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        //        // more details see https://aka.ms/RazorPagesCRUD.
        //        [HttpPost]
        //        public async Task<ActionResult<Product>> PostProduct(Product product)
        //        {
        //            _context.Products.Add(product);
        //            await _context.SaveChangesAsync();

        //            return CreatedAtAction("GetProduct", new { id = product.ProductID }, product);
        //        }

        //        // DELETE: api/Products/5
        //        [HttpDelete("{id}")]
        //        public async Task<ActionResult<Product>> DeleteProduct(int id)
        //        {
        //            var product = await _context.Products.FindAsync(id);
        //            if (product == null)
        //            {
        //                return NotFound();
        //            }

        //            _context.Products.Remove(product);
        //            await _context.SaveChangesAsync();

        //            return product;
        //        }

        //        private bool ProductExists(int id)
        //        {
        //            return _context.Products.Any(e => e.ProductID == id);
        //        }
    }
}
