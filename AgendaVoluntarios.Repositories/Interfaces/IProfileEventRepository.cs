using AgendaVoluntarios.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaVoluntarios.Repositories.Interfaces
{
    public interface IProfileEventRepository
    {
        Task<List<ProfileEvent>> GetByEventIdAsync(Guid eventId);
        Task AddProfileEventAsync(ProfileEvent profileEvent);
        Task ConfirmedUser(Guid profileId, int activityId, Guid eventId, bool isConfirmed);
        Task<bool> CheckIfProfileIsConfirmedForEventAndActivity(int activityId, Guid eventId);
        Task<List<ProfileEvent>> GetAllAsync();
        Task DeleteAsync(Guid id);
    }
}
