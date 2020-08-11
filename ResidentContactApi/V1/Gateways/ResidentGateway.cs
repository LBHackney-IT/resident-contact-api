using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using ResidentContactApi.V1.Boundary.Requests;
using ResidentContactApi.V1.Domain;
using ResidentContactApi.V1.Factories;
using ResidentContactApi.V1.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace ResidentContactApi.V1.Gateways
{
    public class ResidentGateway : IResidentGateway
    {
        private readonly ResidentContactContext _residentContactContext;

        public ResidentGateway(ResidentContactContext residentContactContext)
        {
            _residentContactContext = residentContactContext;
        }
        public List<ResidentDomain> GetResidents(string firstName = null, string lastName = null)
        {
            var residents = _residentContactContext.Residents
                .Where(a => string.IsNullOrEmpty(firstName) || a.FirstName.Trim().ToLower().Contains(firstName.ToLower()))
                .Where(a => string.IsNullOrEmpty(lastName) || a.LastName.Trim().ToLower().Contains(lastName.ToLower()))
                .Include(a => a.Contacts)
                .Include("Contacts.ContactTypeLookup")
                .Include("Contacts.ContactSubTypeLookup")
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

        public ResidentDomain InsertResidentContactDetails()
        {
            throw new NotImplementedException();
        }

        public ResidentDomain UpdateResidentContactDetails()
        {
            throw new NotImplementedException();
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
