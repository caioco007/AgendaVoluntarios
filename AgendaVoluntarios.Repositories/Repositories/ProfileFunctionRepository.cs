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
    public class ProfileFunctionRepository : IProfileFunctionRepository
    {
        private readonly AgendaVoluntariosDbContext _dbContext;

        public ProfileFunctionRepository(AgendaVoluntariosDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ProfileFunction>> GetByProfileIdAsync(Guid profileId) => await _dbContext.ProfileFunction.Where(p => p.ProfileId == profileId).ToListAsync();

        public async Task AddProfileFunctionAsync(ProfileFunction profileFunction)
        {
            await _dbContext.ProfileFunction.AddAsync(profileFunction);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<ProfileFunction>> GetAllAsync() => await _dbContext.ProfileFunction.ToListAsync();

        public async Task<bool> ProfileExistsAsync(Guid profileId)
        {
            return _dbContext.ProfileFunction.Any(p => p.ProfileId == profileId);
        }
        public async Task DeleteAsync(Guid id)
        {
            try
            {
                var profileFunction = await _dbContext.ProfileFunction.FindAsync(id);
                _dbContext.ProfileFunction.Remove(profileFunction);
                await _dbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
