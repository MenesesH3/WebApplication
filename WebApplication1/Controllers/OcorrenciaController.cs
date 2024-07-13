using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Database;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class OcorrenciaController : Controller
    {
        private readonly ChallengedbContext _context;

        public OcorrenciaController(ChallengedbContext context)
        {
            _context = context;
		}
		private List<SelectListItem> GetPageSizes(int selectedPageSize = 5)
		{
			var pagesSizes = new List<SelectListItem>();

			if (selectedPageSize == 5)
				pagesSizes.Add(new SelectListItem("5", "5", true));
			else
				pagesSizes.Add(new SelectListItem("5", "5"));

			for (int lp = 10; lp <= 100; lp += 10)
			{
				if (lp == selectedPageSize)
				{ pagesSizes.Add(new SelectListItem(lp.ToString(), lp.ToString(), true)); }
				else
					pagesSizes.Add(new SelectListItem(lp.ToString(), lp.ToString()));
			}

			return pagesSizes;
		}

		// GET: Ocorrencia
		public async Task<IActionResult> Index(string searchText = "", int pg = 1, int pageSize = 10)
		{
			var query = await _context.Ocorrenciaviews.AsNoTracking().ToListAsync();

			if (pg < 1) pg = 1;


			if (!string.IsNullOrEmpty(searchText))
			{
				query = query.Where(p => p.Descricaotipo.Contains(searchText, StringComparison.OrdinalIgnoreCase) || 
                                         p.CodigoTipo.Contains(searchText, StringComparison.OrdinalIgnoreCase) || 
                                         p.DescricaoTransportador.Contains(searchText, StringComparison.OrdinalIgnoreCase) || 
                                         p.OcorreuEm.Date.ToString().Contains(searchText, StringComparison.OrdinalIgnoreCase) || 
                                         p.SolucaoEm.ToString().Contains(searchText, StringComparison.OrdinalIgnoreCase)).ToList();
			}


			int recsCount = query.Count();

			int recSkip = (pg - 1) * pageSize;

			var retOcorrencia = query.Skip(recSkip).Take(pageSize).ToList();

			Pager SearchPager = new Pager(recsCount, pg, pageSize) { Action = "Index", Controller = "Ocorrencia", SearchText = searchText };
			ViewBag.SearchPager = SearchPager;

			this.ViewBag.PageSizes = GetPageSizes(pageSize);

			return View(retOcorrencia.ToList());

		}

		// GET: Ocorrencia/Details/5
		public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ocorrencia = await _context.Ocorrenciaviews
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ocorrencia == null)
            {
                return NotFound();
            }

            return View(ocorrencia);
        }

        // GET: Ocorrencia/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ocorrencia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdTipo,IdTransportador,OcorreuEm,SolucaoEm")] Ocorrencia ocorrencia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ocorrencia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ocorrencia);
        }

        // GET: Ocorrencia/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ocorrencia = await _context.Ocorrencia.FindAsync(id);
            if (ocorrencia == null)
            {
                return NotFound();
            }
            return View(ocorrencia);
        }

        // POST: Ocorrencia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,IdTipo,IdTransportador,OcorreuEm,SolucaoEm")] Ocorrencia ocorrencia)
        {
            if (id != ocorrencia.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ocorrencia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OcorrenciaExists(ocorrencia.Id))
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
            return View(ocorrencia);
        }

        // GET: Ocorrencia/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ocorrencia = await _context.Ocorrencia
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ocorrencia == null)
            {
                return NotFound();
            }

            return View(ocorrencia);
        }

        // POST: Ocorrencia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var ocorrencia = await _context.Ocorrencia.FindAsync(id);
            if (ocorrencia != null)
            {
                _context.Ocorrencia.Remove(ocorrencia);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OcorrenciaExists(long id)
        {
            return _context.Ocorrencia.Any(e => e.Id == id);
        }
    }
}
