using AgendaVoluntarios.Data.Entities;
using AgendaVoluntarios.DTO.InputModels;
using AgendaVoluntarios.DTO.ViewModels;

namespace AgendaVoluntarios.Services.Interfaces
{
    public interface IMusicService
    {
        Task<List<MusicViewModel>> GetAllAsync();
        Task<MusicViewModel> GetByIdAsync(Guid id);
        Task<MusicViewModel> GetFirstByIdAsync(Guid id);
        Task AddAsync(NewMusicInputModel inputModel);
        Task UpdateAsync(EditMusicInputModel inputModel);
        Task DeleteAsync(Guid id);
        Task<List<MusicViewModel>> GetMusicByEventIdAsync(Guid eventId);
    }
}
