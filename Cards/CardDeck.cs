﻿using System.Text;

namespace Cards;

static class RandomExtensions
{
    public static void Shuffle<T>(this Random rng, T[] array)
    {
        int n = array.Length;
        while (n > 1)
        {
            int k = rng.Next(n--);
            (array[n], array[k]) = (array[k], array[n]);
        }
    }
}

public interface ICardDeck
{
    Card[] Cards { get; }
    void Shuffle();

    void SplitMidPoint(out ICardDeck? firstDeck, out ICardDeck? secondDeck);

    (ICardDeck firstDeck, ICardDeck secondDeck) SplitMidPoint();
}

public class CardDeck : ICardDeck
{
    private readonly Card[] _cards;

    public Card[] Cards => _cards;

    public CardDeck(int cardsNumber)
    {
        _cards = new Card[cardsNumber];
        for (var i = 0; i < _cards.Length / 2; i++)
        {
            _cards[i] = new Card(Color.Black);
        }

        for (var i = _cards.Length / 2; i < _cards.Length; i++)
        {
            _cards[i] = new Card(Color.Red);
        }
    }

    public CardDeck(string cardsOrder)
    {
        _cards = new Card[cardsOrder.Length];
        for (int i = 0; i < cardsOrder.Length; i++)
        {
            _cards[i] = (new Card(cardsOrder[i] == 'R' ? Color.Red : Color.Black));
        }
    }

    public CardDeck(Card[] cards)
    {
        _cards = cards;
    }

    public CardDeck()
    {
    }

    public virtual void Shuffle()
    {
        var rng = new Random();
        rng.Shuffle(_cards);
    }

    private void SplitArray<T>(T[] array, int index, out T[] first, out T[] second)
    {
        first = array.Take(index).ToArray();
        second = array.Skip(index).ToArray();
    }

    private void SplitArrayMidPoint<T>(T[] array, out T[] first, out T[] second)
    {
        SplitArray(array, array.Length / 2, out first, out second);
    }

    public void SplitMidPoint(out ICardDeck firstDeck, out ICardDeck secondDeck)
    {
        SplitArrayMidPoint(_cards, out var first, out var second);
        firstDeck = new CardDeck(first);
        secondDeck = new CardDeck(second);
    }

    public (ICardDeck firstDeck, ICardDeck secondDeck) SplitMidPoint()
    {
        SplitMidPoint(out var first, out var second);

        return (first, second);
    }

    public override string ToString()
    {
        var stringBuilder = new StringBuilder("");
        foreach (var card in Cards)
        {
            stringBuilder.Append(card.color == Color.Black ? "B" : "R");
        }

        return stringBuilder.ToString();
    }
}