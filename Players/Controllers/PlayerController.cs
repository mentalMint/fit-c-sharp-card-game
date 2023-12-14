using Microsoft.AspNetCore.Mvc;
using Players.Models;

namespace Players.Controllers;

[Route("api/cards")]
public class PlayerController : ControllerBase
{
    [HttpPost]
    public IActionResult Post([FromBody] PlayerModel model)
    {
        // Handle the posted data
        // Example: return the received data
        return Ok(model);
    }
    
    [HttpGet]
    public IActionResult Get()
    {
        // Handle the posted data
        // Example: return the received data
        return Ok();
    }
}