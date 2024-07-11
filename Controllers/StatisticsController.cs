using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tamagotchi.Data;
using Tamagotchi.Areas.Admin.Controllers;

namespace Tamagotchi.Controllers
{
    public class StatisticsController : AdminBaseController
    {
        private readonly ApplicationDbContext _context;

        public StatisticsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Statistics
        public async Task<IActionResult> Index()
        {
            return _context.Statistics != null ?
                        View(await _context.Statistics.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Statistics'  is null.");
        }

        // GET: Statistics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Statistics == null)
            {
                return NotFound();
            }

            var statistics = await _context.Statistics
                .FirstOrDefaultAsync(m => m.Id == id);
            if (statistics == null)
            {
                return NotFound();
            }

            return View(statistics);
        }

        // GET: Statistics/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Statistics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Health,Hunger,Energy,Hygiene,Fun,Max_Health,Max_Hunger,Max_Energy,Max_Hygiene,Max_Fun")] Statistics statistics)
        {
            if (ModelState.IsValid)
            {
                _context.Add(statistics);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(statistics);
        }

        // GET: Statistics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Statistics == null)
            {
                return NotFound();
            }

            var statistics = await _context.Statistics.FindAsync(id);
            if (statistics == null)
            {
                return NotFound();
            }
            return View(statistics);
        }

        // POST: Statistics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Health,Hunger,Energy,Hygiene,Fun,Max_Health,Max_Hunger,Max_Energy,Max_Hygiene,Max_Fun")] Statistics statistics)
        {
            if (id != statistics.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(statistics);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StatisticsExists(statistics.Id))
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
            return View(statistics);
        }

        // GET: Statistics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Statistics == null)
            {
                return NotFound();
            }

            var statistics = await _context.Statistics
                .FirstOrDefaultAsync(m => m.Id == id);
            if (statistics == null)
            {
                return NotFound();
            }

            return View(statistics);
        }

        // POST: Statistics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Statistics == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Statistics'  is null.");
            }
            var statistics = await _context.Statistics.FindAsync(id);
            if (statistics != null)
            {
                _context.Statistics.Remove(statistics);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StatisticsExists(int id)
        {
            return (_context.Statistics?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
