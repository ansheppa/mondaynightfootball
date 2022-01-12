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
using System.Collections.Generic;
using System.Linq;
using Azure;

namespace Company.Function
{
    public static class ListMatches
    {   
        [FunctionName("ListMatches")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            var a =  new Match();
            a.PartitionKey = "Match";
            a.RowKey = "r";
            AsyncPageable<Match> queryResults;
            List<Match> Matches = new List<Match>();
            try {
            TableClient tc = new TableClient(System.Environment.GetEnvironmentVariable("StorageConnectionString"),"Match");
            queryResults = tc.QueryAsync<Match>(a => a.PartitionKey == "Match");
            await foreach (var m in queryResults)
            {
                Matches.Add(m);
            }
            }
            catch (Exception ex) {
                log.LogError(ex.Message);
                return new BadRequestObjectResult(ex.Message);
            }
            return new OkObjectResult(Matches);
        }
    }
}
