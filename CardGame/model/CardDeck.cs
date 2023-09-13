namespace CardGame.model;

static class RandomExtensions
{
    public static void Shuffle<T> (this Random rng, T[] array)
    {
        int n = array.Length;
        while (n > 1) 
        {
            int k = rng.Next(n--);
            (array[n], array[k]) = (array[k], array[n]);
        }
    }
}

public class CardDeck
{
    private readonly Card[] _cards;

    public Card[] Cards => _cards;

    public CardDeck(Card[] cards)
    {
        _cards = cards;
    }

    public static CardDeck NewCardDeck()
    {
        var cards = new Card[36];
        for (int i = 0; i < cards.Length / 2; i++)
        {
            cards[i] = Card.Black;
        }
        for (int i = cards.Length / 2; i < cards.Length; i++)
        {
            cards[i] = Card.Red;
        }
        return new CardDeck(cards);
    }
    
    public void Shuffle()
    {
        var rng = new Random();
        rng.Shuffle(_cards);
    }
    
    private void Split<T>(T[] array, int index, out T[] first, out T[] second) {
        first = array.Take(index).ToArray();
        second = array.Skip(index).ToArray();
    }

    private void SplitMidPoint<T>(T[] array, out T[] first, out T[] second) {
        Split(array, array.Length / 2, out first, out second);
    }

    public void SplitMidPoint(out CardDeck? firstDeck, out CardDeck? secondDeck)
    {
        SplitMidPoint(_cards, out var first, out var second);
        firstDeck = new CardDeck(first);
        secondDeck = new CardDeck(second);
    }
}