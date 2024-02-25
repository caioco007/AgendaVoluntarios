using AgendaVoluntarios.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaVoluntarios.Repositories.Interfaces
{
    public interface IEventRepository
    {
        Task<List<Event>> GetAllAsync();
        Task<Event> GetByIdAsync(Guid id);
        Task<Event> GetFirstByIdAsync(Guid id);
        Task AddAsync(Event events);
        Task<bool> EventExistsAsync(Guid id);
        Task DeleteAsync(Guid id);
    }
}
