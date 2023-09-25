using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AspNetCore.Domain.UnitTests.Services
{
    [TestFixture]
    public class EmployeeValidateTests
    {
        public IEmployeeRepository EmployeeRepository { get; set; }
        public EmployeeValidate EmployeeValidate { get; set; }

        /// <summary>
        /// Setup các đối tượng cần thiết cho việc test
        /// </summary>
        /// CreatedBy: hiennt200210 (25/09/2023)
        [SetUp]
        public void Setup()
        {
            EmployeeRepository = Substitute.For<IEmployeeRepository>();
            EmployeeValidate = Substitute.For<EmployeeValidate>(EmployeeRepository);
        }

        /// <summary>
        /// Unit test cho phương thức CheckDuplicateEmployeeCodeAsync với mã nhân viên chưa tồn tại
        /// </summary>
        /// CreatedBy: hiennt200210 (25/09/2023)
        [Test]
        public async Task CheckDuplicateEmployeeCodeAsync_NotDuplicateEmployeeCode_Success()
        {
            // Arrange
            var employeeCode = "NV0001";

            // Act
            await EmployeeValidate.CheckDuplicateEmployeeCodeAsync(employeeCode);

            // Assert
            await EmployeeRepository.Received(1).CheckDuplicateEmployeeCodeAsync(employeeCode);
        }

        /// <summary>
        /// Unit test cho phương thức CheckDuplicateEmployeeCodeAsync với mã nhân viên đã tồn tại
        /// </summary>
        /// CreatedBy: hiennt200210 (25/09/2023)
        [Test]
        public async Task CheckDuplicateEmployeeCodeAsync_DuplicateEmployeeCode_ThrowException()
        {
            // Arrange
            var employeeCode = "NV0001";

            await EmployeeRepository.CheckDuplicateEmployeeCodeAsync(employeeCode);

            // Act & Assert
            var exception = Assert.ThrowsAsync<ConflictException>(async () => await EmployeeValidate.CheckDuplicateEmployeeCodeAsync(employeeCode));
        }
    }
}
