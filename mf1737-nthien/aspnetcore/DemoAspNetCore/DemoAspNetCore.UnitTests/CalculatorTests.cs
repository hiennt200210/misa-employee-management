using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoAspNetCore.UnitTests
{
    [TestFixture]
    public class CalculatorTests
    {
        /// <summary>
        /// Unit test cho phương thức Add với input hợp lệ
        /// </summary>
        /// <param name="x">Số nguyên thứ nhất</param>
        /// <param name="y">Số nguyên thứ hai</param>
        /// <param name="expected">Kết quả mong đợi</param>
        /// 
        /// CreatedBy: hiennt200210 (12/09/2023)
        [Test]
        [TestCase(1, 2, 3)]
        [TestCase(2, 3, 5)]
        [TestCase(3, 4, 7)]
        public void Add_ValidInput_Sum2Digit(int x, int y, int expected)
        {
            // Arrange
            var calculator = new Calculator();

            // Act
            var actual = calculator.Add(x, y);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }


        /// <summary>
        /// Unit test cho phương thức Add với input hợp lệ
        /// </summary>
        /// 
        /// <param name="input">Chuỗi đầu vào hợp lệ</param>
        /// <param name="expected">Kết quả mong đợi</param>
        /// 
        /// CreatedBy: hiennt200210 (12/09/2023)
        [Test]
        [TestCase("", 0)]
        [TestCase("1, 2, 3", 6)]
        [TestCase("2, 3, 5", 10)]
        public void Add_ValidInput_SumOfOperands(string input, int expected)
        {
            // Arrange
            var calculator = new Calculator();

            // Act
            var actual = calculator.Add(input);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        /// <summary>
        /// Unit test cho phương thức Add với input không hợp lệ
        /// </summary>
        /// 
        /// <param name="input">Chuỗi đầu không hợp lệ</param>
        /// <param name="expected">Thông báo chuỗi không hợp lệ</param>
        /// 
        /// CreatedBy: hiennt200210 (12/09/2023)
        [Test]
        [TestCase("2, -abc, 5", "Chuỗi không hợp lệ!")]
        [TestCase("2, -abc, -def", "Chuỗi không hợp lệ!")]
        public void Add_InvalidInput_FormatException(string input, string expected)
        {
            // Arrange
            var calculator = new Calculator();

            // Act và Assert
            try
            {
                var actual = calculator.Add(input);
            }
            catch (FormatException ex)
            {
                Assert.That(ex.Message, Is.EqualTo(expected));
            }
        }

        /// <summary>
        /// Unit test cho phương thức Add với input không hợp lệ
        /// </summary>
        /// 
        /// <param name="input">Chuỗi đầu không hợp lệ</param>
        /// <param name="expected">Thông báo không chấp nhận toán hạng âm</param>
        /// 
        /// CreatedBy: hiennt200210 (12/09/2023)
        [Test]
        [TestCase("2, -3, -5", "Không chấp nhận toán hạng âm: -3, -5")]
        [TestCase("2, -3, -5, -7", "Không chấp nhận toán hạng âm: -3, -5, -7")]
        public void Add_NegativeOperands_FormatException(string input, string expected)
        {
            // Arrange
            var calculator = new Calculator();

            // Act và Assert
            try
            {
                var actual = calculator.Add(input);
            }
            catch (FormatException ex)
            {
                Assert.That(ex.Message, Is.EqualTo(expected));
            }
        }

        /// <summary>
        /// Unit test cho phương thức Sub với input không hợp lệ
        /// </summary>
        /// <param name="x">Số nguyên thứ nhất</param>
        /// <param name="y">Số nguyên thứ hai</param>
        /// <param name="expected">Kết quả mong đợi</param>
        /// 
        /// CreatedBy: hiennt200210 (12/09/2023)
        [Test]
        [TestCase(1, 2, -1)]
        [TestCase(2, 3, -1)]
        [TestCase(3, 4, -1)]
        public void Sub_ValidInput_Sub2Digit(int x, int y, int expected)
        {
            // Arrange
            var calculator = new Calculator();

            // Act
            var actual = calculator.Sub(x, y);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        /// <summary>
        /// Unit test cho phương thức Mul với input hợp lệ
        /// </summary>
        /// <param name="x">Số nguyên thứ nhất</param>
        /// <param name="y">Số nguyên thứ hai</param>
        /// <param name="expected">Kết quả mong đợi</param>
        /// 
        /// CreatedBy: hiennt200210 (12/09/2023)
        [Test]
        [TestCase(1, 2, 2)]
        [TestCase(2, 3, 6)]
        [TestCase(3, 4, 12)]
        public void Mul_ValidInput_Mul2Digit(int x, int y, int expected)
        {
            // Arrange
            var calculator = new Calculator();

            // Act
            var actual = calculator.Mul(x, y);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        /// <summary>
        /// Unit test cho phương thức Div với input hợp lệ
        /// </summary>
        /// <param name="x">Số nguyên thứ nhất</param>
        /// <param name="y">Số nguyên thứ hai</param>
        /// <param name="expected">Kết quả mong đợi</param>
        /// 
        /// CreatedBy: hiennt200210 (12/09/2023)
        [Test]
        [TestCase(1, 2, 0)]
        [TestCase(2, 3, 0)]
        [TestCase(3, 4, 0)]
        public void Div_ValidInput_Div2Digit(int x, int y, int expected)
        {
            // Arrange
            var calculator = new Calculator();

            // Act
            var actual = calculator.Div(x, y);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
