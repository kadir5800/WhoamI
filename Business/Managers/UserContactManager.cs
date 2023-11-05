using Microsoft.EntityFrameworkCore;
using WhoamI.Business.Contracts.DTO.Client;
using WhoamI.Business.Contracts.DTO.DataTable;
using WhoamI.Business.Contracts.DTO.UserContact;
using WhoamI.Business.Contracts.IManager;
using WhoamI.Business.Localization;
using WhoamI.Data.Contracts.Repositories;
using WhoamI.Data.EntityFrameworkCore;
using WhoamI.Data.Entitys.Objects;

namespace WhoamI.Business.Managers
{
    public class UserContactManager : BaseManager, IUserContactManager
    {
        private readonly IUserContactRepository _UserContactRepository;
        private readonly WhoamIDbContext _dbContext;
        public UserContactManager(IUserContactRepository UserContactRepository, WhoamIDbContext dbContext)
        {
            _UserContactRepository = UserContactRepository;
            _dbContext = dbContext;
        }

        public async Task<ClientResult> addUserContact(addUserContactRequest request)
        {

            if (request == null || String.IsNullOrEmpty(request.Address) || String.IsNullOrEmpty(request.Phone))
                return Error(message: BusinesLocalization.FillRequiredFields, code: 402);


            var existingProvince = _UserContactRepository.FirstOrDefault(t => t.Address == request.Address && t.AboutMe == request.AboutMe && t.UserId == request.UserId);


            if (existingProvince != null)
                return Error(message: BusinesLocalization.sameRecordAvailable, code: 402);

            await _UserContactRepository.InsertAsync(new UserContact()
            {
                UserId = request.UserId,
                Address = request.Address,
                City = request.City,
                Region = request.Region,
                PostalCode = request.PostalCode,
                Country = request.Country,
                Phone = request.Phone,
                AboutMe = request.AboutMe,
                CreationDate = DateTime.Now,
                IsDeleted = false
            }, true);

            return Success(message: BusinesLocalization.InsertSuccess, code: 200);

        }

        public async Task<ClientResult> deleteUserContact(getOneRequest request)
        {
            if (request == null || request.Id <= 0)
                return Error(message: BusinesLocalization.FillRequiredFields, code: 402);

            var existingProvince = _UserContactRepository.FirstOrDefault(t => !t.IsDeleted && t.Id == request.Id);

            if (existingProvince == null)
                return Error(message: BusinesLocalization.NotFound, code: 404);

            existingProvince.IsDeleted = true;

            await _UserContactRepository.UpdateAsync(existingProvince, true);
            return Success(message: BusinesLocalization.DeleteSuccess, code: 200);
        }

        public async Task<ClientResult<getAllUserContactResponse>> getAllUserContact(dataTableRequest request)
        {
            try
            {
                if (request == null)
                    return Error<getAllUserContactResponse>(message: BusinesLocalization.FillRequiredFields, code: 402);

                int pageSize = request.Length != null ? Convert.ToInt32(request.Length) : 0;
                int skip = request.Start != null ? Convert.ToInt32(request.Start) : 0;

                int recordsTotal = 0;

                recordsTotal = _UserContactRepository.Where(w => w.IsDeleted == false).Count();

                var takeA = request.Length == "-1" ? recordsTotal : pageSize;
                takeA = takeA == 0 ? 10 : takeA;


                var userSql = "";
                if (request.UserId > 0)
                {
                    userSql = $" AND [t0].[UserId]= {request.UserId}";
                }

                var sqlQuery = $@"SELECT [t0].* FROM [UserContact] AS [t0] LIKE Where [t0].[IsDeleted] = 0 {userSql} AND [t0].[Phone] '%{request.SearchValue}%' ORDER BY [t0].[{request.SortColumn}] {request.SortColumnDir} OFFSET {skip} ROWS FETCH NEXT {takeA} ROWS ONLY";

                var query = await _dbContext.userContacts
                .FromSqlRaw(sqlQuery)
                .Select(u => new getOneUserContactResponse()
                {
                    Id = u.Id,
                    UserId = u.UserId,
                    Address = u.Address,
                    City = u.City,
                    Region = u.Region,
                    PostalCode = u.PostalCode,
                    Country = u.Country,
                    Phone = u.Phone,
                    AboutMe = u.AboutMe,
                    CreationDate = DateTime.Parse(u.CreationDate.ToString()).ToString(),
                }).ToListAsync();

                var response = new getAllUserContactResponse()
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
                return Success<getAllUserContactResponse>(message: ex.Message, code: 500);
            }
        }

        public async Task<ClientResult<getOneUserContactResponse>> getOneUserContact(getOneRequest request)
        {
            if (request == null || request.Id <= 0)
                return Error<getOneUserContactResponse>(message: BusinesLocalization.FillRequiredFields, code: 402);

            var existingBank = _UserContactRepository.Where(t => !t.IsDeleted && t.Id == request.Id).Select(s => new getOneUserContactResponse
            {
                Id = s.Id,
                UserId = s.UserId,
                Address = s.Address,
                City = s.City,
                Region = s.Region,
                PostalCode = s.PostalCode,
                Country = s.Country,
                Phone = s.Phone,
                AboutMe = s.AboutMe,
                CreationDate = DateTime.Parse(s.CreationDate.ToString()).ToString(),
            }).FirstOrDefault();

            if (existingBank == null)
                return Error<getOneUserContactResponse>(message: BusinesLocalization.NotFound, code: 404);

            return Success<getOneUserContactResponse>(message: BusinesLocalization.Success, code: 200, data: existingBank);
        }

        public async Task<ClientResult> updateUserContact(updateUserContactRequest request)
        {
            if (request == null || request.Id <= 0)
                return Error(message: BusinesLocalization.FillRequiredFields, code: 402);

            var existingUserContact = _UserContactRepository.FirstOrDefault(t => !t.IsDeleted && t.Id == request.Id);
            if (existingUserContact == null)
                return Error(message: BusinesLocalization.NotFound, code: 404);

            existingUserContact.Address = request.Address;
            existingUserContact.City = request.City;
            existingUserContact.Region = request.Region;
            existingUserContact.PostalCode = request.PostalCode;
            existingUserContact.Country = request.Country;
            existingUserContact.Phone = request.Phone;
            existingUserContact.AboutMe = request.AboutMe;
            existingUserContact.UserId = request.UserId;

            await _UserContactRepository.UpdateAsync(existingUserContact, true);

            return Success(message: BusinesLocalization.UpdateSuccess, code: 200);
        }
    }
}
