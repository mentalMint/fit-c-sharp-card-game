using CardGame;
using Cards;
using Microsoft.AspNetCore.Mvc;
using Players.Models;
using Strategy;

namespace Players.Controllers;

[Route("api/cards")]
public class PlayerController : ControllerBase
{
    [HttpPost]
    public IActionResult Post([FromBody] PlayerModel model)
    {
        var player = new Player("", new FirstCardStrategy())
        {
            CardDeck = new CardDeck(model.Order)
        };
        var response = new GetCardNumberResponse()
        {
            CardNumber = player.GetCardNumber()
        };

        var jsonContent = Newtonsoft.Json.JsonConvert.SerializeObject(response);
        return Ok(jsonContent);
    }
    
    [HttpGet]
    public IActionResult Get()
    {
        return Ok();
    }
}