using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaVoluntarios.DTO.ViewModels
{
    public class ProfileViewModel
    {
        public ProfileViewModel(Guid id, string fullName, DateTime birthDate, string phoneNumber, string userId)
        {
            Id = id;
            FullName = fullName;
            BirthDate = birthDate;
            PhoneNumber = phoneNumber;
            UserId = userId;
        }
        public Guid Id { get; private set; }
        [DisplayName("Nome completo")]
        public string FullName { get; private set; }
        [DisplayName("Data de Nascimento")]
        public DateTime BirthDate { get; private set; }
        public string BirthDateFormatted => BirthDate.ToShortDateString();
        [DisplayName("Celular")]
        public string PhoneNumber { get; private set; }
        public string UserId { get; private set; }
    }
}
