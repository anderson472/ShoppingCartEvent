﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sales.API.Data;
using Sales.Shared.Entities;

namespace Sales.API.Controllers
{
    [ApiController]
    [Route("/api/productCategory")]
    public class ProductCategoryController : ControllerBase
    {
        private readonly DataContext _context;

        public ProductCategoryController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _context.ProductCategories
                .Include(x => x.Products)
                .ToListAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var state = await _context.ProductCategories
                .Include(x => x.Products)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (state == null)
            {
                return NotFound();
            }

            return Ok(state);
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync(ProductCategory productCategory)
        {
            try
            {
                _context.Add(productCategory);
                await _context.SaveChangesAsync();
                return Ok(productCategory);
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe un producto/categoria con el mismo nombre.");
                }

                return BadRequest(dbUpdateException.Message);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        //[HttpPut]
        //public async Task<ActionResult> PutAsync(ProductCategory productCategory)
        //{
        //    try
        //    {
        //        _context.Update(productCategory);
        //        await _context.SaveChangesAsync();
        //        return Ok(productCategory);
        //    }
        //    catch (DbUpdateException dbUpdateException)
        //    {
        //        if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
        //        {
        //            return BadRequest("Ya existe un Producto/Categoría con el mismo nombre.");
        //        }

        //        return BadRequest(dbUpdateException.Message);
        //    }
        //    catch (Exception exception)
        //    {
        //        return BadRequest(exception.Message);
        //    }
        //}

        //[HttpDelete("{id:int}")]
        //public async Task<IActionResult> DeleteAsync(int id)
        //{
        //    var state = await _context.States.FirstOrDefaultAsync(x => x.Id == id);
        //    if (state == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Remove(state);
        //    await _context.SaveChangesAsync();
        //    return NoContent();
        //}
    }
}