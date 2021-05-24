using Binance.Net;
using Binance.Net.Enums;
using Binance.Net.Objects.Spot;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TradingBOT.manager
{
    public class ClientManager
    {
        public BinanceClient Client { get; }

        public BinanceSocketClient SocketClient { get; private set; }

        public WebCallResult<string> StartResult { get; private set; }

        public ClientManager()
        {
            Client = new BinanceClient();
            //Client = new BinanceClient(new BinanceClientOptions
            //{
            //    ApiCredentials = new ApiCredentials("", "")
            //});

            Init();
        }

        public void Init() 
        {
           // StartResult = Client.Spot.UserStream.StartUserStream();

            //if (!StartResult.Success)
            //    throw new Exception($"Failed to start user stream: {StartResult.Error}");

            SocketClient = new BinanceSocketClient();
        }

       
    }
}
