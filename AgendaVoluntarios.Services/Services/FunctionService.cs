using AgendaVoluntarios.Data.Entities;
using AgendaVoluntarios.DTO.InputModels;
using AgendaVoluntarios.DTO.ViewModels;
using AgendaVoluntarios.Repositories.Interfaces;
using AgendaVoluntarios.Repositories.Repositories;
using AgendaVoluntarios.Services.Interfaces;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AgendaVoluntarios.Services.Services
{
    public class FunctionService : IFunctionService
    {
        private readonly IFunctionRepository _functionRepository;

        public FunctionService(IFunctionRepository functionRepository)
        {
            _functionRepository = functionRepository;
        }

        public async Task AddAsync(NewFunctionInputModel inputModel)
        {
            var function = new Function(Guid.NewGuid(), inputModel.TypeId, inputModel.ActivityId);

            await _functionRepository.AddAsync(function);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _functionRepository.DeleteAsync(id);
        }

        public async Task<FunctionViewModel> GeFirstByIdAsync(Guid id)
        {
            var function = await _functionRepository.GeFirstByIdAsync(id);
            if (function == null)
            {
                return null;
            }
            return new FunctionViewModel(id, function.TypeId, function.ActivityId);
        }

        public async Task<List<FunctionViewModel>> GetAllAsync() {

            var functions = await _functionRepository.GetAllAsync();

            return functions.Select(f => new FunctionViewModel(f.Id, f.TypeId, f.ActivityId)).ToList();
        }

        public async Task<List<FunctionViewModel>> GetFunctionByProfileIdAsync(Guid profileId)
        {
            var functions = await _functionRepository.GetFunctionByProfileIdAsync(profileId);

            return functions.Select(f => new FunctionViewModel(f.Id, f.TypeId, f.ActivityId)).ToList();
        }
    }
}
