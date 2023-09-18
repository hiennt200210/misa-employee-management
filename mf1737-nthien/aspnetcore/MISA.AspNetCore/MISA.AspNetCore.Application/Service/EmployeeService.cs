using MISA.AspNetCore.Domain;
using MISA.AspNetCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AspNetCore.Application
{
    public class EmployeeService : IEmployeeService
    {
        private readonly EmployeeRepository _employeeRepository;

		public EmployeeService(EmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<List<EmployeeModel>> GetAllAsync()
        {
            var employees = await _employeeRepository.GetAllAsync();

            var employeeModels = employees.Select(employee => MapEmployeeToEmployeeModel(employee)).ToList();

            return employeeModels;
        }

        public async Task<EmployeeModel> GetByIdAsync(Guid employeeId)
        {
            var employee = await _employeeRepository.GetByIdAsync(employeeId);

            var employeeModel = MapEmployeeToEmployeeModel(employee);

            return employeeModel;
        }

        public async Task<int> InsertAsync(EmployeeInsertModel employeeInsertModel)
        {
            var employee = MapEmployeeInsertModelToEmployee(employeeInsertModel);

            var affectedRows = await _employeeRepository.InsertAsync(employee);

            return affectedRows;
        }

        public async Task<int> UpdateAsync(EmployeeUpdateModel employeeUpdateModel)
        {
            var employee = MapEmployeeUpdateModelToEmployee(employeeUpdateModel);

            var affectedRows = await _employeeRepository.UpdateAsync(employee);

            return affectedRows;
        }

        public async Task<int> DeleteAsync(Guid employeeId)
        {
            var affectedRows = await _employeeRepository.DeleteAsync(employeeId);

            return affectedRows;
        }

        public Task<int> DeleteMultipleAsync(List<Guid> employeeId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Chuyển đổi từ Entity sang Model
        /// </summary>
        /// <param name="employee">Đối tượng kiểu Entity</param>
        /// <returns>Đối tượng kiểu Model</returns>
        /// CreatedBy: hiennt200210 (16/09/2023)
        private static EmployeeModel MapEmployeeToEmployeeModel(Employee employee)
        {
            var employeeModel = new EmployeeModel()
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

            return employeeModel;
        }

        private static Employee MapEmployeeInsertModelToEmployee(EmployeeInsertModel employeeInsertModel)
        {
            var employee = new Employee()
            {
                CreatedDate = DateTime.Now,
                CreatedBy = employeeInsertModel.CreatedBy,
                ModifiedDate = DateTime.Now,
                ModifiedBy = null,
                EmployeeId = Guid.NewGuid(),
                EmployeeCode = employeeInsertModel.EmployeeCode,
                FullName = employeeInsertModel.FullName,
                Gender = employeeInsertModel.Gender,
                DateOfBirth = employeeInsertModel.DateOfBirth,
                PositionName = employeeInsertModel.PositionName,
                DepartmentId = employeeInsertModel.DepartmentId,
                PhoneNumber = employeeInsertModel.PhoneNumber,
                LandlineNumber = employeeInsertModel.LandlineNumber,
                Email = employeeInsertModel.Email,
                IdentityNumber = employeeInsertModel.IdentityNumber,
                IdentityDate = employeeInsertModel.IdentityDate,
                IdentityPlace = employeeInsertModel.IdentityPlace,
                BankAccount = employeeInsertModel.BankAccount,
                BankName = employeeInsertModel.BankName,
                BankBranch = employeeInsertModel.BankBranch
            };

            return employee;
        }

        private static Employee MapEmployeeUpdateModelToEmployee(EmployeeUpdateModel employeeUpdateModel)
        {
            var employee = new Employee()
            {
                ModifiedDate = DateTime.Now,
                ModifiedBy = null,
                EmployeeCode = employeeUpdateModel.EmployeeCode,
                FullName = employeeUpdateModel.FullName,
                Gender = employeeUpdateModel.Gender,
                DateOfBirth = employeeUpdateModel.DateOfBirth,
                PositionName = employeeUpdateModel.PositionName,
                DepartmentId = employeeUpdateModel.DepartmentId,
                PhoneNumber = employeeUpdateModel.PhoneNumber,
                LandlineNumber = employeeUpdateModel.LandlineNumber,
                Email = employeeUpdateModel.Email,
                IdentityNumber = employeeUpdateModel.IdentityNumber,
                IdentityDate = employeeUpdateModel.IdentityDate,
                IdentityPlace = employeeUpdateModel.IdentityPlace,
                BankAccount = employeeUpdateModel.BankAccount,
                BankName = employeeUpdateModel.BankName,
                BankBranch = employeeUpdateModel.BankBranch
            };

            return employee;
        }
    }
}
