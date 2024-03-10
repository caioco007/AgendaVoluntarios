using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaVoluntarios.Data.Entities
{
    public class ProfileEvent 
    {
        public ProfileEvent(Guid id, Guid profileId, Guid eventId, int activityId)
        {
            Id = id;
            ProfileId = profileId;
            EventId = eventId;
            ActivityId = activityId;
        }

        public void ConfirmedUser()
        {
            IsConfirmedUser = true;
        }

        public Guid Id { get; private set; }
        public Guid ProfileId { get; private set; }
        public Guid EventId { get; private set; }
        public int ActivityId { get; private set; }
        public bool? IsConfirmedUser { get; set; }
    }
}
