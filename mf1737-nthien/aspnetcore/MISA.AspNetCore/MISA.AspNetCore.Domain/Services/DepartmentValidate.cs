using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AspNetCore.Domain
{
    public class DepartmentValidate : IDepartmentValidate
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentValidate(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        /// <summary>
        /// Kiểm tra trùng tên phòng ban
        /// </summary>
        /// <param name="departmentName">Tên phòng ban cần kiểm tra</param>
        /// <exception cref="ConflictException">Tên phòng ban đã tồn tại</exception>
        /// CreatedBy: hiennt200210 (16/09/2023)
        public async Task CheckDuplicateDepartmentNameAsync(string departmentName)
        {
            await _departmentRepository.CheckDuplicateDepartmentNameAsync(departmentName);
        }
    }
}
