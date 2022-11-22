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
    public static class GitHubCloneCount
    {
        [FunctionName("GitHubCloneCount")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req, ILogger log,
            [Sql("dbo.GitHubStats", ConnectionStringSetting = "SqlConnectionString")] IAsyncCollector<CloneItem> newItems)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                JObject jsonObject = JObject.Parse(requestBody);

                foreach (var item in jsonObject["clones"])
                {
                    var repoItem = new CloneItem()
                    {
                        repo_id = Convert.ToUInt64(item["repo_id"]),
                        repo = item["repo"].ToString(),
                        date = Convert.ToDateTime(item["timestamp"]),
                        group = item["group"].ToString(),
                        clones = Convert.ToInt32(item["count"])
                    };

                    await newItems.AddAsync(repoItem);
                }

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
