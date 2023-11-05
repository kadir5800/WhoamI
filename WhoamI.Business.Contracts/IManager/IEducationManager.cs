using WhoamI.Business.Contracts.DTO.Education;
using WhoamI.Business.Contracts.DTO.Client;
using WhoamI.Business.Contracts.DTO.DataTable;

namespace WhoamI.Business.Contracts.IManager
{
    public interface IEducationManager
    {
        Task<ClientResult<getAllEducationResponse>> getAllEducation(dataTableRequest request);
        Task<ClientResult<getOneEducationResponse>> getOneEducation(getOneRequest request);
        Task<ClientResult> addEducation(addEducationRequest request);
        Task<ClientResult> updateEducation(updateEducationRequest request);
        Task<ClientResult> deleteEducation(getOneRequest request);
    }
}
