using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ResidentContactApi.V1.Infrastructure
{
    [Table("residents")]
    public class Resident
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("first_name")]
        public string FirstName { get; set; }

        [Column("last_name")]
        public string LastName { get; set; }

        [Column("date_of_birth")]
        public DateTime? DateOfBirth { get; set; }

        [Column("gender")]
        public char? Gender { get; set; }

        public List<Contact> Contacts { get; set; }
    }
}
