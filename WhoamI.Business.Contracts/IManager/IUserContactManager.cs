using WhoamI.Business.Contracts.DTO.UserContact;
using WhoamI.Business.Contracts.DTO.Client;
using WhoamI.Business.Contracts.DTO.DataTable;

namespace WhoamI.Business.Contracts.IManager
{
    public interface IUserContactManager
    {
        Task<ClientResult<getAllUserContactResponse>> getAllUserContact(dataTableRequest request);
        Task<ClientResult<getOneUserContactResponse>> getOneUserContact(getOneRequest request);
        Task<ClientResult> addUserContact(addUserContactRequest request);
        Task<ClientResult> updateUserContact(updateUserContactRequest request);
        Task<ClientResult> deleteUserContact(getOneRequest request);
    }
}
