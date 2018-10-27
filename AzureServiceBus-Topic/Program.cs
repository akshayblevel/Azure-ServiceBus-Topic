using Microsoft.Azure.ServiceBus;
using System;
using System.Text;

namespace AzureServiceBus_Topic
{
    class Program
    {
        static ITopicClient topicClient;
        static void Main(string[] args)
        {
            string sbConnectionString = "Endpoint=sb://mobilerecharge.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=KVb9ubc9XaV0dT/1dMj/JGvVvUZ64U21IBI=";
            string sbTopic = "offers";

            string messageBody = string.Empty;
            try
            {
                Console.WriteLine("-------------------------------------------------------");
                Console.WriteLine("Publish Offer");
                Console.WriteLine("-------------------------------------------------------");
                Console.WriteLine("Offers");
                Console.WriteLine("1. Recharge with 100 and get talk time of 110");
                Console.WriteLine("2. Get 5 GB data on recharge of 300. Validity 28 days");
                Console.WriteLine("3. 1000 SMS in recharge of 100");
                Console.WriteLine("-------------------------------------------------------");

                Console.WriteLine("Offer:");
                string offer = Console.ReadLine();

                Console.WriteLine("-------------------------------------------------------");

                switch (offer)
                {
                    case "1":
                        offer = "Recharge with 100 and get talk time of 110";
                        break;
                    case "2":
                        offer = "Get 5 GB data on recharge of 300. Validity 28 days";
                        break;
                    case "3":
                        offer = "1000 SMS in recharge of 100";
                        break;
                    default:
                        break;
                }

                messageBody = offer;
                topicClient = new TopicClient(sbConnectionString, sbTopic);

                var message = new Message(Encoding.UTF8.GetBytes(messageBody));
                Console.WriteLine($"Message Published: {messageBody}");

                topicClient.SendAsync(message);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadKey();
                topicClient.CloseAsync();
            }
        }
    }

}
