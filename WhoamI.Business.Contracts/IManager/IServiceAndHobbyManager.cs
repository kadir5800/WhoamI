using WhoamI.Business.Contracts.DTO.ServiceAndHobby;
using WhoamI.Business.Contracts.DTO.Client;
using WhoamI.Business.Contracts.DTO.DataTable;

namespace WhoamI.Business.Contracts.IManager
{
    public interface IServiceAndHobbyManager
    {
        Task<ClientResult<getAllServiceAndHobbyResponse>> getAllServiceAndHobby(dataTableRequest request);
        Task<ClientResult<getOneServiceAndHobbyResponse>> getOneServiceAndHobby(getOneRequest request);
        Task<ClientResult> addServiceAndHobby(addServiceAndHobbyRequest request);
        Task<ClientResult> updateServiceAndHobby(updateServiceAndHobbyRequest request);
        Task<ClientResult> deleteServiceAndHobby(getOneRequest request);
    }
}
