using DogsHouseService.BLL.DTOs.Common;
using DogsHouseService.DAL.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsHouseService.BLL.Extensions
{
    public static class SortingParamsExtension
    {
        public static SortingParams ToSortingParams(this SortingParamsDto sortingParamsDto)
        {
            return new SortingParams
            {
                Attribute = sortingParamsDto.Attribute,
                SortingOrder = sortingParamsDto.SortingOrder,
            };
        }
    }
}
