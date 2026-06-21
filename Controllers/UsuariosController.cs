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
    public class UsuariosController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IUserSessionService _userSession;

        public UsuariosController(AppDbContext context, IUserSessionService userSession)
        {
            _context = context;
            _userSession = userSession;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            var adminResult = EnsureAdmin();
            if (adminResult != null) return adminResult;

            return View(await _context.Usuarios.ToListAsync());
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var adminResult = EnsureAdmin();
            if (adminResult != null) return adminResult;

            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            var adminResult = EnsureAdmin();
            if (adminResult != null) return adminResult;

            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Email,Telefono,EsCliente,EsEmprendedor,EsAdministrador,Activo,FechaCreacion")] Usuario usuario)
        {
            var adminResult = EnsureAdmin();
            if (adminResult != null) return adminResult;

            if (ModelState.IsValid)
            {
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var adminResult = EnsureAdmin();
            if (adminResult != null) return adminResult;

            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Email,Telefono,EsCliente,EsEmprendedor,EsAdministrador,Activo,FechaCreacion")] Usuario usuario)
        {
            var adminResult = EnsureAdmin();
            if (adminResult != null) return adminResult;

            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var usuarioExistente = await _context.Usuarios.FindAsync(id);
                    if (usuarioExistente == null)
                    {
                        return NotFound();
                    }

                    usuarioExistente.Nombre = usuario.Nombre;
                    usuarioExistente.Email = usuario.Email;
                    usuarioExistente.Telefono = usuario.Telefono;
                    usuarioExistente.EsCliente = usuario.EsCliente;
                    usuarioExistente.EsEmprendedor = usuario.EsEmprendedor;
                    usuarioExistente.EsAdministrador = usuario.EsAdministrador;
                    usuarioExistente.Activo = usuario.Activo;
                    usuarioExistente.FechaCreacion = usuario.FechaCreacion;

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Id))
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
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var adminResult = EnsureAdmin();
            if (adminResult != null) return adminResult;

            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var adminResult = EnsureAdmin();
            if (adminResult != null) return adminResult;

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                usuario.Activo = false;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }

        private IActionResult? EnsureAdmin()
        {
            if (!_userSession.IsLoggedIn)
            {
                return RedirectToAction("Login", "Account", new { returnUrl = $"{Request.Path}{Request.QueryString}" });
            }

            if (!_userSession.IsAdmin)
            {
                return StatusCode(403);
            }

            return null;
        }
    }
}
