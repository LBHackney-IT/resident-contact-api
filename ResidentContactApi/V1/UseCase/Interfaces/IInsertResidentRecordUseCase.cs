using ResidentContactApi.V1.Boundary.Requests;
using ResidentContactApi.V1.Boundary.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResidentContactApi.V1.UseCase.Interfaces
{
    public interface IInsertResidentRecordUseCase
    {
        InsertResidentResponse Execute(InsertResidentRequest request);
    }
}
