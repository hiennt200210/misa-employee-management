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
        /// Kiểm tra mã nhân viên đã tồn tại hay chưa
        /// </summary>
        /// <exception cref="ConflictException">Mã nhân viên đã tồn tại</exception>
        /// CreatedBy: hiennt200210 (16/09/2023)
        public async Task CheckEmployeeCodeExists(string employeeCode)
        {
            var employee = await _employeeRepository.CheckEmployeeCodeExistsAsync(employeeCode);

            if (employee != null)
            {
                throw new ConflictException(ErrorCode.Conflict, "Mã nhân viên đã tồn tại trong hệ thống");
            }
        }
    }
}
