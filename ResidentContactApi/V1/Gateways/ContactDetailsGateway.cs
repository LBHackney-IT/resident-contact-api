using Microsoft.EntityFrameworkCore;
using ResidentContactApi.V1.Boundary.Requests;
using ResidentContactApi.V1.Domain;
using ResidentContactApi.V1.Factories;
using ResidentContactApi.V1.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace ResidentContactApi.V1.Gateways
{
    public class ContactDetailsGateway : IContactDetailsGateway
    {
        private readonly ResidentContactContext _residentContactContext;

        public ContactDetailsGateway(ResidentContactContext residentContactContext)
        {
            _residentContactContext = residentContactContext;
        }

        public bool UpdateContactIsDefault(int contactId, bool value)
        {
          var contact = _residentContactContext.ContactDetails
                  .FirstOrDefault(c => c.Id == contactId);

          if (contact == null) return false;
          
          contact.IsDefault = value;

          _residentContactContext.ContactDetails.Update(contact);
            
          try
          {
              _residentContactContext.SaveChanges();
          }
          catch (DbUpdateException ex)
          {
            //We should log something out here
            return false;
          }

          return true;
        }

        public ContactDetailsDomain GetContactById(int contactId)
        {
            var contact = _residentContactContext.ContactDetails
                  .FirstOrDefault(c => c.Id == contactId);

            if (contact == null) return null;

            return contact.ToDomain();
        } 

    }
}
