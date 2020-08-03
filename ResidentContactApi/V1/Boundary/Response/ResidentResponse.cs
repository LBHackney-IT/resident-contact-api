using System;
using System.Collections.Generic;

namespace ResidentContactApi.V1.Boundary.Response
{
    public class ResidentResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; }
        public List<ContactDetailsResponse> Contacts { get; set; }
    }
}
