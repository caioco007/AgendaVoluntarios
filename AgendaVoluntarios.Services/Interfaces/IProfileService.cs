using AgendaVoluntarios.Data.Entities;
using AgendaVoluntarios.DTO.InputModels;
using AgendaVoluntarios.DTO.ViewModels;

namespace AgendaVoluntarios.Services.Interfaces
{
    public interface IProfileService
    {
        Task<ProfileViewModel> GetByIdAsync(Guid id);
        Task<ProfileViewModel> GeFirstByIdAsync(Guid id);
        Task<List<ProfileViewModel>> GetAllAsync();
        Task<ProfileViewModel> GetByUserIdAsync(string userId);
        Task<List<ProfileViewModel>> GetProfileByActivityIdAsync(int activityId);
        Task<List<ProfileViewModel>> GetProfilesByActivityIdAndEventIdAsync(int activityId, Guid eventId);
        Task<bool> CheckIfProfileIsConfirmedForEventAndActivity(int activityId, Guid eventId);
        Task AddAsync(NewProfileInputModel inputModel);
        Task UpdateAsync(EditProfileInputModel inputModel);
        Task<bool> ProfileExistsAsync(Guid id);
        Task DeleteAsync(Guid id);
    }
}
