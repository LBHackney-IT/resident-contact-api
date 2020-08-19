using System.ComponentModel.DataAnnotations;

namespace ResidentContactApi.V1.Boundary.Requests
{
    public class ResidentContact
    {
        [Required]
        public int ResidentId { get; set; }

        //Value eg. a phone/mobile number, e-mail etc.
        [Required]
        public string ContactValue { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public bool IsDefault { get; set; }

        //Integer values used to map to stored lookup values/enums
        [Required]
        public int ContactTypeLookupId { get; set; }

        //Nullable or Integer values used to map to stored lookup values/enums
        public int? ContactSubTypeLookupId { get; set; }
    }
}
