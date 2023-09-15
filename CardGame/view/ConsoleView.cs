using CardGame.model;

namespace CardGame.view;

public class ConsoleView : IObserver<bool>
{
    private readonly Sandbox _sandbox;

    public ConsoleView(Sandbox sandbox)
    {
        _sandbox = sandbox;
        _sandbox.Subscribe(this);
    }
    
    public void OnCompleted()
    {
        
    }

    public void OnError(Exception error)
    {
        Console.Error.WriteLine(error);
    }

    public void OnNext(bool cardColorsMatched)
    {
        Console.WriteLine(cardColorsMatched ? "Colors matched. Fight!" : "Colors didn't match. Not today(");
    }
}
