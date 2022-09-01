using DTO;
using Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]"), ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService) => _customerService = customerService;

        [HttpPost]
        public async Task<ServiceResponseDto> Add(CustomerDto dto) => await _customerService.Add(dto);

        [HttpPut]
        public async Task<ServiceResponseDto> Edit(CustomerDto dto) => await _customerService.Edit(dto);

        [HttpDelete("{id}")]
        public async Task<ServiceResponseDto> Delete(long id) => await _customerService.Delete(id);


        [HttpGet("{id}")]
        public async Task<ServiceResponseDto> GetById(long id) => await _customerService.GetById(id);


        [HttpGet("All")]
        public async Task<ServiceResponseDto> GetAll() => await _customerService.GetAll();
    }
}