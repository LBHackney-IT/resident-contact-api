using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResidentContactApi.V1.Boundary.Requests
{
    public class InsertResidentRequest
    {
        /// <example>
        /// John
        /// </example>
        [Required]
        public string FirstName { get; set; }
        /// <example>
        /// Smith
        /// </example>
        [Required]
        public string LastName { get; set; }
        /// <example>
        /// M
        /// </example>
        public char? Gender { get; set; }
        /// <example>
        /// 01-01-1970
        /// </example>
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public List<InsertExternalReferenceRequest> ExternalReferences { get; set; }
    }
}
