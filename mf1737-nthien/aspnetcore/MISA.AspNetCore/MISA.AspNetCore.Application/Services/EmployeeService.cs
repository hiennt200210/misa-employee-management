using AutoMapper;
using MISA.AspNetCore.Domain;
using MISA.AspNetCore.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.AspNetCore.Application
{
    public class EmployeeService : BaseService<Employee, EmployeeDto, EmployeeInsertDto, EmployeeUpdateDto>, IEmployeeService
    {
        private readonly IEmployeeValidate _employeeValidate;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository, IEmployeeValidate employeeValidate, IMapper mapper) : base(employeeRepository)
        {
            _employeeValidate = employeeValidate;
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Lấy thông tin nhân viên theo bộ lọc, tìm kiếm, sắp xếp, phân trang
        /// </summary>
        /// <param name="limit">Số lượng bản ghi trên một trang</param>
        /// <param name="offset">Vị trí bắt đầu lấy dữ liệu</param>
        /// <returns>Danh sách nhân viên theo kết quá lọc, tìm kiếm, sắp xếp, phân trang</returns>
        public async Task<PaginationDto> PagingAsync(int limit, int offset, string employeeCode, string fullName, string phoneNumber, List<string> orders)
        {
            // Lấy dữ liệu
            var employees = await _employeeRepository.PagingAsync(limit, offset, employeeCode, fullName, phoneNumber, orders);

            int total;

            if (string.IsNullOrEmpty(employeeCode) && string.IsNullOrEmpty(fullName) && string.IsNullOrEmpty(phoneNumber))
            {
                total = await _employeeRepository.CountAsync();
            }
            else
            {
                total = await _employeeRepository.CountAsync(employeeCode, fullName, phoneNumber);
            }

            List<EmployeeDto> dtos = new();

            for (int i = 0; i < employees.Count; i++)
            {
                var employee = employees[i];
                var dto = await MapEntityToDto(employee);
                dtos.Add(dto);
            }

            var paginationDto = new PaginationDto()
            {
                Total = total,
                PageCount = dtos.Count,
                Limit = limit,
                Offset = offset,
                Data = dtos,
            };

            return paginationDto;
        }

        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        /// CreatedBy: hiennt200210 (22/09/2023)
        public async Task<string> GetNewEmployeeCodeAsync()
        {
            var newEmployeeCode = await _employeeRepository.GetNewEmployeeCodeAsync();
            return newEmployeeCode;
        }

        /// <summary>
        /// Chuyển đổi từ Employee sang EmployeeDto
        /// </summary>
        /// CreatedBy: hiennt200210 (16/09/2023)
        public override async Task<EmployeeDto> MapEntityToDto(Employee employee)
        {
            var employeeDto = _mapper.Map<EmployeeDto>(employee);

            // Lấy tên phòng ban
            var department = await _departmentRepository.GetByIdAsync(employee.DepartmentId);

            if (department == null)
            {
                throw new BadRequestException()
                {
                    ErrorCode = ErrorCode.BadRequest,
                    DevMessage = Domain.Resources.Errors.DepartmentNotFound,
                    UserMessage = Domain.Resources.Errors.DepartmentNotFound,
                    MoreInfo = "",
                    TraceId = "",
                    Errors = ""
                };
            }

            employeeDto.DepartmentName = department.DepartmentName;

            return employeeDto;
        }

        /// <summary>
        /// Chuyển đổi từ EmployeeInsertDto sang Employee
        /// </summary>
        /// CreatedBy: hiennt200210 (20/09/2023)
        public override Employee MapInsertDtoToEntity(EmployeeInsertDto insertDto)
        {
            var employee = _mapper.Map<Employee>(insertDto);
            
            // Validate input
            var errors = new List<string>();

            if (employee.EmployeeCode != null && employee.EmployeeCode.Length > 20)
            {
                errors.Add(new string(Domain.Resources.Errors.EmployeeCodeExceedMaxLength));
            }

            if (string.IsNullOrEmpty(employee.EmployeeCode))
            {
                errors.Add(new string(Domain.Resources.Errors.EmployeeCodeCannotBeEmpty));
            }

            if (employee.FullName != null && employee.FullName.Length > 100)
            {
                errors.Add(new string(Domain.Resources.Errors.FullNameExceedMaxLength));
            }

            if (string.IsNullOrEmpty(employee.FullName))
            {
                errors.Add(new string(Domain.Resources.Errors.FullNameCannotBeEmpty));
            }

            if (!Enum.IsDefined(typeof(Gender), employee.Gender)) {
                errors.Add(new string(Domain.Resources.Errors.GenderInvalid));
            }

            if (employee.DateOfBirth != null && employee.DateOfBirth > DateTime.Now)
            {
                errors.Add(new string(Domain.Resources.Errors.DateOfBirthInvalid));
            }

            if (employee.PhoneNumber != null && employee.PhoneNumber.Length > 50)
            {
                errors.Add(new string(Domain.Resources.Errors.PhoneNumberExceedMaxLength));
            }

            if (employee.LandlineNumber != null && employee.LandlineNumber.Length > 50)
            {
                errors.Add(new string(Domain.Resources.Errors.LandlineNumberExceedMaxLength));
            }

            if (employee.Email != null && employee.Email.Length > 100)
            {
                errors.Add(new string(Domain.Resources.Errors.EmailExceedMaxLength));
            }

            if (employee.Email != null && !IsValidEmail(employee.Email))
            {
                errors.Add(new string(Domain.Resources.Errors.EmailInvalid));
            }

            if (employee.Address != null && employee.Address.Length > 255)
            {
                errors.Add(new string(Domain.Resources.Errors.AddressExceedMaxLength));
            }

            if (employee.IdentityNumber != null && employee.IdentityNumber.Length > 25)
            {
                errors.Add(new string(Domain.Resources.Errors.IdentityNumberExceedMaxLength));
            }

            if (employee.IdentityDate != null && employee.IdentityDate > DateTime.Now || employee.IdentityDate < employee.DateOfBirth)
            {
                errors.Add(new string(Domain.Resources.Errors.IdentityDateInvalid));
            }

            if (employee.IdentityPlace != null && employee.IdentityPlace.Length > 255)
            {
                errors.Add(new string(Domain.Resources.Errors.IdentityPlaceExceedMaxLength));
            }

            if (employee.PositionName != null && employee.PositionName.Length > 255)
            {
                errors.Add(new string(Domain.Resources.Errors.PositionNameExceedMaxLength));
            }

            if (employee.BankAccount != null && employee.BankAccount.Length > 25)
            {
                errors.Add(new string(Domain.Resources.Errors.BankAccountExceedMaxLength));
            }

            if (employee.BankName != null && employee.BankName.Length > 255)
            {
                errors.Add(new string(Domain.Resources.Errors.BankNameExceedMaxLength));
            }

            if (employee.BankBranch != null && employee.BankBranch.Length > 255)
            {
                errors.Add(new string(Domain.Resources.Errors.BankBranchExceedMaxLength));
            }

            if (employee.Salary < 0)
            {
                errors.Add(new string(Domain.Resources.Errors.SalaryInvalid));
            }

            if (employee.DepartmentId == Guid.Empty)
            {
                errors.Add(new string(Domain.Resources.Errors.DepartmentCannotBeEmpty));
            }

            if (errors.Count > 0)
            {
                throw new BadRequestException()
                {
                    ErrorCode = ErrorCode.BadRequest,
                    DevMessage = Domain.Resources.Errors.BadRequest,
                    UserMessage = Domain.Resources.Errors.BadRequest,
                    MoreInfo = "",
                    TraceId = "",
                    Errors = errors
                };
            }

            // Sinh EmployeeId mới
            employee.EmployeeId = Guid.NewGuid();

            // Thêm thông tin ngày tạo, người tạo, ngày sửa, người sửa
            employee.CreatedDate = DateTime.Now;
            employee.CreatedBy ??= "Nguyen The Hien";
            employee.ModifiedDate = DateTime.Now;
            employee.ModifiedBy ??= "Nguyen The Hien";

            return employee;
        }

        /// <summary>
        /// Kiểm tra một chuỗi có đúng định dạng email không
        /// </summary>
        /// <param name="email">Chuỗi cần kiểm tra</param>
        /// <returns>
        /// true: đúng định dạng email
        /// false: không đúng định dạng email
        /// </returns>
        /// CreatedBy: hiennt200210 (20/09/2023)
        private static bool IsValidEmail(string email)
        {
            // Biểu thức chính quy để kiểm tra định dạng email
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            // Kiểm tra chuỗi email bằng biểu thức chính quy
            bool isValid = Regex.IsMatch(email, pattern);

            return isValid;
        }

        /// <summary>
        /// Chuyển đổi từ EmployeeUpdateDto sang Employee
        /// </summary>
        /// CreatedBy: hiennt200210 (20/09/2023)
        public override Employee MapUpdateDtoToEntity(EmployeeUpdateDto employeeUpdateDto, Employee oldEmployee)
        {
            var newEmployee = new Employee();
            newEmployee = _mapper.Map(oldEmployee, newEmployee);
            newEmployee = _mapper.Map(employeeUpdateDto, newEmployee);

            // Validate input
            var errors = new List<string>();

            if (newEmployee.EmployeeCode != null && newEmployee.EmployeeCode.Length > 20)
            {
                errors.Add(new string(Domain.Resources.Errors.EmployeeCodeExceedMaxLength));
            }

            if (string.IsNullOrEmpty(newEmployee.EmployeeCode))
            {
                errors.Add(new string(Domain.Resources.Errors.EmployeeCodeCannotBeEmpty));
            }

            if (newEmployee.FullName != null && newEmployee.FullName.Length > 100)
            {
                errors.Add(new string(Domain.Resources.Errors.FullNameExceedMaxLength));
            }

            if (string.IsNullOrEmpty(newEmployee.FullName))
            {
                errors.Add(new string(Domain.Resources.Errors.FullNameCannotBeEmpty));
            }

            if (!Enum.IsDefined(typeof(Gender), newEmployee.Gender))
            {
                errors.Add(new string(Domain.Resources.Errors.GenderInvalid));
            }

            if (newEmployee.DateOfBirth > DateTime.Now)
            {
                errors.Add(new string(Domain.Resources.Errors.DateOfBirthInvalid));
            }

            if (newEmployee.PhoneNumber != null && newEmployee.PhoneNumber.Length > 50)
            {
                errors.Add(new string(Domain.Resources.Errors.PhoneNumberExceedMaxLength));
            }

            if (newEmployee.LandlineNumber != null && newEmployee.LandlineNumber.Length > 50)
            {
                errors.Add(new string(Domain.Resources.Errors.LandlineNumberExceedMaxLength));
            }

            if (newEmployee.Email != null && newEmployee.Email.Length > 100)
            {
                errors.Add(new string(Domain.Resources.Errors.EmailExceedMaxLength));
            }

            if (newEmployee.Email != null && !IsValidEmail(newEmployee.Email))
            {
                errors.Add(new string(Domain.Resources.Errors.EmailInvalid));
            }

            if (newEmployee.Address != null && newEmployee.Address.Length > 255)
            {
                errors.Add(new string(Domain.Resources.Errors.AddressExceedMaxLength));
            }

            if (newEmployee.IdentityNumber != null && newEmployee.IdentityNumber.Length > 25)
            {
                errors.Add(new string(Domain.Resources.Errors.IdentityNumberExceedMaxLength));
            }

            if (newEmployee.IdentityDate > DateTime.Now || newEmployee.IdentityDate < newEmployee.DateOfBirth)
            {
                errors.Add(new string(Domain.Resources.Errors.IdentityDateInvalid));
            }

            if (newEmployee.IdentityPlace != null && newEmployee.IdentityPlace.Length > 255)
            {
                errors.Add(new string(Domain.Resources.Errors.IdentityPlaceExceedMaxLength));
            }

            if (newEmployee.PositionName != null && newEmployee.PositionName.Length > 255)
            {
                errors.Add(new string(Domain.Resources.Errors.PositionNameExceedMaxLength));
            }

            if (newEmployee.BankAccount != null && newEmployee.BankAccount.Length > 25)
            {
                errors.Add(new string(Domain.Resources.Errors.BankAccountExceedMaxLength));
            }

            if (newEmployee.BankName != null && newEmployee.BankName.Length > 255)
            {
                errors.Add(new string(Domain.Resources.Errors.BankNameExceedMaxLength));
            }

            if (newEmployee.BankBranch != null && newEmployee.BankBranch.Length > 255)
            {
                errors.Add(new string(Domain.Resources.Errors.BankBranchExceedMaxLength));
            }

            if (newEmployee.Salary < 0)
            {
                errors.Add(new string(Domain.Resources.Errors.SalaryInvalid));
            }

            if (newEmployee.DepartmentId == Guid.Empty)
            {
                errors.Add(new string(Domain.Resources.Errors.DepartmentCannotBeEmpty));
            }

            if (errors.Count > 0)
            {
                throw new BadRequestException()
                {
                    ErrorCode = ErrorCode.BadRequest,
                    DevMessage = Domain.Resources.Errors.BadRequest,
                    UserMessage = Domain.Resources.Errors.BadRequest,
                    MoreInfo = "",
                    TraceId = "",
                    Errors = errors
                };
            }

            // Thêm thông tin ngày sửa, người sửa
            newEmployee.ModifiedDate = DateTime.Now;
            newEmployee.ModifiedBy ??= "Nguyen The Hien";

            return newEmployee;
        }

        /// <summary>
        /// Validate nghiệp vụ khi thêm mới nhân viên
        /// </summary>
        /// <param name="employee">Dữ liệu cần validate</param>
        /// <exception cref="BadRequestException">Dữ liệu không hợp lệ</exception>
        /// CreatedBy: hiennt200210 (20/09/2023)
        public override async Task ValidateBusinessInsertAsync(Employee employee)
        {
            // Nếu không có phòng ban thì báo lỗi
            try
            {
                var isValidDepartmentId = await _departmentRepository.GetByIdAsync(employee.DepartmentId);
            }
            catch (NotFoundException)
            {
                throw new BadRequestException()
                {
                    ErrorCode = ErrorCode.BadRequest,
                    DevMessage = Domain.Resources.Errors.DepartmentNotFound,
                    UserMessage = Domain.Resources.Errors.DepartmentNotFound,
                    MoreInfo = "",
                    TraceId = "",
                    Errors = ""
                };
            }

            // Kiểm tra trùng mã nhân viên
            if (employee.EmployeeCode != null)
            {
                await _employeeValidate.CheckDuplicateEmployeeCodeAsync(employee.EmployeeCode);
            }
        }

        /// <summary>
        /// Validate nghiệp vụ khi cập nhật
        /// </summary>
        /// <param name="oldEmployee">Dữ liệu cũ</param>
        /// <param name="newEmployee">Dữ liệu mới</param>
        /// <exception cref="BadRequestException">Dữ liệu không hợp lệ</exception>
        /// CreatedBy: hiennt200210 (20/09/2023)
        public override async Task ValidateBusinessUpdateAsync(Employee oldEmployee, Employee newEmployee)
        {
            // Nếu không có phòng ban thì báo lỗi
            try
            {
                var isValidDepartmentId = await _departmentRepository.GetByIdAsync(newEmployee.DepartmentId);
            }
            catch (NotFoundException)
            {
                throw new BadRequestException()
                {
                    ErrorCode = ErrorCode.BadRequest,
                    DevMessage = Domain.Resources.Errors.DepartmentNotFound,
                    UserMessage = Domain.Resources.Errors.DepartmentNotFound,
                    MoreInfo = "",
                    TraceId = "",
                    Errors = ""
                };
            }

            // Nếu mã nhân viên thay đổi thì kiểm tra trùng mã nhân viên
            if (newEmployee.EmployeeCode != null && oldEmployee.EmployeeCode != newEmployee.EmployeeCode)
            {
                await _employeeValidate.CheckDuplicateEmployeeCodeAsync(newEmployee.EmployeeCode);
            }
        }
    }
}
