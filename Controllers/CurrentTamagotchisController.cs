using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tamagotchi.Data;
using Tamagotchi.Areas.Admin.Controllers;

namespace Tamagotchi.Controllers
{
    public class CurrentTamagotchisController : AdminBaseController
    {
        private readonly ApplicationDbContext _context;

        public CurrentTamagotchisController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CurrentTamagotchis
        public async Task<IActionResult> Index()
        {
            return _context.CurrentTamagotchis != null ?
                        View(await _context.CurrentTamagotchis.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.CurrentTamagotchis'  is null.");
        }

        // GET: CurrentTamagotchis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CurrentTamagotchis == null)
            {
                return NotFound();
            }

            var currentTamagotchi = await _context.CurrentTamagotchis
                .FirstOrDefaultAsync(m => m.Id == id);
            if (currentTamagotchi == null)
            {
                return NotFound();
            }

            return View(currentTamagotchi);
        }

        // GET: CurrentTamagotchis/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CurrentTamagotchis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Id_User,Gender,Health,Age,Age_State,Fun,Hygiene,Energy,Hunger,Max_Health,Max_Hunger,Max_Energy,Max_Hygiene,Max_Fun")] CurrentTamagotchi currentTamagotchi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(currentTamagotchi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(currentTamagotchi);
        }

        // GET: CurrentTamagotchis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CurrentTamagotchis == null)
            {
                return NotFound();
            }

            var currentTamagotchi = await _context.CurrentTamagotchis.FindAsync(id);
            if (currentTamagotchi == null)
            {
                return NotFound();
            }
            return View(currentTamagotchi);
        }

        // POST: CurrentTamagotchis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Id_User,Gender,Health,Age,Age_State,Fun,Hygiene,Energy,Hunger,Max_Health,Max_Hunger,Max_Energy,Max_Hygiene,Max_Fun")] CurrentTamagotchi currentTamagotchi)
        {
            if (id != currentTamagotchi.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(currentTamagotchi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CurrentTamagotchiExists(currentTamagotchi.Id))
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
            return View(currentTamagotchi);
        }

        // GET: CurrentTamagotchis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CurrentTamagotchis == null)
            {
                return NotFound();
            }

            var currentTamagotchi = await _context.CurrentTamagotchis
                .FirstOrDefaultAsync(m => m.Id == id);
            if (currentTamagotchi == null)
            {
                return NotFound();
            }

            return View(currentTamagotchi);
        }

        // POST: CurrentTamagotchis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CurrentTamagotchis == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CurrentTamagotchis'  is null.");
            }
            var currentTamagotchi = await _context.CurrentTamagotchis.FindAsync(id);
            if (currentTamagotchi != null)
            {
                _context.CurrentTamagotchis.Remove(currentTamagotchi);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CurrentTamagotchiExists(int id)
        {
            return (_context.CurrentTamagotchis?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
