using Application.IdentityModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class UserRegisterValidator:AbstractValidator<RegisterModel>
    {
        public UserRegisterValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Name is required");
            RuleFor(c => c.LastName).NotEmpty().WithMessage("Last name is required");
            RuleFor(s => s.UserName).NotEmpty().WithMessage("Field UserName is required");
            RuleFor(c => c.ConfirmPassword).Equal(x => x.Password).WithMessage("Your passwords are diffrent");
            RuleFor(x => x.Email).NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("Invalid email format.");

            RuleFor(c => c.Password).NotEmpty()
                    .MinimumLength(6)
                    .Matches("[A-Z]")
                    .Matches("[a-z]")
                    .Matches("[0-9]")
                    .Matches("[^a-zA-Z0-9]");
        }
    }
}
