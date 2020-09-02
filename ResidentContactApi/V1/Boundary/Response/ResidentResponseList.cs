using System.Collections.Generic;

namespace ResidentContactApi.V1.Boundary.Response
{
    public class ResidentResponseList
    {
        public List<ResidentResponse> Residents { get; set; }
        public string NextCursor { get; set; }
    }
}
