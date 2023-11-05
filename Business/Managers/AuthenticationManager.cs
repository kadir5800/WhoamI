using WhoamI.Business.Contracts.DTO.Authentication;
using WhoamI.Business.Contracts.DTO.Client;
using WhoamI.Business.Contracts.IManager;
using WhoamI.Business.Localization;
using WhoamI.Data.EntityFrameworkCore;

namespace WhoamI.Business.Managers
{
    public class AuthenticationManager : BaseManager, IAuthenticationManager
    {
        private readonly WhoamIDbContext _dbContext;
        public AuthenticationManager(WhoamIDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ClientResult> login(loginRequest request)
        {
            if (request == null || String.IsNullOrEmpty(request.userName) || String.IsNullOrEmpty(request.password))
                return Error(message: BusinesLocalization.FillRequiredFields, code: 402);

            var existingBank = _dbContext.admins.FirstOrDefault(t => t.Username == request.userName && t.Password == request.password);

            if (existingBank == null)
                return Error(message: BusinesLocalization.NotFound, code: 404);

            return Success(message: BusinesLocalization.Success, code: 200);
        }
    }
}
