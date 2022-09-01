using DTO;
using System.Net;
using Entity.Model;
using Application.Infra;
using Application.Mapper;
using Application.Interface;
using Microsoft.EntityFrameworkCore;

namespace Application.Service
{
    public class CustomerService:ICustomerService
    {
        private readonly ApplicationDbContext db;
        public CustomerService(ApplicationDbContext db) => this.db = db;

        public async Task<ServiceResponseDto> Add(CustomerDto dto)
        {
            var validation = Validation.Validator(new CustomerValidator(db.Customer),dto);
            if (validation.StatusCode == (int)HttpStatusCode.BadRequest) return validation;

            var person = CustomerMapper.Map(dto);
            db.Customer.Add(person);
            await db.SaveChangesAsync();

            dto.Id = person.Id;
            return new ServiceResponseDto { Message = "Added successfully." };
        }

        public async Task<ServiceResponseDto> Edit(CustomerDto dto)
        {
            var validation = Validation.Validator(new CustomerValidator(db.Customer), dto);
            if (validation.StatusCode == (int)HttpStatusCode.BadRequest) return validation;

            var person = await db.Customer.FirstAsync(x => x.Id == dto.Id);
            CustomerMapper.Map(dto, person);
            db.Customer.Update(person);
            await db.SaveChangesAsync();

            return new ServiceResponseDto { Message = "Edited successfully." };
        }

        public async Task<ServiceResponseDto> Delete(long id)
        {
            var person = await db.Customer.FindAsync(id);
            db.Remove(person);
            await db.SaveChangesAsync();
            return new ServiceResponseDto { Message = "Deleted successfully." };
        }

        public async Task<ServiceResponseDto> GetById(long id)
        {
            var person = await db.Customer.FindAsync(id);
            var dto = CustomerMapper.Map(person);
            return new ServiceResponseDto { Data = dto };
        }

        public async Task<ServiceResponseDto> GetAll()
        {
            var persons = await db.Customer.Select(x => CustomerMapper.Map(x)).ToListAsync();
            return new ServiceResponseDto { Data = persons };
        }
    }
}
