using CardGame.model;
using CardGame.view;

Console.WriteLine("Start");
var model = new Model(CardDeck.NewCardDeck(), new FirstCardStrategy(), new FirstCardStrategy());
var consoleView = new ConsoleView(model);
model.Run();
Console.WriteLine("Finnish");
