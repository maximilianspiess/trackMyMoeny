using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TrackMyMoney;

public class StockApi
{
    private static readonly HttpClient HttpClient = new()
    {
        BaseAddress = new Uri("https://finnhub.io/")
    };

    public async Task<Investment?> AddInvestment(string code, int shares)
    {
        var symbol = code;
        var apiKey = Environment.GetEnvironmentVariable("API_KEY");

        using HttpResponseMessage response =
            await HttpClient.GetAsync("api/v1/quote?" + "&symbol=" + symbol + "&token=" + apiKey);

        if (response.IsSuccessStatusCode)
        {
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var jsonObject = JObject.Parse(jsonResponse);
            var stock = JsonConvert.DeserializeObject<StockData>(jsonObject.ToString());
            if (stock != null)
            {
                return new Investment
                {
                    PurchasePricePerShare = stock.Current,
                    LastPricePerShare = stock.Current,
                    Code = code,
                    Date = DateOnly.FromDateTime(DateTime.Now),
                    Shares = shares,
                    LastUpdate = DateTime.Now
                };
            }
        }

        return null;
    }

    public async Task UpdateSingleInvestment(Investment investment)
    {
        var symbol = investment.Code;
        var apiKey = Environment.GetEnvironmentVariable("API_KEY");

        using HttpResponseMessage response =
            await HttpClient.GetAsync("api/v1/quote?" + "&symbol=" + symbol + "&token=" + apiKey);

        if (response.IsSuccessStatusCode)
        {
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var jsonObject = JObject.Parse(jsonResponse);
            var stock = JsonConvert.DeserializeObject<StockData>(jsonObject.ToString());
            if (stock != null)
            {
                investment.LastPricePerShare = stock.Current;
                investment.UpdateTime();
            }
        }
    }
}

class StockData
    {
        [JsonProperty("c")] public decimal Current { get; set; }
        [JsonProperty("o")] public decimal Open { get; set; }
        [JsonProperty("h")] public decimal High { get; set; }
        [JsonProperty("l")] public decimal Low { get; set; }
    }