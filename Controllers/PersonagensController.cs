using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebPersonagens.Data;
using WebPersonagens.Models;
using Microsoft.AspNetCore.Authorization;

namespace WebPersonagens.Controllers
{
    [Authorize]
    public class PersonagensController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PersonagensController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Personagens
        public async Task<IActionResult> Index(string searchString)
        {
            var personagensQuery = _context.Personagens
                .Include(p => p.Profissao)
                .Include(p => p.Universo)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();

                personagensQuery = personagensQuery.Where(p =>
                    p.Nome.ToLower().Contains(searchString) ||
                    p.Descricao.ToLower().Contains(searchString) ||
                    p.Profissao.Nome.ToLower().Contains(searchString) ||
                    p.Universo.Nome.ToLower().Contains(searchString)
                );
            }

            ViewData["CurrentFilter"] = searchString;

            return View(await personagensQuery.OrderBy(p => p.Nome).ToListAsync());
        }

        // GET: Personagens/DetalhePoder/5
        public async Task<IActionResult> DetalhePoder(int? id)
        {
            if (id == null) return NotFound();

            var personagem = await _context.Personagens
                .Include(p => p.Itens) 
                .Include(p => p.Profissao)
                .Include(p => p.Universo)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (personagem == null) return NotFound();

            decimal totalValorItens = personagem.Itens?.Sum(i => i.Valor) ?? 0;
            decimal poderTotal = personagem.VidaMaxima + totalValorItens;

            var viewModel = new PersonagemDetalheViewModel
            {
                Personagem = personagem,
                TotalValorItens = totalValorItens,
                PoderTotal = poderTotal
            };

            return View(viewModel);
        }

        // GET: Personagens/RelatorioAgrupamento
        public async Task<IActionResult> RelatorioAgrupamento()
        {
            var relatorio = await _context.Personagens
                .Include(p => p.Universo)
                .GroupBy(p => p.Universo.Nome)
                .Select(g => new ContagemPorUniversoViewModel
                {
                    NomeUniverso = g.Key,
                    ContagemPersonagens = g.Count()
                })
                .OrderByDescending(r => r.ContagemPersonagens)
                .ToListAsync();

            return View(relatorio);
        }

        // GET: Personagens/RelatorioPivot
        public IActionResult RelatorioPivot()
        {
            var personagens = _context.Personagens.ToList();

            var nivelBaixo = personagens.Count(p => p.Nivel >= 1 && p.Nivel <= 50);
            var nivelMedio = personagens.Count(p => p.Nivel > 50 && p.Nivel <= 500);
            var nivelAlto = personagens.Count(p => p.Nivel > 500);

            ViewData["NivelBaixo"] = nivelBaixo;
            ViewData["NivelMedio"] = nivelMedio;
            ViewData["NivelAlto"] = nivelAlto;

            return View();
        }


        // GET: Personagens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personagem = await _context.Personagens
                .Include(p => p.Profissao)
                .Include(p => p.Universo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personagem == null)
            {
                return NotFound();
            }

            return View(personagem);
        }

        // GET: Personagens/Create
        public IActionResult Create()
        {
            ViewData["ProfissaoId"] = new SelectList(_context.Profissoes, "Id", "Descricao");
            ViewData["UniversoId"] = new SelectList(_context.Universos, "Id", "Descricao");
            return View();
        }

        // POST: Personagens/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Descricao,Nivel,VidaMaxima,ProfissaoId,UniversoId")] Personagem personagem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(personagem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProfissaoId"] = new SelectList(_context.Profissoes, "Id", "Descricao", personagem.ProfissaoId);
            ViewData["UniversoId"] = new SelectList(_context.Universos, "Id", "Descricao", personagem.UniversoId);
            return View(personagem);
        }

        // GET: Personagens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personagem = await _context.Personagens.FindAsync(id);
            if (personagem == null)
            {
                return NotFound();
            }
            ViewData["ProfissaoId"] = new SelectList(_context.Profissoes, "Id", "Descricao", personagem.ProfissaoId);
            ViewData["UniversoId"] = new SelectList(_context.Universos, "Id", "Descricao", personagem.UniversoId);
            return View(personagem);
        }

        // POST: Personagens/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Descricao,Nivel,VidaMaxima,ProfissaoId,UniversoId")] Personagem personagem)
        {
            if (id != personagem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personagem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonagemExists(personagem.Id))
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
            ViewData["ProfissaoId"] = new SelectList(_context.Profissoes, "Id", "Descricao", personagem.ProfissaoId);
            ViewData["UniversoId"] = new SelectList(_context.Universos, "Id", "Descricao", personagem.UniversoId);
            return View(personagem);
        }

        // GET: Personagens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personagem = await _context.Personagens
                .Include(p => p.Profissao)
                .Include(p => p.Universo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personagem == null)
            {
                return NotFound();
            }

            return View(personagem);
        }

        // POST: Personagens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var personagem = await _context.Personagens.FindAsync(id);
            if (personagem != null)
            {
                _context.Personagens.Remove(personagem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonagemExists(int id)
        {
            return _context.Personagens.Any(e => e.Id == id);
        }
    }
}
