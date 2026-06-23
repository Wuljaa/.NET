using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TuristickaAgencija.Mvc.Data;
using TuristickaAgencija.Mvc.Models;
using TuristickaAgencija.Mvc.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Formats.Asn1;


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
        public async Task<IActionResult> Jeftini(decimal maxCijena = 500)
        {
            var aranzmani = await _aranzmanService.GetByMaxCijenaAsync(maxCijena);
            ViewData["Naslov"] = $"Aranzmani sa cijenom do {maxCijena} €";
            return View("Index",aranzmani);
        }

        public async Task<IActionResult> Nadolazeci()
        {
            var aranzmani = await _aranzmanService.GetNadolazeciAsync();
            ViewData["Naslov"] = "Nadolazeci aranzmani";
            return View("Index", aranzmani);
        }

        // GET: Aranzmani
        public async Task<IActionResult> Index()
        {
            var aranzmani = await _aranzmanService.GetAllAsync();
            return View(aranzmani);
        }

        private async Task NapuniDestinacije()
        {
            var destinacije = await _destinacijaService.GetAllAsync();
            ViewData["DestinacijaId"] = new SelectList(destinacije, "Id", "Naziv");
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
        public async Task<IActionResult> Create()
        {
            await NapuniDestinacije();
            return View();
        }

        // POST: Aranzmani/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Naziv,Opis,Cijena,DatumPolaska,DatumPovratka,DestinacijaId")] Aranzman aranzman)
        {
            if (ModelState.IsValid)
            {
                await _aranzmanService.CreateAsync(aranzman);
                return RedirectToAction(nameof(Index));
            }
            await NapuniDestinacije();
            return View(aranzman);
        }

        // GET: Aranzmani/Edit/5
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
            await NapuniDestinacije();
            return View(aranzman);
        }

        // POST: Aranzmani/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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
            await NapuniDestinacije();
            return View(aranzman);
        }

        // GET: Aranzmani/Delete/5
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
