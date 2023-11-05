using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoamI.Business.Contracts.DTO.Client;
using WhoamI.Business.Contracts.IManager;
using WhoamI.Core.UnitOfWork;

namespace WhoamI.Business.Managers
{
    public abstract class BaseManager: IBaseManager
    {

        protected ClientResult Success(string message = default(string), object data = default(object), int code = 200)
        {
            var cr = new ClientResult
            {
                Success = true,
                Code = code,
                MessageType = GetMessageType(code),
                Message = message,
                Data = data
            };

            return cr;
        }

        protected ClientResult<T> Success<T>(string message = default(string), T data = default(T), int code = 200)
        {
            var cr = new ClientResult<T>
            {
                Success = true,
                Code = code,
                MessageType = GetMessageType(code),
                Message = message,
                Data = data
            };

            return cr;
        }

        protected ClientResult Error(string message = default(string), string internalMessage = default(string), object data = default(object), int code = 500)
        {
            var cr = new ClientResult
            {
                Success = false,
                Code = code,
                MessageType = GetMessageType(code),
                Message = message,
                InternalMessage = internalMessage,
                Data = data
            };

            return cr;
        }

        protected ClientResult<T> Error<T>(string message = default(string), string internalMessage = default(string), T data = default(T), int code = 500)
        {
            var cr = new ClientResult<T>
            {
                Success = false,
                Code = code,
                MessageType = GetMessageType(code),
                Message = message,
                InternalMessage = internalMessage,
                Data = data
            };

            return cr;
        }

        protected ClientResultMessageType GetMessageType(int code)
        {
            switch (code)
            {
                case 200:
                    return ClientResultMessageType.Success;
                case 400:
                    return ClientResultMessageType.Warning;
                case 401:
                    return ClientResultMessageType.Error;
                case 402:
                    return ClientResultMessageType.Warning;
                case 403:
                    return ClientResultMessageType.Error;
                case 404:
                    return ClientResultMessageType.Error;
                case 500:
                    return ClientResultMessageType.Error;
                default:
                    return ClientResultMessageType.Success;
            }
        }
    }
}
