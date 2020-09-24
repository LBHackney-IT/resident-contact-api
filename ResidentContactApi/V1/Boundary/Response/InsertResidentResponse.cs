using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResidentContactApi.V1.Boundary.Response
{
    public class InsertResidentResponse
    {
        public int ResidentId { get; set; }
        public bool ResidentRecordAlreadyPresent { get; set; }
    }
}
