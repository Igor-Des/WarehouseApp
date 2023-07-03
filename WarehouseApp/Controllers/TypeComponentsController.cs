using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WarehouseApp.Data;
using WarehouseApp.Models;

namespace WarehouseApp.Controllers
{
    public class TypeComponentsController : Controller
    {
        private readonly WarehouseContext _context;

        public TypeComponentsController(WarehouseContext context)
        {
            _context = context;
        }

        // GET: TypeComponents
        public async Task<IActionResult> Index()
        {
            return View(await _context.TypeComponents.ToListAsync());
        }

        // GET: TypeComponents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeComponent = await _context.TypeComponents
                .FirstOrDefaultAsync(m => m.TypeComponentId == id);
            if (typeComponent == null)
            {
                return NotFound();
            }

            return View(typeComponent);
        }

        // GET: TypeComponents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TypeComponents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TypeComponentId,Name")] TypeComponent typeComponent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(typeComponent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(typeComponent);
        }

        // GET: TypeComponents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeComponent = await _context.TypeComponents.FindAsync(id);
            if (typeComponent == null)
            {
                return NotFound();
            }
            return View(typeComponent);
        }

        // POST: TypeComponents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TypeComponentId,Name")] TypeComponent typeComponent)
        {
            if (id != typeComponent.TypeComponentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typeComponent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypeComponentExists(typeComponent.TypeComponentId))
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
            return View(typeComponent);
        }

        // GET: TypeComponents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeComponent = await _context.TypeComponents
                .FirstOrDefaultAsync(m => m.TypeComponentId == id);
            if (typeComponent == null)
            {
                return NotFound();
            }

            return View(typeComponent);
        }

        // POST: TypeComponents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var typeComponent = await _context.TypeComponents.FindAsync(id);
            _context.TypeComponents.Remove(typeComponent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypeComponentExists(int id)
        {
            return _context.TypeComponents.Any(e => e.TypeComponentId == id);
        }
    }
}
