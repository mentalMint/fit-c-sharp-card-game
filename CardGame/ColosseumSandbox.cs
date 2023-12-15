using System.Diagnostics;
using Cards;
using Microsoft.Extensions.Hosting;

namespace CardGame;

public interface ISandbox
{
    void Run();

    void Run(ICardDeck cardDeck);

    bool CardsColorsMatched { get; }
}

public class ColosseumSandbox : ISandbox
{
    private readonly ICardDeck _cardDeck;
    private readonly IEnumerable<Player> _players;
    private IPlayer _elon;
    private IPlayer _mark;

    public void Run(ICardDeck cardDeck)
    {
        throw new NotImplementedException();
    }

    public bool CardsColorsMatched { get; private set; } = false;

    public ColosseumSandbox(ICardDeck cardDeck, IEnumerable<Player> players)
    {
        _cardDeck = cardDeck;
        _players = players;
        _elon = _players.ElementAt(0);
        _mark = _players.ElementAt(1);
    }

    private void PrintCards(Card[] cards)
    {
        foreach (var card in cards)
        {
            Console.WriteLine(card.color);
        }
    }

    public void Run()
    {
        _cardDeck.Shuffle();
        var t = _cardDeck.SplitMidPoint();
        _elon.CardDeck = t.firstDeck;
        _mark.CardDeck = t.secondDeck;
        var elonsNumber = _elon.GetCardNumber();
        var marksNumber = _mark.GetCardNumber();
        if (t.firstDeck == null || t.secondDeck == null)
        {
            throw new NullReferenceException();
        }

        CardsColorsMatched = t.firstDeck.Cards[elonsNumber].color.Equals(t.secondDeck.Cards[marksNumber].color);
    }
}

public class ColosseumSandboxNotShuffle : ISandbox
{
    private readonly ICardDeck _cardDeck;
    private readonly IEnumerable<IPlayer> _players;
    private IPlayer _elon;
    private IPlayer _mark;

    public void Run()
    {
        throw new NotImplementedException();
    }

    public bool CardsColorsMatched { get; private set; } = false;

    public ColosseumSandboxNotShuffle(IEnumerable<IPlayer> players)
    {
        _players = players;
        _elon = _players.ElementAt(0);
        _mark = _players.ElementAt(1);
    }

    private void PrintCards(Card[] cards)
    {
        foreach (var card in cards)
        {
            Console.WriteLine(card.color);
        }
    }

    public void Run(ICardDeck cardDeck)
    {
        var t = cardDeck.SplitMidPoint();
        _elon.CardDeck = t.firstDeck;
        _mark.CardDeck = t.secondDeck;
        var elonsNumber = _elon.GetCardNumber();
        var marksNumber = _mark.GetCardNumber();
        if (t.firstDeck == null || t.secondDeck == null)
        {
            throw new NullReferenceException();
        }

        CardsColorsMatched = t.firstDeck.Cards[elonsNumber].color.Equals(t.secondDeck.Cards[marksNumber].color);
    }
}

public class ColosseumSandboxWeb : ISandbox
{
    private readonly ICardDeck _cardDeck;
    private readonly IEnumerable<IPlayer> _players;
    private IPlayer _elon;
    private IPlayer _mark;

    public void Run()
    {
        throw new NotImplementedException();
    }

    public bool CardsColorsMatched { get; private set; } = false;

    public ColosseumSandboxWeb(IEnumerable<IPlayer> players)
    {
        _players = players;
        _elon = _players.ElementAt(0);
        _mark = _players.ElementAt(1);
    }

    private void PrintCards(Card[] cards)
    {
        foreach (var card in cards)
        {
            Console.WriteLine(card.color);
        }
    }

    public void Run(ICardDeck cardDeck)
    {
        cardDeck.Shuffle();
        var t = cardDeck.SplitMidPoint();
        Console.WriteLine("Send first request");
        var elonsTask = _elon.GetCardNumber(t.firstDeck, "http://localhost:5136/api/cards");
        Console.WriteLine("Send second request");
        var marksTask = _mark.GetCardNumber(t.secondDeck, "http://localhost:5137/api/cards");
        if (t.firstDeck == null || t.secondDeck == null)
        {
            throw new NullReferenceException();
        }
        Console.WriteLine("Wait for response");
        elonsTask.Wait();
        marksTask.Wait();
        CardsColorsMatched = t.firstDeck.Cards[elonsTask.Result.CardNumber].color
            .Equals(t.secondDeck.Cards[marksTask.Result.CardNumber].color);
    }
}