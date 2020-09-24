using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResidentContactApi.V1.Domain
{
    public class ExternalReferenceNotInsertedException : Exception
    {
        public ExternalReferenceNotInsertedException(string message) : base(message)
        { }
    }
}
