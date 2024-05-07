using CityProject.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CityProject.Controllers
{
    /// <summary>
    /// API controller for ordering cities based on specified positions.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class CityController : ControllerBase
    {
        public CityController()
        {
        }

        [HttpGet(Name = "GetCitiesOrderedByPosition")]
        public IActionResult GetCitiesOrderedByPosition([FromQuery] string[] cityNamesArray, [FromQuery] string[] positionsArray)
        {
            /// <summary>
            /// Gets a list of cities ordered by their specified positions.
            /// </summary>
            /// <param name="cityNamesArray">An array of city names.</param>
            /// <param name="positionsArray">An array of positions corresponding to the city names.</param>
            /// <returns>The ordered list of city names as a comma-separate d string.</returns>
            try
            {
                if (cityNamesArray.Length < 2) return BadRequest("Add at least 2 cities");
                if (cityNamesArray.Length != positionsArray.Length) return BadRequest("Both arrays need to be the same size");

                List<City> cityList = [];

                for (int i = 0; i < positionsArray.Length; i++)
                {
                    cityList.Add(new City { CityName = cityNamesArray[i], Order = positionsArray[i] });
                }

                string orderedResult = string.Join(", ", cityList.OrderBy(x => x.Order).Select(x => x.CityName));

                return Ok(orderedResult);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
