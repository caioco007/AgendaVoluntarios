using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaVoluntarios.Data.Entities
{
    public class Music 
    {
        public Music(string name, string key)
        {
            Name = name;
            Key = key;

            Events = new List<EventMusic>();
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        //Tom da música
        public string Key { get; private set; }

        public List<EventMusic> Events { get; private set; }
    }
}
