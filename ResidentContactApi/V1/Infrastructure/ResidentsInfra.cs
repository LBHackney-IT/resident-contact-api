using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ResidentContactApi.V1.Infrastructure
{
    [Table("residents")]
    public class ResidentsInfra
    {
        [Column("id")]
        [MaxLength(40)]
        [Key]
        public int Id { get; set; }

        [Column("firstname")]
        [MaxLength(255)]
        public string FirstName { get; set; }

        [Column("lastname")]
        [MaxLength(255)]
        public string LastName { get; set; }

        [Column("dateOfBirth")]
        public DateTime DateOfBirth { get; set; }

        [Column("gender")]
        [MaxLength(1)]
        public string Gender { get; set; }
    }
}
