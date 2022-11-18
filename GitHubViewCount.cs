using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Microsoft.Advocacy
{
    public static class GitHubViewCount
    {
        [FunctionName("GitHubViewCount")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req, ILogger log,
            [Sql("dbo.GitHubStats", ConnectionStringSetting = "SqlConnectionString")] IAsyncCollector<ViewItem> newItems)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                JObject jsonObject = JObject.Parse(requestBody);

                foreach (var item in jsonObject["views"])
                {
                    var repoItem = new ViewItem()
                    {
                        repo = item["repo"].ToString(),
                        date = Convert.ToDateTime(item["timestamp"]),
                        group = item["group"].ToString(),
                        owner = item["owner"].ToString(),
                        views = Convert.ToInt32(item["count"]),
                    };

                    await newItems.AddAsync(repoItem);
                }

                // Rows are upserted here
                await newItems.FlushAsync();

                return new OkResult();
            }
            catch (Exception ex)
            {
              log.LogError(ex, "Error in GitHubViewCount");
              return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
