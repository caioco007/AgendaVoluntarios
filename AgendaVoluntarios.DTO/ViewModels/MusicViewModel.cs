using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaVoluntarios.DTO.ViewModels
{
    public class MusicViewModel
    {
        public MusicViewModel(Guid id, string name, string key)
        {
            Id = id;
            Name = name;
            Key = key;
        }
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        //Tom da música
        public string Key { get; private set; }
    }
}
