using System;
using System.Collections.Generic;
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
