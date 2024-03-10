using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaVoluntarios.Data.Entities
{
    public class ProfileFunction 
    {
        public ProfileFunction(Guid id, Guid profileId, Guid functionId)
        {
            Id = id;
            ProfileId = profileId;
            FunctionId = functionId;
        }

        public Guid Id { get; private set; }
        public Guid ProfileId { get; private set; }
        public Guid FunctionId { get; private set; }
    }
}
