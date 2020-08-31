using Core;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Azure.GreeterApi.Startup))]

namespace Azure.GreeterApi
{
    /// <summary>
    /// Startup class.
    /// </summary>
    public class Startup : FunctionsStartup
    {
        /// <inheritdoc/>
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<IGreeter, Greeter>();
        }

    }
}