using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TPRental4You.Data;
using TPRental4You.Models;
using TPRental4You.Models.ViewModels;

namespace TPRental4You.Controllers
{
    public class EmpresasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public EmpresasController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Empresas
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Index([Bind("EmpresaId,Ativa,ordenarEmpresa,ordenarEstado")]
            PesquisaEmpresaViewModel? pesquisaEmpresa)
        {
            ViewData["EmpresaId"] = new SelectList(_context.Empresas.ToList(), "Id", "Nome");

            bool estado;
            if (pesquisaEmpresa.Ativa == 1)
                estado = true;
            else /*if(pesquisaVeiculo.Ativo == 2)*/
                estado = false;

            if (pesquisaEmpresa.EmpresaId != 0 && pesquisaEmpresa.Ativa != 0)
            {

                if (pesquisaEmpresa.ordenarEmpresa != 0)
                {
                    if (pesquisaEmpresa.ordenarEmpresa == 1)
                    {
                        var applicationDbContext = _context.Empresas.Include(e => e.Reservas).Include(e => e.Veiculos).Include(e => e.Avaliacoes).Include(e => e.ApplicationUsers)
                        .Where(e => e.Id == pesquisaEmpresa.EmpresaId
                        && e.Ativa == estado).OrderBy(e => e.Nome);
                        return View(await applicationDbContext.ToListAsync());

                    }
                    else
                    {
                        var applicationDbContext = _context.Empresas.Include(e => e.Reservas).Include(e => e.Veiculos).Include(e => e.Avaliacoes).Include(e => e.ApplicationUsers)
                        .Where(e => e.Id == pesquisaEmpresa.EmpresaId
                        && e.Ativa == estado).OrderByDescending(e => e.Nome);
                        return View(await applicationDbContext.ToListAsync());
                    }
                }
                else if (pesquisaEmpresa.ordenarEstado != 0)
                {
                    if (pesquisaEmpresa.ordenarEstado == 1)
                    {
                        var applicationDbContext = _context.Empresas.Include(e => e.Reservas).Include(e => e.Veiculos).Include(e => e.Avaliacoes).Include(e => e.ApplicationUsers)
                        .Where(e => e.Id == pesquisaEmpresa.EmpresaId
                        && e.Ativa == estado).OrderBy(e => e.Ativa);
                        return View(await applicationDbContext.ToListAsync());

                    }
                    else
                    {
                        var applicationDbContext = _context.Empresas.Include(e => e.Reservas).Include(e => e.Veiculos).Include(e => e.Avaliacoes).Include(e => e.ApplicationUsers)
                        .Where(e => e.Id == pesquisaEmpresa.EmpresaId
                        && e.Ativa == estado).OrderByDescending(e => e.Ativa);
                        return View(await applicationDbContext.ToListAsync());
                    }
                }
                else
                {

                    var applicationDbContext = _context.Empresas.Include(e => e.Reservas).Include(e => e.Veiculos).Include(e => e.Avaliacoes).Include(e => e.ApplicationUsers)
                        .Where(e => e.Id == pesquisaEmpresa.EmpresaId
                        && e.Ativa == estado);
                    return View(await applicationDbContext.ToListAsync());
                }
            }
            else if (pesquisaEmpresa.EmpresaId != 0)
            {
                if (pesquisaEmpresa.ordenarEmpresa != 0)
                {
                    if (pesquisaEmpresa.ordenarEmpresa == 1)
                    {
                        var applicationDbContext = _context.Empresas.Include(e => e.Reservas).Include(e => e.Veiculos).Include(e => e.Avaliacoes).Include(e => e.ApplicationUsers)
                        .Where(e => e.Id == pesquisaEmpresa.EmpresaId
                        ).OrderBy(e => e.Nome);
                        return View(await applicationDbContext.ToListAsync());

                    }
                    else
                    {
                        var applicationDbContext = _context.Empresas.Include(e => e.Reservas).Include(e => e.Veiculos).Include(e => e.Avaliacoes).Include(e => e.ApplicationUsers)
                        .Where(e => e.Id == pesquisaEmpresa.EmpresaId
                        ).OrderByDescending(e => e.Nome);
                        return View(await applicationDbContext.ToListAsync());
                    }
                }
                else if (pesquisaEmpresa.ordenarEstado != 0)
                {
                    if (pesquisaEmpresa.ordenarEstado == 3)
                    {
                        var applicationDbContext = _context.Empresas.Include(e => e.Reservas).Include(e => e.Veiculos).Include(e => e.Avaliacoes).Include(e => e.ApplicationUsers)
                        .Where(e => e.Id == pesquisaEmpresa.EmpresaId
                        ).OrderBy(e => e.Ativa);
                        return View(await applicationDbContext.ToListAsync());

                    }
                    else
                    {
                        var applicationDbContext = _context.Empresas.Include(e => e.Reservas).Include(e => e.Veiculos).Include(e => e.Avaliacoes).Include(e => e.ApplicationUsers)
                        .Where(e => e.Id == pesquisaEmpresa.EmpresaId
                        ).OrderByDescending(e => e.Ativa);
                        return View(await applicationDbContext.ToListAsync());

                    }
                }
                else
                {
                    var applicationDbContext = _context.Empresas.Include(e => e.Reservas).Include(e => e.Veiculos).Include(e => e.Avaliacoes).Include(e => e.ApplicationUsers)
                        .Where(e => e.Id == pesquisaEmpresa.EmpresaId);
                    return View(await applicationDbContext.ToListAsync());
                }

            }
            else if (pesquisaEmpresa.Ativa != 0)
            {
                if (pesquisaEmpresa.ordenarEmpresa != 0)
                {
                    if (pesquisaEmpresa.ordenarEmpresa == 1)
                    {
                        var applicationDbContext = _context.Empresas.Include(e => e.Reservas).Include(e => e.Veiculos).Include(e => e.Avaliacoes).Include(e => e.ApplicationUsers)
                        .Where(e => e.Ativa == estado
                        ).OrderBy(e => e.Nome);
                        return View(await applicationDbContext.ToListAsync());

                    }
                    else
                    {
                        var applicationDbContext = _context.Empresas.Include(e => e.Reservas).Include(e => e.Veiculos).Include(e => e.Avaliacoes).Include(e => e.ApplicationUsers)
                        .Where(e => e.Ativa == estado
                        ).OrderByDescending(e => e.Nome);
                        return View(await applicationDbContext.ToListAsync());
                    }
                }
                else if (pesquisaEmpresa.ordenarEstado != 0)
                {
                    if (pesquisaEmpresa.ordenarEstado == 3)
                    {
                        var applicationDbContext = _context.Empresas.Include(e => e.Reservas).Include(e => e.Veiculos).Include(e => e.Avaliacoes).Include(e => e.ApplicationUsers)
                        .Where(e => e.Ativa == estado
                        ).OrderBy(e => e.Ativa);
                        return View(await applicationDbContext.ToListAsync());

                    }
                    else
                    {
                        var applicationDbContext = _context.Empresas.Include(e => e.Reservas).Include(e => e.Veiculos).Include(e => e.Avaliacoes).Include(e => e.ApplicationUsers)
                        .Where(e => e.Ativa == estado
                        ).OrderByDescending(e => e.Ativa);
                        return View(await applicationDbContext.ToListAsync());

                    }
                }
                else
                {
                    var applicationDbContext = _context.Empresas.Include(e => e.Reservas).Include(e => e.Veiculos).Include(e => e.Avaliacoes).Include(e => e.ApplicationUsers)
                        .Where(e => e.Ativa == estado);
                    return View(await applicationDbContext.ToListAsync());
                }

            }
            else if (pesquisaEmpresa.ordenarEmpresa == 1)
            {
                var applicationDbContext = _context.Empresas.Include(e => e.Reservas).Include(e => e.Veiculos).Include(e => e.Avaliacoes).Include(e => e.ApplicationUsers)
                        .OrderBy(e => e.Nome);
                return View(await applicationDbContext.ToListAsync());
            }
            else if (pesquisaEmpresa.ordenarEmpresa == 2)
            {
                var applicationDbContext = _context.Empresas.Include(e => e.Reservas).Include(e => e.Veiculos).Include(e => e.Avaliacoes).Include(e => e.ApplicationUsers)
                        .OrderByDescending(e => e.Nome);
                return View(await applicationDbContext.ToListAsync());
            }
            else if (pesquisaEmpresa.ordenarEstado == 3)
            {
                var applicationDbContext = _context.Empresas.Include(e => e.Reservas).Include(e => e.Veiculos).Include(e => e.Avaliacoes).Include(e => e.ApplicationUsers)
                        .OrderBy(e => e.Ativa);
                return View(await applicationDbContext.ToListAsync());
            }
            else /*if(pesquisaVeiculo.ordenarEstado == 4)*/
            {
                var applicationDbContext = _context.Empresas.Include(e => e.Reservas).Include(e => e.Veiculos).Include(e => e.Avaliacoes).Include(e => e.ApplicationUsers)
                        .OrderByDescending(e => e.Ativa);
                return View(await applicationDbContext.ToListAsync());
            }
        }

        // GET: Empresas/Details/5
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Empresas == null)
            {
                return NotFound();
            }

            var empresa = await _context.Empresas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empresa == null)
            {
                return NotFound();
            }

            return View(empresa);
        }

        // GET: Empresas/Create
        [Authorize(Roles = "Administrador")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Empresas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Create([Bind("Id,Nome,Ativa")] Empresa empresa)
        {
            ModelState.Remove(nameof(empresa.Veiculos));
            ModelState.Remove(nameof(empresa.Reservas));
            ModelState.Remove(nameof(empresa.ApplicationUsers));
            ModelState.Remove(nameof(empresa.Avaliacoes));
            ModelState.Remove(nameof(empresa.Lucro));
            if (ModelState.IsValid)
            {
                _context.Add(empresa);
                await _context.SaveChangesAsync();

                var defaultUser = new ApplicationUser
                {
                    UserName = "gestor" + empresa.Nome.ToUpper() + "@gmail.com",
                    Email = "gestor" + empresa.Nome.ToUpper() + "@gmail.com",
                    PrimeiroNome = "Gestor",
                    UltimoNome = empresa.Nome,
                    EmpresaId = empresa.Id,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };

                var user = await _userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await _userManager.CreateAsync(defaultUser, "Empresa...0");
                    await _userManager.AddToRoleAsync(defaultUser,
                    Roles.Gestor.ToString());
                }

                return RedirectToAction(nameof(Index));
            }
            return View(empresa);
        }

        // GET: Empresas/Edit/5
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Empresas == null)
            {
                return NotFound();
            }

            var empresa = await _context.Empresas.FindAsync(id);
            if (empresa == null)
            {
                return NotFound();
            }
            return View(empresa);
        }

        // POST: Empresas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Ativa")] Empresa empresa)
        {
            if (id != empresa.Id)
            {
                return NotFound();
            }

            ModelState.Remove(nameof(empresa.Veiculos));
            ModelState.Remove(nameof(empresa.Reservas));
            ModelState.Remove(nameof(empresa.ApplicationUsers));
            ModelState.Remove(nameof(empresa.Avaliacoes));
            ModelState.Remove(nameof(empresa.Lucro));
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(empresa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpresaExists(empresa.Id))
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
            return View(empresa);
        }

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Ativar(int? id)
        {
            if (id == null || _context.Empresas == null)
            {
                return NotFound();
            }

            var empresa = await _context.Empresas.FindAsync(id);
            if (empresa == null)
            {
                return NotFound();
            }

            foreach (var funcionario in _context.Users.Where(f => f.EmpresaId == id
            && f.Email.Equals("gestor" + empresa.Nome.ToUpper() + "@gmail.com")))
            {
                funcionario.EmailConfirmed = true;
                _context.Update(funcionario);
            }

            empresa.Ativa = true;
            _context.Update(empresa);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Desativar(int? id)
        {
            if (id == null || _context.Empresas == null)
            {
                return NotFound();
            }

            var empresa = await _context.Empresas.Include(e => e.ApplicationUsers).Include(e => e.Veiculos).Include(e => e.Reservas).FirstOrDefaultAsync(m => m.Id == id);
            if (empresa == null)
            {
                return NotFound();
            }

            foreach (var reserva in empresa.Reservas.ToList())
            {
                if (reserva.EstadoReserva == estadoReserva.Por_Confirmar || reserva.EstadoReserva == estadoReserva.Confirmada)
                {
                    reserva.EstadoReserva = estadoReserva.Cancelada;
                    _context.Update(reserva);
                }
            }

            foreach (var funcionario in empresa.ApplicationUsers.ToList())
            {
                funcionario.EmailConfirmed = false;
                _context.Update(funcionario);
            }

            foreach (var veiculo in empresa.Veiculos.ToList())
            {
                veiculo.Ativo = false;
                _context.Update(veiculo);
            }

            empresa.Ativa = false;
            _context.Update(empresa);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Empresas/Delete/5
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Empresas == null)
            {
                return NotFound();
            }

            if (_context.Veiculos.Where(v => v.EmpresaId == id).Count() != 0)
                ViewData["ApagaEmpresa"] = false;
            else
                ViewData["ApagaEmpresa"] = true;

            var empresa = await _context.Empresas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empresa == null)
            {
                return NotFound();
            }

            return View(empresa);
        }

        // POST: Empresas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            foreach (var funcionario in _context.Users.Where(f => f.EmpresaId == id).ToList())
            {
                _context.Users.Remove(funcionario);
                await _context.SaveChangesAsync();
            }

            foreach (var veiculo in _context.Veiculos.Where(v => v.EmpresaId == id).ToList())
            {
                _context.Veiculos.Remove(veiculo);
                await _context.SaveChangesAsync();
            }


            if (_context.Empresas == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Empresas'  is null.");
            }
            var empresa = await _context.Empresas.FindAsync(id);
            if (empresa != null)
            {
                _context.Empresas.Remove(empresa);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Administrador")]
        private bool EmpresaExists(int id)
        {
            return _context.Empresas.Any(e => e.Id == id);
        }
    }
}
