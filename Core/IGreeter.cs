using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;

namespace Core
{
    /// <summary>
    /// Interface to a greeter service.
    /// </summary>
    public interface IGreeter
    {
        /// <summary>
        /// Return back a canned response.
        /// </summary>
        /// <returns>A 200 OK with a canned message.</returns>
        IActionResult ReturnCannedResponse();

        /// <summary>
        /// Return a greeting to a person named in the request.
        /// </summary>
        /// <param name="req">The HttpRequest request.</param>
        /// <param name="msg">
        /// An AzureWebJobsStorage instance for queuing processed messages.
        /// </param>
        /// <returns>
        /// If a non-whitespace name object is specified in the request, a greeting
        /// with the person's name will be returned.
        /// 
        /// Otherwise, a greeting with the placeholder "Noname" will be returned.
        /// </returns>
        Task<IActionResult> SayHello(HttpRequest req, ICollector<string> msg);
    }
}
