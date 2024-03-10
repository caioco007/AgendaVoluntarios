using AgendaVoluntarios.Data.Entities;
using AgendaVoluntarios.Data.Persistence;
using AgendaVoluntarios.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaVoluntarios.Repositories.Repositories
{
    public class FunctionRepository : IFunctionRepository
    {
        private readonly AgendaVoluntariosDbContext _dbContext;

        public FunctionRepository(AgendaVoluntariosDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Function> GetByIdAsync(Guid id) => await _dbContext.Function.SingleOrDefaultAsync(f => f.Id == id);

        public async Task<Function> GeFirstByIdAsync(Guid id) => await _dbContext.Function.FirstOrDefaultAsync(f => f.Id == id);

        public async Task<List<Function>> GetAllAsync() => await _dbContext.Function.ToListAsync();

        public async Task AddAsync(Function function)
        {
            await _dbContext.Function.AddAsync(function);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Function>> GetFunctionByProfileIdAsync(Guid profileId)
        {
            var q = (from f in _dbContext.Function
                     join pf in _dbContext.ProfileFunction on f.Id equals pf.FunctionId 
                     where pf.ProfileId == profileId
                     select f);

            return await q.ToListAsync();
        }

        public async Task<bool> FunctionExistsAsync(Guid id)
        {
            return _dbContext.Function.Any(f => f.Id == id);
        }

        public async Task DeleteAsync(Guid id)
        {
            var function = await _dbContext.Function.FindAsync(id);
            _dbContext.Function.Remove(function);
            await _dbContext.SaveChangesAsync();
        }
    }
}
