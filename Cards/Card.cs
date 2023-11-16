namespace Cards;


public class Card
{
    public readonly Color color;

    public Card(Color color)
    {
        this.color = color;
    }
}

public enum Color
{
    Black,
    Red,
}