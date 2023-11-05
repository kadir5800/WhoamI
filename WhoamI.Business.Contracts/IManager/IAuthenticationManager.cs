using WhoamI.Business.Contracts.DTO.Authentication;
using WhoamI.Business.Contracts.DTO.Client;

namespace WhoamI.Business.Contracts.IManager
{
    public interface IAuthenticationManager
    {
        Task<ClientResult> login(loginRequest request);
    }
}
