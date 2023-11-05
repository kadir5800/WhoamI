using WhoamI.Business.Contracts.DTO.Testimonial;
using WhoamI.Business.Contracts.DTO.Client;
using WhoamI.Business.Contracts.DTO.DataTable;

namespace WhoamI.Business.Contracts.IManager
{
    public interface ITestimonialManager
    {
        Task<ClientResult<getAllTestimonialResponse>> getAllTestimonial(dataTableRequest request);
        Task<ClientResult<getOneTestimonialResponse>> getOneTestimonial(getOneRequest request);
        Task<ClientResult> addTestimonial(addTestimonialRequest request);
        Task<ClientResult> updateTestimonial(updateTestimonialRequest request);
        Task<ClientResult> deleteTestimonial(getOneRequest request);
    }
}
