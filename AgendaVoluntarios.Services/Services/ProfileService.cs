using AgendaVoluntarios.Data.Entities;
using AgendaVoluntarios.DTO.InputModels;
using AgendaVoluntarios.DTO.ViewModels;
using AgendaVoluntarios.Repositories.Interfaces;
using AgendaVoluntarios.Repositories.Repositories;
using AgendaVoluntarios.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AgendaVoluntarios.Services.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IProfileFunctionRepository _profileFunctionRepository;
        private readonly IProfileEventRepository _profileEventRepository;

        public ProfileService(IProfileRepository profileRepository,IProfileFunctionRepository profileFunctionRepository, IProfileEventRepository profileEventRepository)
        {
            _profileRepository = profileRepository;
            _profileFunctionRepository = profileFunctionRepository;
            _profileEventRepository = profileEventRepository;
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

        public async Task<List<ProfileViewModel>> GetAllAsync()
        {
            var profiles = await _profileRepository.GetAllAsync();
            return profiles.Select(p => new ProfileViewModel(p.Id, p.FullName, p.BirthDate, p.PhoneNumber, p.UserId)).ToList();
        }

        public async Task<ProfileViewModel> GetByUserIdAsync(string userId)
        {
            var profile = await _profileRepository.GetByUserIdAsync(userId);
            return profile== null ? null : new ProfileViewModel(profile.Id, profile.FullName, profile.BirthDate, profile.PhoneNumber, profile.UserId);
        }

        public async Task AddAsync(NewProfileInputModel inputModel)
        {
            Guid profileId = Guid.NewGuid();
            var profile = new Profile(profileId, inputModel.FullName, inputModel.BirthDate, inputModel.UserId, inputModel.PhoneNumber);

            await _profileRepository.AddAsync(profile);

            foreach (var functionId in inputModel.Functions)
            {
                var profileFunction = new ProfileFunction(Guid.NewGuid(), profileId, functionId);
                await _profileFunctionRepository.AddProfileFunctionAsync(profileFunction);
            }
        }

        public async Task UpdateAsync(EditProfileInputModel inputModel)
        {
            var profile = new Profile(inputModel.Id, inputModel.FullName, inputModel.BirthDate, inputModel.UserId, inputModel.PhoneNumber);

            await _profileRepository.UpdateAsync(profile);

            foreach (var item in await _profileFunctionRepository.GetByProfileIdAsync(inputModel.Id))
            {
                await _profileFunctionRepository.DeleteAsync(item.Id);
            }

            foreach (var functionId in inputModel.Functions)
            {
                var profileFunction = new ProfileFunction(Guid.NewGuid(), inputModel.Id, functionId);
                await _profileFunctionRepository.AddProfileFunctionAsync(profileFunction);
            }
        }

        public async Task<bool> ProfileExistsAsync(Guid id) => await _profileRepository.ProfileExistsAsync(id);

        public async Task DeleteAsync(Guid id) => await _profileRepository.DeleteAsync(id);

        public async Task<List<ProfileViewModel>> GetProfileByActivityIdAsync(int activityId)
        {
            var profiles = await _profileRepository.GetProfileByActivityIdAsync(activityId);
            return profiles.Select(p => new ProfileViewModel(p.Id, p.FullName, p.BirthDate, p.PhoneNumber, p.UserId)).ToList();
        }

        public async Task<List<ProfileViewModel>> GetProfilesByActivityIdAndEventIdAsync(int activityId, Guid eventId)
        {
            var profiles = await _profileRepository.GetProfilesByActivityIdAndEventIdAsync(activityId, eventId);
            return profiles.Select(p => new ProfileViewModel(p.Id, p.FullName, p.BirthDate, p.PhoneNumber, p.UserId)).ToList();
        }

        public async Task<bool> CheckIfProfileIsConfirmedForEventAndActivity(int activityId, Guid eventId) => await _profileEventRepository.CheckIfProfileIsConfirmedForEventAndActivity(activityId, eventId);
    }
}
