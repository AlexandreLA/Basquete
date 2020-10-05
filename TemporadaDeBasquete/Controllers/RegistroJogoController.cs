using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TemporadaDeBasquete.Models;
using WebAPI.Models;

namespace TemporadaDeBasquete.Controllers
{
    public class RegistroJogoController : Controller
    {
        private readonly BasqueteContext _context;

        public RegistroJogoController(BasqueteContext context)
        {
            _context = context;
        }

        // GET: RegistroJogoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.RegistroJogo.Include(x => x.Temporada).ToListAsync());
        }

        // GET: RegistroJogoes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registroJogo = await _context.RegistroJogo.Include(x => x.Temporada)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registroJogo == null)
            {
                return NotFound();
            }

            return View(registroJogo);
        }

        // GET: RegistroJogoes/Create
        public IActionResult Create()
        {
            ViewData["TemporadaId"] = new SelectList(_context.Temporada, "Id", "TemporadaDescricao");
            return View();
        }

        // POST: RegistroJogoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Placar,MinimoTemporada,MaximoTemporada,QuebraRecordeMaximo,QuebraRecordeMinimo,TemporadaId")] RegistroJogo registroJogo)
        {
            if (ModelState.IsValid)
            {
                registroJogo = CalculaRegistrosTemporada(registroJogo);

                registroJogo.Id = Guid.NewGuid();
                _context.Add(registroJogo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(registroJogo);
        }

        private RegistroJogo CalculaRegistrosTemporada(RegistroJogo registroJogo)
        {
            int? minimoTemporada, maximoTemporada;
            BuscaMinimoEMaximoTemporada(registroJogo, out minimoTemporada, out maximoTemporada);

            if (minimoTemporada == null)
            {
                registroJogo.MinimoTemporada = registroJogo.Placar;
                registroJogo.MaximoTemporada = registroJogo.Placar;
                registroJogo.QuebraRecordeMaximo = 1;
                registroJogo.QuebraRecordeMinimo = 1;
                registroJogo.NumeroJogo = 1;

                return registroJogo;
            }

            registroJogo.NumeroJogo = _context.RegistroJogo.Where(x => x.TemporadaId == registroJogo.TemporadaId).Max(x => x.NumeroJogo) + 1;

            if (registroJogo.Placar < minimoTemporada)
            {
                var quebraRecordeMinimo = BuscaQuebraRecordeMinimo(registroJogo.TemporadaId);

                registroJogo.MaximoTemporada = _context.RegistroJogo.Where(x => x.TemporadaId == registroJogo.TemporadaId).Max(x => x.MaximoTemporada);
                registroJogo.QuebraRecordeMaximo = _context.RegistroJogo.Where(x => x.TemporadaId == registroJogo.TemporadaId).Max(x => x.QuebraRecordeMaximo);

                registroJogo.QuebraRecordeMinimo = quebraRecordeMinimo + 1;
                registroJogo.MinimoTemporada = registroJogo.Placar;

                return registroJogo;
            }
            else if (registroJogo.Placar > maximoTemporada)
            {
                var quebraRecordeMaximo = BuscaQuebraRecordeMaximo(registroJogo.TemporadaId);

                registroJogo.MinimoTemporada = _context.RegistroJogo.Where(x => x.TemporadaId == registroJogo.TemporadaId).Min(x => x.MinimoTemporada);
                registroJogo.QuebraRecordeMinimo = _context.RegistroJogo.Where(x => x.TemporadaId == registroJogo.TemporadaId).Max(x => x.QuebraRecordeMinimo);

                registroJogo.QuebraRecordeMaximo = quebraRecordeMaximo + 1;
                registroJogo.MaximoTemporada = registroJogo.Placar;

                return registroJogo;
            }
            else
            {
                registroJogo.MaximoTemporada = _context.RegistroJogo.Where(x => x.TemporadaId == registroJogo.TemporadaId).Max(x => x.MaximoTemporada);
                registroJogo.QuebraRecordeMaximo = _context.RegistroJogo.Where(x => x.TemporadaId == registroJogo.TemporadaId).Max(x => x.QuebraRecordeMaximo);
                registroJogo.MinimoTemporada = _context.RegistroJogo.Where(x => x.TemporadaId == registroJogo.TemporadaId).Min(x => x.MinimoTemporada);
                registroJogo.QuebraRecordeMinimo = _context.RegistroJogo.Where(x => x.TemporadaId == registroJogo.TemporadaId).Max(x => x.QuebraRecordeMinimo);

                return registroJogo;
            }
        }

        private void BuscaMinimoEMaximoTemporada(RegistroJogo registroJogo, out int? minimoTemporada, out int? maximoTemporada)
        {
            minimoTemporada = _context.RegistroJogo.Where(x => x.TemporadaId == registroJogo.TemporadaId).Min(x => x.MinimoTemporada);
            maximoTemporada = _context.RegistroJogo.Where(x => x.TemporadaId == registroJogo.TemporadaId).Max(x => x.MaximoTemporada);
        }

        private int? BuscaQuebraRecordeMaximo(Guid temporadaId)
        {
            return _context.RegistroJogo.Where(x => x.TemporadaId == temporadaId).Max(x => x.QuebraRecordeMaximo);
        }

        private int? BuscaQuebraRecordeMinimo(Guid temporadaId)
        {
            return _context.RegistroJogo.Where(x => x.TemporadaId == temporadaId).Max(x => x.QuebraRecordeMinimo);
        }

        // GET: RegistroJogoes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registroJogo = await _context.RegistroJogo.FindAsync(id);
            if (registroJogo == null)
            {
                return NotFound();
            }
            return View(registroJogo);
        }

        // POST: RegistroJogoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Placar,DataJogo,MinimoTemporada,MaximoTemporada,QuebraRecordeMaximo,QuebraRecordeMinimo,TemporadaId")] RegistroJogo registroJogo)
        {
            if (id != registroJogo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(registroJogo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegistroJogoExists(registroJogo.Id))
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
            return View(registroJogo);
        }

        // GET: RegistroJogoes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registroJogo = await _context.RegistroJogo.Include(x => x.Temporada)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registroJogo == null)
            {
                return NotFound();
            }

            return View(registroJogo);
        }

        // POST: RegistroJogoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var registroJogo = await _context.RegistroJogo.FindAsync(id);
            _context.RegistroJogo.Remove(registroJogo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegistroJogoExists(Guid id)
        {
            return _context.RegistroJogo.Any(e => e.Id == id);
        }
    }
}
