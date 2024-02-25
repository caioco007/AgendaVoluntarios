using AgendaVoluntarios.DTO.InputModels;
using AgendaVoluntarios.DTO.ViewModels;

namespace AgendaVoluntarios.Services.Interfaces
{
    public interface IEventService
    {
        Task<List<EventViewModel>> GetAllAsync();
        Task<EventViewModel> GetByIdAsync(Guid id);
        Task<EventViewModel> GetFirstByIdAsync(Guid id);
        Task AddAsync(NewEventInputModel inputModel);
        Task<bool> EventExistsAsync(Guid id);
        Task DeleteAsync(Guid id);
    }
}
