using DogsHouseService.BLL.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsHouseService.BLL.Services.Validators
{
    public class DogValidator : AbstractValidator<DogDto>
    {
        public DogValidator() 
        { 
            RuleFor(d => d.Name).NotEmpty();
            RuleFor(d => d.Color).NotEmpty();
            RuleFor(d => d.TailLength).GreaterThan(0);
            RuleFor(d => d.Weight).GreaterThan(0);
        }
    }
}
