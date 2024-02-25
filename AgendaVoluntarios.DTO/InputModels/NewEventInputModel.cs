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
        public List<ProfileViewModel> Profiles { get; set; }
        public List<MusicViewModel> Musics { get; set; }
    }
}
