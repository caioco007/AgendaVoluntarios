using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaVoluntarios.Data.Entities.List
{
    public class EventList
    {
        //public EventList(Guid id, DateTime eventAt, List<Music> musics, int activityId, bool? isConfirmedUser)
        //{
        //    Id = id;
        //    EventAt = eventAt;
        //    Musics = musics;
        //    ActivityId = activityId;
        //    IsConfirmedUser = isConfirmedUser;
        //}
        public EventList()
        {
            Musics = new List<Music>();
            ProfileEvents = new List<ProfileEvent>();
        }
        public Guid Id { get; set; }
        public DateTime EventAt { get; set; }
        public List<Music> Musics { get; set; }
        public List<ProfileEvent> ProfileEvents { get; set; }
        //public int ActivityId { get; set; }
        //public bool? IsConfirmedUser { get; set; }
    }
}
