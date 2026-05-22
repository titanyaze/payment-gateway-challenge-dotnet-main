using PaymentGateway.Api.Models.Requests;
namespace PaymentGateway.Api.Models.Responses;

public class PostPaymentResponse
{
    public Guid Id { get; set; }
    public PaymentStatus Status { get; set; }
    public int CardNumberLastFour { get; set; }
    public int ExpiryMonth { get; set; }
    public int ExpiryYear { get; set; }
    public string Currency { get; set; } = string.Empty;
    public int Amount { get; set; }

    public void setPaymentResponse(PostPaymentRequest request)
    {
        Status = PaymentStatus.Rejected;
        CardNumberLastFour =  request.getCardNumberLastFour();
        ExpiryMonth = request.ExpiryMonth;
        ExpiryYear = request.ExpiryYear;
        Currency = request.Currency;
        Amount = request.Amount;
    }
    //Method overload when the bank has given a response
    public void setPaymentResponse(PostPaymentRequest request,bool authorized)
    {
        if (authorized)
        {
            Status = PaymentStatus.Authorized;
        }
        else
        {
            Status = PaymentStatus.Declined;
        }
        CardNumberLastFour =  request.getCardNumberLastFour();
        ExpiryMonth = request.ExpiryMonth;
        ExpiryYear = request.ExpiryYear;
        Currency = request.Currency;
        Amount = request.Amount;
    }

    public void LogPayment()
    {
        Console.WriteLine("=== Payment Response ===");
        Console.WriteLine($"ID:            {Id}");
        Console.WriteLine($"Status:        {Status}");
        Console.WriteLine($"Card Number:   {CardNumberLastFour}");
        Console.WriteLine($"Expiry Month:  {ExpiryMonth}");
        Console.WriteLine($"Expiry Year:   {ExpiryYear}");
        Console.WriteLine($"Currency:      {Currency}");
        Console.WriteLine($"Amount:        {Amount}");
        Console.WriteLine("========================");
    }
}
