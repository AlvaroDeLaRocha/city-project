using CityProject.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace CityOrdering.Tests
{
    [TestFixture]
    public class CityControllerTests
    {
        [Test]
        public void GetCitiesOrderedByPosition_Should_ReturnOrderedCities()
        {
            // Arrange
            var controller = new CityController();
            var cityNamesArray = new[] { "Tijuana", "Los Angeles", "Queretaro", "Monterrey", "San Diego" };
            var positionsArray = new[] { "3", "2", "1", "5", "4" };

            // Act
            var result = controller.GetCitiesOrderedByPosition(cityNamesArray, positionsArray) as OkObjectResult;
            var orderedCities = result?.Value as string;

            Assert.Multiple(() =>
            {
                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(orderedCities, Is.EqualTo("Queretaro, Los Angeles, Tijuana, San Diego, Monterrey"));
            });
        }

        [Test]
        public void GetCitiesOrderedByPosition_Should_ReturnBadRequest_WhenFewerThanTwoCities()
        {
            // Arrange
            var controller = new CityController();
            var cityNamesArray = new[] { "Tijuana" }; // Less than 2 cities
            var positionsArray = new[] { "1" };

            // Act
            var result = controller.GetCitiesOrderedByPosition(cityNamesArray, positionsArray) as BadRequestObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.EqualTo("Add at least 2 cities"));
        }

        [Test]
        public void GetCitiesOrderedByPosition_Should_ReturnBadRequest_WhenEmptyArrays()
        {
            // Arrange
            var controller = new CityController();
            var emptyCityNamesArray = Array.Empty<string>();
            var emptyPositionsArray = Array.Empty<string>();

            // Act
            var result = controller.GetCitiesOrderedByPosition(emptyCityNamesArray, emptyPositionsArray) as BadRequestObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.EqualTo("Add at least 2 cities"));
        }

        [Test]
        public void GetCitiesOrderedByPosition_Should_ReturnBadRequest_WhenArraysDifferentLength()
        {
            // Arrange
            var controller = new CityController();
            var cityNamesArray = new[] { "Tijuana", "Queretaro" };
            var positionsArray = new[] { "1" }; // 1 more position missing

            // Act
            var result = controller.GetCitiesOrderedByPosition(cityNamesArray, positionsArray) as BadRequestObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.EqualTo("Both arrays need to be the same size"));
        }
    }
}