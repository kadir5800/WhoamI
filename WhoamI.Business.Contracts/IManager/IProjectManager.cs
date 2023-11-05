using WhoamI.Business.Contracts.DTO.Project;
using WhoamI.Business.Contracts.DTO.Client;
using WhoamI.Business.Contracts.DTO.DataTable;

namespace WhoamI.Business.Contracts.IManager
{
    public interface IProjectManager
    {
        Task<ClientResult<getAllProjectResponse>> getAllProject(dataTableRequest request);
        Task<ClientResult<getOneProjectResponse>> getOneProject(getOneRequest request);
        Task<ClientResult> addProject(addProjectRequest request);
        Task<ClientResult> updateProject(updateProjectRequest request);
        Task<ClientResult> deleteProject(getOneRequest request);
    }
}
