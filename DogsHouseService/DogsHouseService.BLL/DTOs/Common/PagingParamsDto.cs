using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsHouseService.BLL.DTOs.Common
{
    public class PagingParamsDto
    {
        public int? PageNumber { get; set; }

        public int? PageSize { get; set; }
    }
}
