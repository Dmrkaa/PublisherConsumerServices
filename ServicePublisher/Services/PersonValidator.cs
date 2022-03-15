using FluentValidation;
using ServicePublisher.Models;
using Shared.Models;

namespace ServicePublisher.Services
{
    public class PersonValidator : AbstractValidator<UserSharedModel>
    {
        public PersonValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.LastName).NotEmpty();            
        }
    }
}
