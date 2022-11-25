using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Microsoft.Advocacy
{
    public static class GitHubPublicStats
    {
        [FunctionName("GitHubPublicStats")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req, ILogger log,
            [Sql("dbo.GitHubStats", ConnectionStringSetting = "SqlConnectionString")] IAsyncCollector<StatsItem> newItems)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

                var repoItem = JsonConvert.DeserializeObject<StatsItem>(requestBody);
                if (repoItem == null)
                {
                    return new BadRequestObjectResult("Invalid JSON");
                }
                
                repoItem.date = DateTime.UtcNow.Date;

                await newItems.AddAsync(repoItem);

                // Rows are upserted here
                return new OkResult();
            }
            catch (Exception ex)
            {
                log.LogError(ex, "Error in GitHubCloneCount");
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
