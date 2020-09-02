using System.Collections.Generic;
using System.Linq;
using ResidentContactApi.V1.Boundary.Requests;
using ResidentContactApi.V1.Boundary.Response;
using ResidentContactApi.V1.Factories;
using ResidentContactApi.V1.Gateways;
using ResidentContactApi.V1.UseCase.Interfaces;

namespace ResidentContactApi.V1.UseCase
{
    public class GetAllUseCase : IGetAllUseCase
    {
        private readonly IResidentGateway _residentGateway;
        public GetAllUseCase(IResidentGateway gateway)
        {
            _residentGateway = gateway;
        }

        public ResidentResponseList Execute(ResidentQueryParam rqp)
        {
            var limit = rqp.Limit < 10 ? 10 : rqp.Limit;
            limit = rqp.Limit > 100 ? 100 : limit;

            var residents = _residentGateway
                .GetResidents(limit, rqp.Cursor, rqp.FirstName, rqp.LastName).ToResponse();

            return new ResidentResponseList
            {
                Residents = residents,
                NextCursor = GetNextCursor(residents, limit)
            };
        }

        private static string GetNextCursor(List<ResidentResponse> residents, int limit)
        {
            return residents.Count == limit ? residents.Max(r => r.Id).ToString() : null;
        }
    }
}


