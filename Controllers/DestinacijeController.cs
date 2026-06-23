using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TuristickaAgencija.Mvc.Data; //ne treba ovo (ostalo od scaffoldinga)
using TuristickaAgencija.Mvc.Models;
using TuristickaAgencija.Mvc.Services;

namespace TuristickaAgencija.Mvc.Controllers
{
    public class DestinacijeController : Controller
    {
        private readonly IDestinacijaService _destinacijaService;

        public DestinacijeController(IDestinacijaService destinacijaService)
        {
            _destinacijaService = destinacijaService;
        }

        // GET: Destinacije
        public async Task<IActionResult> Index()
        {
            var destinacije = await _destinacijaService.GetAllAsync();
            return View(destinacije);
        }

        public async Task<IActionResult> Popularne()
        {
            var destinacije = await _destinacijaService.GetPopularneAsync();
            ViewData["Naslov"] = "Popularne destinacije";
            return View("Index", destinacije);
        }

        public async Task<IActionResult> PoDrzavi(string drzava)
        {
            if (string.IsNullOrEmpty(drzava))
            {
                return RedirectToAction(nameof(Index));
            }

            var destinacije = await _destinacijaService.GetPoDrzaviAsync(drzava);
            ViewData["Naslov"] = $"Destinacije u {drzava}";
            return View("Index", destinacije);
        }
        // GET: Destinacije/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var destinacija = await _destinacijaService.GetByIdAsync(id.Value);
            if (destinacija == null)
            {
                return NotFound();
            }

            return View(destinacija);
        }

        // GET: Destinacije/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Destinacije/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Naziv,Drzava,Opis,Popularna")] Destinacija destinacija)
        {
            if (ModelState.IsValid)
            {
                await _destinacijaService.CreateAsync(destinacija);
                return RedirectToAction(nameof(Index));
            }
            return View(destinacija);
        }

        // GET: Destinacije/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var destinacija = await _destinacijaService.GetByIdAsync(id.Value);
            if (destinacija == null)
            {
                return NotFound();
            }
            return View(destinacija);
        }

        // POST: Destinacije/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Naziv,Drzava,Opis,Popularna")] Destinacija destinacija)
        {
            if (id != destinacija.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _destinacijaService.UpdateAsync(destinacija);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DestinacijaExists(destinacija.Id))
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
            return View(destinacija);
        }

        // GET: Destinacije/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var destinacija = await _destinacijaService.GetByIdAsync(id.Value);
            if (destinacija == null)
            {
                return NotFound();
            }

            return View(destinacija);
        }

        // POST: Destinacije/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _destinacijaService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
        
        private bool DestinacijaExists(int id)
        {
            return _destinacijaService.Exists(id);
        }
    }
}
