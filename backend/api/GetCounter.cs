using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
//^System.Net.Http bc "HttpResponseMessage" below

namespace Company.Function
{
    public static class GetCounter
    {
        [FunctionName("GetCounter")]
        public static HttpResponseMessage Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            //adding code below. Lines up w/the CosmosDB naming I created
            [CosmosDB(databaseName:"capstone", containerName:"counter", Connection = "AzureSiteConnectionString", Id = "1", PartitionKey = "1")] Counter count,
            //AzureSiteConnectionString is the string I added to my local.settings.JSON. This is how it connects to the database
            //---- Counter counter is where we will store this...
            //... not sure if Counter counter is correct for me since i lowercased my containerName...
            //...update 9/3/23, I'm going to use "counter count" instead of 
            //"Counter counter". Also lowercase "counter updatedCounter" for output binding below
            //JK!!! "Counter" count. & "Counter" updatedCounter. Bc it seems "Counter" is a special recognized word we must use.

            //^^this 1st binding allows us to retrieve an item tht has the Id = "1".
            [CosmosDB(databaseName:"capstone", containerName:"counter", Connection = "AzureSiteConnectionString", Id = "1", PartitionKey = "1")] out Counter updatedCounter,
            //We're also creating an output binding bc the count will be continuously changing.

            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            //set the updatedCounter object to be the same as teh incoming counter 'count' object
            updatedCounter = count;
            //our input binding will grab & populate the 'count' object when teh function is executed.
            //the output object 'updatedCounter' does not get populated bc it's output/'out'

            updatedCounter.Count += 1;
            //updatedCounter is the one we're returning to the database 
            var jsonToReturn = JsonConvert.SerializeObject(count);
            //remember we're serializing teh current counter 'count', not the updated one


            return new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent(jsonToReturn, Encoding.UTF8, "application/json")
            }; //gps did "jsonToRetun" missing the 'r'. I spelled it correctly above.
        }
    }
}
