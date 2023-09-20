using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AspNetCore.Domain
{
    public class EmployeeValidate : IEmployeeValidate
    {
        #region Fields
        private readonly IEmployeeRepository _employeeRepository;
        #endregion

        #region Constructors
        public EmployeeValidate(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Kiểm tra trùng mã nhân viên
        /// </summary>
        /// <exception cref="ConflictException">
        /// Nếu mã nhân viên đã tồn tại
        /// </exception>
        /// <returns>
        /// False nếu mã nhân viên chưa tồn tại
        /// </returns>
        /// CreatedBy: hiennt200210 (16/09/2023)
        public async Task<bool> CheckDuplicateEmployeeCode(Employee employee)
        {
            var isDuplicated = await _employeeRepository.CheckDuplicateEmployeeCodeAsync(employee.EmployeeCode);

            if (isDuplicated)
            {
                throw new ConflictException(Resources.ErrorResource.EmployeeCodeDuplicated);
            }

            return false;
        }
        #endregion
    }
}
