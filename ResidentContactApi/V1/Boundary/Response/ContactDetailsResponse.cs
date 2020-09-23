using System;

namespace ResidentContactApi.V1.Boundary.Response
{
    public class ContactDetailsResponse
    {
        public int? Id { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
        public string SubType { get; set; }
        public bool Default { get; set; }
        public bool Active { get; set; }
        public string AddedBy { get; set; }
        public DateTime DateAdded { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime DateLastModified { get; set; }
    }
}
