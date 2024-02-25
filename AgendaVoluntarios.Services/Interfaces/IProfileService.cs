using AgendaVoluntarios.DTO.InputModels;
using AgendaVoluntarios.DTO.ViewModels;

namespace AgendaVoluntarios.Services.Interfaces
{
    public interface IProfileService
    {
        Task<ProfileViewModel> GetByIdAsync(Guid id);
        Task<ProfileViewModel> GeFirstByIdAsync(Guid id);
        Task<List<ProfileViewModel>> GetByUserIdAsync(string userId);
        Task AddAsync(NewProfileInputModel inputModel);
        Task UpdateAsync(EditProfileInputModel inputModel);
        Task<bool> ProfileExistsAsync(Guid id);
        Task DeleteAsync(Guid id);
    }
}
