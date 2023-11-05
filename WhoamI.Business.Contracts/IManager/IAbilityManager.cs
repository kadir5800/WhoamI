using WhoamI.Business.Contracts.DTO.Ability;
using WhoamI.Business.Contracts.DTO.Client;
using WhoamI.Business.Contracts.DTO.DataTable;

namespace WhoamI.Business.Contracts.IManager
{
    public interface IAbilityManager
    {
        Task<ClientResult<getAllAbilityResponse>> getAllAbility(dataTableRequest request);
        Task<ClientResult<getOneAbilityResponse>> getOneAbility(getOneRequest request);
        Task<ClientResult> addAbility(addAbilityRequest request);
        Task<ClientResult> updateAbility(updateAbilityRequest request);
        Task<ClientResult> deleteAbility(getOneRequest request);
    }
}
