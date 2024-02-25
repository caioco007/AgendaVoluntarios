using AgendaVoluntarios.Data.Entities;
using AgendaVoluntarios.DTO.InputModels;
using AgendaVoluntarios.DTO.ViewModels;
using AgendaVoluntarios.Repositories.Interfaces;
using AgendaVoluntarios.Repositories.Repositories;
using AgendaVoluntarios.Services.Interfaces;

namespace AgendaVoluntarios.Services.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;

        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<List<EventViewModel>> GetAllAsync()
        {
            var events = await _eventRepository.GetAllAsync();

            return events.Select(e => new EventViewModel(e.Id, e.EventAt)).ToList();
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
        public async Task AddAsync(NewEventInputModel inputModel)
        {
            var eventModel = new Event(inputModel.EventAt);

            await _eventRepository.AddAsync(eventModel);
        }
        public async Task DeleteAsync(Guid id) => await _eventRepository.DeleteAsync(id);

        public Task<bool> EventExistsAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
