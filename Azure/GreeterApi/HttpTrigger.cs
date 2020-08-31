using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Core;

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
                case "GET": return _greeter.ReturnCannedResponse();
                case "POST": return await _greeter.SayHello(req, msg);
                default:
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
                        message = $"{req.Method} not implemented for this function"
                    });
                    response.StatusCode = StatusCodes.Status405MethodNotAllowed;
                    return response;
            }
        }
    }
}
