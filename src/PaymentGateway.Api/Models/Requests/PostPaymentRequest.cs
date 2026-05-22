namespace PaymentGateway.Api.Models.Requests;

public class PostPaymentRequest
{
    private static readonly HashSet<string> Currencies = new() { "GBP", "USD", "EUR" };
    public required string CardNumber { get; set; }
    public required int ExpiryMonth { get; set; }
    public required int ExpiryYear { get; set; }
    public required string Currency { get; set; }
    public required int Amount { get; set; }
    public required string Cvv { get; set; }

    public string createExpiryDate()
    {
        return $"{ExpiryMonth}/{ExpiryYear}";
    }
    public bool checkValidRequest()
    {
        bool isValid = true;
        if(CardNumber.Length < 14 || CardNumber.Length > 19 || !CardNumber.All(char.IsDigit))
        {
            isValid = false;
            if (!CardNumber.All(char.IsDigit))
            {
                CardNumber = "0000000000000000";
            }
        }
        if(Cvv.Length < 3 || Cvv.Length > 4 || !CardNumber.All(char.IsDigit))
        {
            if (!CardNumber.All(char.IsDigit))
            {
                Cvv = "000";
            }
            isValid = false;
        }

        if (!Currencies.Contains(Currency.ToUpper()) || Currency.Length != 3)
        {
            isValid = false;
        }
        if(ExpiryMonth < 0 || ExpiryMonth > 12)
        {
            return false;
        }
        if(new DateOnly(ExpiryYear, ExpiryMonth, 1) < DateOnly.FromDateTime(DateTime.UtcNow))
        {
            isValid = false;
        }
        return isValid;
    }

    public int getCardNumberLastFour()
    {
        return int.Parse(CardNumber.ToString().Substring(CardNumber.ToString().Length - 4));
    }
}