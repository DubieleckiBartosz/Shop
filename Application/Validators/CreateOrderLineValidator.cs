using Application.DtoModels.OrderLineModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class CreateOrderLineValidator:AbstractValidator<CreateOrderLineDto>
    {
        public CreateOrderLineValidator()
        {
            RuleFor(s => s.Quantity).InclusiveBetween(1, 100);
        }
    }
}
