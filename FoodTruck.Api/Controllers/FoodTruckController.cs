using FoodTruck.Common.Models;
using FoodTruck.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodTruck.Api.Controllers;
[ApiController]
[Route("foodTruck-api/[controller]")]
public class FoodTruckController : ControllerBase
{
    private readonly ILogger<FoodTruckController> _logger;
    private readonly IFoodTruckService _foodTruckService;

    public FoodTruckController(ILogger<FoodTruckController> logger, IFoodTruckService foodTruckService)
    {
        _logger = logger;
        _foodTruckService = foodTruckService;
    }

    [HttpGet("GetFoodTrucksInRadius")]
    [ProducesResponseType(typeof(List<GetFoodTruckResponse>), StatusCodes.Status200OK)]
    public IActionResult GetFoodTrucksInRadius([FromQuery] GetFoodTrucksRequest request)
    {
        var response = _foodTruckService.GetFoodTrucksInRadius(request);

        return Ok(response);
    }

    [HttpGet("GetFoodTrucksAsync")]
    [ProducesResponseType(typeof(List<GetFoodTruckResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetFoodTrucksAsync([FromQuery] GetFoodTrucksRequest request)
    {
        var response = await _foodTruckService.GetFoodTrucksAsync(request);

        return Ok(response);
    }
}
