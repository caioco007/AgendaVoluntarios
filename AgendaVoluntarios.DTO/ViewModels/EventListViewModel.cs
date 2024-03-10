using AgendaVoluntarios.DTO.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaVoluntarios.DTO.ViewModels
{
    public class EventListViewModel
    {
        public EventListViewModel(Guid id, DateTime eventAt, List<MusicViewModel> musics, List<ProfileEventViewModel> profileEvents)
        {
            Id = id;
            EventAt = eventAt;
            Musics = musics;
            ProfileEvents = profileEvents;
            //ActivityId = activityId;
            //IsConfirmedUser = isConfirmedUser;
        }
        public Guid Id { get; private set; }
        [DisplayName("Data do Evento")]
        public DateTime EventAt { get; private set; }
        public List<MusicViewModel> Musics { get; private set; }
        public string MusicsName
        {
            get
            {
                var stringBuilder = new StringBuilder();
                foreach (var music in Musics)
                {
                    stringBuilder.Append(music.Name);
                    stringBuilder.Append(", "); // Adiciona uma vírgula após cada nome de música
                }
                if (stringBuilder.Length > 0)
                {
                    // Remove a última vírgula e espaço extra
                    stringBuilder.Length -= 2;
                }
                return stringBuilder.ToString();
            }
        }
        public List<ProfileEventViewModel> ProfileEvents { get; private set; }
        //public int ActivityId { get; private set; }
        //public string ActivityTypeName
        //{
        //    get
        //    {
        //        return ActivityId switch
        //        {
        //            (int)ActivityTypes.Vocals => "Vocal",
        //            (int)ActivityTypes.Drums => "Bateria",
        //            (int)ActivityTypes.AcousticGuitar => "Violão",
        //            (int)ActivityTypes.BassGuitar => "Contra Baixo",
        //            (int)ActivityTypes.ElectricGuitar => "Guitarra",
        //            (int)ActivityTypes.Keyboard => "Teclado",
        //            (int)ActivityTypes.Photography => "Fotografia",
        //            (int)ActivityTypes.SocialMedia => "Media Social",
        //            (int)ActivityTypes.Projector => "Projeção",
        //            _ => "-",
        //        };
        //    }
        //}
        //public bool? IsConfirmedUser { get; private set; }
    }
}
