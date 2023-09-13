using CardGame.model;

namespace CardGame.view;

public class ConsoleView : IObserver<bool>
{
    private readonly Model _model;

    public ConsoleView(Model model)
    {
        _model = model;
        _model.Subscribe(this);
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
        if (cardColorsMatched)
        {
            Console.WriteLine("Colors matched. Fight!");
        }
        else
        {
            Console.WriteLine("Colors didn't match. Not today(");
        }
    }
}
