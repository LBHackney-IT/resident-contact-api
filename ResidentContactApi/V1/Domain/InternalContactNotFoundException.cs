using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResidentContactApi.V1.Domain
{
    public class InternalContactNotFoundException : Exception
    {
        public InternalContactNotFoundException() { }
        public InternalContactNotFoundException(string message) : base(message)
        { }
    }
}
