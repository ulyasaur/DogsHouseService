using DogsHouseService.BLL.DTOs;
using DogsHouseService.BLL.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsHouseService.BLL.Abstractions
{
    public interface IDogsService
    {
        Task AddDogAsync(DogDto dogDto);

        Task<List<DogDto>> GetDogsAsync(PagingParamsDto pagingParamsDto, SortingParamsDto sortingParamsDto);
    }
}
