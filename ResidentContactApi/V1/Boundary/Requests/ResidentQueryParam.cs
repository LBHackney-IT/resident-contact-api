using Microsoft.AspNetCore.Mvc;

namespace ResidentContactApi.V1.Boundary.Requests
{
    public class ResidentQueryParam
    {
        [FromQuery(Name = "first_name")]
        public string FirstName { get; set; }

        [FromQuery(Name = "last_name")]
        public string LastName { get; set; }

        public int Limit { get; set; } = 20;
        public int Cursor { get; set; } = 0;
    }
}
