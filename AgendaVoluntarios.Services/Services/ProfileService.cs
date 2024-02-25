using AgendaVoluntarios.Data.Entities;
using AgendaVoluntarios.DTO.InputModels;
using AgendaVoluntarios.DTO.ViewModels;
using AgendaVoluntarios.Repositories.Interfaces;
using AgendaVoluntarios.Repositories.Repositories;
using AgendaVoluntarios.Services.Interfaces;

namespace AgendaVoluntarios.Services.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _profileRepository;
        //private readonly IAuthService _authService;

        public ProfileService(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public async Task<ProfileViewModel> GetByIdAsync(Guid id)
        {
            var profile = await _profileRepository.GetByIdAsync(id);
            if (profile == null)
            {
                return null;
            }
            return new ProfileViewModel(id, profile.FullName, profile.BirthDate, profile.PhoneNumber, profile.UserId);
        }

        public async Task<ProfileViewModel> GeFirstByIdAsync(Guid id)
        {
            var profile = await _profileRepository.GeFirstByIdAsync(id);
            if (profile == null)
            {
                return null;
            }
            return new ProfileViewModel(id, profile.FullName, profile.BirthDate, profile.PhoneNumber, profile.UserId);
        }

        public async Task<List<ProfileViewModel>> GetByUserIdAsync(string userId)
        {
            var profiles = await _profileRepository.GetByUserIdAsync(userId);
            return profiles.Select(p => new ProfileViewModel(p.Id, p.FullName, p.BirthDate, p.PhoneNumber, p.UserId)).ToList();
        }

        public async Task AddAsync(NewProfileInputModel inputModel)
        {
            var profile = new Profile(Guid.NewGuid(), inputModel.FullName, inputModel.BirthDate, inputModel.UserId, inputModel.PhoneNumber);

            await _profileRepository.AddAsync(profile);
        }

        public async Task UpdateAsync(EditProfileInputModel inputModel)
        {
            var profile = new Profile(inputModel.Id, inputModel.FullName, inputModel.BirthDate, inputModel.UserId, inputModel.PhoneNumber);

            await _profileRepository.UpdateAsync(profile);
        }

        public async Task<bool> ProfileExistsAsync(Guid id) => await _profileRepository.ProfileExistsAsync(id);

        public async Task DeleteAsync(Guid id) => await _profileRepository.DeleteAsync(id);
    }
}
