using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ResidentContactApi.V1.Boundary.Requests
{
    [DataContract]
    public class ResidentContact
    {
        [Required]
        public int ResidentId { get; set; }

        //Value eg. a phone/mobile number, e-mail etc.
        [Required]
        [DataMember(Name = "value")]
        public string ContactValue { get; set; }

        [Required]
        [DataMember(Name = "active")]
        public bool IsActive { get; set; }

        [Required]
        [DataMember(Name = "default")]
        public bool IsDefault { get; set; }

        //Integer values used to map to stored lookup values/enums
        [Required]
        [DataMember(Name = "typeId")]
        public int ContactTypeLookupId { get; set; }

        //Nullable or Integer values used to map to stored lookup values/enums
        [DataMember(Name = "subTypeId")]
        public int? ContactSubTypeLookupId { get; set; }
    }
}
