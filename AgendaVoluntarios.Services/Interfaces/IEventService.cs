using AgendaVoluntarios.DTO.InputModels;
using AgendaVoluntarios.DTO.ViewModels;

namespace AgendaVoluntarios.Services.Interfaces
{
    public interface IEventService
    {
        Task<List<EventViewModel>> GetAllAsync();
        Task<EventViewModel> GetByIdAsync(Guid id);
        Task<EventViewModel> GetFirstByIdAsync(Guid id);
        Task<List<EventViewModel>> GetEventsByProfileIdAsync(Guid profileId);
        Task<List<EventListViewModel>> GetListAllAsync(Guid? profileId);
        Task AddAsync(NewEventInputModel inputModel);
        Task UpdateAsync(EditEventInputModel inputModel);
        Task<bool> EventExistsAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task SetIsConfirmedUserInEvent(Guid profileId, int activityId, Guid eventId, bool isConfirmed);
    }
}
