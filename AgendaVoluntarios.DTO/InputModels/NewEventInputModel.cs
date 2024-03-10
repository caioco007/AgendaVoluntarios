using AgendaVoluntarios.DTO.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaVoluntarios.DTO.InputModels
{
    public class NewEventInputModel
    {
        public DateTime EventAt { get; set; }
        public List<NewProfileVocalsEventInputModel> ProfileVocalsEvents { get; set; }
        public List<NewProfileEventInputModel> ProfileEvents { get; set; }
        public List<Guid> Musics { get; set; }
    }
}
