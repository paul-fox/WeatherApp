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
    public class LocationSQLsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LocationSQLsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LocationSQLs
        public async Task<IActionResult> Index()
        {
            return View(await _context.LocationsSQL.ToListAsync());
        }

        // GET: LocationSQLs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locationSQL = await _context.LocationsSQL
                .FirstOrDefaultAsync(m => m.Id == id);
            if (locationSQL == null)
            {
                return NotFound();
            }

            return View(locationSQL);
        }

        // GET: LocationSQLs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LocationSQLs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,City,Latitude,Longitude,Country,State")] LocationSQL locationSQL)
        {
            if (ModelState.IsValid)
            {
                _context.Add(locationSQL);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(locationSQL);
        }

        // GET: LocationSQLs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locationSQL = await _context.LocationsSQL.FindAsync(id);
            if (locationSQL == null)
            {
                return NotFound();
            }
            return View(locationSQL);
        }

        // POST: LocationSQLs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,City,Latitude,Longitude,Country,State")] LocationSQL locationSQL)
        {
            if (id != locationSQL.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(locationSQL);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocationSQLExists(locationSQL.Id))
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
            return View(locationSQL);
        }

        // GET: LocationSQLs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locationSQL = await _context.LocationsSQL
                .FirstOrDefaultAsync(m => m.Id == id);
            if (locationSQL == null)
            {
                return NotFound();
            }

            return View(locationSQL);
        }

        // POST: LocationSQLs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var locationSQL = await _context.LocationsSQL.FindAsync(id);
            if (locationSQL != null)
            {
                _context.LocationsSQL.Remove(locationSQL);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LocationSQLExists(int id)
        {
            return _context.LocationsSQL.Any(e => e.Id == id);
        }
    }
}
