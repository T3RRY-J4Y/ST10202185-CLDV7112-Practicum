using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace ST10202185_CLDV7112_Functions
{
    public class ProcessEventData
    {
        private readonly ILogger<ProcessEventData> _logger;

        public ProcessEventData(ILogger<ProcessEventData> logger)
        {
            _logger = logger;
        }

        [Function("ProcessEventData")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("ProcessEventData function triggered.");

            var eventData = new
            {
                EventId = Guid.NewGuid().ToString(),
                EventType = "OrderPlaced",
                Timestamp = DateTime.UtcNow,
                SourceSystem = "EcommerceApp",
                Data = new
                {
                    OrderId = "ORD-" + new Random().Next(1000, 9999),
                    Amount = Math.Round(new Random().NextDouble() * 500, 2),
                    CustomerId = "CUST-" + new Random().Next(100, 999)
                },
                Status = "Processed Successfully"
            };

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(eventData);
            return response;
        }
    }
}