using System.Text.Json.Serialization;

namespace PaymentGateway.Api.Models.Requests;

public class PostBankRequest
{
    [JsonPropertyName("card_number")]
    public string Card_Number { get; set; }

    [JsonPropertyName("expiry_date")]
    public string Expiry_Date { get; set; }

    [JsonPropertyName("currency")]
    public string Currency { get; set; }

    [JsonPropertyName("amount")]
    public int Amount { get; set; }

    [JsonPropertyName("cvv")]
    public string Cvv { get; set; }

}