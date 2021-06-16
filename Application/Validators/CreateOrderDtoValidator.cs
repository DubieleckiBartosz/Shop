using Application.DtoModels.OrderModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class CreateOrderDtoValidator:AbstractValidator<CreateOrderDto>
    {
        public CreateOrderDtoValidator()
        {
            //RuleFor(c => c.Line).Must(s => s.Count() >= 0).WithMessage("Order must have some products");//?
            RuleForEach(c => c.Line).NotNull().SetValidator(new CreateOrderLineValidator());
            RuleFor(c => c.City).NotNull().MaximumLength(50);
            RuleFor(c => c.PostalCode).NotNull();
            RuleFor(c=>c.Street).NotNull().MaximumLength(50); 
        }
    }
}
