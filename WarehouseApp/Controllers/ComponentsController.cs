using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using WarehouseApp.Data;
using WarehouseApp.Infrastructure;
using WarehouseApp.Models;
using WarehouseApp.Services;
using WarehouseApp.ViewModels;
using X.PagedList;

namespace WarehouseApp.Controllers
{
    public class ComponentsController : Controller
    {
        private readonly WarehouseContext _context;
        private string _currentSearchSupplier = "searchSupplier";
        private string _currentSearchType = "searchType";

        public ComponentsController(WarehouseContext context)
        {
            _context = context;
        }

        // GET: Components
        public ActionResult Index(SortState sortOrder, string currentFilter1,
            string currentFilter2, string searchSupplier, string searchType, int? page, int? reset)
        {
            if (searchSupplier != null || searchType != null || (searchSupplier != null & searchType != null))
            {
                page = 1;
            }
            else
            {
                searchSupplier = currentFilter1;
                searchType = currentFilter2;
            }

            IEnumerable<ComponentViewModel> componentViewModel;
            ICached<Component> cachedComponents = _context.GetService<ICached<Component>>();

            if (reset == 1 || !HttpContext.Session.Keys.Contains("components"))
            {
                componentViewModel = GetMechanic(cachedComponents.GetList());
                HttpContext.Session.SetList("components", componentViewModel);
                HttpContext.Session.Remove(_currentSearchSupplier);
                HttpContext.Session.Remove(_currentSearchType);
            }
            else
            {
                componentViewModel = HttpContext.Session.Get<IEnumerable<ComponentViewModel>>("components");
            }
            componentViewModel = _SearchType(_SearchSupplier(componentViewModel, searchSupplier), searchType);
            ViewBag.CurrentSort = sortOrder;
            componentViewModel = _Sort(componentViewModel, sortOrder);

            if (!HttpContext.Session.Keys.Contains("components") || searchSupplier != null || searchType != null)
            {
                HttpContext.Session.SetList("components", componentViewModel);
                HttpContext.Session.SetString(_currentSearchSupplier, searchSupplier ?? string.Empty);
                HttpContext.Session.SetString(_currentSearchType, searchType ?? string.Empty);
            }

            int pageSize = 10;
            int pageNumber = page ?? 1;

            ViewBag.CurrentFilter1 = HttpContext.Session.GetString(_currentSearchSupplier);
            ViewBag.CurrentFilter2 = HttpContext.Session.GetString(_currentSearchType);

            return View(componentViewModel.ToPagedList(pageNumber, pageSize));
        }

        // GET: Components/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var component = await _context.Components
                .Include(c => c.Supplier)
                .Include(c => c.TypeComponent)
                .FirstOrDefaultAsync(m => m.ComponentId == id);
            if (component == null)
            {
                return NotFound();
            }

            return View(component);
        }

        // GET: Components/Create
        public IActionResult Create()
        {
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "Name");
            ViewData["TypeComponentId"] = new SelectList(_context.TypeComponents, "TypeComponentId", "Name");
            return View();
        }

        // POST: Components/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ComponentId,Name,SupplierId,TypeComponentId,Price,Amount,Date")] Component component)
        {
            if (ModelState.IsValid)
            {
                _context.Add(component);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "Name", component.SupplierId);
            ViewData["TypeComponentId"] = new SelectList(_context.TypeComponents, "TypeComponentId", "Name", component.TypeComponentId);
            return View(component);
        }

        // GET: Components/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var component = await _context.Components.FindAsync(id);
            if (component == null)
            {
                return NotFound();
            }
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "Name", component.SupplierId);
            ViewData["TypeComponentId"] = new SelectList(_context.TypeComponents, "TypeComponentId", "Name", component.TypeComponentId);
            return View(component);
        }

        // POST: Components/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ComponentId,Name,SupplierId,TypeComponentId,Price,Amount,Date")] Component component)
        {
            if (id != component.ComponentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(component);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComponentExists(component.ComponentId))
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
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "Name", component.SupplierId);
            ViewData["TypeComponentId"] = new SelectList(_context.TypeComponents, "TypeComponentId", "Name", component.TypeComponentId);
            return View(component);
        }

        // GET: Components/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var component = await _context.Components
                .Include(c => c.Supplier)
                .Include(c => c.TypeComponent)
                .FirstOrDefaultAsync(m => m.ComponentId == id);
            if (component == null)
            {
                return NotFound();
            }

            return View(component);
        }

        // POST: Components/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var component = await _context.Components.FindAsync(id);
            _context.Components.Remove(component);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private IEnumerable<ComponentViewModel> _SearchSupplier(IEnumerable<ComponentViewModel> components, string searchSupplier)
        {
            if (!String.IsNullOrEmpty(searchSupplier))
            {
                components = components.Where(c => c.SupplierName.Contains(searchSupplier));
            }
            return components;
        }

        private IEnumerable<ComponentViewModel> _SearchType(IEnumerable<ComponentViewModel> components, string searchType)
        {
            if (!String.IsNullOrEmpty(searchType))
            {
                components = components.Where(c => c.TypeComponentName.Contains(searchType));
            }
            return components;
        }

        private IEnumerable<ComponentViewModel> _Sort(IEnumerable<ComponentViewModel> components, SortState sortOrder)
        {
            ViewData["Price"] = sortOrder == SortState.PriceComponentAsc ? SortState.PriceComponentDesc : SortState.PriceComponentAsc;
            ViewData["Amount"] = sortOrder == SortState.AmountComponentAsc ? SortState.AmountComponentDesc : SortState.AmountComponentAsc;
            ViewData["Date"] = sortOrder == SortState.DateComponentAsc ? SortState.DateComponentDesc : SortState.DateComponentAsc;
            components = sortOrder switch
            {
                SortState.PriceComponentAsc => components.OrderBy(c => c.Price),
                SortState.PriceComponentDesc => components.OrderByDescending(c => c.Price),
                SortState.AmountComponentAsc => components.OrderBy(c => c.Amount),
                SortState.AmountComponentDesc => components.OrderByDescending(c => c.Amount),
                SortState.DateComponentAsc => components.OrderBy(c => c.Date),
                SortState.DateComponentDesc => components.OrderByDescending(c => c.Date),
                _ => components.OrderBy(c => c.ComponentId),
            };
            return components;
        }

        private bool ComponentExists(int id)
        {
            return _context.Components.Any(e => e.ComponentId == id);
        }

        public IEnumerable<ComponentViewModel> GetMechanic(IEnumerable<Component> components)
        {
            IEnumerable<ComponentViewModel> componentViewModel = from c in components
                                                                 join typecomp in _context.TypeComponents
                                                                 on c.TypeComponentId equals typecomp.TypeComponentId
                                                                 join s in _context.Suppliers
                                                                 on c.SupplierId equals s.SupplierId
                                                                 select new ComponentViewModel
                                                               {
                                                                   ComponentId = c.ComponentId,
                                                                   Name = c.Name,
                                                                   SupplierName = s.Name,
                                                                   TypeComponentName = typecomp.Name,
                                                                   Price = c.Price,
                                                                   Amount = c.Amount,
                                                                   Date = c.Date
                                                               };
            return componentViewModel;
        }
    }
}
