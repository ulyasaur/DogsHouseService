using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsHouseService.BLL.DTOs.Common
{
    public class SortingParamsDto
    {
        public string? Attribute { get; set; }

        public string? SortingOrder { get; set; }
    }
}
