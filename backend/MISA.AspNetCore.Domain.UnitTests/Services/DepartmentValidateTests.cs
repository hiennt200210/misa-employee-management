using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AspNetCore.Domain.UnitTests
{
    [TestFixture]
    public class DepartmentValidateTests
    {
        public IDepartmentRepository DepartmentRepository { get; set; }
        public DepartmentValidate DepartmentValidate { get; set; }

        /// <summary>
        /// Setup các đối tượng cần thiết cho việc test
        /// </summary>
        /// CreatedBy: hiennt200210 (25/09/2023)
        [SetUp]
        public void Setup()
        {
            DepartmentRepository = Substitute.For<IDepartmentRepository>();
            DepartmentValidate = Substitute.For<DepartmentValidate>(DepartmentRepository);
        }

        /// <summary>
        /// Unit test cho phương thức CheckDuplicateDepartmentNameAsync với tên phòng ban chưa tồn tại
        /// </summary>
        /// CreatedBy: hiennt200210 (25/09/2023)
        [Test]
        public async Task CheckDuplicateDepartmentNameAsync_NotDuplicateDepartmentName_Success()
        {
            // Arrange
            var departmentName = "Phòng kế toán";

            // Act
            await DepartmentValidate.CheckDuplicateDepartmentNameAsync(departmentName);

            // Assert
            await DepartmentRepository.Received(1).CheckDuplicateDepartmentNameAsync(departmentName);
        }

        /// <summary>
        /// Unit test cho phương thức CheckDuplicateDepartmentNameAsync với tên phòng ban đã tồn tại
        /// </summary>
        /// CreatedBy: hiennt200210 (25/09/2023)
        [Test]
        public async Task CheckDuplicateDepartmentNameAsync_DuplicateDepartmentName_ThrowException()
        {
            // Arrange
            var departmentName = "Phòng kế toán";

            await DepartmentRepository.CheckDuplicateDepartmentNameAsync(departmentName);

            // Act & Assert
            var exception = Assert.ThrowsAsync<ConflictException>(async () => await DepartmentValidate.CheckDuplicateDepartmentNameAsync(departmentName));
        }
    }
}
