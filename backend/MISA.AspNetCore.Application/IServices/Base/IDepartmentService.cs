﻿using MISA.AspNetCore.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AspNetCore.Application
{
    public interface IDepartmentService : IBaseService<DepartmentDto, DepartmentInsertDto, DepartmentUpdateDto>
    {
    }
}
