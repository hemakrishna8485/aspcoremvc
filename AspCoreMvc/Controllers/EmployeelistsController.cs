using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AspCoreMvc.Models;

namespace AspCoreMvc.Controllers
{
    public class EmployeelistsController : Controller
    {
        private readonly employeeContext _context;

        public EmployeelistsController(employeeContext context)
        {
            _context = context;
        }

        // GET: Employeelists
        public async Task<IActionResult> Index()
        {
            return View(await _context.Employeelists.ToListAsync());
        }

        // GET: Employeelists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeelist = await _context.Employeelists
                .FirstOrDefaultAsync(m => m.Eid == id);
            if (employeelist == null)
            {
                return NotFound();
            }

            return View(employeelist);
        }

        // GET: Employeelists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employeelists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Eid,Ename,Eaddress,Esal")] Employeelist employeelist)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employeelist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employeelist);
        }

        // GET: Employeelists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeelist = await _context.Employeelists.FindAsync(id);
            if (employeelist == null)
            {
                return NotFound();
            }
            return View(employeelist);
        }

        // POST: Employeelists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Eid,Ename,Eaddress,Esal")] Employeelist employeelist)
        {
            if (id != employeelist.Eid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeelist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeelistExists(employeelist.Eid))
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
            return View(employeelist);
        }

        // GET: Employeelists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeelist = await _context.Employeelists
                .FirstOrDefaultAsync(m => m.Eid == id);
            if (employeelist == null)
            {
                return NotFound();
            }

            return View(employeelist);
        }

        // POST: Employeelists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employeelist = await _context.Employeelists.FindAsync(id);
            _context.Employeelists.Remove(employeelist);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeelistExists(int id)
        {
            return _context.Employeelists.Any(e => e.Eid == id);
        }
    }
}
