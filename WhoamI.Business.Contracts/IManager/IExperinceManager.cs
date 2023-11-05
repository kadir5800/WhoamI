using WhoamI.Business.Contracts.DTO.Experince;
using WhoamI.Business.Contracts.DTO.Client;
using WhoamI.Business.Contracts.DTO.DataTable;

namespace WhoamI.Business.Contracts.IManager
{
    public interface IExperinceManager
    {
        Task<ClientResult<getAllExperinceResponse>> getAllExperince(dataTableRequest request);
        Task<ClientResult<getOneExperinceResponse>> getOneExperince(getOneRequest request);
        Task<ClientResult> addExperince(addExperinceRequest request);
        Task<ClientResult> updateExperince(updateExperinceRequest request);
        Task<ClientResult> deleteExperince(getOneRequest request);
    }
}
