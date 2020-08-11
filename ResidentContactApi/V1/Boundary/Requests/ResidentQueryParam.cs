using Microsoft.AspNetCore.Mvc;

namespace ResidentContactApi.V1.Boundary.Requests
{
    public class ResidentQueryParam
    {
        [FromQuery(Name = "firstName")]
        public string FirstName { get; set; }

        [FromQuery(Name = "lastName")]
        public string LastName { get; set; }

    }
}
