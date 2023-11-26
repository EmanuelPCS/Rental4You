using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using TPRental4You.Data;
using TPRental4You.Migrations;
using TPRental4You.Models;
using TPRental4You.Models.ViewModels;

namespace TPRental4You.Controllers
{
    public class VeiculosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VeiculosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Veiculos
        [Authorize(Roles = "Funcionario, Gestor")]
        public async Task<IActionResult> Index([Bind("TiposDeVeiculoId,Ativo,ordenarTipo,ordenarEstado")]
            PesquisaVeiculoViewModel? pesquisaVeiculo)
        {
            ViewData["TiposDeVeiculoId"] = new SelectList(_context.TiposDeVeiculos.ToList(), "Id", "Nome");

            bool estado;
            if (pesquisaVeiculo.Ativo == 1)
                estado = true;
            else /*if(pesquisaVeiculo.Ativo == 2)*/
                estado = false;

            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userFuncionario = await _context.Users.Include(u => u.Empresa).Include(u => u.Reservas)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (pesquisaVeiculo.TiposDeVeiculoId != 0 && pesquisaVeiculo.Ativo != 0)
            {

                if (pesquisaVeiculo.ordenarTipo != 0)
                {
                    if (pesquisaVeiculo.ordenarTipo == 1)
                    {
                        var applicationDbContext = _context.Veiculos.Include(v => v.empresa).Include(v => v.localizacao).Include(v => v.tiposDeVeiculo).Include(v => v.Reservas)
                        .Where(v => v.EmpresaId == userFuncionario.EmpresaId
                        && v.TiposDeVeiculoId == pesquisaVeiculo.TiposDeVeiculoId
                        && v.Ativo == estado).OrderBy(v => v.TiposDeVeiculoId);
                        return View(await applicationDbContext.ToListAsync());

                    }
                    else
                    {
                        var applicationDbContext = _context.Veiculos.Include(v => v.empresa).Include(v => v.localizacao).Include(v => v.tiposDeVeiculo).Include(v => v.Reservas)
                        .Where(v => v.EmpresaId == userFuncionario.EmpresaId
                        && v.TiposDeVeiculoId == pesquisaVeiculo.TiposDeVeiculoId
                        && v.Ativo == estado).OrderByDescending(v => v.TiposDeVeiculoId);
                        return View(await applicationDbContext.ToListAsync());
                    }
                }
                else if (pesquisaVeiculo.ordenarEstado != 0)
                {
                    if (pesquisaVeiculo.ordenarEstado == 3)
                    {
                        var applicationDbContext = _context.Veiculos.Include(v => v.empresa).Include(v => v.localizacao).Include(v => v.tiposDeVeiculo).Include(v => v.Reservas)
                        .Where(v => v.EmpresaId == userFuncionario.EmpresaId
                        && v.TiposDeVeiculoId == pesquisaVeiculo.TiposDeVeiculoId
                        && v.Ativo == estado).OrderBy(v => v.Ativo);
                        return View(await applicationDbContext.ToListAsync());

                    }
                    else
                    {
                        var applicationDbContext = _context.Veiculos.Include(v => v.empresa).Include(v => v.localizacao).Include(v => v.tiposDeVeiculo).Include(v => v.Reservas)
                        .Where(v => v.EmpresaId == userFuncionario.EmpresaId
                        && v.TiposDeVeiculoId == pesquisaVeiculo.TiposDeVeiculoId
                        && v.Ativo == estado).OrderByDescending(v => v.Ativo);
                        return View(await applicationDbContext.ToListAsync());

                    }
                }
                else
                {

                    var applicationDbContext = _context.Veiculos.Include(v => v.empresa).Include(v => v.localizacao).Include(v => v.tiposDeVeiculo).Include(v => v.Reservas)
                        .Where(v => v.EmpresaId == userFuncionario.EmpresaId
                        && v.TiposDeVeiculoId == pesquisaVeiculo.TiposDeVeiculoId
                        && v.Ativo == estado);
                    return View(await applicationDbContext.ToListAsync());
                }

            }
            else if (pesquisaVeiculo.TiposDeVeiculoId != 0)
            {
                if (pesquisaVeiculo.ordenarTipo != 0)
                {
                    if (pesquisaVeiculo.ordenarTipo == 1)
                    {
                        var applicationDbContext = _context.Veiculos.Include(v => v.empresa).Include(v => v.localizacao).Include(v => v.tiposDeVeiculo).Include(v => v.Reservas)
                        .Where(v => v.EmpresaId == userFuncionario.EmpresaId
                        && v.TiposDeVeiculoId == pesquisaVeiculo.TiposDeVeiculoId)
                        .OrderBy(v => v.TiposDeVeiculoId);
                        return View(await applicationDbContext.ToListAsync());

                    }
                    else
                    {
                        var applicationDbContext = _context.Veiculos.Include(v => v.empresa).Include(v => v.localizacao).Include(v => v.tiposDeVeiculo).Include(v => v.Reservas)
                        .Where(v => v.EmpresaId == userFuncionario.EmpresaId
                        && v.TiposDeVeiculoId == pesquisaVeiculo.TiposDeVeiculoId)
                        .OrderByDescending(v => v.TiposDeVeiculoId);
                        return View(await applicationDbContext.ToListAsync());
                    }
                }
                else if (pesquisaVeiculo.ordenarEstado != 0)
                {
                    if (pesquisaVeiculo.ordenarEstado == 3)
                    {
                        var applicationDbContext = _context.Veiculos.Include(v => v.empresa).Include(v => v.localizacao).Include(v => v.tiposDeVeiculo).Include(v => v.Reservas)
                        .Where(v => v.EmpresaId == userFuncionario.EmpresaId
                        && v.TiposDeVeiculoId == pesquisaVeiculo.TiposDeVeiculoId)
                        .OrderBy(v => v.Ativo);
                        return View(await applicationDbContext.ToListAsync());

                    }
                    else
                    {
                        var applicationDbContext = _context.Veiculos.Include(v => v.empresa).Include(v => v.localizacao).Include(v => v.tiposDeVeiculo).Include(v => v.Reservas)
                        .Where(v => v.EmpresaId == userFuncionario.EmpresaId
                        && v.TiposDeVeiculoId == pesquisaVeiculo.TiposDeVeiculoId)
                        .OrderByDescending(v => v.Ativo);
                        return View(await applicationDbContext.ToListAsync());

                    }
                }
                else
                {

                    var applicationDbContext = _context.Veiculos.Include(v => v.empresa).Include(v => v.localizacao).Include(v => v.tiposDeVeiculo).Include(v => v.Reservas)
                        .Where(v => v.EmpresaId == userFuncionario.EmpresaId
                        && v.TiposDeVeiculoId == pesquisaVeiculo.TiposDeVeiculoId);
                    return View(await applicationDbContext.ToListAsync());
                }

            }
            else if (pesquisaVeiculo.Ativo != 0)
            {

                if (pesquisaVeiculo.ordenarTipo != 0)
                {
                    if (pesquisaVeiculo.ordenarTipo == 1)
                    {
                        var applicationDbContext = _context.Veiculos.Include(v => v.empresa).Include(v => v.localizacao).Include(v => v.tiposDeVeiculo).Include(v => v.Reservas)
                        .Where(v => v.EmpresaId == userFuncionario.EmpresaId
                        && v.Ativo == estado)
                        .OrderBy(v => v.TiposDeVeiculoId);
                        return View(await applicationDbContext.ToListAsync());

                    }
                    else
                    {
                        var applicationDbContext = _context.Veiculos.Include(v => v.empresa).Include(v => v.localizacao).Include(v => v.tiposDeVeiculo).Include(v => v.Reservas)
                        .Where(v => v.EmpresaId == userFuncionario.EmpresaId
                         && v.Ativo == estado)
                        .OrderByDescending(v => v.TiposDeVeiculoId);
                        return View(await applicationDbContext.ToListAsync());
                    }
                }
                else if (pesquisaVeiculo.ordenarEstado != 0)
                {
                    if (pesquisaVeiculo.ordenarEstado == 3)
                    {
                        var applicationDbContext = _context.Veiculos.Include(v => v.empresa).Include(v => v.localizacao).Include(v => v.tiposDeVeiculo).Include(v => v.Reservas)
                        .Where(v => v.EmpresaId == userFuncionario.EmpresaId
                         && v.Ativo == estado)
                        .OrderBy(v => v.Ativo);
                        return View(await applicationDbContext.ToListAsync());

                    }
                    else
                    {
                        var applicationDbContext = _context.Veiculos.Include(v => v.empresa).Include(v => v.localizacao).Include(v => v.tiposDeVeiculo).Include(v => v.Reservas)
                        .Where(v => v.EmpresaId == userFuncionario.EmpresaId
                         && v.Ativo == estado)
                        .OrderByDescending(v => v.Ativo);
                        return View(await applicationDbContext.ToListAsync());

                    }
                }
                else
                {
                    var applicationDbContext = _context.Veiculos.Include(v => v.empresa).Include(v => v.localizacao).Include(v => v.tiposDeVeiculo).Include(v => v.Reservas)
                        .Where(v => v.EmpresaId == userFuncionario.EmpresaId
                        && v.Ativo == estado);
                    return View(await applicationDbContext.ToListAsync());
                }

            }
            else if (pesquisaVeiculo.ordenarTipo == 1)
            {
                var applicationDbContext = _context.Veiculos.Include(v => v.empresa).Include(v => v.localizacao).Include(v => v.tiposDeVeiculo).Include(v => v.Reservas)
                    .Where(v => v.EmpresaId == userFuncionario.EmpresaId)
                    .OrderBy(v => v.TiposDeVeiculoId);
                return View(await applicationDbContext.ToListAsync());
            }
            else if (pesquisaVeiculo.ordenarTipo == 2)
            {
                var applicationDbContext = _context.Veiculos.Include(v => v.empresa).Include(v => v.localizacao).Include(v => v.tiposDeVeiculo).Include(v => v.Reservas)
                    .Where(v => v.EmpresaId == userFuncionario.EmpresaId)
                    .OrderByDescending(v => v.TiposDeVeiculoId);
                return View(await applicationDbContext.ToListAsync());
            }
            else if (pesquisaVeiculo.ordenarEstado == 3)
            {
                var applicationDbContext = _context.Veiculos.Include(v => v.empresa).Include(v => v.localizacao).Include(v => v.tiposDeVeiculo).Include(v => v.Reservas)
                    .Where(v => v.EmpresaId == userFuncionario.EmpresaId)
                    .OrderBy(v => v.Ativo);
                return View(await applicationDbContext.ToListAsync());
            }
            else /*if(pesquisaVeiculo.ordenarEstado == 4)*/
            {
                var applicationDbContext = _context.Veiculos.Include(v => v.empresa).Include(v => v.localizacao).Include(v => v.tiposDeVeiculo).Include(v => v.Reservas)
                    .Where(v => v.EmpresaId == userFuncionario.EmpresaId)
                    .OrderByDescending(v => v.Ativo);
                return View(await applicationDbContext.ToListAsync());
            }

            //var applicationDbContext = _context.Veiculos.Include(v => v.empresa).Include(v => v.localizacao).Include(v => v.tiposDeVeiculo);

        }

        // GET: Veiculos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Veiculos == null)
            {
                return NotFound();
            }

            var veiculo = await _context.Veiculos
                .Include(v => v.empresa)
                .Include(v => v.localizacao)
                .Include(v => v.tiposDeVeiculo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (veiculo == null)
            {
                return NotFound();
            }

            return View(veiculo);
        }

        [HttpPost]
        public async Task<IActionResult> Search([Bind("DataPesquisaInicial,DataPesquisaFinal,TiposDeVeiculoId,LocalizacaoId,EmpresaId,ordenarPreco,ordenarClassificacao")]
            PesquisaVeiculoViewModel pesquisaVeiculo)
        {
            //quando insere localizacao
            if (pesquisaVeiculo.LocalizacaoId != 0)
            {
                ViewData["LocalizacaoId"] = pesquisaVeiculo.LocalizacaoId;
                ViewData["pesquisaVeiculo"] = pesquisaVeiculo;
            }
            else
            {
                //pesquisaVeiculo.LocalizacaoId = ViewBag.LocalizacaoId;
                //ViewData["pesquisaVeiculo"] = pesquisaVeiculo;
                //pesquisaVeiculo = ViewBag.pesquisaVeiculo;
                //pesquisaVeiculo.LocalizacaoId = ViewBag.LocalizacaoId;
            }

            ViewData["TiposDeVeiculoId"] = new SelectList(_context.TiposDeVeiculos.ToList(), "Id", "Nome");
            ViewData["VeiculoTipo"] = pesquisaVeiculo.TiposDeVeiculoId;
            ViewData["EmpresaId"] = new SelectList(_context.Empresas.ToList(), "Id", "Nome");


            //se nao selecionar empresa -> inicio

            //caso checkbox do preço e da classificacao estejam selecionada
            /*if (pesquisaVeiculo.ordenarPreco != 0 && pesquisaVeiculo.ordenarClassificacao != 0)
            {*/


            //selecionamos tipo e empresa
            if (pesquisaVeiculo.TiposDeVeiculoId != 0 && pesquisaVeiculo.EmpresaId != 0)
            {

                //vamos ver o resultado do preco e classificacao
                switch (pesquisaVeiculo.ordenarPreco)
                {
                    case 0:
                        {
                            if (pesquisaVeiculo.ordenarClassificacao == 3)
                            {
                                pesquisaVeiculo.ListaDeVeiculos = await _context.Veiculos.Include("tiposDeVeiculo").Include("localizacao").Include("empresa").
                            Where(c => c.TiposDeVeiculoId == pesquisaVeiculo.TiposDeVeiculoId
                            && c.EmpresaId == pesquisaVeiculo.EmpresaId
                            && c.LocalizacaoId == pesquisaVeiculo.LocalizacaoId
                            && (c.DataInicial <= pesquisaVeiculo.DataPesquisaInicial && c.DataFim >= pesquisaVeiculo.DataPesquisaFinal && pesquisaVeiculo.DataPesquisaInicial <= pesquisaVeiculo.DataPesquisaFinal)
                            && c.Ativo).OrderBy(c => c.empresa.Avaliacao).ToListAsync();

                            }
                            else if (pesquisaVeiculo.ordenarClassificacao == 4)
                            {
                                pesquisaVeiculo.ListaDeVeiculos = await _context.Veiculos.Include("tiposDeVeiculo").Include("localizacao").Include("empresa").
                           Where(c => c.TiposDeVeiculoId == pesquisaVeiculo.TiposDeVeiculoId
                            && c.EmpresaId == pesquisaVeiculo.EmpresaId
                            && c.LocalizacaoId == pesquisaVeiculo.LocalizacaoId
                           && (c.DataInicial <= pesquisaVeiculo.DataPesquisaInicial && c.DataFim >= pesquisaVeiculo.DataPesquisaFinal && pesquisaVeiculo.DataPesquisaInicial <= pesquisaVeiculo.DataPesquisaFinal)
                           && c.Ativo).OrderByDescending(c => c.empresa.Avaliacao).ToListAsync();
                            }
                            else
                            {
                                pesquisaVeiculo.ListaDeVeiculos = await _context.Veiculos.Include("tiposDeVeiculo").Include("localizacao").Include("empresa").
                           Where(c => c.TiposDeVeiculoId == pesquisaVeiculo.TiposDeVeiculoId
                            && c.EmpresaId == pesquisaVeiculo.EmpresaId
                            && c.LocalizacaoId == pesquisaVeiculo.LocalizacaoId
                           && (c.DataInicial <= pesquisaVeiculo.DataPesquisaInicial && c.DataFim >= pesquisaVeiculo.DataPesquisaFinal && pesquisaVeiculo.DataPesquisaInicial <= pesquisaVeiculo.DataPesquisaFinal)
                           && c.Ativo).ToListAsync();
                            }

                            break;
                        }
                    case 1:
                        {
                            if (pesquisaVeiculo.ordenarClassificacao == 3)
                            {
                                pesquisaVeiculo.ListaDeVeiculos = await _context.Veiculos.Include("tiposDeVeiculo").Include("localizacao").Include("empresa").
                            Where(c => c.TiposDeVeiculoId == pesquisaVeiculo.TiposDeVeiculoId
                            && c.EmpresaId == pesquisaVeiculo.EmpresaId
                            && c.LocalizacaoId == pesquisaVeiculo.LocalizacaoId
                            && (c.DataInicial <= pesquisaVeiculo.DataPesquisaInicial && c.DataFim >= pesquisaVeiculo.DataPesquisaFinal && pesquisaVeiculo.DataPesquisaInicial <= pesquisaVeiculo.DataPesquisaFinal)
                            && c.Ativo).OrderBy(c => c.empresa.Avaliacao).OrderBy(c => c.Preco).ToListAsync();

                            }
                            else if (pesquisaVeiculo.ordenarClassificacao == 4)
                            {
                                pesquisaVeiculo.ListaDeVeiculos = await _context.Veiculos.Include("tiposDeVeiculo").Include("localizacao").Include("empresa").
                           Where(c => c.TiposDeVeiculoId == pesquisaVeiculo.TiposDeVeiculoId
                            && c.EmpresaId == pesquisaVeiculo.EmpresaId
                            && c.LocalizacaoId == pesquisaVeiculo.LocalizacaoId
                           && (c.DataInicial <= pesquisaVeiculo.DataPesquisaInicial && c.DataFim >= pesquisaVeiculo.DataPesquisaFinal && pesquisaVeiculo.DataPesquisaInicial <= pesquisaVeiculo.DataPesquisaFinal)
                           && c.Ativo).OrderByDescending(c => c.empresa.Avaliacao).OrderBy(c => c.Preco).ToListAsync();
                            }
                            else
                            {
                                pesquisaVeiculo.ListaDeVeiculos = await _context.Veiculos.Include("tiposDeVeiculo").Include("localizacao").Include("empresa").
                            Where(c => c.TiposDeVeiculoId == pesquisaVeiculo.TiposDeVeiculoId
                            && c.EmpresaId == pesquisaVeiculo.EmpresaId
                            && c.LocalizacaoId == pesquisaVeiculo.LocalizacaoId
                            && (c.DataInicial <= pesquisaVeiculo.DataPesquisaInicial && c.DataFim >= pesquisaVeiculo.DataPesquisaFinal && pesquisaVeiculo.DataPesquisaInicial <= pesquisaVeiculo.DataPesquisaFinal)
                            && c.Ativo).OrderBy(c => c.Preco).ToListAsync();
                            }

                            break;
                        }
                    case 2:
                        {
                            if (pesquisaVeiculo.ordenarClassificacao == 3)
                            {
                                pesquisaVeiculo.ListaDeVeiculos = await _context.Veiculos.Include("tiposDeVeiculo").Include("localizacao").Include("empresa").
                            Where(c => c.TiposDeVeiculoId == pesquisaVeiculo.TiposDeVeiculoId
                            && c.EmpresaId == pesquisaVeiculo.EmpresaId
                            && c.LocalizacaoId == pesquisaVeiculo.LocalizacaoId
                            && (c.DataInicial <= pesquisaVeiculo.DataPesquisaInicial && c.DataFim >= pesquisaVeiculo.DataPesquisaFinal && pesquisaVeiculo.DataPesquisaInicial <= pesquisaVeiculo.DataPesquisaFinal)
                            && c.Ativo).OrderBy(c => c.empresa.Avaliacao).OrderByDescending(c => c.Preco).ToListAsync();

                            }
                            else if (pesquisaVeiculo.ordenarClassificacao == 4)
                            {
                                pesquisaVeiculo.ListaDeVeiculos = await _context.Veiculos.Include("tiposDeVeiculo").Include("localizacao").Include("empresa").
                           Where(c => c.TiposDeVeiculoId == pesquisaVeiculo.TiposDeVeiculoId
                            && c.EmpresaId == pesquisaVeiculo.EmpresaId
                            && c.LocalizacaoId == pesquisaVeiculo.LocalizacaoId
                           && (c.DataInicial <= pesquisaVeiculo.DataPesquisaInicial && c.DataFim >= pesquisaVeiculo.DataPesquisaFinal && pesquisaVeiculo.DataPesquisaInicial <= pesquisaVeiculo.DataPesquisaFinal)
                           && c.Ativo).OrderByDescending(c => c.empresa.Avaliacao).OrderByDescending(c => c.Preco).ToListAsync();
                            }
                            else
                            {
                                pesquisaVeiculo.ListaDeVeiculos = await _context.Veiculos.Include("tiposDeVeiculo").Include("localizacao").Include("empresa").
                            Where(c => c.TiposDeVeiculoId == pesquisaVeiculo.TiposDeVeiculoId
                            && c.EmpresaId == pesquisaVeiculo.EmpresaId
                            && c.LocalizacaoId == pesquisaVeiculo.LocalizacaoId
                            && (c.DataInicial <= pesquisaVeiculo.DataPesquisaInicial && c.DataFim >= pesquisaVeiculo.DataPesquisaFinal && pesquisaVeiculo.DataPesquisaInicial <= pesquisaVeiculo.DataPesquisaFinal)
                            && c.Ativo).OrderByDescending(c => c.Preco).ToListAsync();
                            }

                            break;
                        }
                }
            }
            else if (pesquisaVeiculo.TiposDeVeiculoId != 0)
            {
                switch (pesquisaVeiculo.ordenarPreco)
                {
                    case 0:
                        {
                            if (pesquisaVeiculo.ordenarClassificacao == 3)
                            {
                                pesquisaVeiculo.ListaDeVeiculos = await _context.Veiculos.Include("tiposDeVeiculo").Include("localizacao").Include("empresa").
                            Where(c => c.TiposDeVeiculoId == pesquisaVeiculo.TiposDeVeiculoId
                            && c.LocalizacaoId == pesquisaVeiculo.LocalizacaoId
                            && (c.DataInicial <= pesquisaVeiculo.DataPesquisaInicial && c.DataFim >= pesquisaVeiculo.DataPesquisaFinal && pesquisaVeiculo.DataPesquisaInicial <= pesquisaVeiculo.DataPesquisaFinal)
                            && c.Ativo).OrderBy(c => c.empresa.Avaliacao).ToListAsync();

                            }
                            else if (pesquisaVeiculo.ordenarClassificacao == 4)
                            {
                                pesquisaVeiculo.ListaDeVeiculos = await _context.Veiculos.Include("tiposDeVeiculo").Include("localizacao").Include("empresa").
                           Where(c => c.TiposDeVeiculoId == pesquisaVeiculo.TiposDeVeiculoId
                            && c.LocalizacaoId == pesquisaVeiculo.LocalizacaoId
                           && (c.DataInicial <= pesquisaVeiculo.DataPesquisaInicial && c.DataFim >= pesquisaVeiculo.DataPesquisaFinal && pesquisaVeiculo.DataPesquisaInicial <= pesquisaVeiculo.DataPesquisaFinal)
                           && c.Ativo).OrderByDescending(c => c.empresa.Avaliacao).ToListAsync();
                            }
                            else
                            {
                                pesquisaVeiculo.ListaDeVeiculos = await _context.Veiculos.Include("tiposDeVeiculo").Include("localizacao").Include("empresa").
                           Where(c => c.TiposDeVeiculoId == pesquisaVeiculo.TiposDeVeiculoId
                            && c.LocalizacaoId == pesquisaVeiculo.LocalizacaoId
                           && (c.DataInicial <= pesquisaVeiculo.DataPesquisaInicial && c.DataFim >= pesquisaVeiculo.DataPesquisaFinal && pesquisaVeiculo.DataPesquisaInicial <= pesquisaVeiculo.DataPesquisaFinal)
                           && c.Ativo).ToListAsync();
                            }

                            break;
                        }
                    case 1:
                        {
                            if (pesquisaVeiculo.ordenarClassificacao == 3)
                            {
                                pesquisaVeiculo.ListaDeVeiculos = await _context.Veiculos.Include("tiposDeVeiculo").Include("localizacao").Include("empresa").
                            Where(c => c.TiposDeVeiculoId == pesquisaVeiculo.TiposDeVeiculoId
                            && c.LocalizacaoId == pesquisaVeiculo.LocalizacaoId
                            && (c.DataInicial <= pesquisaVeiculo.DataPesquisaInicial && c.DataFim >= pesquisaVeiculo.DataPesquisaFinal && pesquisaVeiculo.DataPesquisaInicial <= pesquisaVeiculo.DataPesquisaFinal)
                            && c.Ativo).OrderBy(c => c.empresa.Avaliacao).OrderBy(c => c.Preco).ToListAsync();

                            }
                            else if (pesquisaVeiculo.ordenarClassificacao == 4)
                            {
                                pesquisaVeiculo.ListaDeVeiculos = await _context.Veiculos.Include("tiposDeVeiculo").Include("localizacao").Include("empresa").
                           Where(c => c.TiposDeVeiculoId == pesquisaVeiculo.TiposDeVeiculoId
                            && c.LocalizacaoId == pesquisaVeiculo.LocalizacaoId
                           && (c.DataInicial <= pesquisaVeiculo.DataPesquisaInicial && c.DataFim >= pesquisaVeiculo.DataPesquisaFinal && pesquisaVeiculo.DataPesquisaInicial <= pesquisaVeiculo.DataPesquisaFinal)
                           && c.Ativo).OrderByDescending(c => c.empresa.Avaliacao).OrderBy(c => c.Preco).ToListAsync();
                            }
                            else
                            {
                                pesquisaVeiculo.ListaDeVeiculos = await _context.Veiculos.Include("tiposDeVeiculo").Include("localizacao").Include("empresa").
                            Where(c => c.TiposDeVeiculoId == pesquisaVeiculo.TiposDeVeiculoId
                            && c.LocalizacaoId == pesquisaVeiculo.LocalizacaoId
                            && (c.DataInicial <= pesquisaVeiculo.DataPesquisaInicial && c.DataFim >= pesquisaVeiculo.DataPesquisaFinal && pesquisaVeiculo.DataPesquisaInicial <= pesquisaVeiculo.DataPesquisaFinal)
                            && c.Ativo).OrderBy(c => c.Preco).ToListAsync();
                            }

                            break;
                        }
                    case 2:
                        {
                            if (pesquisaVeiculo.ordenarClassificacao == 3)
                            {
                                pesquisaVeiculo.ListaDeVeiculos = await _context.Veiculos.Include("tiposDeVeiculo").Include("localizacao").Include("empresa").
                            Where(c => c.TiposDeVeiculoId == pesquisaVeiculo.TiposDeVeiculoId
                            && c.LocalizacaoId == pesquisaVeiculo.LocalizacaoId
                            && (c.DataInicial <= pesquisaVeiculo.DataPesquisaInicial && c.DataFim >= pesquisaVeiculo.DataPesquisaFinal && pesquisaVeiculo.DataPesquisaInicial <= pesquisaVeiculo.DataPesquisaFinal)
                            && c.Ativo).OrderBy(c => c.empresa.Avaliacao).OrderByDescending(c => c.Preco).ToListAsync();

                            }
                            else if (pesquisaVeiculo.ordenarClassificacao == 4)
                            {
                                pesquisaVeiculo.ListaDeVeiculos = await _context.Veiculos.Include("tiposDeVeiculo").Include("localizacao").Include("empresa").
                           Where(c => c.TiposDeVeiculoId == pesquisaVeiculo.TiposDeVeiculoId
                            && c.LocalizacaoId == pesquisaVeiculo.LocalizacaoId
                           && (c.DataInicial <= pesquisaVeiculo.DataPesquisaInicial && c.DataFim >= pesquisaVeiculo.DataPesquisaFinal && pesquisaVeiculo.DataPesquisaInicial <= pesquisaVeiculo.DataPesquisaFinal)
                           && c.Ativo).OrderByDescending(c => c.empresa.Avaliacao).OrderByDescending(c => c.Preco).ToListAsync();
                            }
                            else
                            {
                                pesquisaVeiculo.ListaDeVeiculos = await _context.Veiculos.Include("tiposDeVeiculo").Include("localizacao").Include("empresa").
                            Where(c => c.TiposDeVeiculoId == pesquisaVeiculo.TiposDeVeiculoId
                            && c.LocalizacaoId == pesquisaVeiculo.LocalizacaoId
                            && (c.DataInicial <= pesquisaVeiculo.DataPesquisaInicial && c.DataFim >= pesquisaVeiculo.DataPesquisaFinal && pesquisaVeiculo.DataPesquisaInicial <= pesquisaVeiculo.DataPesquisaFinal)
                            && c.Ativo).OrderByDescending(c => c.Preco).ToListAsync();
                            }

                            break;
                        }
                }
            }
            else if (pesquisaVeiculo.EmpresaId != 0)
            {
                switch (pesquisaVeiculo.ordenarPreco)
                {
                    case 0:
                        {
                            if (pesquisaVeiculo.ordenarClassificacao == 3)
                            {
                                pesquisaVeiculo.ListaDeVeiculos = await _context.Veiculos.Include("tiposDeVeiculo").Include("localizacao").Include("empresa").
                            Where(c => c.EmpresaId == pesquisaVeiculo.EmpresaId
                            && c.LocalizacaoId == pesquisaVeiculo.LocalizacaoId
                            && (c.DataInicial <= pesquisaVeiculo.DataPesquisaInicial && c.DataFim >= pesquisaVeiculo.DataPesquisaFinal && pesquisaVeiculo.DataPesquisaInicial <= pesquisaVeiculo.DataPesquisaFinal)
                            && c.Ativo).OrderBy(c => c.empresa.Avaliacao).ToListAsync();

                            }
                            else if (pesquisaVeiculo.ordenarClassificacao == 4)
                            {
                                pesquisaVeiculo.ListaDeVeiculos = await _context.Veiculos.Include("tiposDeVeiculo").Include("localizacao").Include("empresa").
                           Where(c => c.EmpresaId == pesquisaVeiculo.EmpresaId
                            && c.LocalizacaoId == pesquisaVeiculo.LocalizacaoId
                           && (c.DataInicial <= pesquisaVeiculo.DataPesquisaInicial && c.DataFim >= pesquisaVeiculo.DataPesquisaFinal && pesquisaVeiculo.DataPesquisaInicial <= pesquisaVeiculo.DataPesquisaFinal)
                           && c.Ativo).OrderByDescending(c => c.empresa.Avaliacao).ToListAsync();
                            }
                            else
                            {
                                pesquisaVeiculo.ListaDeVeiculos = await _context.Veiculos.Include("tiposDeVeiculo").Include("localizacao").Include("empresa").
                           Where(c => c.EmpresaId == pesquisaVeiculo.EmpresaId
                            && c.LocalizacaoId == pesquisaVeiculo.LocalizacaoId
                           && (c.DataInicial <= pesquisaVeiculo.DataPesquisaInicial && c.DataFim >= pesquisaVeiculo.DataPesquisaFinal && pesquisaVeiculo.DataPesquisaInicial <= pesquisaVeiculo.DataPesquisaFinal)
                           && c.Ativo).ToListAsync();
                            }

                            break;
                        }
                    case 1:
                        {
                            if (pesquisaVeiculo.ordenarClassificacao == 3)
                            {
                                pesquisaVeiculo.ListaDeVeiculos = await _context.Veiculos.Include("tiposDeVeiculo").Include("localizacao").Include("empresa").
                            Where(c => c.EmpresaId == pesquisaVeiculo.EmpresaId
                            && c.LocalizacaoId == pesquisaVeiculo.LocalizacaoId
                            && (c.DataInicial <= pesquisaVeiculo.DataPesquisaInicial && c.DataFim >= pesquisaVeiculo.DataPesquisaFinal && pesquisaVeiculo.DataPesquisaInicial <= pesquisaVeiculo.DataPesquisaFinal)
                            && c.Ativo).OrderBy(c => c.empresa.Avaliacao).OrderBy(c => c.Preco).ToListAsync();

                            }
                            else if (pesquisaVeiculo.ordenarClassificacao == 4)
                            {
                                pesquisaVeiculo.ListaDeVeiculos = await _context.Veiculos.Include("tiposDeVeiculo").Include("localizacao").Include("empresa").
                           Where(c => c.EmpresaId == pesquisaVeiculo.EmpresaId
                            && c.LocalizacaoId == pesquisaVeiculo.LocalizacaoId
                           && (c.DataInicial <= pesquisaVeiculo.DataPesquisaInicial && c.DataFim >= pesquisaVeiculo.DataPesquisaFinal && pesquisaVeiculo.DataPesquisaInicial <= pesquisaVeiculo.DataPesquisaFinal)
                           && c.Ativo).OrderByDescending(c => c.empresa.Avaliacao).OrderBy(c => c.Preco).ToListAsync();
                            }
                            else
                            {
                                pesquisaVeiculo.ListaDeVeiculos = await _context.Veiculos.Include("tiposDeVeiculo").Include("localizacao").Include("empresa").
                            Where(c => c.EmpresaId == pesquisaVeiculo.EmpresaId
                            && c.LocalizacaoId == pesquisaVeiculo.LocalizacaoId
                            && (c.DataInicial <= pesquisaVeiculo.DataPesquisaInicial && c.DataFim >= pesquisaVeiculo.DataPesquisaFinal && pesquisaVeiculo.DataPesquisaInicial <= pesquisaVeiculo.DataPesquisaFinal)
                            && c.Ativo).OrderBy(c => c.Preco).ToListAsync();
                            }

                            break;
                        }
                    case 2:
                        {
                            if (pesquisaVeiculo.ordenarClassificacao == 3)
                            {
                                pesquisaVeiculo.ListaDeVeiculos = await _context.Veiculos.Include("tiposDeVeiculo").Include("localizacao").Include("empresa").
                            Where(c => c.EmpresaId == pesquisaVeiculo.EmpresaId
                            && c.LocalizacaoId == pesquisaVeiculo.LocalizacaoId
                            && (c.DataInicial <= pesquisaVeiculo.DataPesquisaInicial && c.DataFim >= pesquisaVeiculo.DataPesquisaFinal && pesquisaVeiculo.DataPesquisaInicial <= pesquisaVeiculo.DataPesquisaFinal)
                            && c.Ativo).OrderBy(c => c.empresa.Avaliacao).OrderByDescending(c => c.Preco).ToListAsync();

                            }
                            else if (pesquisaVeiculo.ordenarClassificacao == 4)
                            {
                                pesquisaVeiculo.ListaDeVeiculos = await _context.Veiculos.Include("tiposDeVeiculo").Include("localizacao").Include("empresa").
                           Where(c => c.EmpresaId == pesquisaVeiculo.EmpresaId
                            && c.LocalizacaoId == pesquisaVeiculo.LocalizacaoId
                           && (c.DataInicial <= pesquisaVeiculo.DataPesquisaInicial && c.DataFim >= pesquisaVeiculo.DataPesquisaFinal && pesquisaVeiculo.DataPesquisaInicial <= pesquisaVeiculo.DataPesquisaFinal)
                           && c.Ativo).OrderByDescending(c => c.empresa.Avaliacao).OrderByDescending(c => c.Preco).ToListAsync();
                            }
                            else
                            {
                                pesquisaVeiculo.ListaDeVeiculos = await _context.Veiculos.Include("tiposDeVeiculo").Include("localizacao").Include("empresa").
                            Where(c => c.EmpresaId == pesquisaVeiculo.EmpresaId
                            && c.LocalizacaoId == pesquisaVeiculo.LocalizacaoId
                            && (c.DataInicial <= pesquisaVeiculo.DataPesquisaInicial && c.DataFim >= pesquisaVeiculo.DataPesquisaFinal && pesquisaVeiculo.DataPesquisaInicial <= pesquisaVeiculo.DataPesquisaFinal)
                            && c.Ativo).OrderByDescending(c => c.Preco).ToListAsync();
                            }

                            break;
                        }
                }
            }
            else
            {
                switch (pesquisaVeiculo.ordenarPreco)
                {
                    case 0:
                        {
                            if (pesquisaVeiculo.ordenarClassificacao == 3)
                            {
                                pesquisaVeiculo.ListaDeVeiculos = await _context.Veiculos.Include("tiposDeVeiculo").Include("localizacao").Include("empresa").
                            Where(c => c.LocalizacaoId == pesquisaVeiculo.LocalizacaoId
                            && (c.DataInicial <= pesquisaVeiculo.DataPesquisaInicial && c.DataFim >= pesquisaVeiculo.DataPesquisaFinal && pesquisaVeiculo.DataPesquisaInicial <= pesquisaVeiculo.DataPesquisaFinal)
                            && c.Ativo).OrderBy(c => c.empresa.Avaliacao).ToListAsync();

                            }
                            else if (pesquisaVeiculo.ordenarClassificacao == 4)
                            {
                                pesquisaVeiculo.ListaDeVeiculos = await _context.Veiculos.Include("tiposDeVeiculo").Include("localizacao").Include("empresa").
                           Where(c => c.LocalizacaoId == pesquisaVeiculo.LocalizacaoId
                           && (c.DataInicial <= pesquisaVeiculo.DataPesquisaInicial && c.DataFim >= pesquisaVeiculo.DataPesquisaFinal && pesquisaVeiculo.DataPesquisaInicial <= pesquisaVeiculo.DataPesquisaFinal)
                           && c.Ativo).OrderByDescending(c => c.empresa.Avaliacao).ToListAsync();
                            }
                            else
                            {
                                pesquisaVeiculo.ListaDeVeiculos = await _context.Veiculos.Include("tiposDeVeiculo").Include("localizacao").Include("empresa").
                           Where(c => c.LocalizacaoId == pesquisaVeiculo.LocalizacaoId
                           && (c.DataInicial <= pesquisaVeiculo.DataPesquisaInicial && c.DataFim >= pesquisaVeiculo.DataPesquisaFinal && pesquisaVeiculo.DataPesquisaInicial <= pesquisaVeiculo.DataPesquisaFinal)
                           && c.Ativo).ToListAsync();
                            }

                            break;
                        }
                    case 1:
                        {
                            if (pesquisaVeiculo.ordenarClassificacao == 3)
                            {
                                pesquisaVeiculo.ListaDeVeiculos = await _context.Veiculos.Include("tiposDeVeiculo").Include("localizacao").Include("empresa").
                            Where(c => c.LocalizacaoId == pesquisaVeiculo.LocalizacaoId
                            && (c.DataInicial <= pesquisaVeiculo.DataPesquisaInicial && c.DataFim >= pesquisaVeiculo.DataPesquisaFinal && pesquisaVeiculo.DataPesquisaInicial <= pesquisaVeiculo.DataPesquisaFinal)
                            && c.Ativo).OrderBy(c => c.empresa.Avaliacao).OrderBy(c => c.Preco).ToListAsync();

                            }
                            else if (pesquisaVeiculo.ordenarClassificacao == 4)
                            {
                                pesquisaVeiculo.ListaDeVeiculos = await _context.Veiculos.Include("tiposDeVeiculo").Include("localizacao").Include("empresa").
                           Where(c => c.LocalizacaoId == pesquisaVeiculo.LocalizacaoId
                           && (c.DataInicial <= pesquisaVeiculo.DataPesquisaInicial && c.DataFim >= pesquisaVeiculo.DataPesquisaFinal && pesquisaVeiculo.DataPesquisaInicial <= pesquisaVeiculo.DataPesquisaFinal)
                           && c.Ativo).OrderByDescending(c => c.empresa.Avaliacao).OrderBy(c => c.Preco).ToListAsync();
                            }
                            else
                            {
                                pesquisaVeiculo.ListaDeVeiculos = await _context.Veiculos.Include("tiposDeVeiculo").Include("localizacao").Include("empresa").
                            Where(c => c.LocalizacaoId == pesquisaVeiculo.LocalizacaoId
                            && (c.DataInicial <= pesquisaVeiculo.DataPesquisaInicial && c.DataFim >= pesquisaVeiculo.DataPesquisaFinal && pesquisaVeiculo.DataPesquisaInicial <= pesquisaVeiculo.DataPesquisaFinal)
                            && c.Ativo).OrderBy(c => c.Preco).ToListAsync();
                            }

                            break;
                        }
                    case 2:
                        {
                            if (pesquisaVeiculo.ordenarClassificacao == 3)
                            {
                                pesquisaVeiculo.ListaDeVeiculos = await _context.Veiculos.Include("tiposDeVeiculo").Include("localizacao").Include("empresa").
                            Where(c => c.LocalizacaoId == pesquisaVeiculo.LocalizacaoId
                            && (c.DataInicial <= pesquisaVeiculo.DataPesquisaInicial && c.DataFim >= pesquisaVeiculo.DataPesquisaFinal && pesquisaVeiculo.DataPesquisaInicial <= pesquisaVeiculo.DataPesquisaFinal)
                            && c.Ativo).OrderBy(c => c.empresa.Avaliacao).OrderByDescending(c => c.Preco).ToListAsync();

                            }
                            else if (pesquisaVeiculo.ordenarClassificacao == 4)
                            {
                                pesquisaVeiculo.ListaDeVeiculos = await _context.Veiculos.Include("tiposDeVeiculo").Include("localizacao").Include("empresa").
                           Where(c => c.LocalizacaoId == pesquisaVeiculo.LocalizacaoId
                           && (c.DataInicial <= pesquisaVeiculo.DataPesquisaInicial && c.DataFim >= pesquisaVeiculo.DataPesquisaFinal && pesquisaVeiculo.DataPesquisaInicial <= pesquisaVeiculo.DataPesquisaFinal)
                           && c.Ativo).OrderByDescending(c => c.empresa.Avaliacao).OrderByDescending(c => c.Preco).ToListAsync();
                            }
                            else
                            {
                                pesquisaVeiculo.ListaDeVeiculos = await _context.Veiculos.Include("tiposDeVeiculo").Include("localizacao").Include("empresa").
                            Where(c => c.LocalizacaoId == pesquisaVeiculo.LocalizacaoId
                            && (c.DataInicial <= pesquisaVeiculo.DataPesquisaInicial && c.DataFim >= pesquisaVeiculo.DataPesquisaFinal && pesquisaVeiculo.DataPesquisaInicial <= pesquisaVeiculo.DataPesquisaFinal)
                            && c.Ativo).OrderByDescending(c => c.Preco).ToListAsync();
                            }

                            break;
                        }
                }
            }

            pesquisaVeiculo.NumResultados = pesquisaVeiculo.ListaDeVeiculos.Count;

            ViewData["Title"] = "Os nossos veiculos";

            return View(pesquisaVeiculo);
        }

        // GET: Veiculos/Create
        [Authorize(Roles = "Funcionario, Gestor")]
        public async Task<IActionResult> Create()
        {
            var userAtualId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userFuncionario = await _context.Users.Include(u => u.Empresa).Include(u => u.Reservas)
                .FirstOrDefaultAsync(u => u.Id == userAtualId);

            ViewData["EmpresaId"] = new SelectList(_context.Empresas.Where(e => e.Id == userFuncionario.EmpresaId), "Id", "Nome");
            ViewData["LocalizacaoId"] = new SelectList(_context.Localizacoes, "Id", "Nome");
            ViewData["TiposDeVeiculoId"] = new SelectList(_context.TiposDeVeiculos, "Id", "Nome");
            return View();
        }

        [Authorize(Roles = "Funcionario, Gestor")]
        public bool isValidFileType(string filename)
        {
            List<string> fileExtensions = new List<string>() { "png", "jpg", "jpeg" };
            List<string> filenameSeparated = filename.Split('.').Reverse().ToList<string>();

            foreach (var extension in fileExtensions)
                if (extension.Equals(filenameSeparated[0]))
                    return true;

            return false;
        }

        // POST: Veiculos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Funcionario, Gestor")]
        public async Task<IActionResult> Create([Bind("Id,Matricula,Marca,Nome,Km,Preco,Ativo,Lugares,Malas,Mudancas,DataInicial,DataFim,LocalizacaoId,TiposDeVeiculoId,EmpresaId,Foto,FotoFile")] Veiculo veiculo)
        {

            string aux = Enum.GetName(typeof(Marcas), Int32.Parse(veiculo.Marca));
            veiculo.Marca = aux;

            aux = Enum.GetName(typeof(tipoMudancas), Int32.Parse(veiculo.Mudancas));
            veiculo.Mudancas = aux;

            if (veiculo.FotoFile != null)
            {
                /*if (veiculo.FotoFile.Length > (200 * 1024) && !isValidFileType(veiculo.FotoFile.FileName))
                {*/
                using (var dataStream = new MemoryStream())
                {
                    await veiculo.FotoFile.CopyToAsync(dataStream);
                    veiculo.Foto = dataStream.ToArray();
                }
                //}
            }

            ModelState.Remove(nameof(veiculo.empresa));
            ModelState.Remove(nameof(veiculo.localizacao));
            ModelState.Remove(nameof(veiculo.tiposDeVeiculo));
            ModelState.Remove(nameof(veiculo.Reservas));
            if (ModelState.IsValid)
            {
                var veiculos = _context.Veiculos.Where(v => v.Matricula.Equals(veiculo.Matricula)).ToList();

                if (veiculo.DataInicial.CompareTo(DateTime.Now) > 0 && veiculo.DataFim > veiculo.DataInicial && veiculos.Count() == 0)
                {

                    _context.Add(veiculo);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["EmpresaId"] = new SelectList(_context.Empresas, "Id", "Nome", veiculo.EmpresaId);
            ViewData["LocalizacaoId"] = new SelectList(_context.Localizacoes, "Id", "Nome", veiculo.LocalizacaoId);
            ViewData["TiposDeVeiculoId"] = new SelectList(_context.TiposDeVeiculos, "Id", "Nome", veiculo.TiposDeVeiculoId);
            return View(veiculo);
        }

        // GET: Veiculos/Edit/5
        [Authorize(Roles = "Funcionario, Gestor")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Veiculos == null)
            {
                return NotFound();
            }

            //var veiculo = await _context.Veiculos.FindAsync(id);

            var veiculo = _context.Veiculos.Include(v => v.Reservas).Where(v => v.Id == id).FirstOrDefault();
            if (veiculo == null)
            {
                return NotFound();
            }

            var edita = true;
            //se veiculo tiver reserva ativa nao pode editar
            foreach(var item in veiculo.Reservas)
            {
                var reserva = _context.Reservas.Include(r => r.estados).Where(r => r.Id == item.Id).FirstOrDefault();
                if (item.estados.Count() == 1)
                    edita = false;
            }

            ViewData["EditaVeiculo"] = edita;
            ViewData["EmpresaId"] = new SelectList(_context.Empresas, "Id", "Id", veiculo.EmpresaId);
            ViewData["LocalizacaoId"] = new SelectList(_context.Localizacoes, "Id", "Id", veiculo.LocalizacaoId);
            ViewData["TiposDeVeiculoId"] = new SelectList(_context.TiposDeVeiculos, "Id", "Id", veiculo.TiposDeVeiculoId);
            return View(veiculo);
        }

        // POST: Veiculos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Funcionario, Gestor")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Matricula,Marca,Nome,Km,Preco,Ativo,Lugares,Malas,Mudancas,DataInicial,DataFim,LocalizacaoId,TiposDeVeiculoId,EmpresaId,Foto,FotoFile")] Veiculo veiculo)
        {
            if (id != veiculo.Id)
            {
                return NotFound();
            }

            string aux = Enum.GetName(typeof(Marcas), Int32.Parse(veiculo.Marca));
            veiculo.Marca = aux;

            aux = Enum.GetName(typeof(tipoMudancas), Int32.Parse(veiculo.Mudancas));
            veiculo.Mudancas = aux;

            if (veiculo.FotoFile != null)
            {
                /*if (veiculo.FotoFile.Length > (200 * 1024) && !isValidFileType(veiculo.FotoFile.FileName))
                {*/
                using (var dataStream = new MemoryStream())
                {
                    await veiculo.FotoFile.CopyToAsync(dataStream);
                    veiculo.Foto = dataStream.ToArray();
                }
                //}
            }

            ModelState.Remove(nameof(veiculo.empresa));
            ModelState.Remove(nameof(veiculo.localizacao));
            ModelState.Remove(nameof(veiculo.tiposDeVeiculo));
            ModelState.Remove(nameof(veiculo.Reservas));
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(veiculo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VeiculoExists(veiculo.Id))
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
            ViewData["EmpresaId"] = new SelectList(_context.Empresas, "Id", "Id", veiculo.EmpresaId);
            ViewData["LocalizacaoId"] = new SelectList(_context.Localizacoes, "Id", "Id", veiculo.LocalizacaoId);
            ViewData["TiposDeVeiculoId"] = new SelectList(_context.TiposDeVeiculos, "Id", "Id", veiculo.TiposDeVeiculoId);
            return View(veiculo);
        }

        // GET: Veiculos/Delete/5
        [Authorize(Roles = "Funcionario, Gestor")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Veiculos == null)
            {
                return NotFound();
            }

            var veiculo = await _context.Veiculos
                .Include(v => v.empresa)
                .Include(v => v.localizacao)
                .Include(v => v.tiposDeVeiculo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (veiculo == null)
            {
                return NotFound();
            }

            return View(veiculo);
        }

        // POST: Veiculos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Funcionario, Gestor")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Veiculos == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Veiculos'  is null.");
            }
            var veiculo = await _context.Veiculos.FindAsync(id);

            bool apaga = true;
            foreach (var item in _context.Reservas.ToList())
            {
                if (item.VeiculoId == id && (((int?)item.EstadoReserva) == 0 || ((int?)item.EstadoReserva) == 1))
                    apaga = false;

            }

            if (veiculo != null && apaga)
            {
                _context.Veiculos.Remove(veiculo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Funcionario, Gestor")]
        private bool VeiculoExists(int id)
        {
            return _context.Veiculos.Any(e => e.Id == id);
        }
    }
}
