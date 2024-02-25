using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaVoluntarios.Data.Entities
{
    public class Profile 
    {
        public Profile(Guid id, string fullName, DateTime birthDate, string userId, string phoneNumber)
        {
            Id = id;
            FullName = fullName;
            BirthDate = birthDate;
            Active = true;
            UserId = userId;
            PhoneNumber = phoneNumber;

            CreatedAt = DateTime.Now;
            Events = new List<ProfileEvent>();
            Functions = new List<ProfileFunction>();
        }

        public Guid Id { get; private set; }
        public string FullName { get; private set; }
        public DateTime BirthDate { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool Active { get; set; }
        public string UserId { get; private set; }
        public string PhoneNumber { get; private set; }

        public List<ProfileEvent> Events { get; private set; }
        public List<ProfileFunction> Functions { get; private set; }
    }
}
