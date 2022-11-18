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
  "stats": [
    {
      "timestamp": "2022-11-04T00:00:00Z",
      "uniques": 3,
      "clones": 3,
      "repo": "newpatiente2e/Contoso-New-Patient-App",
      "group": "aiml"
    },
    {
      "timestamp": "2022-11-06T00:00:00Z",
      "uniques": 1,
      "clones": 1,
      "repo": "newpatiente2e/Contoso-New-Patient-App",
      "group": "aiml"
    },
    {
      "timestamp": "2022-11-07T00:00:00Z",
      "uniques": 1,
      "clones": 2,
      "repo": "newpatiente2e/Contoso-New-Patient-App",
      "group": "aiml"
    },
    {
      "timestamp": "2022-11-08T00:00:00Z",
      "uniques": 1,
      "clones": 1,
      "repo": "newpatiente2e/Contoso-New-Patient-App",
      "group": "aiml"
    },
    {
      "timestamp": "2022-11-13T00:00:00Z",
      "uniques": 1,
      "clones": 2,
      "repo": "newpatiente2e/Contoso-New-Patient-App",
      "group": "aiml"
    },
    {
      "timestamp": "2022-11-14T00:00:00Z",
      "uniques": 1,
      "clones": 1,
      "repo": "newpatiente2e/Contoso-New-Patient-App",
      "group": "aiml"
    },
    {
      "timestamp": "2022-11-15T00:00:00Z",
      "uniques": 1,
      "clones": 2,
      "repo": "newpatiente2e/Contoso-New-Patient-App",
      "group": "aiml"
    },
    {
      "timestamp": "2022-11-16T00:00:00Z",
      "uniques": 3,
      "clones": 4,
      "repo": "newpatiente2e/Contoso-New-Patient-App",
      "group": "aiml"
    },
    {
      "timestamp": "2022-11-17T00:00:00Z",
      "uniques": 6,
      "clones": 10,
      "repo": "newpatiente2e/Contoso-New-Patient-App",
      "group": "aiml"
    }
  ]
}

*/

namespace Microsoft.Advocacy
{
    public class RepoItem
    {
        public DateTime date { get; set; }
        public int clones { get; set; }
        public string repo { get; set; }
        public string group { get; set; }
        public string owner { get; set; }
    }
}

namespace Microsoft.Advocacy
{
    public static class GitHubCloneCount
    {
        [FunctionName("GitHubCloneCount")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req, ILogger log,
            [Sql("dbo.GitHubStats", ConnectionStringSetting = "SqlConnectionString")] IAsyncCollector<RepoItem> newItems)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                JObject jsonObject = JObject.Parse(requestBody);

                foreach (var item in jsonObject["stats"])
                {
                    var repoItem = new RepoItem()
                    {
                        repo = item["repo"].ToString(),
                        date = Convert.ToDateTime(item["timestamp"]),
                        group = item["group"].ToString(),
                        owner = item["owner"].ToString(),
                        clones = Convert.ToInt32(item["clones"]),
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
