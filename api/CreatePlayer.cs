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
    public static class CreatePlayer
    {   
        [FunctionName("CreatePlayer")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            var p =  new Player();
            
            try {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                p = JsonConvert.DeserializeObject<Player>(requestBody);
                p.PartitionKey = "0";
                p.RowKey = System.Guid.NewGuid().ToString();
                var tc = new TableClient(System.Environment.GetEnvironmentVariable("StorageConnectionString"),"Players");
                await tc.UpsertEntityAsync<Player>(p);
            }
            catch (Exception ex) {
                log.LogError(ex.Message);
                return new BadRequestObjectResult(ex.Message);
            }
            return new OkResult();
        }
    }
}
