using DTO;
using Entity.Model;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Application
{
    public class CustomerValidator : AbstractValidator<CustomerDto>
    {
        private readonly DbSet<Customer> _customers;
        public CustomerValidator(DbSet<Customer> customers)
        {
            _customers = customers;

            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x).MustAsync((x, y) => FirstNameLastNameDateOfBirthMustUniq(x))
                .WithMessage("FirstName, lastName and date of birth is not uniq.");
            RuleFor(x => x).MustAsync((x, y) => EmailMustUniq(x)).WithMessage("Email is not uniq.");
            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .NotNull().WithMessage("Phone number is required.")
                .MinimumLength(10).WithMessage("Phone number must not be less than 10 characters.")
                .MaximumLength(20).WithMessage("Phone number must not exceed 20 characters.")
                .Matches(new Regex(@"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}")).WithMessage("Phone number not valid");
            RuleFor(x => x.Email)
                .NotEmpty()
                .Matches(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$")
                .WithMessage("Email not valid");
        }

        private async Task<bool> FirstNameLastNameDateOfBirthMustUniq(CustomerDto customer)
        {
            return !await _customers
                .AnyAsync(x => x.FirstName == customer.FirstName && x.LastName == customer.LastName && x.DateOfBirth == customer.DateOfBirth && x.Id != customer.Id);
        }

        private async Task<bool> EmailMustUniq(CustomerDto customer)
        {
            return !await _customers.AnyAsync(x =>x.Email== customer.Email && x.Id != customer.Id);
        }
    }
}
