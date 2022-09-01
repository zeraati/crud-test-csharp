using DTO;

namespace Application.Interface
{
    public interface ICustomerService
    {
        Task<ServiceResponseDto> Add(CustomerDto dto);
        Task<ServiceResponseDto> Edit(CustomerDto dto);
        Task<ServiceResponseDto> Delete(long id);
        Task<ServiceResponseDto> GetById(long id);
        Task<ServiceResponseDto> GetAll();
    }
}
