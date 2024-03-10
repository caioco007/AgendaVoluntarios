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
    public class MusicRepository : IMusicRepository
    {
        private readonly AgendaVoluntariosDbContext _dbContext;

        public MusicRepository(AgendaVoluntariosDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Music>> GetAllAsync() => await _dbContext.Music.ToListAsync();
        public async Task<Music> GetByIdAsync(Guid id) => await _dbContext.Music.SingleOrDefaultAsync(m => m.Id == id);
        public async Task AddAsync(Music music)
        {
            await _dbContext.Music.AddAsync(music);
            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Music music)
        {
            _dbContext.Music.Update(music);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Music> GetFirstByIdAsync(Guid id) => await _dbContext.Music.FirstOrDefaultAsync(p => p.Id == id);

        public async Task DeleteAsync(Guid id)
        {
            var music = await _dbContext.Music.FindAsync(id);
            _dbContext.Music.Remove(music);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Music>> GetMusicByEventIdAsync(Guid eventId)
        {
            var q = await (from m in _dbContext.Music
                           join em in _dbContext.EventMusic on m.Id equals em.MusicId
                           where em.EventId == eventId
                           select m).ToListAsync();
            return q;
        }
    }
}
