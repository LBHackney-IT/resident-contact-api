using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ResidentContactApi.V1.Infrastructure
{
    [Table("contact_details")]
    public class ContactDetailsInfra
    {
        [Column("id")]
        [MaxLength(40)]
        [Key]
        public int Id { get; set; }

        [Column("is_default")]
        [MaxLength(40)]
        public bool IsDefault { get; set; }

        [Column("is_active")]
        [MaxLength(40)]
        public bool IsActive { get; set; }

        [Column("added_by")]
        [MaxLength(40)]
        public string AddedBy { get; set; }

        [Column("date_added")]
        [MaxLength(40)]
        public DateTime DateAdded { get; set; }

        [Column("modified_by")]
        [MaxLength(40)]
        public string ModifiedBy { get; set; }

    }
}
