using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaVoluntarios.DTO.InputModels
{
    public class NewProfileVocalsEventInputModel
    {
        public List<Guid> ProfileIds { get; set; }
        public int ActivityId { get; set; }
        public bool IsConfirmedUser { get; set; }
    }
}
