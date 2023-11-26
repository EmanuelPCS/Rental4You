using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TPRental4You.Data;
using TPRental4You.Migrations;
using TPRental4You.Models;

namespace TPRental4You.Controllers
{
    public class TiposDeVeiculosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TiposDeVeiculosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TiposDeVeiculos
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.TiposDeVeiculos.Include(t => t.Veiculos).ToListAsync());
        }

        // GET: TiposDeVeiculos/Details/5
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TiposDeVeiculos == null)
            {
                return NotFound();
            }

            var tiposDeVeiculo = await _context.TiposDeVeiculos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tiposDeVeiculo == null)
            {
                return NotFound();
            }

            return View(tiposDeVeiculo);
        }

        // GET: TiposDeVeiculos/Create
        [Authorize(Roles = "Administrador")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: TiposDeVeiculos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Create([Bind("Id,Nome")] TiposDeVeiculo tiposDeVeiculo)
        {

            ModelState.Remove(nameof(tiposDeVeiculo.Veiculos));
            if (ModelState.IsValid)
            {
                _context.Add(tiposDeVeiculo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tiposDeVeiculo);
        }

        // GET: TiposDeVeiculos/Edit/5
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TiposDeVeiculos == null)
            {
                return NotFound();
            }

            var tiposDeVeiculo = await _context.TiposDeVeiculos.FindAsync(id);
            if (tiposDeVeiculo == null)
            {
                return NotFound();
            }
            return View(tiposDeVeiculo);
        }

        // POST: TiposDeVeiculos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome")] TiposDeVeiculo tiposDeVeiculo)
        {
            if (id != tiposDeVeiculo.Id)
            {
                return NotFound();
            }

            ModelState.Remove(nameof(tiposDeVeiculo.Veiculos));
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tiposDeVeiculo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TiposDeVeiculoExists(tiposDeVeiculo.Id))
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
            return View(tiposDeVeiculo);
        }

        // GET: TiposDeVeiculos/Delete/5
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TiposDeVeiculos == null)
            {
                return NotFound();
            }

            var tiposDeVeiculo = await _context.TiposDeVeiculos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tiposDeVeiculo == null)
            {
                return NotFound();
            }

            return View(tiposDeVeiculo);
        }

        // POST: TiposDeVeiculos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TiposDeVeiculos == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TiposDeVeiculos'  is null.");
            }
            var tiposDeVeiculo = await _context.TiposDeVeiculos.FindAsync(id);
            if (tiposDeVeiculo != null)
            {
                _context.TiposDeVeiculos.Remove(tiposDeVeiculo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Administrador")]
        private bool TiposDeVeiculoExists(int id)
        {
            return _context.TiposDeVeiculos.Any(e => e.Id == id);
        }
    }
}
