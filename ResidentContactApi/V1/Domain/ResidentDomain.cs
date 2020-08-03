using ResidentContactApi.V1.UseCase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResidentContactApi.V1.Enums;

namespace ResidentContactApi.V1.Domain
{
    public class ResidentDomain
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public GenderTypeEnum Gender { get; set; }
        public List<ContactDetailsDomain> Contacts { get; set; }

    }
}
