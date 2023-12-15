using CardGame;
using Cards;

namespace Players;

public class WebAppPlayer : Player
{
    public WebAppPlayer(string name, Strategy.Strategy strategy) : base(name, strategy)
    {
    }
}