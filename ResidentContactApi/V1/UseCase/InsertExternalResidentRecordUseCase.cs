using ResidentContactApi.V1.Boundary.Requests;
using ResidentContactApi.V1.Boundary.Response;
using ResidentContactApi.V1.Gateways;
using ResidentContactApi.V1.UseCase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResidentContactApi.V1.UseCase
{
    public class InsertExternalResidentRecordUseCase : IInsertExternalResidentRecordUseCase
    {
        private IResidentGateway _residentGateway;
        public InsertExternalResidentRecordUseCase(IResidentGateway gateway)
        {
            _residentGateway = gateway;
        }
        public InsertResidentResponse Execute(InsertResidentRequest request)
        {
            var resident = _residentGateway.InsertNewResident(request);
            //even if resident already in db, attempt to insert possible new external references
            _residentGateway.InsertExternalReferences(request, resident.ResidentId);
            return resident;
        }

    }
}
