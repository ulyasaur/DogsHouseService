using DogsHouseService.BLL.Abstractions;
using DogsHouseService.BLL.DTOs;
using DogsHouseService.BLL.DTOs.Common;
using DogsHouseService.BLL.Extensions;
using DogsHouseService.BLL.Services.Validators;
using DogsHouseService.DAL.Abstractions;
using DogsHouseService.DAL.Models;
using DogsHouseService.DAL.Models.Common;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsHouseService.BLL.Services
{
    public class DogsService : IDogsService
    {
        private readonly IDogsRepository _dogsRepository;

        private readonly IValidator<DogDto> _dogValidator;

        private readonly ILogger<DogsService> _logger;

        public DogsService(IDogsRepository dogsRepository,
            IValidator<DogDto> dogValidator,
            ILogger<DogsService> logger)
        {
            _dogsRepository = dogsRepository;
            _dogValidator = dogValidator;
            _logger = logger;
        }

        public async Task AddDogAsync(DogDto dogDto)
        {
            ValidationResult validationResult = _dogValidator.Validate(dogDto);

            if (!validationResult.IsValid)
            {
                throw new ArgumentException("The provided dog is not valid");
            }

            try
            {
                Dog existingDog = await _dogsRepository.FindDogByExpressionAsync(d => d.Name.ToLower() == dogDto.Name.ToLower());

                if (existingDog is not null)
                {
                    throw new InvalidOperationException("The dog with such name already exists");
                }

                Dog dog = dogDto.ToDog();

                await _dogsRepository.InsertDogAsync(dog);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "The dog with name {Name} could not be created", dogDto.Name);
                throw;
            }
        }

        public async Task<List<DogDto>> GetDogsAsync(PagingParamsDto pagingParamsDto, SortingParamsDto sortingParamsDto)
        {
            try
            {
                SortingParams sortingParams = sortingParamsDto.ToSortingParams();
                PagingParams pagingParams = pagingParamsDto.ToPagingParams();

                List<Dog> dogs = await _dogsRepository.GetDogsAsync(pagingParams, sortingParams);
                List<DogDto> dogDtos = dogs.ConvertAll(d => d.ToDogDto());

                return dogDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "The list of dogs could not be retrieved");
                throw;
            }
        }
    }
}
