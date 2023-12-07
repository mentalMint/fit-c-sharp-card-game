using System.CodeDom.Compiler;
using Cards;

namespace CardGame;

public static class CardDeckGenerator
{
    public static IEnumerable<CardDeck> Generate(int number)
    {
        for (var i = 0; i < number; i++)
        {
            var cardDeck = new CardDeck(36);
            cardDeck.Shuffle();
            yield return cardDeck;
        }
    }
}