using System.Net;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;

namespace HsDeckTracker
{
    public class DeckMessage
    {
        public int LegendaryCount { get; set; }
        public int EpicCount { get; set; }
        public int RareCount { get; set; }
    }

    public static class SaveDeckWebhook
    {
        [FunctionName("SaveDeckWebhook")]
        public static async Task<object> Run([HttpTrigger(WebHookType = "genericJson")]HttpRequestMessage req, TraceWriter log)
        {
            log.Info($"Webhook was triggered!");

            string jsonContent = await req.Content.ReadAsStringAsync();
            DeckMessage data = JsonConvert.DeserializeObject<DeckMessage>(jsonContent);

            return req.CreateResponse(HttpStatusCode.OK, new
            {
                greeting = $"Legendary count: {data.LegendaryCount}, epic count: {data.EpicCount}, rare count: {data.RareCount}"
            });
        }
    }
}
