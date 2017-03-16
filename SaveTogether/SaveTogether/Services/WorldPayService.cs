using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SaveTogether.DAL.Entities;
using SaveTogether.Interfaces;
using SaveTogether.Models;
using Worldpay.Sdk;
using Worldpay.Sdk.Enums;
using Worldpay.Sdk.Models;
using WorldPay.Sdk;

namespace SaveTogether.Services
{
    public class WorldPayService : IPayService
    {
        private readonly string _serverToken;
        private const string Url = "https://api.worldpay.com/v1";

        public WorldPayService(string serverToken = "T_S_a2c41c2b-a7cd-408d-a7d1-5372f51c50ea")
        {
            _serverToken = serverToken;
        }

        public bool MakePayment(Donation donation)
        {
            //TODO: TEST and rearange Donations
            var worldpayRestClient = new WorldpayRestClient(Url, _serverToken);
            var orderRequest = new OrderRequest()
            {
                token = donation.Token,
                amount = donation.Sum,
                currencyCode = donation.CurrencyCode.ToString(),
                name = donation.Person?.UserName,
                settlementCurrency = CurrencyCode.USD.ToString(),
                orderDescription = "Donate"
            };
            OrderService orderService = worldpayRestClient.GetOrderService();
            OrderResponse orderResponse = orderService.Create(orderRequest);

            return orderResponse.paymentStatus == OrderStatus.SUCCESS;
        }
    }
}