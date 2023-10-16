using DogsHouseService.BLL.DTOs;
using DogsHouseService.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsHouseService.BLL.Extensions
{
    public static class DogExtensions
    {
        public static DogDto ToDogDto(this Dog dog)
        {
            return new DogDto
            {
                Name = dog.Name,
                Color = dog.Color,
                TailLength = dog.TailLength,
                Weight = dog.Weight,
            };
        }

        public static Dog ToDog(this DogDto dog)
        {
            return new Dog
            {
                Name = dog.Name,
                Color = dog.Color,
                TailLength = dog.TailLength,
                Weight = dog.Weight,
            };
        }
    }
}
