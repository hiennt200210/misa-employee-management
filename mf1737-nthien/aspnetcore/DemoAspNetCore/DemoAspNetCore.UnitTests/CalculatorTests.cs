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
