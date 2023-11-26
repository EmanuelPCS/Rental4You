using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
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
    public class LucrosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public LucrosController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Lucro
        [Authorize(Roles = "Gestor,Administrador")]
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Gestor"))
            {
                float valor7Dias = 0;
                float valor30Dias = 0;
                float media30Dias = 0;
                List<float> listaMedia30Dias = new List();

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return NotFound();
                }

                var empresa = await _context.Empresas.Include(e => e.Lucro).FirstOrDefaultAsync(m => m.Id == user.EmpresaId);

                foreach (var item in empresa.Lucro.ToList())
                {
                    if ((DateTime.Now - item.diaHora).TotalDays < 7)
                    {
                        valor7Dias += item.valor;
                    }

                    if ((DateTime.Now - item.diaHora).TotalDays < 30)
                    {
                        valor30Dias += item.valor;
                        listaMedia30Dias.Add(item.valor);
                    }
                }

                media30Dias = (float)Math.Round((listaMedia30Dias.Count() / 30.0), 2);

                LucroViewModel lucroViewModel = new LucroViewModel();

                lucroViewModel.Lucro7Dias = (float)Math.Round(valor7Dias,2);
                lucroViewModel.Lucro30Dias = (float)Math.Round(valor30Dias,2);
                lucroViewModel.MediaReservas30Dias = (float)Math.Round(media30Dias,2);

                //var applicationDbContext = _context.Lucro.Include(f => f.empresa);
                return View(lucroViewModel);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [Authorize(Roles = "Gestor")]
        // POST: Cursos/GraficoVendas/5
        public async Task<IActionResult> GetDadosReservas()
        {
            //dados de exemplo
            List<object> dados = new List<object>();

            DataTable dt = new DataTable();
            dt.Columns.Add("Dias", System.Type.GetType("System.String"));
            dt.Columns.Add("Reservas", System.Type.GetType("System.Int32"));
            DataRow dr = dt.NewRow();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var empresa = await _context.Empresas.Include(e => e.Reservas).FirstOrDefaultAsync(m => m.Id == user.EmpresaId);

            /*foreach (var item in empresa.Lucro.ToList())
            {
                if ((DateTime.Now - item.diaHora).TotalDays < 30)
                {
                    dr["Dias"] = "Dia " + item.diaHora.Day;
                    dr["Lucro"] = item.valor;
                    dt.Rows.Add(dr);
                    dr = dt.NewRow();
                }
            }*/

            //correr 30 vezes
            for (int i = 0; i < 30; i++)
            {
                var count = 0;

                dr["Dias"] = "Dia " + (DateTime.Now.AddDays(-i).Day);

                //correr todas as reservas
                foreach (var item in empresa.Reservas.Where(r => (DateTime.Now.AddDays(-i).Day) == r.DataDeReserva.Day).ToList())
                {
                    count++;
                }
                dr["Reservas"] = count;
                dt.Rows.Add(dr);
                dr = dt.NewRow();
            }

            /*foreach (var item in empresa.Reservas.ToList())
            {
                if ((DateTime.Now - item.DataDeLevantamento).TotalDays < 30)
                {
                    dr["Dias"] = "Dia " + item.DataDeLevantamento.Day;
                    dr["Reservas"] = item.Preco;
                    dt.Rows.Add(dr);
                    dr = dt.NewRow();
                }
            }*/


            foreach (DataColumn dc in dt.Columns)
            {
                List<object> x = new List<object>();
                x = (from DataRow drr in dt.Rows select drr[dc.ColumnName]).ToList();
                dados.Add(x);
            }
            return Json(dados);

        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        // POST: Cursos/GraficoVendas/5
        public async Task<IActionResult> GetDadosReservasAdmin()
        {
            //dados de exemplo
            List<object> dados = new List<object>();

            DataTable dt = new DataTable();
            dt.Columns.Add("Dias", System.Type.GetType("System.String"));
            dt.Columns.Add("Reservas", System.Type.GetType("System.Int32"));
            DataRow dr = dt.NewRow();

            /*foreach (var item in empresa.Lucro.ToList())
            {
                if ((DateTime.Now - item.diaHora).TotalDays < 30)
                {
                    dr["Dias"] = "Dia " + item.diaHora.Day;
                    dr["Lucro"] = item.valor;
                    dt.Rows.Add(dr);
                    dr = dt.NewRow();
                }
            }*/

            //correr 30 vezes
            for (int i = 0; i < 30; i++)
            {
                var count = 0;

                dr["Dias"] = "Dia " + (DateTime.Now.AddDays(-i).Day);

                //correr todas as reservas
                foreach (var item in _context.Reservas.Where(r => (DateTime.Now.AddDays(-i).Day) == r.DataDeReserva.Day).ToList())
                {
                    count++;
                }
                dr["Reservas"] = count;
                dt.Rows.Add(dr);
                dr = dt.NewRow();
            }

            /*foreach (var item in empresa.Reservas.ToList())
            {
                if ((DateTime.Now - item.DataDeLevantamento).TotalDays < 30)
                {
                    dr["Dias"] = "Dia " + item.DataDeLevantamento.Day;
                    dr["Reservas"] = item.Preco;
                    dt.Rows.Add(dr);
                    dr = dt.NewRow();
                }
            }*/


            foreach (DataColumn dc in dt.Columns)
            {
                List<object> x = new List<object>();
                x = (from DataRow drr in dt.Rows select drr[dc.ColumnName]).ToList();
                dados.Add(x);
            }
            return Json(dados);

        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        // POST: Cursos/GraficoVendas/5
        public async Task<IActionResult> GetDadosReservas12Admin()
        {
            //dados de exemplo
            List<object> dados = new List<object>();

            DataTable dt = new DataTable();
            dt.Columns.Add("Mes", System.Type.GetType("System.String"));
            dt.Columns.Add("Reservas", System.Type.GetType("System.Int32"));
            DataRow dr = dt.NewRow();

            //correr 30 vezes
            for (int i = 0; i < 12; i++)
            {
                var count = 0;

                dr["Mes"] = "Mês " + (DateTime.Now.AddMonths(-i).Month);

                //correr todas as reservas
                foreach (var item in _context.Reservas.Where(r => (DateTime.Now.AddMonths(-i).Month) == r.DataDeReserva.Month).ToList())
                {
                    count++;
                }
                dr["Reservas"] = count;
                dt.Rows.Add(dr);
                dr = dt.NewRow();
            }

            foreach (DataColumn dc in dt.Columns)
            {
                List<object> x = new List<object>();
                x = (from DataRow drr in dt.Rows select drr[dc.ColumnName]).ToList();
                dados.Add(x);
            }
            return Json(dados);

        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        // POST: Cursos/GraficoVendas/5
        public async Task<IActionResult> GetDadosClientes12Admin()
        {
            //dados de exemplo
            List<object> dados = new List<object>();

            DataTable dt = new DataTable();
            dt.Columns.Add("Mes", System.Type.GetType("System.String"));
            dt.Columns.Add("Clientes", System.Type.GetType("System.Int32"));
            DataRow dr = dt.NewRow();

            var clientes = await _userManager.GetUsersInRoleAsync("Cliente");

            //correr 12 vezes
            for (int i = 0; i < 12; i++)
            {
                var count = 0;

                dr["Mes"] = "Mês " + (DateTime.Now.AddMonths(-i).Month);

                //correr todas as reservas
                foreach (var item in clientes.Where(u => (DateTime.Now.AddMonths(-i).Month) == u.DataDeRegisto.Month).ToList())
                {

                    count++;
                }
                dr["Clientes"] = count;
                dt.Rows.Add(dr);
                dr = dt.NewRow();
            }

            foreach (DataColumn dc in dt.Columns)
            {
                List<object> x = new List<object>();
                x = (from DataRow drr in dt.Rows select drr[dc.ColumnName]).ToList();
                dados.Add(x);
            }
            return Json(dados);

        }

        // GET: Lucros/Details/5
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Lucro == null)
            {
                return NotFound();
            }

            var lucro = await _context.Lucro
                .Include(l => l.empresa)
                .Include(l => l.reserva)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lucro == null)
            {
                return NotFound();
            }

            return View(lucro);
        }

        // GET: Lucros/Create
        [Authorize(Roles = "Administrador")]
        public IActionResult Create()
        {
            ViewData["EmpresaId"] = new SelectList(_context.Empresas, "Id", "Id");
            ViewData["ReservaId"] = new SelectList(_context.Reservas, "Id", "Id");
            return View();
        }

        // POST: Lucros/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Create([Bind("Id,valor,ReservaId,EmpresaId,diaHora")] Lucro lucro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lucro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmpresaId"] = new SelectList(_context.Empresas, "Id", "Id", lucro.EmpresaId);
            ViewData["ReservaId"] = new SelectList(_context.Reservas, "Id", "Id", lucro.ReservaId);
            return View(lucro);
        }

        // GET: Lucros/Edit/5
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Lucro == null)
            {
                return NotFound();
            }

            var lucro = await _context.Lucro.FindAsync(id);
            if (lucro == null)
            {
                return NotFound();
            }
            ViewData["EmpresaId"] = new SelectList(_context.Empresas, "Id", "Id", lucro.EmpresaId);
            ViewData["ReservaId"] = new SelectList(_context.Reservas, "Id", "Id", lucro.ReservaId);
            return View(lucro);
        }

        // POST: Lucros/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,valor,ReservaId,EmpresaId,diaHora")] Lucro lucro)
        {
            if (id != lucro.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lucro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LucroExists(lucro.Id))
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
            ViewData["EmpresaId"] = new SelectList(_context.Empresas, "Id", "Id", lucro.EmpresaId);
            ViewData["ReservaId"] = new SelectList(_context.Reservas, "Id", "Id", lucro.ReservaId);
            return View(lucro);
        }

        // GET: Lucros/Delete/5
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Lucro == null)
            {
                return NotFound();
            }

            var lucro = await _context.Lucro
                .Include(l => l.empresa)
                .Include(l => l.reserva)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lucro == null)
            {
                return NotFound();
            }

            return View(lucro);
        }

        // POST: Lucros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Lucro == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Lucro'  is null.");
            }
            var lucro = await _context.Lucro.FindAsync(id);
            if (lucro != null)
            {
                _context.Lucro.Remove(lucro);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LucroExists(int id)
        {
          return _context.Lucro.Any(e => e.Id == id);
        }
    }

    internal class List : List<float>
    {
    }
}
