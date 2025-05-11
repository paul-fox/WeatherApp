using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WeatherApp.Context;
using WeatherApp.Models;

namespace WeatherApp.Controllers
{
    public class WeatherSQLsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WeatherSQLsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: WeatherSQLs
        public async Task<IActionResult> Index()
        {
            return View(await _context.WeathersSQL.ToListAsync());
        }

        // GET: WeatherSQLs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weatherSQL = await _context.WeathersSQL
                .FirstOrDefaultAsync(m => m.Id == id);
            if (weatherSQL == null)
            {
                return NotFound();
            }

            return View(weatherSQL);
        }

        // GET: WeatherSQLs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WeatherSQLs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,WeatherDisc,Main,Datetime,CityId,City")] WeatherSQL weatherSQL)
        {
            if (ModelState.IsValid)
            {
                _context.Add(weatherSQL);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(weatherSQL);
        }

        // GET: WeatherSQLs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weatherSQL = await _context.WeathersSQL.FindAsync(id);
            if (weatherSQL == null)
            {
                return NotFound();
            }
            return View(weatherSQL);
        }

        // POST: WeatherSQLs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,WeatherDisc,Main,Datetime,CityId,City")] WeatherSQL weatherSQL)
        {
            if (id != weatherSQL.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(weatherSQL);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WeatherSQLExists(weatherSQL.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(weatherSQL);
        }

        // GET: WeatherSQLs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weatherSQL = await _context.WeathersSQL
                .FirstOrDefaultAsync(m => m.Id == id);
            if (weatherSQL == null)
            {
                return NotFound();
            }

            return View(weatherSQL);
        }

        // POST: WeatherSQLs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var weatherSQL = await _context.WeathersSQL.FindAsync(id);
            if (weatherSQL != null)
            {
                _context.WeathersSQL.Remove(weatherSQL);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WeatherSQLExists(int id)
        {
            return _context.WeathersSQL.Any(e => e.Id == id);
        }
    }
}
