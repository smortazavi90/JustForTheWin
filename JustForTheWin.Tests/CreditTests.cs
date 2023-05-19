using JustForTheWin.Models;

namespace JustForTheWin.Tests
{
    [TestFixture]
    public class CreditTests
    {
        [Test]
        public void Decrease_ShouldDecreaseValueByAmount()
        {
            // Arrange
            Credit credit = new();
            var initialAmount = credit.GetValue();
            var decreaseAmount = 10;

            // Act
            var result = credit.Decrease(decreaseAmount).GetValue();

            // Assert
            Assert.AreEqual(initialAmount - decreaseAmount, result);
        }

        [Test]
        public void Increase_ShouldIncreaseValueByAmount()
        {
            // Arrange
            Credit credit = new();
            var initialAmount = credit.GetValue();
            var increaseAmount = 20;

            // Act
            var result = credit.Increase(increaseAmount).GetValue();

            // Assert
            Assert.AreEqual(initialAmount + increaseAmount, result);
        }

        [Test]
        public void Print_ShouldWriteCurrentValueToConsole()
        {
            // Arrange
            Credit credit = new();
            var initialAmount = credit.GetValue();

            // Act
            using StringWriter sw = new();
            Console.SetOut(sw);
            credit.Print();
            var result = sw.ToString().Trim();

            // Assert
            Assert.AreEqual($"Current credit: {initialAmount}", result);
        }
    }
}