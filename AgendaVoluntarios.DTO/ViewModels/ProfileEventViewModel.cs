using AgendaVoluntarios.DTO.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaVoluntarios.DTO.ViewModels
{
    public class ProfileEventViewModel
    {
        public ProfileEventViewModel(Guid id, Guid profileId, Guid eventId, int activityId, bool? isConfirmedUser)
        {
            Id = id;
            ProfileId = profileId;
            EventId = eventId;
            ActivityId = activityId;
            IsConfirmedUser = isConfirmedUser;
        }

        public Guid Id { get; private set; }
        public Guid ProfileId { get; private set; }
        public Guid EventId { get; private set; }
        public int ActivityId { get; private set; }
        public string ActivityTypeName
        {
            get
            {
                return ActivityId switch
                {
                    (int)ActivityTypes.Vocals => "Vocal",
                    (int)ActivityTypes.Drums => "Bateria",
                    (int)ActivityTypes.AcousticGuitar => "Violão",
                    (int)ActivityTypes.BassGuitar => "Contra Baixo",
                    (int)ActivityTypes.ElectricGuitar => "Guitarra",
                    (int)ActivityTypes.Keyboard => "Teclado",
                    (int)ActivityTypes.Photography => "Fotografia",
                    (int)ActivityTypes.SocialMedia => "Media Social",
                    (int)ActivityTypes.Projector => "Projeção",
                    _ => "-",
                };
            }
        }
        public bool? IsConfirmedUser { get; private set; }
    }
}
