using Microsoft.EntityFrameworkCore;
using ResidentContactApi.V1.Boundary.Requests;
using ResidentContactApi.V1.Boundary.Response;
using ResidentContactApi.V1.Domain;
using ResidentContactApi.V1.Factories;
using ResidentContactApi.V1.Infrastructure;
using System;
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

        public InsertResidentResponse InsertNewResident(InsertResidentRequest request)
        {
            try
            {
                //find CRM system lookup id to compare against request
                var crmLookupId = _residentContactContext.ExternalSystemLookups.Where(x => x.Name == "CRM").FirstOrDefault();
                var crmId = request.ExternalReferences.Find(x => x.ExternalReferenceName == "ContactId" && x.ExternalSystemId == crmLookupId.Id);
                if (crmId != null)
                {   //check if resident exists
                    var residentExists = FindResidentIdByContactId(crmId.ExternalReferenceValue);
                    //if resident already present, return id
                    if (residentExists != null) return new InsertResidentResponse { ResidentId = (int) residentExists, ResidentRecordAlreadyPresent = true };
                }

                var newResident = new Resident
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    DateOfBirth = request.DateOfBirth,
                    Gender = request.Gender
                };

                _residentContactContext.Residents.Add(newResident);
                _residentContactContext.SaveChanges();

                return new InsertResidentResponse { ResidentId = newResident.Id, ResidentRecordAlreadyPresent = false };
            }
            catch (DbUpdateException ex)
            {
                throw new ResidentNotInsertedException(ex.InnerException.Message);
            }
        }

        public void InsertExternalReferences(InsertResidentRequest request, int residentId)
        {
            try
            {
                foreach (var externalReference in request.ExternalReferences)
                {
                    //check if external reference already exists for resident
                    var externalRefExists = _residentContactContext.ExternalSystemIds.Any(x => x.ExternalSystemLookupId == externalReference.ExternalSystemId
                        && x.ExternalIdName == externalReference.ExternalReferenceName && x.ExternalIdValue == externalReference.ExternalReferenceValue
                        && x.ResidentId == residentId);

                    if (!externalRefExists)
                    {
                        var newExternalReference = new ExternalSystemId()
                        {
                            ExternalIdName = externalReference.ExternalReferenceName,
                            ExternalIdValue = externalReference.ExternalReferenceValue,
                            ExternalSystemLookupId = externalReference.ExternalSystemId,
                            ResidentId = residentId
                        };
                        _residentContactContext.ExternalSystemIds.Add(newExternalReference);
                    }
                }

                _residentContactContext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new ExternalReferenceNotInsertedException(ex.InnerException.Message);
            }
        }
    }
}
