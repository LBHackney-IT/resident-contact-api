using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResidentContactApi.V1.Domain
{
    public class ResidentNotInsertedException : Exception
    {
        public ResidentNotInsertedException(string message) : base(message)
        { }
    }
}
