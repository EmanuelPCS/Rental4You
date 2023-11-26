using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TPRental4You.Data;
using TPRental4You.Models;
using TPRental4You.Models.ViewModels;

namespace TPRental4You.Controllers
{
    public class ReservasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReservasController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Reservas
        [Authorize(Roles = "Cliente,Funcionario, Gestor")]
        public async Task<IActionResult> Index([Bind("EstadoReserva,TiposDeVeiculoId,VeiculoId,ClienteId,DataDeLevantamento,DataDeEntrega")]
            PesquisaReservaViewModel? pesquisaReserva)
        {

            ViewData["TiposDeVeiculoId"] = new SelectList(_context.TiposDeVeiculos.ToList(), "Id", "Nome");
            ViewData["VeiculoId"] = new SelectList(_context.Veiculos.ToList(), "Id", "Matricula");
            var clientes = await _userManager.GetUsersInRoleAsync("Cliente");
            ViewData["ClienteId"] = new SelectList(clientes.ToList(), "Id", "Email");



            if (User.IsInRole("Administrador"))
            {
                var applicationDbContext = _context.Reservas.Include(r => r.cliente).Include(r => r.veiculo).Include(r => r.empresa).Include(r => r.estados);
                return View(await applicationDbContext.ToListAsync());
            }
            else if (User.IsInRole("Funcionario") || User.IsInRole("Gestor"))
            {
                var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var userFuncionario = await _context.Users.Include(u => u.Empresa).Include(u => u.Reservas)
                    .FirstOrDefaultAsync(u => u.Id == id);

                if (pesquisaReserva.EstadoReserva != 0 && pesquisaReserva.TiposDeVeiculoId != 0 && pesquisaReserva.VeiculoId != 0
                    && pesquisaReserva.ClienteId != null && !pesquisaReserva.ClienteId.Equals("Cliente")
                    && !pesquisaReserva.DataDeLevantamento.ToString().Equals("01/01/0001 00:00:00") && !pesquisaReserva.DataDeEntrega.ToString().Equals("01/01/0001 00:00:00"))
                {

                    if (pesquisaReserva.EstadoReserva == 1)
                    {
                        var applicationDbContext = _context.Reservas.Include(r => r.cliente).Include(r => r.veiculo).Include(r => r.empresa).Include(r => r.estados).
                        Where(r => r.EmpresaId == userFuncionario.EmpresaId
                        && ((int?)r.EstadoReserva) == 0
                        && r.veiculo.TiposDeVeiculoId == pesquisaReserva.TiposDeVeiculoId
                        && r.VeiculoId == pesquisaReserva.VeiculoId
                        && r.ClienteId.Equals(pesquisaReserva.ClienteId)
                        && r.DataDeLevantamento.Day.Equals(pesquisaReserva.DataDeLevantamento.Day)
                        && r.DataDeEntrega.Day.Equals(pesquisaReserva.DataDeEntrega.Day));

                        return View(await applicationDbContext.ToListAsync());
                    }
                    else if (pesquisaReserva.EstadoReserva == 2)
                    {
                        var applicationDbContext = _context.Reservas.Include(r => r.cliente).Include(r => r.veiculo).Include(r => r.empresa).Include(r => r.estados).
                        Where(r => r.EmpresaId == userFuncionario.EmpresaId
                        && ((int?)r.EstadoReserva) == 1
                        && r.veiculo.TiposDeVeiculoId == pesquisaReserva.TiposDeVeiculoId
                        && r.VeiculoId == pesquisaReserva.VeiculoId
                        && r.ClienteId.Equals(pesquisaReserva.ClienteId)
                        && r.DataDeLevantamento.Day.Equals(pesquisaReserva.DataDeLevantamento.Day)
                        && r.DataDeEntrega.Day.Equals(pesquisaReserva.DataDeEntrega.Day));

                        return View(await applicationDbContext.ToListAsync());
                    }
                    else if (pesquisaReserva.EstadoReserva == 3)
                    {
                        var applicationDbContext = _context.Reservas.Include(r => r.cliente).Include(r => r.veiculo).Include(r => r.empresa).Include(r => r.estados).
                        Where(r => r.EmpresaId == userFuncionario.EmpresaId
                        && ((int?)r.EstadoReserva) == 2
                        && r.veiculo.TiposDeVeiculoId == pesquisaReserva.TiposDeVeiculoId
                        && r.VeiculoId == pesquisaReserva.VeiculoId
                        && r.ClienteId.Equals(pesquisaReserva.ClienteId)
                        && r.DataDeLevantamento.Day.Equals(pesquisaReserva.DataDeLevantamento.Day)
                        && r.DataDeEntrega.Day.Equals(pesquisaReserva.DataDeEntrega.Day));

                        return View(await applicationDbContext.ToListAsync());
                    }
                    else
                    {
                        var applicationDbContext = _context.Reservas.Include(r => r.cliente).Include(r => r.veiculo).Include(r => r.empresa).Include(r => r.estados).
                        Where(r => r.EmpresaId == userFuncionario.EmpresaId
                        && ((int?)r.EstadoReserva) == 3
                        && r.veiculo.TiposDeVeiculoId == pesquisaReserva.TiposDeVeiculoId
                        && r.VeiculoId == pesquisaReserva.VeiculoId
                        && r.ClienteId.Equals(pesquisaReserva.ClienteId)
                        && r.DataDeLevantamento.Day.Equals(pesquisaReserva.DataDeLevantamento.Day)
                        && r.DataDeEntrega.Day.Equals(pesquisaReserva.DataDeEntrega.Day));

                        return View(await applicationDbContext.ToListAsync());
                    }
                }
                else if (pesquisaReserva.EstadoReserva != 0)
                {
                    if (pesquisaReserva.EstadoReserva == 1)
                    {
                        var applicationDbContext = _context.Reservas.Include(r => r.cliente).Include(r => r.veiculo).Include(r => r.empresa).Include(r => r.estados).
                        Where(r => r.EmpresaId == userFuncionario.EmpresaId
                        && ((int?)r.EstadoReserva) == 0);

                        return View(await applicationDbContext.ToListAsync());
                    }
                    else if (pesquisaReserva.EstadoReserva == 2)
                    {
                        var applicationDbContext = _context.Reservas.Include(r => r.cliente).Include(r => r.veiculo).Include(r => r.empresa).Include(r => r.estados).
                        Where(r => r.EmpresaId == userFuncionario.EmpresaId
                        && ((int?)r.EstadoReserva) == 1);

                        return View(await applicationDbContext.ToListAsync());
                    }
                    else if (pesquisaReserva.EstadoReserva == 3)
                    {
                        var applicationDbContext = _context.Reservas.Include(r => r.cliente).Include(r => r.veiculo).Include(r => r.empresa).Include(r => r.estados).
                        Where(r => r.EmpresaId == userFuncionario.EmpresaId
                        && ((int?)r.EstadoReserva) == 2);

                        return View(await applicationDbContext.ToListAsync());
                    }
                    else
                    {
                        var applicationDbContext = _context.Reservas.Include(r => r.cliente).Include(r => r.veiculo).Include(r => r.empresa).Include(r => r.estados).
                        Where(r => r.EmpresaId == userFuncionario.EmpresaId
                        && ((int?)r.EstadoReserva) == 3);

                        return View(await applicationDbContext.ToListAsync());
                    }
                }
                else if (pesquisaReserva.TiposDeVeiculoId != 0)
                {
                    var applicationDbContext = _context.Reservas.Include(r => r.cliente).Include(r => r.veiculo).Include(r => r.empresa).Include(r => r.estados).
                    Where(r => r.EmpresaId == userFuncionario.EmpresaId
                    && r.veiculo.TiposDeVeiculoId == pesquisaReserva.TiposDeVeiculoId);

                    return View(await applicationDbContext.ToListAsync());

                }
                else if (pesquisaReserva.VeiculoId != 0)
                {
                    var applicationDbContext = _context.Reservas.Include(r => r.cliente).Include(r => r.veiculo).Include(r => r.empresa).Include(r => r.estados).
                    Where(r => r.EmpresaId == userFuncionario.EmpresaId
                    && r.VeiculoId == pesquisaReserva.VeiculoId);

                    return View(await applicationDbContext.ToListAsync());

                }
                else if (pesquisaReserva.ClienteId != null && !pesquisaReserva.ClienteId.Equals("Cliente"))
                {
                    var applicationDbContext = _context.Reservas.Include(r => r.cliente).Include(r => r.veiculo).Include(r => r.empresa).Include(r => r.estados).
                    Where(r => r.EmpresaId == userFuncionario.EmpresaId
                    && r.ClienteId.Equals(pesquisaReserva.ClienteId));

                    return View(await applicationDbContext.ToListAsync());

                }
                else if (!pesquisaReserva.DataDeLevantamento.ToString().Equals("01/01/0001 00:00:00"))
                {
                    var applicationDbContext = _context.Reservas.Include(r => r.cliente).Include(r => r.veiculo).Include(r => r.empresa).Include(r => r.estados).
                    Where(r => r.EmpresaId == userFuncionario.EmpresaId
                    && r.DataDeLevantamento.Day.Equals(pesquisaReserva.DataDeLevantamento.Day));

                    return View(await applicationDbContext.ToListAsync());

                }
                else if (!pesquisaReserva.DataDeEntrega.ToString().Equals("01/01/0001 00:00:00"))
                {
                    var applicationDbContext = _context.Reservas.Include(r => r.cliente).Include(r => r.veiculo).Include(r => r.empresa).Include(r => r.estados).
                    Where(r => r.EmpresaId == userFuncionario.EmpresaId
                    && r.DataDeEntrega.Day.Equals(pesquisaReserva.DataDeEntrega.Day));

                    return View(await applicationDbContext.ToListAsync());

                }
                else
                {
                    var applicationDbContext = _context.Reservas.Include(r => r.cliente).Include(r => r.veiculo).Include(r => r.empresa).Include(r => r.estados).
                Where(r => r.EmpresaId == userFuncionario.EmpresaId);
                    return View(await applicationDbContext.ToListAsync());
                }

            }
            else
            {
                var applicationDbContext = _context.Reservas.Include(r => r.cliente).Include(r => r.veiculo).Include(r => r.empresa).Include(r => r.estados).
                Where(r => r.ClienteId == User.FindFirstValue(ClaimTypes.NameIdentifier));
                return View(await applicationDbContext.ToListAsync());
            }

        }

        // GET: Reservas/Details/5
        [Authorize(Roles = "Cliente,Funcionario, Gestor")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Reservas == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas
                .Include(r => r.cliente)
                .Include(r => r.veiculo)
                .Include(r => r.empresa)
                .Include(r => r.estados)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reserva == null)
            {
                return NotFound();
            }

            var reservaPath = Path.Combine(Directory.GetCurrentDirectory(), ("wwwroot/imagens/danosReserva/" + id.ToString()));
            if (!Directory.Exists(reservaPath))
                Directory.CreateDirectory(reservaPath);
            //LINK SYNTAX
            var files = from file in
                            Directory.EnumerateFiles(reservaPath)
                        select string.Format(
                            "/imagens/danosReserva//{0}/{1}",
                            id,
                            Path.GetFileName(file));

            if(files.Any())
                ViewData["Ficheiros"] = files; //lista de strings para a vista
            else
                ViewData["Ficheiros"] = null;

            return View(reserva);
        }

        // GET: Reservas/Create
        [Authorize(Roles = "Cliente")]
        public IActionResult Create(int id, string dataPesquisaInicial, string dataPesquisaFinal)
        {
            //int? id
            var veiculo = _context.Veiculos.Where(u => u.Id == id).FirstOrDefault();

            if (veiculo == null)
                return RedirectToAction(nameof(Index));

            double NrHoras = 0;
            DateTime final = Convert.ToDateTime(dataPesquisaFinal);
            DateTime inicial = Convert.ToDateTime(dataPesquisaInicial);
            NrHoras = (final - inicial).TotalHours;

            Reserva r = new Reserva();
            //r.Confirmada = "espera";
            r.VeiculoId = id;
            r.ClienteId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            r.EmpresaId = veiculo.EmpresaId;
            r.DataDeLevantamento = inicial;
            r.DataDeEntrega = final;
            r.DataDeReserva = DateTime.Now;
            r.Preco =  (float)Math.Round(veiculo.Preco * (float)NrHoras,1);

            return View(r);
        }

        // POST: Reservas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Cliente")]
        public async Task<IActionResult> Create([Bind("EstadoReserva,DataDeLevantamento,DataDeEntrega,DataDeReserva,ClienteId,VeiculoId,EmpresaId,Preco,Situacao")] Reserva reserva)
        {
            ModelState.Remove(nameof(reserva.cliente));
            ModelState.Remove(nameof(reserva.veiculo));
            ModelState.Remove(nameof(reserva.estados));
            ModelState.Remove(nameof(reserva.empresa));
            if (ModelState.IsValid)
            {
                _context.Add(reserva);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["ClienteId"] = new SelectList(_context.Users, "Id", "Id", reserva.ClienteId);
            //ViewData["VeiculoId"] = new SelectList(_context.Veiculos, "Id", "Id", reserva.VeiculoId);
            return View(reserva);
        }

        [Authorize(Roles = "Cliente")]
        public async Task<IActionResult> Pagar(int? id)
        {
            if (id == null || _context.Reservas == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Users, "Id", "Email", reserva.ClienteId);
            ViewData["VeiculoId"] = new SelectList(_context.Veiculos, "Id", "Nome", reserva.VeiculoId);
            ViewData["EmpresaId"] = new SelectList(_context.Empresas, "Id", "Nome", reserva.EmpresaId);

            return View(reserva);
        }

        [Authorize(Roles = "Cliente")]
        public async Task<IActionResult> Devolver(int? id)
        {
            if (id == null || _context.Reservas == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Users, "Id", "Email", reserva.ClienteId);
            ViewData["VeiculoId"] = new SelectList(_context.Veiculos, "Id", "Nome", reserva.VeiculoId);
            ViewData["EmpresaId"] = new SelectList(_context.Empresas, "Id", "Nome", reserva.EmpresaId);

            return View(reserva);
        }

        [Authorize(Roles = "Cliente")]
        public async Task<IActionResult> Devolucao(int? id)
        {
            if (id == null || _context.Reservas == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Users, "Id", "Email", reserva.ClienteId);
            ViewData["VeiculoId"] = new SelectList(_context.Veiculos, "Id", "Nome", reserva.VeiculoId);
            ViewData["EmpresaId"] = new SelectList(_context.Empresas, "Id", "Nome", reserva.EmpresaId);

            return View(reserva);
        }

        // GET: Reservas/Edit/5
        [Authorize(Roles = "Cliente,Funcionario,Gestor")]
        public async Task<IActionResult> Edit(int? id, int? confirmar)
        {
            if (id == null || _context.Reservas == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas.FindAsync(id);
            if (confirmar == 2)
            {
                reserva.EstadoReserva = estadoReserva.Confirmada;
            } else if(confirmar == 1)
            {
                reserva.EstadoReserva = estadoReserva.Rejeitada;
            }
            if (reserva == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Users, "Id", "Email", reserva.ClienteId);
            ViewData["VeiculoId"] = new SelectList(_context.Veiculos, "Id", "Nome", reserva.VeiculoId);
            ViewData["EmpresaId"] = new SelectList(_context.Empresas, "Id", "Nome", reserva.EmpresaId);
            return View(reserva);
        }

        // POST: Reservas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Cliente,Funcionario,Gestor")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EstadoReserva,DataDeLevantamento,DataDeEntrega,DataDeReserva,ClienteId,VeiculoId,EmpresaId,Preco,Situacao,Classificacao")] Reserva reserva)
        {
            if (id != reserva.Id)
            {
                return NotFound();
            }

            ModelState.Remove(nameof(reserva.cliente));
            ModelState.Remove(nameof(reserva.veiculo));
            ModelState.Remove(nameof(reserva.estados));
            ModelState.Remove(nameof(reserva.empresa));
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reserva);

                    if (((int)reserva.Situacao.Value) == 1)
                    {
                        Lucro lucro = new Lucro();
                        lucro.valor = reserva.Preco;
                        lucro.ReservaId = reserva.Id;
                        lucro.EmpresaId = reserva.EmpresaId;
                        lucro.diaHora = DateTime.Now;

                        _context.Add(lucro);

                        /*reserva.Lucro = lucro;
                        _context.Update(reserva);*/
                    }
                    else if (((int)reserva.Situacao.Value) == 3)
                    {
                        var lucro = await _context.Lucro.FirstOrDefaultAsync(m => m.ReservaId == reserva.Id);
                        _context.Remove(lucro);
                    }

                    if (reserva.Classificacao != null)
                    {
                        Avaliacao avaliacao = new Avaliacao();
                        avaliacao.classificacao = (float)reserva.Classificacao;
                        avaliacao.EmpresaId = reserva.EmpresaId;
                        avaliacao.ReservaId = reserva.Id;
                        avaliacao.ClienteId = reserva.ClienteId;

                        _context.Add(avaliacao);

                        var empresa = _context.Empresas.Include(e => e.Avaliacoes).Where(c => c.Id == reserva.EmpresaId).FirstOrDefault();

                        float media = 0;
                        foreach (var item in empresa.Avaliacoes.ToList())
                        {
                            media += item.classificacao;
                        }
                        media = (float)Math.Round(media / empresa.Avaliacoes.Count(), 1);

                        empresa.Avaliacao = media;
                        _context.Update(empresa);
                    }

                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservaExists(reserva.Id))
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
            ViewData["ClienteId"] = new SelectList(_context.Users, "Id", "Id", reserva.ClienteId);
            ViewData["VeiculoId"] = new SelectList(_context.Veiculos, "Id", "Id", reserva.VeiculoId);
            ViewData["EmpresaId"] = new SelectList(_context.Empresas, "Id", "Nome", reserva.EmpresaId);
            return View(reserva);
        }

        // GET: Reservas/Delete/5
        [Authorize(Roles = "Funcionario,Gestor")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Reservas == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas
                .Include(r => r.cliente)
                .Include(r => r.veiculo)
                .Include(r => r.empresa)
                .Include(r => r.estados)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // POST: Reservas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Funcionario,Gestor")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Reservas == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Reservas'  is null.");
            }
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva != null)
            {
                _context.Reservas.Remove(reserva);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Funcionario,Gestor")]
        private bool ReservaExists(int id)
        {
            return _context.Reservas.Any(e => e.Id == id);
        }
    }
}
