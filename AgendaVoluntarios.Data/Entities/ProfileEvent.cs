using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaVoluntarios.Data.Entities
{
    public class ProfileEvent 
    {
        public ProfileEvent(Guid profileId, Guid eventId)
        {
            ProfileId = profileId;
            EventId = eventId;
            IsConfirmedUser = false;
        }

        public void ConfirmedUser()
        {
            IsConfirmedUser = true;
        }

        public Guid Id { get; private set; }
        public Guid ProfileId { get; private set; }
        public Guid EventId { get; private set; }
        public bool IsConfirmedUser { get; set; }
    }
}
