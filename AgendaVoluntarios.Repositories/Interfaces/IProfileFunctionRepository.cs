using AgendaVoluntarios.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaVoluntarios.Repositories.Interfaces
{
    public interface IProfileFunctionRepository
    {
        Task<List<ProfileFunction>> GetByProfileIdAsync(Guid id);
        Task AddProfileFunctionAsync(ProfileFunction profileFunction);
        Task<List<ProfileFunction>> GetAllAsync();
        Task<bool> ProfileExistsAsync(Guid profileId);
        Task DeleteAsync(Guid id);
    }
}
