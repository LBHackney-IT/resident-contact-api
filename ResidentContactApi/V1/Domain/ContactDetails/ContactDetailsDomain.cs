using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResidentContactApi.V1.Domain
{
    public class ContactDetailsDomain
    {
        public int Id { get; set; }
        public int ContactTValue { get; set; }
        public string ContactType { get; set; }
        public bool IsDefault { get; set; }
        public bool Active { get; set; }
        public string AddedBy { get; set; }
        public DateTime DateAdded { get; set; }
        public string ModifiedBy { get; set; }
        public ResidentDomain ResidentId { get; set; }
    }
}
