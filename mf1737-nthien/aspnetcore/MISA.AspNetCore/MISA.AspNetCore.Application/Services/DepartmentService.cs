using AutoMapper;
using MISA.AspNetCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AspNetCore.Application
{
    public class DepartmentService : BaseService<Department, DepartmentDto, DepartmentInsertDto, DepartmentUpdateDto>, IDepartmentService
    {
        private readonly IDepartmentValidate _departmentValidate;
        private readonly IMapper _mapper;        

        public DepartmentService(IDepartmentRepository departmentRepository, IDepartmentValidate departmentValidate, IMapper mapper) : base(departmentRepository)
        {
            _departmentValidate = departmentValidate;
            _mapper = mapper;
        }

        /// <summary>
        /// Chuyển đổi từ Department sang DepartmentDto
        /// </summary>
        /// CreatedBy: hiennt200210 (16/09/2023)
        public override DepartmentDto MapEntityToDto(Department department)
        {
            var departmentDto = _mapper.Map<DepartmentDto>(department);
            return departmentDto;
        }

        /// <summary>
        /// Chuyển đổi từ DepartmentInsertDto sang Department
        /// </summary>
        /// CreatedBy: hiennt200210 (20/09/2023)
        public override Department MapInsertDtoToEntity(DepartmentInsertDto insertDto)
        {
            var department = _mapper.Map<Department>(insertDto);

            // Sinh DepartmentId mới
            department.DepartmentId = Guid.NewGuid();

            // Thêm thông tin ngày tạo, người tạo, ngày sửa, người sửa
            department.CreatedDate = DateTime.Now;
            department.CreatedBy ??= "Nguyen The Hien";
            department.ModifiedDate = DateTime.Now;
            department.ModifiedBy ??= "Nguyen The Hien";

            // Kiểm tra trùng tên phòng ban
            _departmentValidate.CheckDuplicateDepartmentNameAsync(department.DepartmentName);

            return department;
        }

        /// <summary>
        /// Chuyển đổi từ DepartmentUpdateDto sang Department
        /// </summary>
        /// CreatedBy: hiennt200210 (20/09/2023)
        public override Department MapUpdateDtoToEntity(DepartmentUpdateDto departmentUpdateDto, Department oldDepartment)
        {
            var newDepartment = _mapper.Map(departmentUpdateDto, oldDepartment);

            // Thêm thông tin ngày sửa, người sửa
            newDepartment.ModifiedDate = DateTime.Now;
            newDepartment.ModifiedBy ??= "Nguyen The Hien";

            return newDepartment;
        }

        /// <summary>
        /// Validate nghiệp vụ khi thêm mới nhân viên
        /// </summary>
        /// <param name="department">Dữ liệu cần validate</param>
        /// <exception cref="BadRequestException">Dữ liệu không hợp lệ</exception>
        /// CreatedBy: hiennt200210 (20/09/2023)
        public override async Task ValidateBusinessInsertAsync(Department department)
        {
            await _departmentValidate.CheckDuplicateDepartmentNameAsync(department.DepartmentName);
        }

        /// <summary>
        /// Validate nghiệp vụ khi cập nhật
        /// </summary>
        /// <param name="oldDepartment">Dữ liệu cũ</param>
        /// <param name="newDepartment">Dữ liệu mới</param>
        /// <exception cref="BadRequestException">Dữ liệu không hợp lệ</exception>
        /// CreatedBy: hiennt200210 (20/09/2023)
        public override async Task ValidateBusinessUpdateAsync(Department oldDepartment, Department newDepartment)
        {
            if (oldDepartment.DepartmentName != newDepartment.DepartmentName)
            {
                await _departmentValidate.CheckDuplicateDepartmentNameAsync(newDepartment.DepartmentName);
            }
        }
    }
}
