using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using HTTP.Services;
namespace HTTP
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //
            HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

            builder.Services.AddHttpClient<HttpBinService>(client =>
            {
                client.BaseAddress = new Uri("https://httpbin.org/");
            });
            builder.Services.AddLogging(configure => configure.AddConsole());

            var provider = builder.Build().Services;


            var httpBinService= provider.GetRequiredService<HttpBinService>();

            ILogger logger = provider.GetRequiredService<ILogger<Program>>();

            logger.LogInformation("Get");
            var getRes = await httpBinService.GetIPAsync();

            logger.LogInformation("Get Url:{Url} \n\r ", getRes.Url);

            //

            logger.LogInformation("Delete");
            var deleteRes = await httpBinService.DeleteAsync();
            logger.LogInformation("Delete Url:{Url} \n\r ", deleteRes.Url);

            //
            logger.LogInformation("Patch");
            var patchRes = await httpBinService.PatchAsync("patch method");
            logger.LogInformation("Patch Url:{Url} \n\r ", patchRes.Url);

            //
            logger.LogInformation("Post");
            var postRes = await httpBinService.PostAsync("post method");
            logger.LogInformation("Post Url:{Url} \n\r ", postRes.Url);

            //
            logger.LogInformation("Put");
            var putRes = await httpBinService.PutAsync("put method");
            logger.LogInformation("Put Url:{Url} \n\r ", putRes.Url);

            //
            logger.LogInformation("Get Auth");
            var getAuthRes = await httpBinService.GetAuthAsync("asdf", "123");
            logger.LogInformation("User : {user} Get authenticated : {result} \n\r ",getAuthRes.User, getAuthRes.Authenticated);
        
            //
            logger.LogInformation("Get Deny Robot.Txt");
            var getDenyRobotTxtRes = await httpBinService.GetDenyRobotTxtAsync();
            logger.LogInformation(getDenyRobotTxtRes + "\n\r");

            //
            logger.LogInformation("Get image");
            await httpBinService.GetImageAsync("jpeg");
            await httpBinService.GetImageAsync("png");
            await httpBinService.GetImageAsync("webp");
            logger.LogInformation("Get image successfully \n\r");
        }
    }
}
