using Microsoft.EntityFrameworkCore;
using ResidentContactApi.V1.Boundary.Requests;
using ResidentContactApi.V1.Domain;
using ResidentContactApi.V1.Factories;
using ResidentContactApi.V1.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace ResidentContactApi.V1.Gateways
{
    public class ResidentGateway : IResidentGateway
    {
        private readonly ResidentContactContext _residentContactContext;

        public ResidentGateway(ResidentContactContext residentContactContext)
        {
            _residentContactContext = residentContactContext;
        }

        public List<ResidentDomain> GetResidents(int limit, int cursor, string firstName = null, string lastName = null)
        {
            var residents = _residentContactContext.Residents
                .Where(a => string.IsNullOrEmpty(firstName) || a.FirstName.Trim().ToLower().Contains(firstName.ToLower()))
                .Where(a => string.IsNullOrEmpty(lastName) || a.LastName.Trim().ToLower().Contains(lastName.ToLower()))
                .Where(a => a.Id > cursor)
                .Include(a => a.Contacts)
                .Include("Contacts.ContactTypeLookup")
                .Include("Contacts.ContactSubTypeLookup")
                .OrderBy(a => a.Id)
                .Take(limit)
                .ToDomain();

            return residents;

        }
        public ResidentDomain GetResidentById(int id)
        {
            var databaseRecord = _residentContactContext.Residents.Find(id);
            if (databaseRecord == null) return null;

            var contactForResident = _residentContactContext.ContactDetails
                .Include(c => c.ContactTypeLookup)
                .Include(c => c.ContactSubTypeLookup)
                .Where(c => c.ResidentId == databaseRecord.Id);

            return MapPersonAndContactToResident(databaseRecord, contactForResident);

        }

        public int? InsertResidentContactDetails(int? residentId, string nccContactId, ContactDetailsDomain contactDetails)
        {
            if (ResidentNotFoundForId(residentId)) residentId = null;

            residentId = residentId ?? FindResidentIdByContactId(nccContactId);

            if (residentId == null) return null;

            var contact = new Contact
            {
                ResidentId = residentId.Value,
                ContactValue = contactDetails.ContactValue,
                IsActive = contactDetails.IsActive,
                IsDefault = contactDetails.IsDefault,
                ContactTypeLookupId = contactDetails.TypeId,
                ContactSubTypeLookupId = contactDetails.SubtypeId
            };

            _residentContactContext.ContactDetails.Add(contact);
            _residentContactContext.SaveChanges();

            return contact.Id;
        }

        private int? FindResidentIdByContactId(string nccContactId)
        {
            var residentIdByContactId = _residentContactContext.ExternalSystemIds
                .Include(e => e.ExternalSystem)
                .Where(e => e.ExternalSystem.Name == "CRM")
                .Where(e => e.ExternalIdName == "ContactId")
                .FirstOrDefault(e => e.ExternalIdValue == nccContactId)?
                .ResidentId;
            return residentIdByContactId;
        }

        private bool ResidentNotFoundForId(int? residentId)
        {
            return _residentContactContext.Residents.Find(residentId) == null;
        }

        private static ResidentDomain MapPersonAndContactToResident(Resident resident, IEnumerable<Contact> contacts)
        {
            var person = resident.ToDomain();
            var contactDomain = contacts.Select(contact => contact.ToDomain()).ToList();

            person.Contacts = contactDomain.Any()
                ? contactDomain
                : null;
            return person;
        }

    }
}
