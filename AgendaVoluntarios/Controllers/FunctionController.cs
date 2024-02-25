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
    [Authorize(Roles = "Admin")]
    public class FunctionController : Controller
    {
        private readonly IFunctionService _functionService;

        public FunctionController(IFunctionService functionService)
        {
            _functionService = functionService;
        }

        // GET: Function
        public async Task<IActionResult> Index()
        {
            var functions = await _functionService.GetAllAsync();

            return View(functions);
        }

        // GET: Function/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Function/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NewFunctionInputModel functions,
                                                IFormCollection form, 
                                                [FromServices] IHttpClientFactory clientFactory)
        {
            if (ModelState.IsValid)
            {
                await _functionService.AddAsync(functions);
                return RedirectToAction(nameof(Index));
            }
            return View(functions);
        }

        // GET: Function/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var functions = await _functionService.GeFirstByIdAsync(id.Value);
            if (functions == null)
            {
                return NotFound();
            }

            return View(functions);
        }

        // POST: Function/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _functionService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
