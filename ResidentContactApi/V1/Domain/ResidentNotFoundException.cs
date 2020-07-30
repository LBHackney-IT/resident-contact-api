using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResidentContactApi.V1.Domain
{
    public class ResidentNotFoundException : Exception
    {
        public ResidentNotFoundException() { }
        public ResidentNotFoundException(string message) : base(message)
        { }
    }
}
