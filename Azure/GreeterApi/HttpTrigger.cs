using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Core;
using Newtonsoft.Json;
using System.IO;

namespace Azure.GreeterApi
{
    public class HttpTrigger
    {
        private IGreeter _greeter;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="greeter"></param>
        public HttpTrigger(IGreeter greeter) => _greeter = greeter;

        /// <summary>
        /// Run the function.
        /// </summary>
        /// <returns>An IActionResult.</returns>
        [FunctionName("Greet")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "v1/greet")] HttpRequest req,
            [Queue("outqueue"), StorageAccount("AzureWebJobsStorage")] ICollector<string> msg,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            switch (req.Method)
            {
                case "GET": return HandleGet();
                case "POST": return await HandlePost(req, msg);
                default: return HandleUnimplemented(req.Method);

            }
        }

        private IActionResult HandleGet() => new OkObjectResult(_greeter.ReturnCannedResponse());

        private async Task<IActionResult> HandlePost(HttpRequest req, ICollector<string> msg)
        {
            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            msg.Add(_greeter.SayHello(name));
            return new OkObjectResult(_greeter.SayHello(name));
        }


        private IActionResult HandleUnimplemented(string method)
        {
            // CATCH ALL
            //
            // This code path shouldn't execute unless an HTTP
            // method is specified in the HttpTriggerAttribute, but
            // was unimplemented in the case statement above.
            //
            // Unspecified methods will be filtered out before
            // calling this method and will return a 404.
            //
            // Unimplemented methods will return a 405 below.
            //
            var response = new ObjectResult(new
            {
                statusCode = StatusCodes.Status405MethodNotAllowed,
                message = $"{method} not implemented for this function"
            });
            response.StatusCode = StatusCodes.Status405MethodNotAllowed;
            return response;
        }
    }
}
