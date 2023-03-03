﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sales.API.Data;
using Sales.Shared.Entities;


namespace Sales.API.Controllers
{
    [ApiController]
    [Route("/api/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly DataContext _context;

        public CategoriesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _context.Categories
                .ToListAsync());
        }

        //[HttpGet("full")]
        //public async Task<IActionResult> GetFullAsync()
        //{
        //    return Ok(await _context.Countries
        //        .Include(x => x.States!)
        //        .ThenInclude(x => x.Cities)
        //        .ToListAsync());
        //}

        //[HttpGet("{id:int}")]
        //public async Task<IActionResult> GetAsync(int id)
        //{
        //    var country = await _context.Countries
        //        .Include(x => x.States!)
        //        .ThenInclude(x => x.Cities)
        //        .FirstOrDefaultAsync(x => x.Id == id);
        //    if (country == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(country);
        //}

        [HttpPost]
        public async Task<ActionResult> PostAsync(Category category)
        {
            try
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                return Ok(category);
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe una categoría con el mismo nombre.");
                }

                return BadRequest(dbUpdateException.Message);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        //[HttpPut]
        //public async Task<ActionResult> PutAsync(Country country)
        //{
        //    try
        //    {
        //        _context.Update(country);
        //        await _context.SaveChangesAsync();
        //        return Ok(country);
        //    }
        //    catch (DbUpdateException dbUpdateException)
        //    {
        //        if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
        //        {
        //            return BadRequest("Ya existe un país con el mismo nombre.");
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
        //    var country = await _context.Countries.FirstOrDefaultAsync(x => x.Id == id);
        //    if (country == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Remove(country);
        //    await _context.SaveChangesAsync();
        //    return NoContent();
        //}
    }

    //}
}