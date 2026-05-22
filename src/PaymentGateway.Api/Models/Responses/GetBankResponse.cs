namespace PaymentGateway.Api.Models.Responses;

using System.Text.Json.Serialization;
public class GetBankResponse
{
    [JsonPropertyName("authorized")]
    public bool Authorized { get; set; }

    [JsonPropertyName("authorization_code")]
    public string AuthorizationCode { get; set; }
}