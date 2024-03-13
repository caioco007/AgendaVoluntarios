using AgendaVoluntarios.Data.Entities;
using AgendaVoluntarios.Data.Entities.List;
using AgendaVoluntarios.DTO.InputModels;
using AgendaVoluntarios.DTO.ViewModels;
using AgendaVoluntarios.Repositories.Interfaces;
using AgendaVoluntarios.Repositories.Repositories;
using AgendaVoluntarios.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Transactions;

namespace AgendaVoluntarios.Services.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IEventMusicRepository _eventMusicRepository;
        private readonly IMusicRepository _musicRepository;
        private readonly IProfileEventRepository _profileEventRepository;

        public EventService(IEventRepository eventRepository, IEventMusicRepository eventMusicRepository, IMusicRepository musicRepository, IProfileEventRepository profileEventRepository)
        {
            _eventRepository = eventRepository;
            _eventMusicRepository = eventMusicRepository;
            _musicRepository = musicRepository;
            _profileEventRepository = profileEventRepository;
        }

        public async Task<List<EventViewModel>> GetAllAsync()
        {
            var events = await _eventRepository.GetAllAsync();

            return events.Select(e => new EventViewModel(e.Id, e.EventAt)).ToList();
        }
        public async Task<List<EventListViewModel>> GetListAllAsync(Guid? profileId)
        {
            var eventViewModels = new List<EventListViewModel>();
            if (profileId.HasValue)
            {
                var profileEvents = await _profileEventRepository.GetAllAsync();

                foreach (var profileEvent in profileEvents.Where(x => x.ProfileId == profileId.Value).ToList())
                {
                    var eventModel = await _eventRepository.GetByIdAsync(profileEvent.EventId);
                    var music = await _musicRepository.GetMusicByEventIdAsync(eventModel.Id);
                    var musicViewModels = music.Select(m => new MusicViewModel(m.Id, m.Name, m.Key)).ToList();

                    var profileEventViewModel = new ProfileEventViewModel(profileEvent.Id, profileEvent.ProfileId, profileEvent.EventId, profileEvent.ActivityId, profileEvent.IsConfirmedUser);

                    var eventViewModel = new EventListViewModel(eventModel.Id, eventModel.EventAt, musicViewModels, new List<ProfileEventViewModel> { profileEventViewModel });
                    eventViewModels.Add(eventViewModel);
                }
            }
            else
            {
                var events = await _eventRepository.GetAllAsync();

                foreach (var item in events)
                {
                    var music = await _musicRepository.GetMusicByEventIdAsync(item.Id);
                    var musicViewModels = music.Select(m => new MusicViewModel(m.Id, m.Name, m.Key)).ToList();

                    var profileEvents = await _profileEventRepository.GetByEventIdAsync(item.Id);
                    var profileEventsViewModels = profileEvents.Select(pe => new ProfileEventViewModel(pe.Id, pe.ProfileId, pe.EventId, pe.ActivityId, pe.IsConfirmedUser)).ToList();

                    var eventViewModel = new EventListViewModel(item.Id, item.EventAt, musicViewModels, profileEventsViewModels);
                    eventViewModels.Add(eventViewModel);
                }
            }

            return eventViewModels;

        }
        public async Task<EventViewModel> GetByIdAsync(Guid id)
        {
            var events = await _eventRepository.GetByIdAsync(id);
            if (events == null)
            {
                return null;
            }
            return new EventViewModel(events.Id, events.EventAt);
        }
        public async Task<EventViewModel> GetFirstByIdAsync(Guid id)
        {
            var events = await _eventRepository.GetFirstByIdAsync(id);
            if (events == null)
            {
                return null;
            }
            return new EventViewModel(id, events.EventAt);
        }
        public async Task<List<EventViewModel>> GetEventsByProfileIdAsync(Guid profileId)
        {
            var events = await _eventRepository.GetEventsByProfileIdAsync(profileId);

            return events.Select(e => new EventViewModel(e.Id, e.EventAt)).ToList();
        }
        public async Task AddAsync(NewEventInputModel inputModel)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                Guid eventId = Guid.NewGuid();
                var eventModel = new Event(eventId, inputModel.EventAt);

                await _eventRepository.AddAsync(eventModel);

                foreach (var item in inputModel.Musics)
                {
                    var eventMusic = new EventMusic(Guid.NewGuid(), eventId, item);
                    await _eventMusicRepository.AddAsync(eventMusic);
                }

                foreach (var item in inputModel.ProfileVocalsEvents[0].ProfileIds)
                {
                    var profileEvent = new ProfileEvent(Guid.NewGuid(), item, eventId, inputModel.ProfileVocalsEvents[0].ActivityId);
                    await _profileEventRepository.AddProfileEventAsync(profileEvent);
                }

                foreach (var item in inputModel.ProfileEvents)
                {
                    if (!item.ProfileId.HasValue) continue;
                    var profileEvent = new ProfileEvent(Guid.NewGuid(), item.ProfileId.Value, eventId, item.ActivityId);
                    await _profileEventRepository.AddProfileEventAsync(profileEvent);
                }
                transactionScope.Complete();
            }
        }

        public async Task UpdateAsync(EditEventInputModel inputModel)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var eventModel = new Event(inputModel.Id, inputModel.EventAt);

                foreach (var item in await _profileEventRepository.GetByEventIdAsync(inputModel.Id))
                {
                    await _profileEventRepository.DeleteAsync(item.Id);
                }

                foreach (var item in inputModel.ProfileVocalsEvents[0].ProfileIds)
                {
                    var profileEvent = new ProfileEvent(Guid.NewGuid(), item, inputModel.Id, inputModel.ProfileVocalsEvents[0].ActivityId);
                    await _profileEventRepository.AddProfileEventAsync(profileEvent);
                }

                foreach (var item in inputModel.ProfileEvents)
                {
                    if (!item.ProfileId.HasValue) continue;
                    var profileEvent = new ProfileEvent(Guid.NewGuid(), item.ProfileId.Value, inputModel.Id, item.ActivityId);
                    await _profileEventRepository.AddProfileEventAsync(profileEvent);
                }
                transactionScope.Complete();
            }
        }

        public async Task DeleteAsync(Guid id) => await _eventRepository.DeleteAsync(id);

        public async Task<bool> EventExistsAsync(Guid id)
        {
            return await _eventRepository.EventExistsAsync(id);
        }
        public async Task SetIsConfirmedUserInEvent(Guid profileId, int activityId, Guid eventId, bool isConfirmed) => await _profileEventRepository.ConfirmedUser(profileId, activityId, eventId, isConfirmed);
    }
}
