using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResidentResponse = ResidentContactApi.V1.Boundary.Response.Residents.ResidentResponse;

namespace ResidentContactApi.V1.Boundary.Response
{
    public class ContactDetailsResponse
    {
        public int Id { get; set; }
        public int ContactValue { get; set; }
        public string ContactType { get; set; }
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }
        public string AddedBy { get; set; }
        public DateTime DateAdded { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime DateLastModified { get; set; }
    }
}
