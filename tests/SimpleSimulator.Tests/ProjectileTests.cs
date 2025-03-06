using Xunit;
using SimpleSimulator.Models;
using System;

namespace SimpleSimulator.Tests
{
    public class ProjectileTests
    {
        private const double Gravity = 9.81;

        [Fact]
        public void CalculateProjectileYPosition_ReturnsCorrectValue()
        {
            // Arrange
            double initialHeight = 10;
            double speed = 20;
            double angle = 45;
            var projectile = new ProjectileModel(speed, angle, initialHeight);

            double time = 2;
            double expectedYPos = initialHeight + (speed * time * Math.Sin(angle * Math.PI / 180)) - (0.5 * Gravity * time * time);

            // Act
            double actualYPos = projectile.GetYPosition(time);

            // Assert
            Assert.Equal(expectedYPos, actualYPos, 2); // Allow slight rounding error
        }

        [Fact]
        public void CalculateProjectileXPosition_ReturnsCorrectValue()
        {
            // Arrange
            double initialHeight = 10;
            double speed = 20;
            double angle = 45;
            var projectile = new ProjectileModel(speed, angle, initialHeight);

            double time = 2;
            double expectedXPosition = speed * Math.Cos(angle * Math.PI / 180) * time;

            // Act
            double actualXPosition = projectile.GetXPosition(time);

            // Assert
            Assert.Equal(expectedXPosition, actualXPosition, 2); // Allow slight rounding error
        }
    }
}
