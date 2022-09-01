using DTO;
using System.Net;
using FluentValidation;

namespace Application.Infra
{
    public class Validation
    {
        public static ServiceResponseDto Validator(IValidator validator, object model)
        {
            var response= new ServiceResponseDto { };

            var result = validator.Validate(new ValidationContext<object>(model));
            if (result.IsValid) return new ServiceResponseDto { };

            var errors = result.Errors.GroupBy(x => x.PropertyName).Select(x => new 
            {
                PropertyName=x.Key,
                Errors=string.Join(",",result.Errors.Where(y => y.PropertyName == x.Key).Select(y => y.ErrorMessage))
            }).ToList();

            response.StatusCode= (int)HttpStatusCode.BadRequest;
            response.Errors = errors;

            return response;
        }
    }
}
