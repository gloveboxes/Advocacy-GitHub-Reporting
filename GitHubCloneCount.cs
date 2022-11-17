using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Threading.Tasks;

/*
{
  "count": 50,
  "uniques": 41,
  "clones": [
    {
      "timestamp": "2022-11-02T00:00:00Z",
      "count": 8,
      "uniques": 7,
      "repo": "dave"
    },
    {
      "timestamp": "2022-11-03T00:00:00Z",
      "count": 30,
      "uniques": 25,
      "repo": "dave"
    },
    {
      "timestamp": "2022-11-04T00:00:00Z",
      "count": 3,
      "uniques": 3,
      "repo": "dave"
    },
    {
      "timestamp": "2022-11-06T00:00:00Z",
      "count": 1,
      "uniques": 1,
      "repo": "dave"
    },
    {
      "timestamp": "2022-11-07T00:00:00Z",
      "count": 2,
      "uniques": 1,
      "repo": "dave"
    },
    {
      "timestamp": "2022-11-08T00:00:00Z",
      "count": 1,
      "uniques": 1,
      "repo": "dave"
    },
    {
      "timestamp": "2022-11-13T00:00:00Z",
      "count": 2,
      "uniques": 1,
      "repo": "dave"
    },
    {
      "timestamp": "2022-11-14T00:00:00Z",
      "count": 1,
      "uniques": 1,
      "repo": "dave"
    },
    {
      "timestamp": "2022-11-15T00:00:00Z",
      "count": 2,
      "uniques": 1,
      "repo": "dave"
    }
  ]
}
*/

namespace Microsoft.Advocacy
{
    public class RepoItem
    {
        public DateTime date { get; set; }
        public int count { get; set; }
        public string repo { get; set; }
    }
}

namespace Microsoft.Advocacy
{
    public static class GitHubCloneCount
    {
        [FunctionName("GitHubCloneCount")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req, ILogger log,
            [Sql("dbo.GitHubCloneStats", ConnectionStringSetting = "SqlConnectionString")] IAsyncCollector<RepoItem> newItems)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                JObject jsonObject = JObject.Parse(requestBody);

                foreach (var item in jsonObject["clones"])
                {
                    var repoItem = new RepoItem()
                    {
                        repo = item["repo"].ToString(),
                        count = Convert.ToInt32(item["count"]),
                        date = Convert.ToDateTime(item["timestamp"])
                    };

                    await newItems.AddAsync(repoItem);
                }

                // Rows are upserted here
                await newItems.FlushAsync();

                return new OkResult();
            }
            catch (Exception ex)
            {
                return new OkObjectResult(ex.Message);
            }
        }
    }
}
