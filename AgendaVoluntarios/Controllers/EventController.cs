using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AgendaVoluntarios.Areas.Identity.Data;
using AgendaVoluntarios.DTO.InputModels;
using AgendaVoluntarios.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace PB_RedeSocial.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        // GET: Event
        public async Task<IActionResult> Index()
        {
            var events = await _eventService.GetAllAsync();

            return View(events);
        }
         
        // GET: Event/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var events = await _eventService.GetByIdAsync(id.Value);
            if (events == null)
            {
                return NotFound();
            }

            return View(events);
        }

        // GET: Event/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Event/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NewEventInputModel events,
                                                IFormCollection form, 
                                                [FromServices] IHttpClientFactory clientFactory)
        {
            if (ModelState.IsValid)
            {
                await _eventService.AddAsync(events);
                return RedirectToAction(nameof(Index));
            }
            return View(events);
        }

        // GET: Event/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var events = await _eventService.GetByIdAsync(id.Value);

            if (events == null)
            {
                return NotFound();
            }
            return View(new EditEventInputModel {Id = events.Id, EventAt = events.EventAt});
        }


        // GET: Event/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var events = await _eventService.GetFirstByIdAsync(id.Value);
            if (events == null)
            {
                return NotFound();
            }

            return View(events);
        }

        // POST: Event/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _eventService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
