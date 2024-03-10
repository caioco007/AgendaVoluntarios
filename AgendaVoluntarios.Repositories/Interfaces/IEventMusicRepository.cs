using AgendaVoluntarios.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaVoluntarios.Repositories.Interfaces
{
    public interface IEventMusicRepository
    {
        Task<List<EventMusic>> GetAllAsync();
        Task<EventMusic> GetByIdAsync(Guid id);
        Task<EventMusic> GetFirstByIdAsync(Guid id);
        Task AddAsync(EventMusic eventMusic);
        Task DeleteAsync(Guid id);
    }
}
