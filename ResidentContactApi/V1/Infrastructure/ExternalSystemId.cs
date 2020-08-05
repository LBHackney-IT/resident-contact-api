using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResidentContactApi.V1.Infrastructure
{
    [Table("external_system_ids")]
    public class ExternalSystemId
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("resident_id")]
        public int ResidentId { get; set; }
        public Resident Resident { get; set; }

        [Column("external_system_lookup_id")]
        public int ExternalSystemLookupId { get; set; }
        public ExternalSystemLookup ExternalSystem { get; set; }

        [Column("external_id_value")]
        [Required]
        public string ExternalIdValue { get; set; }

        [Column("external_id_name")]
        [Required]
        public string ExternalIdName { get; set; }
    }
}
