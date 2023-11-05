using WhoamI.Business.Contracts.DTO.Portfolio;
using WhoamI.Business.Contracts.DTO.Client;
using WhoamI.Business.Contracts.DTO.DataTable;

namespace WhoamI.Business.Contracts.IManager
{
    public interface IPortfolioManager
    {
        Task<ClientResult<getAllPortfolioResponse>> getAllPortfolio(dataTableRequest request);
        Task<ClientResult<getOnePortfolioResponse>> getOnePortfolio(getOneRequest request);
        Task<ClientResult> addPortfolio(addPortfolioRequest request);
        Task<ClientResult> updatePortfolio(updatePortfolioRequest request);
        Task<ClientResult> deletePortfolio(getOneRequest request);
    }
}
