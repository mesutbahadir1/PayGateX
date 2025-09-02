namespace PayGateX.Interfaces;

public interface ITcmbCurrencyService
{
    Task<decimal> GetUsdTryRateAsync();
    Task<decimal> GetEurTryRateAsync();
}