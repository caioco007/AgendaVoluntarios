using AgendaVoluntarios.Data.Entities;
using AgendaVoluntarios.Data.Persistence;
using AgendaVoluntarios.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaVoluntarios.Repositories.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly AgendaVoluntariosDbContext _dbContext;

        public EventRepository(AgendaVoluntariosDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Event>> GetAllAsync() => await _dbContext.Event.ToListAsync();
        public async Task<Event> GetByIdAsync(Guid id) => await _dbContext.Event.SingleOrDefaultAsync(e => e.Id == id);
        public async Task<Event> GetFirstByIdAsync(Guid id) => await _dbContext.Event.FirstOrDefaultAsync(p => p.Id == id);
        public async Task AddAsync(Event events)
        {
            await _dbContext.Event.AddAsync(events);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> EventExistsAsync(Guid id)
        {
            return _dbContext.Event.Any(p => p.Id == id);
        }

        public async Task DeleteAsync(Guid id)
        {
            var events = await _dbContext.Event.FindAsync(id);
            _dbContext.Event.Remove(events);
            await _dbContext.SaveChangesAsync();
        }
    }
}
