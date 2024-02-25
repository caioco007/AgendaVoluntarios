using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaVoluntarios.Data.Entities
{
    public class Function 
    {
        public Function(Guid id, int typeId, int activityId)
        {
            Id = id;
            TypeId = typeId;
            ActivityId = activityId;
        }

        public Guid Id { get; private set; }
        public int TypeId { get; private set; }
        public int ActivityId { get; private set; }
    }
}
