using AutoMapper;
using MISA.AspNetCore.Domain;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AspNetCore.Application.UnitTests
{
    [TestFixture]
    public class EmployeeServiceTests
    {
        public IEmployeeValidate EmployeeValidate { get; set; }
        public IEmployeeRepository EmployeeRepository { get; set; }
        public IDepartmentRepository DepartmentRepository { get; set; }
        public EmployeeService EmployeeService { get; set; }
        public IMapper Mapper { get; set; }

        /// <summary>
        /// Setup các đối tượng cần thiết cho việc test
        /// </summary>
        [SetUp]
        public void Setup()
        {
            EmployeeValidate = Substitute.For<IEmployeeValidate>();
            EmployeeRepository = Substitute.For<IEmployeeRepository>();
            EmployeeService = new EmployeeService(EmployeeRepository, DepartmentRepository, EmployeeValidate, Mapper);
            Mapper = Substitute.For<IMapper>();
        }

        /// <summary>
        /// Unit test cho phương thức GetAllAsync
        /// </summary>
        /// CreatedBy: hiennt200210 (25/09/2023)
        [Test]
        public async Task GetAllAsync_Success()
        {
            // Arrange
            var employees = new List<Employee>();

            EmployeeRepository.GetAllAsync().Returns(employees);

            // Act
            var result = await EmployeeService.GetAllAsync();

            // Assert
            await EmployeeRepository.Received(1).GetAllAsync();
        }

        /// <summary>
        /// Unit test cho phương thức GetByIdAsync với EmployeeId hợp lệ
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetByIdAsync_ValidInput_Success()
        {
            // Arrange
            var id = Guid.NewGuid();

            var employee = new Employee();

            EmployeeRepository.GetByIdAsync(id).Returns(employee);

            // Act
            var result = await EmployeeService.GetByIdAsync(id);

            // Assert
            await EmployeeRepository.Received(1).GetByIdAsync(id);
        }

        /// <summary>
        /// Unit test cho phương thức GetByIdAsync với EmployeeId rỗng
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetByIdAsync_InvalidInput_ThrowException()
        {
            // Arrange
            var id = Guid.Empty;

            // Act & Assert
            var exception = Assert.ThrowsAsync<BadRequestException>(async () => await EmployeeService.GetByIdAsync(id));
        }

        /// <summary>
        /// Unit test cho phương thức InsertAsync với EmployeeId rỗng
        /// </summary>
        /// CreatedBy: hiennt200210 (25/09/2023)
        [Test]
        public async Task InsertAsync_EmptyEmployeeId_IdNotNull()
        {
            // Arrange
            var employeeInsertDto = new EmployeeInsertDto();

            var employee = EmployeeService.MapInsertDtoToEntity(employeeInsertDto);

            employee.EmployeeId = Guid.Empty;

            // Act
            var result = await EmployeeService.InsertAsync(employeeInsertDto);

            // Assert
            Assert.That(employee.EmployeeId, Is.Not.EqualTo(Guid.Empty));
        }

        /// <summary>
        /// Unit test cho phương thức InsertAsync với CreatedBy và ModifiedBy bằng null
        /// </summary>
        /// CreatedBy: hiennt200210 (25/09/2023)
        [Test]
        public async Task InsertAsync_CreatedByAndModifiedByNull_CreatedByAndModifiedByNotNull()
        {
            // Arrange
            var employeeInsertDto = new EmployeeInsertDto();

            var employee = EmployeeService.MapInsertDtoToEntity(employeeInsertDto);

            employee.CreatedBy = null;
            employee.ModifiedBy = null;

            // Act
            var result = await EmployeeService.InsertAsync(employeeInsertDto);

            // Assert
            Assert.That(employee.CreatedBy, Is.Not.Null);
            Assert.That(employee.ModifiedBy, Is.Not.Null);
        }

        /// <summary>
        /// Unit test cho phương thức InsertAsync với dữ liệu hợp lệ
        /// </summary>
        /// CreatedBy: hiennt200210 (25/09/2023)
        [Test]
        public async Task InsertAsync_ValidInput_Success()
        {
            var employeeInsertDto = new EmployeeInsertDto();

            var employee = EmployeeService.MapInsertDtoToEntity(employeeInsertDto);

            //Act
            var result = await EmployeeService.InsertAsync(employeeInsertDto);

            //Assert
            await EmployeeService.Received(1).ValidateBusinessInsertAsync(employee);
            await EmployeeRepository.Received(1).InsertAsync(employee);
        }

        /// <summary>
        /// Unit test cho phương thức InsertAsync với dữ liệu không hợp lệ
        /// </summary>
        /// CreatedBy: hiennt200210 (25/09/2023)
        [Test]
        public async Task InsertAsync_InvalidInput_ThrowException()
        {
            // Arrange
            var employeeInsertDto = new EmployeeInsertDto();

            var employee = EmployeeService.MapInsertDtoToEntity(employeeInsertDto);

            // Act & Assert
            var exception = Assert.ThrowsAsync<BadRequestException>(async () => await EmployeeService.InsertAsync(employeeInsertDto));
        }

        /// <summary>
        /// Unit test cho phương thức ValidateBusinessInsertAsync với dữ liệu hợp lệ
        /// </summary>
        /// CreatedBy: hiennt200210 (25/09/2023)
        [Test]
        public async Task ValidateBusinessInsertAsync_ValidInput_Success()
        {
            // Arrange
            var employee = new Employee()
            {
                EmployeeId = Guid.NewGuid(),
                EmployeeCode = "NV0001",
                FullName = "Nguyen The Hien",
                DepartmentId = Guid.NewGuid(),
            };

            // Act
            await EmployeeService.ValidateBusinessInsertAsync(employee);

            // Assert
            await EmployeeValidate.Received(1).CheckDuplicateEmployeeCodeAsync(employee.EmployeeCode);
        }

        /// <summary>
        /// Unit test cho phương thức ValidateBusinessInsertAsync với DepartmentId không hợp lệ
        /// </summary>
        /// CreatedBy: hiennt200210 (25/09/2023)
        [Test]
        public async Task ValidateBusinessInsertAsync_InvalidInput_ThrowException()
        {
            // Arrange
            var employee = new Employee()
            {
                EmployeeId = Guid.NewGuid(),
                EmployeeCode = "NV0001",
                FullName = "Nguyen The Hien",
                DepartmentId = Guid.Empty,
            };

            // Act & Assert
            var exception = Assert.ThrowsAsync<BadRequestException>(async () => await EmployeeService.ValidateBusinessInsertAsync(employee));
        }

        /// <summary>
        /// Unit test cho phương thức UpdateAsync với CreatedBy và ModifiedBy bằng null
        /// </summary>
        /// CreatedBy: hiennt200210 (25/09/2023)
        [Test]
        public async Task UpdateAsync_CreatedByAndModifiedByNull_CreatedByAndModifiedByNotNull()
        {
            // Arrange
            var employeeUpdateDto = new EmployeeUpdateDto();

            var employee = new Employee();

            employee = EmployeeService.MapUpdateDtoToEntity(employeeUpdateDto, employee);

            employee.CreatedBy = null;
            employee.ModifiedBy = null;

            // Act
            var result = await EmployeeService.UpdateAsync(Guid.NewGuid(), employeeUpdateDto);

            // Assert
            Assert.That(employee.ModifiedBy, Is.Not.Null);
        }

        /// <summary>
        /// Unit test cho phương thức UpdateAsync với dữ liệu hợp lệ
        /// </summary>
        /// CreatedBy: hiennt200210 (25/09/2023)
        [Test]
        public async Task UpdateAsync_ValidInput_Success()
        {
            // Arrange
            var employeeUpdateDto = new EmployeeUpdateDto();

            var employee = new Employee();

            employee = EmployeeService.MapUpdateDtoToEntity(employeeUpdateDto, employee);

            // Act
            var result = await EmployeeService.UpdateAsync(Guid.NewGuid(), employeeUpdateDto);

            // Assert
            await EmployeeService.Received(1).ValidateBusinessUpdateAsync(new Employee(), employee);
            await EmployeeRepository.Received(1).UpdateAsync(Guid.NewGuid(), employee);
        }

        /// <summary>
        /// Unit test cho phương thức UpdateAsync với dữ liệu không hợp lệ
        /// </summary>
        /// CreatedBy: hiennt200210 (25/09/2023)
        [Test]
        public async Task UpdateAsync_InvalidInput_ThrowException()
        {
            // Arrange
            var employeeUpdateDto = new EmployeeUpdateDto();

            var employee = new Employee();

            employee = EmployeeService.MapUpdateDtoToEntity(employeeUpdateDto, employee);

            employee.EmployeeId = Guid.Empty;

            // Act & Assert
            var exception = Assert.ThrowsAsync<BadRequestException>(async () => await EmployeeService.UpdateAsync(Guid.NewGuid(), employeeUpdateDto));
        }

        /// <summary>
        /// Unit test cho phương thức ValidateBusinessUpdateAsync với dữ liệu hợp lệ
        /// </summary>
        /// CreatedBy: hiennt200210 (25/09/2023)
        [Test]
        public async Task ValidateBusinessUpdateAsync_ValidInput_Success()
        {
            // Arrange
            var oldEmployee = new Employee()
            {
                EmployeeId = Guid.NewGuid(),
                EmployeeCode = "NV0001",
                FullName = "Nguyen The Hien",
                DepartmentId = Guid.NewGuid(),
            };

            var newEmployee = new Employee()
            {
                EmployeeId = Guid.NewGuid(),
                EmployeeCode = "NV0001",
                FullName = "Nguyen The Hien",
                DepartmentId = Guid.NewGuid(),
            };

            // Act
            await EmployeeService.ValidateBusinessUpdateAsync(oldEmployee, newEmployee);

            // Assert
            await EmployeeValidate.Received(1).CheckDuplicateEmployeeCodeAsync(newEmployee.EmployeeCode);
        }

        /// <summary>
        /// Unit test cho phương thức ValidateBusinessUpdateAsync với DepartmentId mới không hợp lệ
        /// </summary>
        /// CreatedBy: hiennt200210 (25/09/2023)
        [Test]
        public async Task ValidateBusinessUpdateAsync_InvalidInput_ThrowException()
        {
            // Arrange
            var oldEmployee = new Employee()
            {
                EmployeeId = Guid.NewGuid(),
                EmployeeCode = "NV0001",
                FullName = "Nguyen The Hien",
                DepartmentId = Guid.NewGuid(),
            };

            var newEmployee = new Employee()
            {
                EmployeeId = Guid.NewGuid(),
                EmployeeCode = "NV0001",
                FullName = "Nguyen The Hien",
                DepartmentId = Guid.Empty,
            };

            // Act & Assert
            var exception = Assert.ThrowsAsync<BadRequestException>(async () => await EmployeeService.ValidateBusinessUpdateAsync(oldEmployee, newEmployee));
        }

        /// <summary>
        /// Unit test cho phương thức DeleteAsync với dữ liệu hợp lệ
        /// </summary>
        /// CreatedBy: hiennt200210 (25/09/2023)
        [Test]
        public async Task DeleteAsync_ValidInput_Success()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var result = await EmployeeService.DeleteAsync(id);

            // Assert
            await EmployeeRepository.Received(1).DeleteAsync(id);
        }

        /// <summary>
        /// Unit test cho phương thức DeleteAsync với dữ liệu không hợp lệ
        /// </summary>
        /// CreatedBy: hiennt200210 (25/09/2023)
        [Test]
        public async Task DeleteAsync_InvalidInput_ThrowException()
        {
            // Arrange
            var id = Guid.Empty;

            // Act & Assert
            var exception = Assert.ThrowsAsync<BadRequestException>(async () => await EmployeeService.DeleteAsync(id));
        }

        /// <summary>
        /// Unit test cho phương thức DeleteMultipleAsync với dữ liệu hợp lệ
        /// </summary>
        /// CreatedBy: hiennt200210 (25/09/2023)
        [Test]
        public async Task DeleteMultipleAsync_ValidInput_Success()
        {
            // Arrange
            var ids = new List<Guid>() { Guid.NewGuid(), Guid.NewGuid() };

            // Act
            var result = await EmployeeService.DeleteMultipleAsync(ids);

            // Assert
            await EmployeeRepository.Received(1).DeleteMultipleAsync(ids);
        }

        /// <summary>
        /// Unit test cho phương thức DeleteMultipleAsync với dữ liệu không hợp lệ
        /// </summary>
        /// CreatedBy: hiennt200210 (25/09/2023)
        [Test]
        public async Task DeleteMultipleAsync_InvalidInput_ThrowException()
        {
            // Arrange
            var ids = new List<Guid>() { Guid.Empty, Guid.Empty };

            // Act & Assert
            var exception = Assert.ThrowsAsync<BadRequestException>(async () => await EmployeeService.DeleteMultipleAsync(ids));
        }

        /// <summary>
        /// Unit test cho phương thức GetNewEmployeeCodeAsync
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetNewEmployeeCodeAsync_ValidInput_Success()
        {
            // Arrange

            // Act
            var result = await EmployeeService.GetNewEmployeeCodeAsync();

            // Assert
            await EmployeeRepository.Received(1).GetNewEmployeeCodeAsync();
        }
    }
}
