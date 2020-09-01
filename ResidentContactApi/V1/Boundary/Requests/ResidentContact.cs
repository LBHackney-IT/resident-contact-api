using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ResidentContactApi.V1.Boundary.Requests
{
    public class ResidentContact
    {
        [Required]
        public int ResidentId { get; set; }

        //Value eg. a phone/mobile number, e-mail etc.
        [Required]
        [BindProperty(Name = "value")]
        public string ContactValue { get; set; }

        [Required]
        [BindProperty(Name = "active")]
        public bool IsActive { get; set; }

        [Required]
        [BindProperty(Name = "default")]
        public bool IsDefault { get; set; }

        //Integer values used to map to stored lookup values/enums
        [Required]
        [BindProperty(Name = "type")]
        public int ContactTypeLookupId { get; set; }

        //Nullable or Integer values used to map to stored lookup values/enums
        [BindProperty(Name = "subType")]
        public int? ContactSubTypeLookupId { get; set; }
    }
}
