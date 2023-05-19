using JustForTheWin.Enums;
using JustForTheWin.Models;

namespace JustForTheWin.Tests
{
    [TestFixture]
    public class GameTests
    {
        [Test]
        public void UpdatePlayerBalance_WinBallType_ShouldIncreasePlayerCredits()
        {
            // Arrange
            Player player = new();
            var ballType = BallType.Win;

            // Act
            Game.UpdatePlayerBalance(player, ballType);

            // Assert
            Assert.That(GameConfiguration.WinCredits - GameConfiguration.PickCost,
                Is.EqualTo(player.Credit.GetValue()));
        }

        [Test]
        public void UpdatePlayerBalance_NoWinBallType_ShouldDecreasePlayerCredits()
        {
            // Arrange
            Player player = new();
            var ballType = BallType.NoWin;

            // Act
            Game.UpdatePlayerBalance(player, ballType);

            // Assert
            Assert.That(-GameConfiguration.PickCost, Is.EqualTo(player.Credit.GetValue()));
        }

        [Test]
        public void PickBall_ShouldReturnValidBallType()
        {
            // Arrange
            Random random = new();

            // Act
            var ballType = Game.PickBall(random);

            // Assert
            Assert.That(Enum.IsDefined(typeof(BallType), ballType));
        }
    }
}