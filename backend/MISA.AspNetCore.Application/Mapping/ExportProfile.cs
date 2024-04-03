using AutoMapper;
using MISA.AspNetCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AspNetCore.Application
{
    public class ExportProfile : Profile
    {
        public ExportProfile()
        {
            CreateMap<Export, EmployeeExportDto>();
        }
    }
}
