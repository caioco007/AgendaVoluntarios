using AgendaVoluntarios.Data.Entities;
using AgendaVoluntarios.DTO.InputModels;
using AgendaVoluntarios.DTO.ViewModels;
using AgendaVoluntarios.Repositories.Interfaces;
using AgendaVoluntarios.Repositories.Repositories;
using AgendaVoluntarios.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AgendaVoluntarios.Services.Services
{
    public class MusicService : IMusicService
    {
        private readonly IMusicRepository _musicRepository;
        private readonly IEventMusicRepository _eventMusicRepository;

        public MusicService(IMusicRepository musicRepository, IEventMusicRepository eventMusicRepository)
        {
            _musicRepository = musicRepository;
            _eventMusicRepository = eventMusicRepository;
        }

        public async Task<List<MusicViewModel>> GetAllAsync()
        {
            var musics = await _musicRepository.GetAllAsync();

            return musics.Select(m => new MusicViewModel(m.Id, m.Name, m.Key)).ToList();
        }
        public async Task<MusicViewModel> GetByIdAsync(Guid id) 
        {
            var music = await _musicRepository.GetByIdAsync(id);
            if (music == null)
            {
                return null;
            }
            return new MusicViewModel(music.Id, music.Name, music.Key);
        }

        public async Task AddAsync(NewMusicInputModel inputModel)
        {
            var music = new Music(Guid.NewGuid(), inputModel.Name, inputModel.Key);

            await _musicRepository.AddAsync(music);
        }

        public async Task UpdateAsync(EditMusicInputModel inputModel)
        {
            var music = new Music(inputModel.Id, inputModel.Name, inputModel.Key);

            await _musicRepository.UpdateAsync(music);
        }

        public async Task<MusicViewModel> GetFirstByIdAsync(Guid id)
        {
            var music = await _musicRepository.GetFirstByIdAsync(id);
            if (music == null)
            {
                return null;
            }
            return new MusicViewModel(id, music.Name, music.Key);
        }

        public async Task<List<MusicViewModel>> GetMusicByEventIdAsync(Guid eventId)
        {
            var musics = await _musicRepository.GetMusicByEventIdAsync(eventId);

            return musics.Select(m => new MusicViewModel(m.Id, m.Name, m.Key)).ToList();
        }

        public async Task DeleteAsync(Guid id) => await _musicRepository.DeleteAsync(id);
    }
}
