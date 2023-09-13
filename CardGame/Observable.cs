namespace CardGame;

public abstract class Observable : IObservable<bool>
{
    protected readonly List<IObserver<bool>> Observers;

    protected Observable()
    {
        Observers = new List<IObserver<bool>>();
    }

    public IDisposable Subscribe(IObserver<bool> observer)
    {
        if (!Observers.Contains(observer))
            Observers.Add(observer);
        return new Unsubscriber(Observers, observer);
    }

    private class Unsubscriber : IDisposable
    {
        private readonly List<IObserver<bool>> _observers;
        private readonly IObserver<bool> _observer;

        public Unsubscriber(List<IObserver<bool>> observers, IObserver<bool> observer)
        {
            this._observers = observers;
            this._observer = observer;
        }

        public void Dispose()
        {
            if (_observer != null && _observers.Contains(_observer))
                _observers.Remove(_observer);
        }
    }
}