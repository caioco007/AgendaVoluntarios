﻿using AgendaVoluntarios.DTO.InputModels;
using AgendaVoluntarios.DTO.ViewModels;

namespace AgendaVoluntarios.Services.Interfaces
{
    public interface IFunctionService
    {
        Task<List<FunctionViewModel>> GetAllAsync();
        Task AddAsync(NewFunctionInputModel inputModel);
        Task DeleteAsync(Guid id);
        Task<FunctionViewModel> GeFirstByIdAsync(Guid id);
    }
}
