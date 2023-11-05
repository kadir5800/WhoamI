using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text.RegularExpressions;
using WhoamI.Business.Contracts.DTO.Client;
using WhoamI.Business.Contracts.DTO.DataTable;
using WhoamI.Business.Contracts.DTO.User;
using WhoamI.Business.Contracts.IManager;
using WhoamI.Business.Localization;
using WhoamI.Data.Contracts.Repositories;
using WhoamI.Data.EntityFrameworkCore;
using WhoamI.Data.Entitys.Objects;

namespace WhoamI.Business.Managers
{
    public class UserManager : BaseManager, IUserManager
    {
        private readonly IUsersRepository _usersRepository;
        private readonly WhoamIDbContext _dbContext;
        public UserManager(IUsersRepository usersRepository, WhoamIDbContext dbContext)
        {
            _usersRepository = usersRepository;
            _dbContext = dbContext;
        }

        public async Task<ClientResult> addUser(addUserRequest request)
        {

            if (request == null || String.IsNullOrEmpty(request.Surname) || String.IsNullOrEmpty(request.Name) || String.IsNullOrEmpty(request.Email))
                return Error(message: BusinesLocalization.FillRequiredFields, code: 402);


            var existingProvince = _usersRepository.FirstOrDefault(t => t.Name == request.Name && t.Surname == request.Surname && t.Email == request.Email);


            if (existingProvince != null)
                return Error(message: BusinesLocalization.sameRecordAvailable, code: 402);

            await _usersRepository.InsertAsync(new User()
            {
                Surname = request.Surname,
                Name = request.Name,
                Email = request.Email,
                CreationDate = DateTime.Now,
                IsDeleted = false
            }, true);

            return Success(message: BusinesLocalization.InsertSuccess, code: 200);

        }

        public async Task<ClientResult> deleteUser(getOneRequest request)
        {
            if (request == null || request.Id <= 0)
                return Error(message: BusinesLocalization.FillRequiredFields, code: 402);

            var existingProvince = _usersRepository.FirstOrDefault(t => !t.IsDeleted && t.Id == request.Id);

            if (existingProvince == null)
                return Error(message: BusinesLocalization.NotFound, code: 404);

            existingProvince.IsDeleted = true;

            await _usersRepository.UpdateAsync(existingProvince, true);
            return Success(message: BusinesLocalization.DeleteSuccess, code: 200);
        }

        public async Task<ClientResult<getAllUserResponse>> getAllUser(dataTableRequest request)
        {
            try
            {
                if (request == null)
                    return Error<getAllUserResponse>(message: BusinesLocalization.FillRequiredFields, code: 402);

                int pageSize = request.Length != null ? Convert.ToInt32(request.Length) : 0;
                int skip = request.Start != null ? Convert.ToInt32(request.Start) : 0;

                int recordsTotal = 0;

                recordsTotal = _usersRepository.Where(w => w.IsDeleted == false).Count();

                var takeA = request.Length == "-1" ? recordsTotal : pageSize;
                takeA = takeA == 0 ? 10 : takeA;

                var sqlQuery = $@"SELECT [t0].* FROM [User] AS [t0] Where [t0].[IsDeleted] = 0 AND ([t0].[Name] +' '+ [t0].[Surname]) LIKE '%{request.SearchValue}%' ORDER BY [t0].[{request.SortColumn}] {request.SortColumnDir} OFFSET {skip} ROWS FETCH NEXT {takeA} ROWS ONLY";

                var query = await _dbContext.users
                .FromSqlRaw(sqlQuery)
                .Select(u => new getOneUserResponse()
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    Surname = u.Surname,
                    CreationDate = DateTime.Parse(u.CreationDate.ToString()).ToString(),
                }).ToListAsync();

                var response = new getAllUserResponse()
                {
                    draw = request.Draw,
                    recordsFiltered = recordsTotal,
                    recordsTotal = recordsTotal,
                    data = query
                };

                return Success(message: BusinesLocalization.Success, data: response);
            }
            catch (Exception ex)
            {
                return Success<getAllUserResponse>(message: ex.Message, code: 500);
            }
        }

        public async Task<ClientResult<getOneUserResponse>> getOneUser(getOneRequest request)
        {
            if (request == null || request.Id <= 0)
                return Error<getOneUserResponse>(message: BusinesLocalization.FillRequiredFields, code: 402);

            var existingBank = _usersRepository.Where(t => !t.IsDeleted && t.Id == request.Id).Select(s => new getOneUserResponse
            {
                Id = s.Id,
                Name = s.Name,
                Email = s.Email,
                Surname = s.Surname,
                CreationDate = s.CreationDate.ToString(),
            }).FirstOrDefault();

            if (existingBank == null)
                return Error<getOneUserResponse>(message: BusinesLocalization.NotFound, code: 404);

            return Success<getOneUserResponse>(message: BusinesLocalization.Success, code: 200, data: existingBank);
        }

        public async Task<ClientResult> updateUser(updateUserRequest request)
        {
            if (request == null || request.Id <= 0)
                return Error(message: BusinesLocalization.FillRequiredFields, code: 402);

            var existingUser = _usersRepository.FirstOrDefault(t => !t.IsDeleted && t.Id == request.Id);
            if (existingUser == null)
                return Error(message: BusinesLocalization.NotFound, code: 404);
            
            existingUser.Id = request.Id;
            existingUser.Name = request.Name;
            existingUser.Email = request.Email;
            existingUser.Surname = request.Surname;

            await _usersRepository.UpdateAsync(existingUser, true);

            return Success(message: BusinesLocalization.UpdateSuccess, code: 200);
        }
    }
}
