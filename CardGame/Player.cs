using Cards;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace CardGame;

public interface IPlayer
{
    ICardDeck CardDeck { get; set; }
    int GetCardNumber();

    Task<GetCardNumberResponse> GetCardNumber(ICardDeck cardDeck, string url);
}

public class Player : IPlayer
{
    public string _name;
    private readonly Strategy.Strategy _strategy;

    private ICardDeck _cardDeck;

    public ICardDeck CardDeck
    {
        get => _cardDeck;
        set
        {
            _cardDeck = value;
            _strategy.CardDeck = value;
        }
    }

    public Player(string name, Strategy.Strategy strategy)
    {
        _name = name;
        _strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
    }

    public int GetCardNumber()
    {
        return _strategy.GetCardNumber();
    }

    public Task<GetCardNumberResponse> GetCardNumber(ICardDeck cardDeck, string url)
    {
        throw new NotImplementedException();
    }
}

public class WebPlayer : IPlayer
{
    public string _name;
    private readonly Strategy.Strategy _strategy;

    private ICardDeck _cardDeck;

    public ICardDeck CardDeck
    {
        get => _cardDeck;
        set
        {
            _cardDeck = value;
            _strategy.CardDeck = value;
        }
    }

    public async Task<GetCardNumberResponse> GetCardNumber(ICardDeck cardDeck, string url)
    {
        var cardDto = new CardDTO
        {
            Order = cardDeck.ToString()
        };

        // Convert the DTO to JSON
        var jsonContent = Newtonsoft.Json.JsonConvert.SerializeObject(cardDto);
        return await SendPostRequest(jsonContent, url);
    }

    private static async Task<GetCardNumberResponse> SendPostRequest(string postData, string url)
    {
        var apiUrl = url;
        using var httpClient = new HttpClient();
        HttpContent content = new StringContent(postData, System.Text.Encoding.UTF8, "application/json");
        try
        {
            var response = await httpClient.PostAsync(apiUrl, content);
            var responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Response: " + responseContent);
                return JsonSerializer.Deserialize<GetCardNumberResponse>(responseContent) ??
                       new GetCardNumberResponse() { CardNumber = 0 };
            }
            else
            {
                Console.WriteLine("Error: " + response.StatusCode);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception: " + ex.Message);
        }

        return new GetCardNumberResponse() { CardNumber = 0 };
    }

    public WebPlayer(string name, Strategy.Strategy strategy)
    {
        _name = name;
        _strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
    }

    public int GetCardNumber()
    {
        return _strategy.GetCardNumber();
    }
}