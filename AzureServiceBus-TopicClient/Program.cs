using Microsoft.Azure.ServiceBus;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AzureServiceBus_TopicClient
{
    class Program
    {
        static ISubscriptionClient subscriptionClient;
        static void Main(string[] args)
        {
            string sbConnectionString = "Endpoint=sb://mobilerecharge.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=KVb9ubc9XaV0dT/1dMj/JGvVvUZ64U21IBI=";
            string sbTopic = "offers";
            string sbSubscription = "akki5677";
            try
            {
                subscriptionClient = new SubscriptionClient(sbConnectionString, sbTopic, sbSubscription);

                var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
                {
                    MaxConcurrentCalls = 1,
                    AutoComplete = false
                };
                subscriptionClient.RegisterMessageHandler(ReceiveMessagesAsync, messageHandlerOptions);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadKey();
                subscriptionClient.CloseAsync();
            }
        }

        static async Task ReceiveMessagesAsync(Message message, CancellationToken token)
        {
            Console.WriteLine($"Subscribed message: {Encoding.UTF8.GetString(message.Body)}");

            await subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
        }

        static Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            Console.WriteLine(exceptionReceivedEventArgs.Exception);
            return Task.CompletedTask;
        }
    }
}
