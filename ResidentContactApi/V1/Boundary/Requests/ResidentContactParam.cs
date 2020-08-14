using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResidentContactApi.V1.Boundary.Requests
{
    public class ResidentContactParam
    {
        public int ResidentId { get; set; }

        //Value eg. a phone/mobile number, e-mail etc.
        public string Value { get; set; }

        public bool Active { get; set; }

        public bool Default { get; set; }

        //Integer values used to map to stored lookup values/enums
        public int Type { get; set; }

        //Integer values used to map to stored lookup values/enums
        public int SubType { get; set; }
    }
}
