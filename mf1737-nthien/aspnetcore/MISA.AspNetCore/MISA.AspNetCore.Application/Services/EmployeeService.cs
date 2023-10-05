using AutoMapper;
using MISA.AspNetCore.Domain;
using MISA.AspNetCore.Domain.Entities.Base;
using OfficeOpenXml;
using OfficeOpenXml.Style;
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
        /// Xuất file danh sách nhân viên theo bộ lọc
        /// </summary>
        /// <param name="search">Từ khoá tìm kiếm (Mã nhân viên, Họ và tên, Số điện thoại)</param>
        /// <param name="orders">Sắp xếp theo các trường (VD: EmployeeCode, +FullName, -PhoneNumber)</param>
        /// <returns>File danh sách nhân viên theo kết quả lọc</returns>
        /// CreatedBy: hiennt200210 (02/10/2023)
        public async Task<EmployeeExportDto> ExportAsync(string? search, List<string>? orders)
        {
            // Lấy dữ liệu
            var paginationDto = await PagingAsync(0, 0, search, search, search, orders);

            var employeeDtos = paginationDto.Data;

            //var employeeExportExcelTasks = employeeDtos.Select(async (employee, index) => await MapEmployeeToEmployeeExportExcel(employee, index));

            List<EmployeeExportExcelDto> employeeExportExcelTasks = new();

            for (int i = 0; i < employeeDtos.Count; i++)
            {
                var employee = employeeDtos[i];
                var employeeExportExcel = await MapEmployeeToEmployeeExportExcel(employee, i);
                employeeExportExcelTasks.Add(employeeExportExcel);
            }

            var data = employeeExportExcelTasks;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add(Domain.Resources.Employee.EmployeesUpperCase);

                // Thiết lập độ rộng cột
                var columnWidths = new double[] { 5, 20, 30, 15, 20, 25, 25, 20, 30 };
                for (int i = 0; i < columnWidths.Length; i++)
                {
                    worksheet.Column(i + 1).Width = columnWidths[i];
                }

                // Định dạng cho tên cột và dòng tiêu đề
                var columnHeaderStyle = worksheet.Cells["A3:I3"].Style;
                columnHeaderStyle.Font.Bold = true;
                columnHeaderStyle.Font.Size = 12;
                columnHeaderStyle.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                columnHeaderStyle.VerticalAlignment = ExcelVerticalAlignment.Center;
                columnHeaderStyle.Fill.PatternType = ExcelFillStyle.Solid;
                columnHeaderStyle.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);

                // Merge hai dòng tiêu đề đầu
                var headerRow = worksheet.Cells["A1:I1"];
                worksheet.Row(1).Height = 25;
                headerRow.Merge = true;
                headerRow.Value = Domain.Resources.Employee.EmployeesUpperCase;
                headerRow.Style.Font.Size = 16;
                headerRow.Style.Font.Bold = true;
                headerRow.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                headerRow.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                var headerRow2 = worksheet.Cells["A2:I2"];
                worksheet.Row(2).Height = 25;
                headerRow2.Merge = true;
                headerRow2.Value = String.Empty;
                headerRow2.Style.Font.Size = 16;
                headerRow2.Style.Font.Bold = true;
                headerRow2.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                headerRow2.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                // Định dạng cho border
                var dataRange = worksheet.Cells["A3:I3"].LoadFromCollection(data, true);
                dataRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                dataRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                dataRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                dataRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;

                // Căn giữa các cột
                worksheet.Cells["A3:A" + (data.Count + 3)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["E3:E" + (data.Count + 3)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                //// Đặt phông chữ
                //var fontArial = worksheet.Cells.Style.Font;
                //fontArial.Name = "Arial";
                //worksheet.Cells["A1:I3"].Style.Font = fontArial;

                //var fontTimesNewRoman = worksheet.Cells.Style.Font;
                //fontTimesNewRoman.Name = "Times New Roman";
                //worksheet.Cells["A4:A"].Style.Font = fontTimesNewRoman;

                // Chuyển đổi tệp Excel thành mảng byte và trả về 
                var fileBytes = package.GetAsByteArray();
                var result = new EmployeeExportDto()
                {
                    File = fileBytes,
                    MimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    FileName = "DanhSachNhanVien.xlsx"
                };
                return result;
            }
        }

        /// <summary>
        /// Chuyển đổi Employee sang EmployeeExportExcelDto
        /// </summary>
        /// <param name="employee">Nhân viên</param>
        /// <param name="index">indexx</param>
        /// <returns>EmployeeExportExcelDto</returns>
        private async Task<EmployeeExportExcelDto> MapEmployeeToEmployeeExportExcel(EmployeeDto employee, int index)
        {
            var dateOfBirthFormat = "";
            var identityDateFormat = "";
            var genderFormat = "";

            if (employee.DateOfBirth.HasValue)
            {
                dateOfBirthFormat = employee.DateOfBirth.Value.ToString("dd/MM/yyyy");
            }
            else
            {
                dateOfBirthFormat = string.Empty;
            }


            if (employee.IdentityDate.HasValue)
            {
                identityDateFormat = employee.IdentityDate.Value.ToString("dd/MM/yyyy");
            }
            else
            {
                identityDateFormat = string.Empty;
            }


            if (employee.Gender == Gender.Male)
            {
                genderFormat = Domain.Resources.Employee.Male;
            }
            else if (employee.Gender == Gender.Female)
            {
                genderFormat = Domain.Resources.Employee.Female;
            }
            else if (employee.Gender == Gender.Other)
            {
                genderFormat = Domain.Resources.Employee.Other;
            }
            else
            {
                genderFormat = "";
            }

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

            var employeeExportExcel = new EmployeeExportExcelDto()
            {
                NumericalOrder = index + 1,
                EmployeeCode = employee.EmployeeCode,
                FullName = employee.FullName,
                Gender = genderFormat,
                DateOfBirth = dateOfBirthFormat,
                PositionName = employee.PositionName,
                DepartmentName = department.DepartmentName,
                BankAccount = employee.BankAccount,
                BankName = employee.BankName,
            };

            return employeeExportExcel;
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

            if (!Enum.IsDefined(typeof(Gender), employee.Gender))
            {
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
