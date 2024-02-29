using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClementoniWebAPI.Models.DB;

namespace ClementoniWebAPI.Controllers
{
    public class ComunedinascitaController : Controller
    {
        private readonly FormazioneDBContext _context;

        public ComunedinascitaController(FormazioneDBContext context)
        {
            _context = context;
        }

        // GET: Comunedinascita
        public async Task<IActionResult> Index()
        {
            return View(await _context.Comunedinascita.ToListAsync());
        }

        // GET: Comunedinascita/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comunedinascita = await _context.Comunedinascita
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comunedinascita == null)
            {
                return NotFound();
            }

            return View(comunedinascita);
        }

        // GET: Comunedinascita/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Comunedinascita/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NomeComuneDiNascita")] Comunedinascita comunedinascita)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comunedinascita);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(comunedinascita);
        }

        // GET: Comunedinascita/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comunedinascita = await _context.Comunedinascita.FindAsync(id);
            if (comunedinascita == null)
            {
                return NotFound();
            }
            return View(comunedinascita);
        }

        // POST: Comunedinascita/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NomeComuneDiNascita")] Comunedinascita comunedinascita)
        {
            if (id != comunedinascita.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comunedinascita);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComunedinascitaExists(comunedinascita.Id))
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
            return View(comunedinascita);
        }

        // GET: Comunedinascita/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comunedinascita = await _context.Comunedinascita
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comunedinascita == null)
            {
                return NotFound();
            }

            return View(comunedinascita);
        }

        // POST: Comunedinascita/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comunedinascita = await _context.Comunedinascita.FindAsync(id);
            if (comunedinascita != null)
            {
                _context.Comunedinascita.Remove(comunedinascita);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComunedinascitaExists(int id)
        {
            return _context.Comunedinascita.Any(e => e.Id == id);
        }
    }
}
