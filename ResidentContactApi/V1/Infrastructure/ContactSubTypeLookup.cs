using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResidentContactApi.V1.Infrastructure
{
    [Table("contact_sub_type_lookup")]
    public class ContactSubTypeLookup
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("name")]
        [Required]
        public string Name { get; set; }
    }
}
