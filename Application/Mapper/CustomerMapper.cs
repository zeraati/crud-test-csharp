using DTO;
using Entity.Model;

namespace Application.Mapper
{
    public class CustomerMapper
    {
        public static Customer Map(CustomerDto dto, Customer model = null)
        {
            model ??= new Customer();
            model.Id = dto.Id;
            model.FirstName = dto.FirstName;
            model.LastName = dto.LastName;
            model.PhoneNumber = dto.PhoneNumber;
            model.DateOfBirth = dto.DateOfBirth;
            model.Email = dto.Email;
            model.BankAccountNumber = dto.BankAccountNumber;
            return model;
        }

        public static CustomerDto Map(Customer model)
        {
            return new CustomerDto
            {

                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                FullName = $"{model.FirstName} {model.LastName}",
                PhoneNumber = model.PhoneNumber,
                DateOfBirth = model.DateOfBirth,
                Email = model.Email,
                BankAccountNumber = model.BankAccountNumber,
            };
        }
    }
}