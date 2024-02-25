using AgendaVoluntarios.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaVoluntarios.Repositories.Interfaces
{
    public interface IFunctionRepository
    {
        Task<Function> GetByIdAsync(Guid id);
        Task<Function> GeFirstByIdAsync(Guid id);
        Task<List<Function>> GetAllAsync();
        Task AddAsync(Function profile);
        Task<bool> FunctionExistsAsync(Guid id);
        Task DeleteAsync(Guid id);
    }
}
