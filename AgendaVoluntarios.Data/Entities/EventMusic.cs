using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaVoluntarios.Data.Entities
{
    public class EventMusic 
    {
        public EventMusic()
        {

        }
        public Guid Id { get; private set; }
        public Guid EventId { get; private set; }
        public Guid MusicId { get; private set; }
    }
}
