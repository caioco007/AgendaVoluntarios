using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AgendaVoluntarios.Areas.Identity.Data;
using AgendaVoluntarios.DTO.InputModels;
using AgendaVoluntarios.DTO.ViewModels;
using AgendaVoluntarios.Services.Interfaces;
using AgendaVoluntarios.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AgendaVoluntarios.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        private readonly IProfileService _profileService;
        private readonly UserManager<User> _userManager;
        private readonly IEventService _eventService;

        public EventController(IProfileService profileService,
                                UserManager<User> userManager,
                                IEventService eventService)
        {
            _profileService = profileService;
            _userManager = userManager;
            _eventService = eventService;
        }

        // GET: Event
        public async Task<IActionResult> Index()
        {

            var userId = _userManager.GetUserId(User);
            List<EventListViewModel> events = new List<EventListViewModel>();
            if (User.IsInRole("Admin"))
                events = await _eventService.GetListAllAsync(null);
            else
            {
                var profile = await _profileService.GetByUserIdAsync(userId);
                events = await _eventService.GetListAllAsync(profile.Id);
            }

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
            return View(new EditEventInputModel { Id = events.Id, EventAt = events.EventAt });
        }

        // POST: Profile/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, EditEventInputModel events)
        {
            if (id != events.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _eventService.UpdateAsync(events);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _eventService.EventExistsAsync(events.Id))
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
            return View(events);
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

        [HttpGet, ActionName("ConfirmedUser")]
        public async Task<IActionResult> SetIsConfirmedUserInEvent([FromQuery] Guid profileId, [FromQuery] int activityId, [FromQuery] Guid eventId, [FromQuery] bool isConfirmed)
        {
            await _eventService.SetIsConfirmedUserInEvent(profileId, activityId, eventId, isConfirmed);
            return RedirectToAction(nameof(Index));
        }

    }
}
