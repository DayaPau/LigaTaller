using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LigaTaller.Data;
using LigaTaller.Models;

namespace LigaTaller.Controllers
{
    public class JugadoresController : Controller
    {
        private readonly LigaTallerContext _context;

        public JugadoresController(LigaTallerContext context)
        {
            _context = context;
        }

        // GET: Jugadores
        public async Task<IActionResult> Index()
        {
            return View(await _context.Jugadores.ToListAsync());
        }

        // GET: Jugadores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jugadores = await _context.Jugadores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jugadores == null)
            {
                return NotFound();
            }

            return View(jugadores);
        }

        // GET: Jugadores/Create

        public async Task<IActionResult> Index(int? equipoId)
        {
            ViewBag.EquipoId = new SelectList(await _context.Equipo.ToListAsync(), "Id", "Nombre");

            var jugadores = _context.Jugadores.Include(j => j.Equipo); // Asegúrate de que tienes una relación con el modelo de Equipo

            if (equipoId.HasValue && equipoId.Value > 0)
            {
                jugadores = jugadores.Where(j => j.IdEquipo == equipoId.Value);
            }

            return View(await jugadores.ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }

        // POST: Jugadores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Posicion,Edad,IdEquipo")] Jugadores jugadores)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jugadores);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jugadores);
        }




        // GET: Jugadores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jugadores = await _context.Jugadores.FindAsync(id);
            if (jugadores == null)
            {
                return NotFound();
            }
            return View(jugadores);
        }

        // POST: Jugadores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Posicion,Edad,IdEquipo")] Jugadores jugadores)
        {
            if (id != jugadores.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jugadores);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JugadoresExists(jugadores.Id))
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
            return View(jugadores);
        }

        // GET: Jugadores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jugadores = await _context.Jugadores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jugadores == null)
            {
                return NotFound();
            }

            return View(jugadores);
        }

        // POST: Jugadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jugadores = await _context.Jugadores.FindAsync(id);
            if (jugadores != null)
            {
                _context.Jugadores.Remove(jugadores);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JugadoresExists(int id)
        {
            return _context.Jugadores.Any(e => e.Id == id);
        }
    }
}
