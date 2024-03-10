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
    public class ProfileEventRepository : IProfileEventRepository
    {
        private readonly AgendaVoluntariosDbContext _dbContext;

        public ProfileEventRepository(AgendaVoluntariosDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddProfileEventAsync(ProfileEvent profileEvent)
        {
            await _dbContext.ProfileEvent.AddAsync(profileEvent);
            await _dbContext.SaveChangesAsync();
        }

        public async Task ConfirmedUser(Guid profileId, int activityId, Guid eventId, bool isConfirmed)
        {
            var profileEvent = await _dbContext.ProfileEvent.SingleOrDefaultAsync(x => x.ProfileId == profileId && x.ActivityId == activityId && x.EventId == eventId);
            if (profileEvent != null)
            {
                if (isConfirmed)
                {
                    profileEvent.ConfirmedUser();
                    await _dbContext.SaveChangesAsync();

                    var profileEvents = await _dbContext.ProfileEvent.Where(x => x.ProfileId == profileId && x.EventId == eventId && !x.IsConfirmedUser.HasValue).ToListAsync();
                    foreach (var item in profileEvents)
                    {
                        await DeleteAsync(item.Id);

                    }
                }
                else
                {
                    await DeleteAsync(profileEvent.Id);
                }
            }
        }

        public async Task<bool> CheckIfProfileIsConfirmedForEventAndActivity(int activityId, Guid eventId) => await _dbContext.ProfileEvent.AnyAsync(x => x.ActivityId == activityId && x.EventId == eventId && x.IsConfirmedUser.Value);

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                var profileEvent = await _dbContext.ProfileEvent.FindAsync(id);
                _dbContext.ProfileEvent.Remove(profileEvent);
                await _dbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<List<ProfileEvent>> GetAllAsync() => await _dbContext.ProfileEvent.ToListAsync();

        public async Task<List<ProfileEvent>> GetByEventIdAsync(Guid eventId) => await _dbContext.ProfileEvent.Where(p => p.EventId == eventId).ToListAsync();
    }
}
