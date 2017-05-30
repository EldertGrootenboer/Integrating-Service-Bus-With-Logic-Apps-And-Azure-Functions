namespace ServiceBusTester
{
    using System;
    using System.IO;
    using System.Runtime.Serialization.Json;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.ServiceBus.Messaging;

    using Newtonsoft.Json;

    /// <summary>
    /// The program which will help us test various Service Bus scenarios.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Client used to connect to queue.
        /// </summary>
        private static readonly QueueClient queueClient =
            QueueClient.CreateFromConnectionString(
                "Endpoint=sb://servicebustester2.servicebus.windows.net/;SharedAccessKeyName=Send;SharedAccessKey=09uoFKo4KD2igiIGSSXtP5RtdBIIC5VE7Of6ir3w3/4=;EntityPath=directqueuetest");

        /// <summary>
        /// Client used to connect to event hubs.
        /// </summary>
        private static readonly EventHubClient eventHubsClient =
            EventHubClient.CreateFromConnectionString(
                "Endpoint=sb://servicebustester2.servicebus.windows.net/;SharedAccessKeyName=Send;SharedAccessKey=v9Xh5lvNyJ40KsTGHHnT+346twoZXdvMmLKZY3kjlbg=;EntityPath=eventhubstest");

        /// <summary>
        /// The main method, this is where our application will start.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        public static void Main(string[] args)
        {
            SendToQueue().Wait();
            SendToEventHubs().Wait();

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        /// <summary>
        /// Send a message to a queue.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public static async Task SendToQueue()
        {
            // Inform user
            Console.WriteLine("Sending to queue directly");

            // Send to ServiceBus with object in constructor
            await
                queueClient.SendAsync(
                    new BrokeredMessage(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new Payload { Name = "Direct Queue", Value = "Object - UTF8 GetBytes - JsonConvert" }))));

            // Send to ServiceBus with Stream in constructor
            await
                queueClient.SendAsync(
                    new BrokeredMessage(
                        new MemoryStream(
                            Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new Payload { Name = "Direct Queue", Value = "Stream - UTF8 GetBytes - JsonConvert" })))));

            // Inform user
            Console.WriteLine("Finished sending to queue directly");
        }

        /// <summary>
        /// Send message to EventHubs.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public static async Task SendToEventHubs()
        {
            // Inform user
            Console.WriteLine("Sending via Event Hubs");

            // Send to Event Hubs
            await
                eventHubsClient.SendAsync(
                    new EventData(new Payload { Name = "Event Hubs", Value = "Object - JsonSerializer" }, new DataContractJsonSerializer(typeof(Payload))));

            // Inform user
            Console.WriteLine("Finished sending via Event Hubs");
        }
    }
}
