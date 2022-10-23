using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace VolumioServiceLibrary.Services;

public class VolumioWebSocketService
{     
    
    // Define the cancellation token.
    public CancellationTokenSource source = new CancellationTokenSource();
   
    private ClientWebSocket client { get; set; }


    public VolumioWebSocketService()
    {
        client = new ClientWebSocket();
    }

    public async void ConnectToServerAsync()
    {
        //CancellationToken token = source.Token;

        await client.ConnectAsync(new Uri("ws://192.168.2.21:3000"), source.Token);
        //UpdateClientState();

        await Task.Factory.StartNew(async () =>
        {
            while (true)
            {
                await ReadMessage();
            }
        }, source.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
    }

    async Task ReadMessage()
    {
        WebSocketReceiveResult result;
        var message = new ArraySegment<byte>(new byte[4096]);
        do
        {
            result = await client.ReceiveAsync(message, source.Token);
            if (result.MessageType != WebSocketMessageType.Text)
                break;
            var messageBytes = message.Skip(message.Offset).Take(result.Count).ToArray();
            string receivedMessage = Encoding.UTF8.GetString(messageBytes);
            Console.WriteLine("Received: {0}", receivedMessage);
        }
        while (!result.EndOfMessage);
    }
}
