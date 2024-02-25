using AgendaVoluntarios.DTO.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaVoluntarios.DTO.ViewModels
{
    public class FunctionViewModel
    {
        public FunctionViewModel(Guid id, int typeId, int activityId)
        {
            Id = id;
            TypeId = typeId;
            ActivityId = activityId;
        }
        public Guid Id { get; private set; }
        public int TypeId { get; private set; }
        public string FunctionTypeName
        {
            get
            {
                return TypeId switch
                {
                    (int)FunctionTypes.Worship => "Louvor",
                    (int)FunctionTypes.Media => "Mídia",
                    _ => "-",
                };
            }
        }
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
    }
}
