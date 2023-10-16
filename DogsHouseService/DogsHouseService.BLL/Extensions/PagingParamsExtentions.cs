using DogsHouseService.BLL.DTOs.Common;
using DogsHouseService.DAL.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsHouseService.BLL.Extensions
{
    public static class PagingParamsExtentions
    {
        public static PagingParams ToPagingParams(this PagingParamsDto pagingParamsDto)
        {
            return new PagingParams
            {
                PageSize = pagingParamsDto.PageSize,
                PageNumber = pagingParamsDto.PageNumber,
            };
        }
    }
}
