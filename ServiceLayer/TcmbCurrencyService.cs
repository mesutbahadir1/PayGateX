using PayGateX.Interfaces;

namespace PayGateX.Service;

using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

public class TcmbCurrencyService : ITcmbCurrencyService
{
    private readonly HttpClient _httpClient;
    private const string TcmbUrl = "https://www.tcmb.gov.tr/kurlar/today.xml";

    public TcmbCurrencyService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<decimal> GetUsdTryRateAsync()
    {
        return await GetCurrencyRateAsync("USD");
    }

    public async Task<decimal> GetEurTryRateAsync()
    {
        return await GetCurrencyRateAsync("EUR");
    }

    private async Task<decimal> GetCurrencyRateAsync(string currencyCode)
    {
        var xmlString = await _httpClient.GetStringAsync(TcmbUrl);

        var xml = XDocument.Parse(xmlString);

        var currencyElement = xml.Root?
            .Element("Currency")?.Parent?
            .Elements("Currency")?
            .FirstOrDefault(c => (string)c.Attribute("CurrencyCode") == currencyCode);

        if (currencyElement == null)
            throw new Exception($"{currencyCode} not found.");

        var rateString = currencyElement.Element("ForexSelling")?.Value;

        if (string.IsNullOrEmpty(rateString))
            throw new Exception($"{currencyCode} ForexSelling value not found.");

        rateString = rateString.Replace('.', ','); 

        if (!decimal.TryParse(rateString, out decimal rate))
            throw new Exception($"{currencyCode} rate did not convert to decimal.");

        return rate;
    }
}
