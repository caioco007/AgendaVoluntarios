using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaVoluntarios.Data.Entities
{
    public class Event 
    {
        public Event(DateTime eventAt)
        {
            EventAt = eventAt;
            CreatedAt = DateTime.Now;

            Profiles = new List<ProfileEvent>();
            Musics = new List<EventMusic>();
        }

        public Guid Id { get; private set; }
        public DateTime EventAt { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public List<ProfileEvent> Profiles { get; private set; }
        public List<EventMusic> Musics { get; private set; }
    }
}
