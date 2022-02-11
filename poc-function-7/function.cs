using Azure.Messaging.ServiceBus;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.ServiceBus;
using System;
using System.Text;
using System.Threading.Tasks;

namespace poc_function_7
{
    public class function
    {
        [FunctionName("Func-Meu-Teste")]
        public async Task RunFunc([ServiceBusTrigger("%topic%", "%subs%", AutoCompleteMessages = false, Connection = "conn")] ServiceBusReceivedMessage[] messages, ServiceBusMessageActions messageActions)
        {
            Console.WriteLine("consumindo a mensagem");

            foreach (ServiceBusReceivedMessage message in messages)
            {
                var conteudo = Encoding.UTF8.GetString(message.Body);
                Console.WriteLine(conteudo);

                if (message.MessageId == "1")                
                    await messageActions.DeadLetterMessageAsync(message);                
                else                
                    await messageActions.CompleteMessageAsync(message);
                
            }
        }
    }
}
