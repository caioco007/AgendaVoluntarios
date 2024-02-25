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
    public class ProfileRepository : IProfileRepository
    {
        private readonly AgendaVoluntariosDbContext _dbContext;

        public ProfileRepository(AgendaVoluntariosDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Profile> GetByIdAsync(Guid id) => await _dbContext.Profile.SingleOrDefaultAsync(p => p.Id == id);

        public async Task<Profile> GeFirstByIdAsync(Guid id) => await _dbContext.Profile.FirstOrDefaultAsync(p => p.Id == id);

        public async Task AddAsync(Profile profile)
        {
            await _dbContext.Profile.AddAsync(profile);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Profile>> GetByUserIdAsync(string userId) => await _dbContext.Profile.Where(p => p.UserId == userId).ToListAsync();

        public async Task UpdateAsync(Profile profile)
        {
            _dbContext.Profile.Update(profile);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> ProfileExistsAsync(Guid id)
        {
            return _dbContext.Profile.Any(p => p.Id == id);
        }

        public async Task DeleteAsync(Guid id)
        {
            var profile = await _dbContext.Profile.FindAsync(id);
            _dbContext.Profile.Remove(profile);
            await _dbContext.SaveChangesAsync();
        }
    }
}
