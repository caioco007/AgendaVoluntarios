using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaVoluntarios.DTO.ViewModels
{
    public class EventViewModel
    {
        public EventViewModel(Guid id, DateTime eventAt)
        {
            Id = id;
            EventAt = eventAt;
        }
        public Guid Id { get; private set; }
        [DisplayName("Data do Evento")]
        public DateTime EventAt { get; private set; }
    }
}
