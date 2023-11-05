using WhoamI.Business.Contracts.DTO.ProjectImage;
using WhoamI.Business.Contracts.DTO.Client;
using WhoamI.Business.Contracts.DTO.DataTable;

namespace WhoamI.Business.Contracts.IManager
{
    public interface IProjectImageManager
    {
        Task<ClientResult<getAllProjectImageResponse>> getAllProjectImage(dataTableRequest request);
        Task<ClientResult<getOneProjectImageResponse>> getOneProjectImage(getOneRequest request);
        Task<ClientResult> addProjectImage(addProjectImageRequest request);
        Task<ClientResult> updateProjectImage(updateProjectImageRequest request);
        Task<ClientResult> deleteProjectImage(getOneRequest request);
    }
}
