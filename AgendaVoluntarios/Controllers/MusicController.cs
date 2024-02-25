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
    public class MusicController : Controller
    {
        private readonly IMusicService _musicService;

        public MusicController(IMusicService musicService)
        {
            _musicService = musicService;
        }

        // GET: Music
        public async Task<IActionResult> Index()
        {
            var musics = await _musicService.GetAllAsync();

            return View(musics);
        }
         
        // GET: Music/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musics = await _musicService.GetByIdAsync(id.Value);
            if (musics == null)
            {
                return NotFound();
            }

            return View(musics);
        }

        // GET: Music/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Music/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NewMusicInputModel musics,
                                                IFormCollection form, 
                                                [FromServices] IHttpClientFactory clientFactory)
        {
            if (ModelState.IsValid)
            {
                await _musicService.AddAsync(musics);
                return RedirectToAction(nameof(Index));
            }
            return View(musics);
        }

        // GET: Music/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musics = await _musicService.GetByIdAsync(id.Value);

            if (musics == null)
            {
                return NotFound();
            }
            return View(new EditMusicInputModel {Id = musics.Id, Name = musics.Name, Key = musics.Key});
        }


        // GET: Music/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musics = await _musicService.GetFirstByIdAsync(id.Value);
            if (musics == null)
            {
                return NotFound();
            }

            return View(musics);
        }

        // POST: Music/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _musicService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
