using WhoamI.Business.Contracts.DTO.SocialMedia;
using WhoamI.Business.Contracts.DTO.Client;
using WhoamI.Business.Contracts.DTO.DataTable;

namespace WhoamI.Business.Contracts.IManager
{
    public interface ISocialMediaManager
    {
        Task<ClientResult<getAllSocialMediaResponse>> getAllSocialMedia(dataTableRequest request);
        Task<ClientResult<getOneSocialMediaResponse>> getOneSocialMedia(getOneRequest request);
        Task<ClientResult> addSocialMedia(addSocialMediaRequest request);
        Task<ClientResult> updateSocialMedia(updateSocialMediaRequest request);
        Task<ClientResult> deleteSocialMedia(getOneRequest request);
    }
}
