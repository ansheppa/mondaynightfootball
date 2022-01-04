using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Azure.Data.Tables;

namespace Company.Function
{
    public static class HttpTrigger1
    {   
        public class Match : ITableEntity {
            public string PartitionKey {get; set;}
            public string RowKey {get; set;}
            public Azure.ETag ETag {get; set;}
            public DateTimeOffset? Timestamp {get; set;}
        }

        [FunctionName("CreateMatch")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            var a =  new Match();
            a.PartitionKey = "p";
            a.RowKey = "r";
            var tc = new TableClient("DefaultEndpointsProtocol=https;AccountName=storageaccountmnfa359;AccountKey=b32TMkqbD56Y1It5rXvzgqCmpmlesT60Ne2rXNo4Ye5Vza31202jm4/lOkH/n4w35tW68RJXsZq9To2m1VRyqA==;EndpointSuffix=core.windows.net","Match");
            await tc.UpsertEntityAsync<Match>(a);
            return new OkObjectResult("Done");
        }
    }
}
