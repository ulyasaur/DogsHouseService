﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsHouseService.DAL.Models.Common
{
    public class PagingParams
    {
        public int? PageNumber { get; set; } = 1;

        public int? PageSize { get; set; } = 10;
    }
}
