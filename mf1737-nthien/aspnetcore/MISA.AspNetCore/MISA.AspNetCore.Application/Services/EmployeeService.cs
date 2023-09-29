using AutoMapper;
using MISA.AspNetCore.Domain;
using MISA.AspNetCore.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AspNetCore.Application
{
    public class EmployeeService : BaseService<Employee, EmployeeDto, EmployeeInsertDto, EmployeeUpdateDto>, IEmployeeService
    {
        private readonly IEmployeeValidate _employeeValidate;
        private readonly IEmployeeRepository _employeeRepository;
        IDepartmentRepository _departmentRepository;
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

            List<EmployeeDto> dtos = new List<EmployeeDto>();

            for (int i = 0; i < employees.Count; i++)
            {
                var employee = employees[i];
                var dto = await MapEntityToDto(employee);
                dtos.Add(dto);
            }

            var paginationDto = new PaginationDto()
            {
                Total = dtos.Count,
                PageCount = dtos.Count,
                Limit = limit,
                Offset = offset,
                Data = dtos,
            };

            return dtos;
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
        /// Chuyển đổi từ EmployeeUpdateDto sang Employee
        /// </summary>
        /// CreatedBy: hiennt200210 (20/09/2023)
        public override Employee MapUpdateDtoToEntity(EmployeeUpdateDto employeeUpdateDto, Employee oldEmployee)
        {
            var newEmployee = new Employee();
            newEmployee = _mapper.Map(oldEmployee, newEmployee);
            newEmployee = _mapper.Map(employeeUpdateDto, newEmployee);

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
            await _employeeValidate.CheckDuplicateEmployeeCodeAsync(employee.EmployeeCode);
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
            if (oldEmployee.EmployeeCode != newEmployee.EmployeeCode)
            {
                await _employeeValidate.CheckDuplicateEmployeeCodeAsync(newEmployee.EmployeeCode);
            }
        }
    }
}
