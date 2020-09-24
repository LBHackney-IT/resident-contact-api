using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResidentContactApi.V1.Boundary.Response
{
    public class InsertExternalResidentResponse
    {
        public int ExternalReferenceId { get; set; }
        public bool AlreadyPresent { get; set; }
    }
}
