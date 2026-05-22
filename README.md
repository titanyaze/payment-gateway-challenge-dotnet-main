
![Logo](https://cdn.prod.website-files.com/64db80a5e88c6b1723ff760b/6788c0858b085eea2bcca244_Property%201%3Dweb-default.svg)


# Checkout.com Takehome Test Abid Asab

Takehome home solution that I have produced in C# and .NET



## Assumptions 

- Every payment request that is sent, regardless of the response (Authorized, Declined or Rejected) with in the payment gateway will need to be processed as a PostPaymentRequest object.

- Note: If a payment is rejected it will still need to be processed as a PostPaymentRequest but it will not be processed by the acquiring bank.



## API Reference

#### Get Payment

```http
  GET /api/Payments/{id}
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `id` | `string` | **Required**. Merchant will enter a unique id |

#### Post payment

```http
  POST /api/Payments
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `request`      | `PostPaymentRequest` | **Required**. Payment will be validated if it should be sent to acquiring bank |


