using System.Collections;
using Cards;
using Strategy;

namespace CardGame;

public interface IPlayer
{
    ICardDeck CardDeck { get; set; }
    int GetCardNumber();
}

public class Player : IPlayer
{
    public string _name;
    private readonly Strategy.Strategy _strategy;

    private ICardDeck _cardDeck;

    public ICardDeck CardDeck
    {
        get => _cardDeck;
        set
        {
            _cardDeck = value;
            _strategy.CardDeck = value;
        }
    }

    public Player(string name, Strategy.Strategy strategy)
    {
        _name = name;
        _strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
    }

    public int GetCardNumber()
    {
        return _strategy.GetCardNumber();
    }
}