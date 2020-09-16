using System;

namespace ResidentContactApi.V1.Domain
{
    public class ContactDetailsDomain
    {
        public int Id { get; set; }
        public string ContactValue { get; set; }
        public string Type { get; set; }
        public int TypeId { get; set; }
        public string SubType { get; set; }
        public int? SubtypeId { get; set; }
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }
        public string AddedBy { get; set; }
        public DateTime DateAdded { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime DateLastModified { get; set; }
        public int ResidentId { get; set; }
        public ResidentDomain Resident { get; set; }


    }
}
