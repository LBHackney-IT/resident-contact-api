using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResidentContactApi.V1.Domain
{
    public class ContactNotFoundException : Exception
    {
        public ContactNotFoundException() { }
        public ContactNotFoundException(string message) : base(message)
        { }
    }
}
