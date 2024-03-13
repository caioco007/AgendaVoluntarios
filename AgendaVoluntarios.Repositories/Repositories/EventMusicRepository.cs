using AgendaVoluntarios.Data.Entities;
using AgendaVoluntarios.Data.Persistence;
using AgendaVoluntarios.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaVoluntarios.Repositories.Repositories
{
    public class EventMusicRepository : IEventMusicRepository
    {
        private readonly AgendaVoluntariosDbContext _dbContext;

        public EventMusicRepository(AgendaVoluntariosDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(EventMusic eventMusic)
        {
            await _dbContext.EventMusic.AddAsync(eventMusic);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var eventMusic = await _dbContext.EventMusic.FindAsync(id);
            _dbContext.EventMusic.Remove(eventMusic);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<EventMusic>> GetAllAsync() => await _dbContext.EventMusic.ToListAsync();

        public async Task<EventMusic> GetByIdAsync(Guid id) => await _dbContext.EventMusic.SingleOrDefaultAsync(e => e.Id == id);

        public async Task<List<EventMusic>> GetEventsByMusicIdAsync(Guid musicId)
        {
            return await _dbContext.EventMusic.Where(em => em.MusicId == musicId).ToListAsync();
        }

        public async Task<EventMusic> GetFirstByIdAsync(Guid id) => await _dbContext.EventMusic.FirstOrDefaultAsync(e => e.Id == id);
    }
}
