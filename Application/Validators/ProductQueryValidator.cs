using Application.Filters;
using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class ProductQueryValidator:AbstractValidator<ProductParameters>
    {
        private string[] SortByColumnName = { nameof(Product.Name), nameof(Product.Price), };
        public ProductQueryValidator()
        {
            RuleFor(c => c.SortBy).Must(value => string.IsNullOrEmpty(value) || SortByColumnName.Contains(value))
                .WithMessage($"Sort by is optional, or must be in [{string.Join(",", SortByColumnName)}]");
        }
    }
}
