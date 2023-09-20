using MISA.AspNetCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AspNetCore.Application
{
    public class EmployeeService : BaseService<Employee, EmployeeDto, EmployeeInsertDto, EmployeeUpdateDto>, IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeeValidate _employeeValidate;

        public EmployeeService(IEmployeeRepository employeeRepository, IEmployeeValidate employeeValidate) : base(employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _employeeValidate = employeeValidate;
        }

        /// <summary>
        /// Chuyển đổi từ Entity sang Model
        /// </summary>
        /// <param name="employee">Đối tượng kiểu Entity</param>
        /// <returns>Đối tượng kiểu Model</returns>
        /// CreatedBy: hiennt200210 (16/09/2023)
        public override EmployeeDto MapEntityToDto(Employee employee)
        {
            var employeeDto = new EmployeeDto()
            {
                CreatedDate = employee.CreatedDate,
                CreatedBy = employee.CreatedBy,
                ModifiedDate = employee.ModifiedDate,
                ModifiedBy = employee.ModifiedBy,
                EmployeeId = employee.EmployeeId,
                EmployeeCode = employee.EmployeeCode,
                FullName = employee.FullName,
                Gender = employee.Gender,
                DateOfBirth = employee.DateOfBirth,
                PositionName = employee.PositionName,
                DepartmentId = employee.DepartmentId,
                PhoneNumber = employee.PhoneNumber,
                LandlineNumber = employee.LandlineNumber,
                Email = employee.Email,
                IdentityNumber = employee.IdentityNumber,
                IdentityDate = employee.IdentityDate,
                IdentityPlace = employee.IdentityPlace,
                BankAccount = employee.BankAccount,
                BankName = employee.BankName,
                BankBranch = employee.BankBranch
            };

            return employeeDto;
        }

        public override Employee MapInsertDtoToEntity(EmployeeInsertDto insertDto)
        {
            var employee = new Employee()
            {
                CreatedDate = DateTime.Now,
                CreatedBy = insertDto.CreatedBy,
                ModifiedDate = DateTime.Now,
                ModifiedBy = "hiennt200210",
                EmployeeId = Guid.NewGuid(),
                EmployeeCode = insertDto.EmployeeCode,
                FullName = insertDto.FullName,
                Gender = insertDto.Gender,
                DateOfBirth = insertDto.DateOfBirth,
                PositionName = insertDto.PositionName,
                DepartmentId = insertDto.DepartmentId,
                PhoneNumber = insertDto.PhoneNumber,
                LandlineNumber = insertDto.LandlineNumber,
                Email = insertDto.Email,
                IdentityNumber = insertDto.IdentityNumber,
                IdentityDate = insertDto.IdentityDate,
                IdentityPlace = insertDto.IdentityPlace,
                BankAccount = insertDto.BankAccount,
                BankName = insertDto.BankName,
                BankBranch = insertDto.BankBranch
            };

            return employee;
        }

        public override async Task ValidateInsertBusiness(Employee employee)
        {
            await _employeeValidate.CheckDuplicateEmployeeCode(employee);
        }

        public override Employee MapUpdateDtoToEntity(EmployeeUpdateDto updateDto, Employee entity)
        {
            var employee = new Employee()
            {
                ModifiedDate = DateTime.Now,
                ModifiedBy = "hiennt200210",
                EmployeeCode = updateDto.EmployeeCode,
                FullName = updateDto.FullName,
                Gender = updateDto.Gender,
                DateOfBirth = updateDto.DateOfBirth,
                PositionName = updateDto.PositionName,
                DepartmentId = updateDto.DepartmentId,
                PhoneNumber = updateDto.PhoneNumber,
                LandlineNumber = updateDto.LandlineNumber,
                Email = updateDto.Email,
                IdentityNumber = updateDto.IdentityNumber,
                IdentityDate = updateDto.IdentityDate,
                IdentityPlace = updateDto.IdentityPlace,
                BankAccount = updateDto.BankAccount,
                BankName = updateDto.BankName,
                BankBranch = updateDto.BankBranch
            };

            return employee;
        }
    }
}
