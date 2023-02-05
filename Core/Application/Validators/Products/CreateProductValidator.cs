using Application.ViewModels.Products;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.Products
{
    public class CreateProductValidator : AbstractValidator<VM_Create_Product>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Please enter a product name")
                .MaximumLength(150)
                .MinimumLength(4)
                    .WithMessage("Please Enter a Name shorter then 150 characters and longer then 4.");
            RuleFor(x => x.Price)
                .NotNull()
                .NotEmpty()
                    .WithMessage("Price cant be emtpy")
                .Must(s => s >= 0)
                .WithMessage("Please enter a positive value");

            RuleFor(x => x.Stock)
                .NotNull()
                .NotEmpty()
                    .WithMessage("Stock cant be emtpy")
                .Must(s => s >= 0)
                .WithMessage("Please enter a positive value");
        }
    }
}
