using Microsoft.AspNetCore.Mvc;

using PaymentGateway.Api.Models.Responses;
using PaymentGateway.Api.Models.Requests;
using PaymentGateway.Api.Services;

namespace PaymentGateway.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentsController : ControllerBase
{
    private readonly PaymentsRepository _paymentsRepository;
    private readonly HttpClient _httpClient;

    public PaymentsController(PaymentsRepository paymentsRepository,IHttpClientFactory httpClientFactory)
    {
        _paymentsRepository = paymentsRepository;
        _httpClient = httpClientFactory.CreateClient("PaymentGateway");
    }

    [HttpGet("{id:guid}")]
    [ActionName("GetPaymentAsync")]
    public async Task<ActionResult<PostPaymentResponse?>> GetPaymentAsync(Guid id)
    {
        var payment = _paymentsRepository.Get(id);
        if(payment is null)
        {
            return NotFound();
        }
        return new OkObjectResult(payment);
    }
    
    [HttpPost]
    public async Task<ActionResult<PostPaymentResponse>> PostPaymentAsync([FromBody] PostPaymentRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var payment = new PostPaymentResponse
        {
            Id = Guid.NewGuid()
        };
        // If request is valid to send to acquiring bank
        if (request.checkValidRequest())
        {
            var bankRequest = new PostBankRequest
            {
                Card_Number = request.CardNumber,
                Expiry_Date = request.createExpiryDate(),
                Currency = request.Currency,
                Amount = request.Amount,
                Cvv = request.Cvv.ToString()
            };
            var httprequest = await _httpClient.PostAsJsonAsync("/payments", bankRequest);
            var bankResponse = await httprequest.Content.ReadFromJsonAsync<GetBankResponse>();
            if (bankResponse is null)
            {
                return StatusCode(500);
            }
            Console.WriteLine($"Payment Successful: {bankResponse.Authorized}, Auth Code: {bankResponse.AuthorizationCode}");
            payment.setPaymentResponse(request, bankResponse.Authorized);
        }
        else
        {
            payment.setPaymentResponse(request);
        }
        payment.LogPayment();
        _paymentsRepository.Add(payment);
        return CreatedAtAction(nameof(GetPaymentAsync), new { id = payment.Id }, payment);
    }


}