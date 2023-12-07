﻿using Cards;
using Microsoft.Extensions.Hosting;

namespace CardGame;

public interface ISandbox
{
    void Run();
    bool CardsColorsMatched { get; }
}

public class ColosseumSandbox : ISandbox
{
    private readonly ICardDeck _cardDeck;
    private readonly IEnumerable<Player> _players;
    private IPlayer _elon;
    private IPlayer _mark;
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
    private readonly IEnumerable<Player> _players;
    private IPlayer _elon;
    private IPlayer _mark;
    public bool CardsColorsMatched { get; private set; } = false;

    public ColosseumSandboxNotShuffle(ICardDeck cardDeck, IEnumerable<Player> players)
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