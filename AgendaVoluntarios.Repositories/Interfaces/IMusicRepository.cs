using AgendaVoluntarios.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaVoluntarios.Repositories.Interfaces
{
    public interface IMusicRepository
    {
        Task<List<Music>> GetAllAsync();
        Task<Music> GetByIdAsync(Guid id);
        Task<Music> GetFirstByIdAsync(Guid id);
        Task AddAsync(Music music);
        Task DeleteAsync(Guid id);
    }
}
