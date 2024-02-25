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
    public class ProfileController : Controller
    {
        private readonly IProfileService _profileService;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public ProfileController(IProfileService profileService,
                                UserManager<User> userManager,
                                SignInManager<User> signInManager)
        {
            _profileService = profileService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: Profile
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var profile = await _profileService.GetByUserIdAsync(userId);

            return View(profile);
        }
         
        // GET: Profile/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profile = await _profileService.GetByIdAsync(id.Value);
            if (profile == null)
            {
                return NotFound();
            }

            return View(profile);
        }

        // GET: Profile/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Profile/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NewProfileInputModel profile,
                                                IFormCollection form, 
                                                [FromServices] IHttpClientFactory clientFactory)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);

                profile.UserId = userId;

                await _profileService.AddAsync(profile);
                return RedirectToAction(nameof(Index));
            }
            return View(profile);
        }

        // GET: Profile/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var profile = await _profileService.GetByIdAsync(id.Value);

            if (profile == null)
            {
                return NotFound();
            }
            return View(new EditProfileInputModel {Id = profile.Id, FullName = profile.FullName, BirthDate = profile.BirthDate, UserId = profile.UserId, PhoneNumber = profile.PhoneNumber });
        }

        // POST: Profile/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, EditProfileInputModel profile)
        {
            if (id != profile.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _profileService.UpdateAsync(profile);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _profileService.ProfileExistsAsync(profile.Id))
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
            return View(profile);
        }

        // GET: Profile/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profile = await _profileService.GeFirstByIdAsync(id.Value);
            if (profile == null)
            {
                return NotFound();
            }

            return View(profile);
        }

        // POST: Profile/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _profileService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
