using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaVoluntarios.DTO.ViewModels
{
    public class RoleViewModel
    {
        public RoleViewModel(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
        public Guid Id { get; private set; }
        public string Name { get; private set; }
    }
}
