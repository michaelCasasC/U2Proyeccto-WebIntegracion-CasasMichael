using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyecto.Data;
using Proyecto.Models;
using Proyecto.Services;

namespace Proyecto.Controllers
{
    public class EmprendimientosController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IUserSessionService _userSession;

        public EmprendimientosController(AppDbContext context, IUserSessionService userSession)
        {
            _context = context;
            _userSession = userSession;
        }

        // GET: Emprendimientos
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Emprendimientos.Include(e => e.Usuario);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Emprendimientos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emprendimiento = await _context.Emprendimientos
                .Include(e => e.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (emprendimiento == null)
            {
                return NotFound();
            }

            return View(emprendimiento);
        }

        // GET: Emprendimientos/Create
        public IActionResult Create()
        {
            if (!_userSession.IsLoggedIn)
            {
                return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("Create", "Emprendimientos") });
            }

            return View();
        }

        // POST: Emprendimientos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Descripcion,Activo,FechaCreacion")] Emprendimiento emprendimiento)
        {
            if (!_userSession.IsLoggedIn)
            {
                return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("Create", "Emprendimientos") });
            }

            var currentUserId = _userSession.GetCurrentUserId();
            if (!currentUserId.HasValue)
            {
                return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("Create", "Emprendimientos") });
            }

            if (ModelState.IsValid)
            {
                emprendimiento.UsuarioId = currentUserId.Value;
                emprendimiento.FechaCreacion = DateTime.UtcNow;
                emprendimiento.Activo = true;

                _context.Add(emprendimiento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(emprendimiento);
        }

        // GET: Emprendimientos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emprendimiento = await _context.Emprendimientos.FindAsync(id);
            if (emprendimiento == null)
            {
                return NotFound();
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Email", emprendimiento.UsuarioId);
            return View(emprendimiento);
        }

        // POST: Emprendimientos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Descripcion,Activo,FechaCreacion,UsuarioId")] Emprendimiento emprendimiento)
        {
            if (id != emprendimiento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(emprendimiento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmprendimientoExists(emprendimiento.Id))
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
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Email", emprendimiento.UsuarioId);
            return View(emprendimiento);
        }

        // GET: Emprendimientos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emprendimiento = await _context.Emprendimientos
                .Include(e => e.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (emprendimiento == null)
            {
                return NotFound();
            }

            return View(emprendimiento);
        }

        // POST: Emprendimientos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var emprendimiento = await _context.Emprendimientos.FindAsync(id);
            if (emprendimiento != null)
            {
                _context.Emprendimientos.Remove(emprendimiento);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmprendimientoExists(int id)
        {
            return _context.Emprendimientos.Any(e => e.Id == id);
        }
    }
}
