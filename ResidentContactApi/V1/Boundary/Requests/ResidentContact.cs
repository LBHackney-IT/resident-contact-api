using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ResidentContactApi.V1.Boundary.Requests
{
    public class ResidentContact
    {
        public int? ResidentId { get; set; }

        public int? Id { get; set; }
        public string NccContactId { get; set; }

        //Value eg. a phone or mobile number, e-mail address etc.
        [Required]
        public string Value { get; set; }

        [Required]
        public bool Active { get; set; }

        [Required]
        public bool Default { get; set; }

        //Integer values used to map to stored lookup values/enums
        [Required]
        public int TypeId { get; set; }

        //Nullable or Integer values used to map to stored lookup values/enums
        public int? SubtypeId { get; set; }
    }
}
