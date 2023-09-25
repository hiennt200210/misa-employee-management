using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AspNetCore.Domain
{
    public class EmployeeValidate : IEmployeeValidate
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeValidate(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        /// <summary>
        /// Kiểm tra trùng mã nhân viên
        /// </summary>
        /// <param name="employeeCode">Mã nhân viên cần kiểm tra</param>
        /// <exception cref="ConflictException">Mã nhân viên đã tồn tại</exception>
        /// CreatedBy: hiennt200210 (16/09/2023)
        public async Task CheckDuplicateEmployeeCodeAsync(string employeeCode)
        {
            await _employeeRepository.CheckDuplicateEmployeeCodeAsync(employeeCode);
        }
    }
}
