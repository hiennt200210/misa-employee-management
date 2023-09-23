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
        [Test]
        public void CheckDuplicateEmployeeCodeAsync_NotDuplicateEmployeeCode_ReturnsFalse()
        {
            //// Arrange
            //var employeeCode = "NV0001";
            //var employeeId = Guid.NewGuid();
            //var employeeRepository = new Mock<IEmployeeRepository>();
            //employeeRepository.Setup(m => m.CheckDuplicateEmployeeCodeAsync(employeeCode, employeeId)).ReturnsAsync(true);
            //var employeeService = new EmployeeService(employeeRepository.Object);
            //// Act
            //var result = employeeService.CheckDuplicateEmployeeCodeAsync(employeeCode, employeeId).Result;
            //// Assert
            //Assert.IsFalse(result);
        }
    }
}
