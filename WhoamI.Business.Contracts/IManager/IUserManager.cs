using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoamI.Business.Contracts.DTO.Client;
using WhoamI.Business.Contracts.DTO.DataTable;
using WhoamI.Business.Contracts.DTO.User;

namespace WhoamI.Business.Contracts.IManager
{
    public interface IUserManager
    {
        Task<ClientResult<getAllUserResponse>> getAllUser(dataTableRequest request);
        Task<ClientResult<getOneUserResponse>> getOneUser(getOneRequest request);
        Task<ClientResult> addUser(addUserRequest request);
        Task<ClientResult> updateUser(updateUserRequest request);
        Task<ClientResult> deleteUser(getOneRequest request);
    }
}
