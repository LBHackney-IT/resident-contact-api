using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResidentContactApi.V1.Boundary.Requests
{
    public class InsertExternalReferenceRequest
    {
        //Integer values used to map to stored lookup values/enums
        /// <example>
        /// 1
        /// </example>
        [Required]
        public int ExternalSystemId { get; set; }
        /// <example>
        /// house_ref
        /// </example>
        [Required]
        public string ExternalReferenceName { get; set; }
        /// <example>
        /// 012345
        /// </example>
        [Required]
        public string ExternalReferenceValue { get; set; }
    }
}
