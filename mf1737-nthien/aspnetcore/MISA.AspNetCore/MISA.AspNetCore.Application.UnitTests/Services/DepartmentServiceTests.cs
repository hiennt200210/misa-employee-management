using AutoMapper;
using MISA.AspNetCore.Domain;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AspNetCore.Application.UnitTests
{
    [TestFixture]
    public class DepartmentServiceTests
    {
        public IDepartmentValidate DepartmentValidate { get; set; }
        public IDepartmentRepository DepartmentRepository { get; set; }
        public DepartmentService DepartmentService { get; set; }
        public IMapper Mapper { get; set; }

        /// <summary>
        /// Setup các đối tượng cần thiết cho việc test
        /// </summary>
        /// CreatedBy: hiennt200210 (25/09/2023)
        [SetUp]
        public void Setup()
        {
            DepartmentValidate = Substitute.For<IDepartmentValidate>();
            DepartmentRepository = Substitute.For<IDepartmentRepository>();
            DepartmentService = new DepartmentService(DepartmentRepository, DepartmentValidate, Mapper);
            Mapper = Substitute.For<IMapper>();
        }

        /// <summary>
        /// Unit test cho phương thức ValidateBusinessInsertAsync với dữ liệu hợp lệ
        /// </summary>
        /// CreatedBy: hiennt200210 (25/09/2023)
        [Test]
        public async Task ValidateBusinessInsertAsync_ValidInput_Success()
        {
            // Arrange
            var department = new Department()
            {
                DepartmentId = Guid.NewGuid(),
                DepartmentName = "Phòng kế toán",
            };

            // Act
            await DepartmentService.ValidateBusinessInsertAsync(department);

            // Assert
            await DepartmentValidate.Received(1).CheckDuplicateDepartmentNameAsync(department.DepartmentName);
        }

        /// <summary>
        /// Unit test cho phương thức ValidateBusinessInsertAsync với dữ liệu không hợp lệ
        /// </summary>
        /// CreatedBy: hiennt200210 (25/09/2023)
        [Test]
        public async Task ValidateBusinessInsertAsync_InvalidInput_ThrowException()
        {
            // Arrange
            var department = new Department()
            {
                DepartmentId = Guid.Empty,
                DepartmentName = "Phòng kế toán",
            };

            // Act & Assert
            //var exception = Assert.ThrowsAsync<BadRequestException>(async () => await EmployeeService.ValidateBusinessInsertAsync(department));
        }

        /// <summary>
        /// Unit test cho phương thức ValidateBusinessInsertAsync với DepartmentName hợp lệ
        /// </summary>
        /// CreatedBy: hiennt200210 (25/09/2023)
        [Test]
        public async Task ValidateBusinessUpdateAsync_ValidInput_Success()
        {
            // Arrange
            var oldDepartment = new Department()
            {
                DepartmentId = Guid.NewGuid(),
                DepartmentName = "Phòng kế toán",
            };

            var newDepartment = new Department()
            {
                DepartmentId = Guid.NewGuid(),
                DepartmentName = "Phòng kế toán",
            };

            // Act
            await DepartmentService.ValidateBusinessUpdateAsync(oldDepartment, newDepartment);

            // Assert
            await DepartmentValidate.Received(1).CheckDuplicateDepartmentNameAsync(newDepartment.DepartmentName);
        }

        /// <summary>
        /// Unit test cho phương thức ValidateBusinessInsertAsync với dữ liệu không hợp lệ
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task ValidateBusinessUpdateAsync_InvalidInput_ThrowException()
        {
            // Arrange
            var oldDepartment = new Department()
            {
                DepartmentId = Guid.NewGuid(),
                DepartmentName = "Phòng kế toán",
            };

            var newDepartment = new Department()
            {
                DepartmentId = Guid.Empty,
                DepartmentName = "Phòng kế toán",
            };

            // Act & Assert
            //var exception = Assert.ThrowsAsync<BadRequestException>(async () => await EmployeeService.ValidateBusinessUpdateAsync(oldDepartment, newDepartment));
        }
    }
}
