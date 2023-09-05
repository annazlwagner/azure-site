using Newtonsoft.Json;

namespace Company.Function
{
    public class Counter
    {
        [JsonProperty(PropertyName ="id")]
        public string Id {get; set;}
        [JsonProperty(PropertyName ="count")] //supposed to match the property names we created in our container in our Azure Cosmos DB account
        public int Count {get;set;}
    }
}