using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ResidentContactApi.V1.Boundary.Requests
{
    public class ContactDetails
    {
        public bool IsDefault { get; set; }
    }
}
