using AgendaVoluntarios.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaVoluntarios.Repositories.Interfaces
{
    public interface IProfileRepository
    {
        Task<Profile> GetByIdAsync(Guid id);
        Task<Profile> GeFirstByIdAsync(Guid id);
        Task<List<Profile>> GetByUserIdAsync(string userId);
        Task AddAsync(Profile profile);
        Task UpdateAsync(Profile profile);
        Task<bool> ProfileExistsAsync(Guid id);
        Task DeleteAsync(Guid id);
    }
}
