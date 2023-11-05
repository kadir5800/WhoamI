using WhoamI.Business.Contracts.DTO.Article;
using WhoamI.Business.Contracts.DTO.Client;
using WhoamI.Business.Contracts.DTO.DataTable;

namespace WhoamI.Business.Contracts.IManager
{
    public interface IArticleManager
    {
        Task<ClientResult<getAllArticleResponse>> getAllArticle(dataTableRequest request);
        Task<ClientResult<getOneArticleResponse>> getOneArticle(getOneRequest request);
        Task<ClientResult> addArticle(addArticleRequest request);
        Task<ClientResult> updateArticle(updateArticleRequest request);
        Task<ClientResult> deleteArticle(getOneRequest request);
    }
}
