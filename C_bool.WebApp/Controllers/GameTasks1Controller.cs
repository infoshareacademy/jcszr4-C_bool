using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using C_bool.BLL.DAL.Context;
using C_bool.BLL.DAL.Entities;

namespace C_bool.WebApp.Controllers
{
    public class GameTasks1Controller : Controller
    {
        private readonly ApplicationDbContext _context;

        public GameTasks1Controller(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: GameTasks1
        public async Task<IActionResult> Index()
        {
            return View(await _context.GameTasks.ToListAsync());
        }

        // GET: GameTasks1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameTask = await _context.GameTasks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gameTask == null)
            {
                return NotFound();
            }

            return View(gameTask);
        }

        // GET: GameTasks1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GameTasks1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,ShortDescription,Description,Photo,Type,Points,ValidFrom,ValidThru,IsActive,CreatedById,CreatedByName,Id,Created")] GameTask gameTask)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gameTask);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gameTask);
        }

        // GET: GameTasks1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameTask = await _context.GameTasks.FindAsync(id);
            if (gameTask == null)
            {
                return NotFound();
            }
            return View(gameTask);
        }

        // POST: GameTasks1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,ShortDescription,Description,Photo,Type,Points,ValidFrom,ValidThru,IsActive,CreatedById,CreatedByName,Id,Created")] GameTask gameTask)
        {
            if (id != gameTask.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gameTask);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameTaskExists(gameTask.Id))
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
            return View(gameTask);
        }

        // GET: GameTasks1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameTask = await _context.GameTasks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gameTask == null)
            {
                return NotFound();
            }

            return View(gameTask);
        }

        // POST: GameTasks1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gameTask = await _context.GameTasks.FindAsync(id);
            _context.GameTasks.Remove(gameTask);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameTaskExists(int id)
        {
            return _context.GameTasks.Any(e => e.Id == id);
        }
    }
}
