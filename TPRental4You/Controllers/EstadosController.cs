using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TPRental4You.Data;
using TPRental4You.Models;

namespace TPRental4You.Controllers
{
    public class EstadosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EstadosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Estados
        [Authorize(Roles = "Funcionario,Gestor")]
        public async Task<IActionResult> Index()
        {
            ViewData["FuncionarioId"] = new SelectList(_context.Users, "Id", "Email");
            var applicationDbContext = _context.Estados.Include(e => e.reserva).Include(e => e.funcionario);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Estados/Details/5
        [Authorize(Roles = "Funcionario,Gestor")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Estados == null)
            {
                return NotFound();
            }

            var estado = await _context.Estados
                .Include(e => e.reserva)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estado == null)
            {
                return NotFound();
            }

            return View(estado);
        }

        // GET: Estados/Create
        [Authorize(Roles = "Funcionario,Gestor")]
        public IActionResult Create(int id, int tipoEstado)
        {
            var reserva = _context.Reservas.Where(u => u.Id == id).FirstOrDefault();

            if (reserva == null)
                return RedirectToAction(nameof(Index));

            var veiculo = _context.Veiculos.Where(u => u.Id == reserva.VeiculoId).FirstOrDefault();

            if (veiculo == null)
                return RedirectToAction(nameof(Index));


            Estado e = new Estado();
            if (tipoEstado == 0)
            {
                e.Tipo = Tipo.Levantamento;
            }
            else
            {
                e.Tipo = Tipo.Entrega;

            }

            e.Km = veiculo.Km;
            e.FuncionarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var veiculoPath = Path.Combine(Directory.GetCurrentDirectory(), ("wwwroot/imagens/danosReserva/" + id.ToString()));
            if (!Directory.Exists(veiculoPath))
                Directory.CreateDirectory(veiculoPath);
            //LINK SYNTAX
            var files = from file in
                            Directory.EnumerateFiles(veiculoPath)
                        select string.Format(
                            "/imagens/danosReserva//{0}/{1}",
                            id,
                            Path.GetFileName(file));

            ViewData["Ficheiros"] = files; //lista de strings para a vista
            //ViewData["VeiculoId"] = veiculo.Id;
            //ViewBag.Ficheiros = files;

            ViewData["ReservaId"] = reserva.Id;
            ViewData["FuncionarioId"] = new SelectList(_context.Users, "Id", "Email");
            return View(e);
        }

        // POST: Estados/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Funcionario,Gestor")]
        public async Task<IActionResult> Create([Bind("Tipo,Km,DanosDoVeiculo,Observacoes,ReservaId,FuncionarioId")] Estado estado,
            [FromForm] List<IFormFile>? ficheiros)
        {
            ModelState.Remove(nameof(estado.funcionario));
            ModelState.Remove(nameof(estado.reserva));
            if (ModelState.IsValid)
            {
                _context.Add(estado);
                await _context.SaveChangesAsync();


                var reserva = _context.Reservas.Where(u => u.Id == estado.ReservaId).FirstOrDefault();
                var veiculo = _context.Veiculos.Where(u => u.Id == reserva.VeiculoId).FirstOrDefault();

                if (veiculo.Ativo == true)
                    veiculo.Ativo = false;
                else
                {
                    veiculo.Ativo = true;
                    veiculo.Km = estado.Km;
                    reserva.EstadoReserva = estadoReserva.Finalizada;
                }

                _context.Update(veiculo);
                await _context.SaveChangesAsync();



                _context.Update(reserva);
                await _context.SaveChangesAsync();

                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens/danosReserva/");
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                // Dir relativo aos ficheiros do curso
                path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens/danosReserva/" + estado.ReservaId.ToString());
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                foreach (var formFile in ficheiros)
                {
                    if (formFile.Length > 0)
                    {
                        var filePath = Path.Combine(path, Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName));
                        while (System.IO.File.Exists(filePath))
                        {
                            filePath = Path.Combine(path, Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName));
                        }
                        using (var stream = System.IO.File.Create(filePath))
                        {
                            await formFile.CopyToAsync(stream);
                        }
                    }
                }

                return RedirectToAction("Index", "Reservas");
            }

            ViewData["ReservaId"] = new SelectList(_context.Reservas, "Id", "Id", estado.ReservaId);
            ViewData["FuncionarioId"] = new SelectList(_context.Users, "Id", "Email", estado.FuncionarioId);
            return View(estado);
        }

        [Authorize(Roles = "Funcionario,Gestor")]
        public async Task<IActionResult> deleteImage(int id, string image, int tipoEstado)
        {
            if (id == null || _context.Reservas == null)
                return NotFound();

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), ("wwwroot/" + image));

            System.IO.File.Delete(filePath);

            //int idReserva, int tipoEstado
            return RedirectToAction("Create", new { Id = id, TipoEstado = tipoEstado });
        }


        // GET: Estados/Edit/5
        [Authorize(Roles = "Funcionario,Gestor")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Estados == null)
            {
                return NotFound();
            }

            var estado = await _context.Estados.FindAsync(id);
            if (estado == null)
            {
                return NotFound();
            }
            ViewData["ReservaId"] = new SelectList(_context.Reservas, "Id", "Id", estado.ReservaId);
            ViewData["FuncionarioId"] = new SelectList(_context.Users, "Id", "Email", estado.FuncionarioId);
            return View(estado);
        }

        // POST: Estados/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Funcionario,Gestor")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Tipo,Km,DanosDoVeiculo,Observacoes,ReservaId,FuncionariorId")] Estado estado)
        {
            if (id != estado.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstadoExists(estado.Id))
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
            ViewData["ReservaId"] = new SelectList(_context.Reservas, "Id", "Id", estado.ReservaId);
            ViewData["FuncionarioId"] = new SelectList(_context.Users, "Id", "Email", estado.FuncionarioId);
            return View(estado);
        }

        // GET: Estados/Delete/5
        [Authorize(Roles = "Funcionario,Gestor")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Estados == null)
            {
                return NotFound();
            }

            var estado = await _context.Estados
                .Include(e => e.reserva)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estado == null)
            {
                return NotFound();
            }

            return View(estado);
        }

        // POST: Estados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Funcionario,Gestor")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Estados == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Estados'  is null.");
            }
            var estado = await _context.Estados.FindAsync(id);
            if (estado != null)
            {
                _context.Estados.Remove(estado);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Funcionario,Gestor")]
        private bool EstadoExists(int id)
        {
            return _context.Estados.Any(e => e.Id == id);
        }
    }
}
