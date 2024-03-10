using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaVoluntarios.DTO.InputModels
{
    public class NewProfileInputModel
    {
        [DisplayName("Nome completo")]
        public string FullName { get; set; }
        [DisplayName("Data de Nascimento")]
        public DateTime BirthDate { get; set; }
        [DisplayName("Celular")]
        //[RegularExpression(@"^[0-9]*$", ErrorMessage = "O campo Celular deve conter apenas números.")]
        //[StringLength(11, MinimumLength = 11, ErrorMessage = "O campo Celular deve ter 11 dígitos.")]
        public string PhoneNumber { get; set; }
        public string? UserId { get; set; }
        public List<Guid> Functions { get; set; }
    }
}
