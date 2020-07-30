using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ResidentContactApi.V1.Infrastructure
{
    [Table("contact_details")]
    public class Contact
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }


        [Column("is_default")]
        public bool IsDefault { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; }

        [Column("added_by")]
        public string AddedBy { get; set; }

        [Column("date_added")]
        public DateTime DateAdded { get; set; }

        [Column("modified_by")]
        public string ModifiedBy { get; set; }

        [Column("type_lookup_id")]
        public string ContactType { get; set; }

        [Column("contact_details_value")]
        public string ContactValue { get; set; }

        [Column("date_modified")]
        public DateTime DateLastModified { get; set; }

        [Column("subtype_lookup_id")]
        public string SubContactType { get; set; }

        [Column("Resident_id")]
        public int ResidentId { get; set; }

        public Resident Resident { get; set; }

    }
}
