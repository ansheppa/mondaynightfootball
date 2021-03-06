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
    public static class CreateMatch
    {   
        [FunctionName("CreateMatch")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            var a =  new Match();
            a.PartitionKey = "Match";
            a.RowKey = "r";
            try {
            var tc = new TableClient(System.Environment.GetEnvironmentVariable("StorageConnectionString"),"Match");
            await tc.UpsertEntityAsync<Match>(a);
            }
            catch (Exception ex) {
                log.LogError(ex.Message);
                return new BadRequestObjectResult(ex.Message);
            }
            return new OkObjectResult(a);
        }
    }
}
