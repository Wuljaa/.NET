using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TuristickaAgencija.Mvc.Models;
using TuristickaAgencija.Mvc.Services;

namespace TuristickaAgencija.Mvc.Controllers
{
    public class AranzmaniController : Controller
    {
        private readonly IAranzmanService _aranzmanService;
        private readonly IDestinacijaService _destinacijaService;

        public AranzmaniController(IAranzmanService aranzmanService, IDestinacijaService destinacijaService)
        {
            _aranzmanService = aranzmanService;
            _destinacijaService = destinacijaService;
        }

        private async Task NapuniDestinacijeDropDown()
        {
            var destinacije = await _destinacijaService.GetAllAsync();
            ViewData["DestinacijaId"] = new SelectList(destinacije, "Id", "Naziv");
        }

        // GET: Aranzmani
        public async Task<IActionResult> Index()
        {
            var aranzmani = await _aranzmanService.GetAllAsync();
            return View(aranzmani);
        }

        public async Task<IActionResult> Jeftini(decimal maxCijena = 500)
        {
            var aranzmani = await _aranzmanService.GetByMaxCijenaAsync(maxCijena);

            ViewData["Naslov"] = $"Aranžmani do {maxCijena} €";

            return View("Index", aranzmani);
        }

        public async Task<IActionResult> Nadolazeci()
        {
            var aranzmani = await _aranzmanService.GetNadolazeciAsync();

            ViewData["Naslov"] = "Nadolazeći aranžmani";

            return View("Index", aranzmani);
        }

        // GET: Aranzmani/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aranzman = await _aranzmanService.GetByIdAsync(id.Value);

            if (aranzman == null)
            {
                return NotFound();
            }

            return View(aranzman);
        }

        // GET: Aranzmani/Create
        [Authorize]
        public async Task<IActionResult> Create()
        {
            await NapuniDestinacijeDropDown();
            return View();
        }

        // POST: Aranzmani/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Naziv,Opis,Cijena,DatumPolaska,DatumPovratka,DestinacijaId")] Aranzman aranzman)
        {
            if (ModelState.IsValid)
            {
                await _aranzmanService.CreateAsync(aranzman);
                return RedirectToAction(nameof(Index));
            }

            await NapuniDestinacijeDropDown();
            return View(aranzman);
        }

        // GET: Aranzmani/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aranzman = await _aranzmanService.GetByIdAsync(id.Value);

            if (aranzman == null)
            {
                return NotFound();
            }

            await NapuniDestinacijeDropDown();
            return View(aranzman);
        }

        // POST: Aranzmani/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Naziv,Opis,Cijena,DatumPolaska,DatumPovratka,DestinacijaId")] Aranzman aranzman)
        {
            if (id != aranzman.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _aranzmanService.UpdateAsync(aranzman);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AranzmanExists(aranzman.Id))
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

            await NapuniDestinacijeDropDown();
            return View(aranzman);
        }

        // GET: Aranzmani/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aranzman = await _aranzmanService.GetByIdAsync(id.Value);

            if (aranzman == null)
            {
                return NotFound();
            }

            return View(aranzman);
        }

        // POST: Aranzmani/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _aranzmanService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool AranzmanExists(int id)
        {
            return _aranzmanService.Exists(id);
        }
    }
}