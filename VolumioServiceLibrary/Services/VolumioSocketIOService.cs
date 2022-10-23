using SocketIOClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace VolumioServiceLibrary.Services
{
    public  class VolumioSocketIOService
    {

        private static SocketIO _client { get;set; }

        public VolumioSocketIOService()
        {

            onInit();
        }

        public async void onInit()
        {
            _client = new SocketIO("http://192.168.2.21:3000/");


            //_client.On
            _client.On("pushState", response =>
            {
                // You can print the returned data first to decide what to do next.
                // output: ["hi client"]
                Console.WriteLine(response);

                //string text = response.GetValue<string>();

                // The socket.io server code looks like this:
                // socket.emit('hi', 'hi client');
            });

            await _client.ConnectAsync();
            //await client.ConnectAsync(new Uri("http://192.168.2.21:3000/"));
            //var client = new SocketIO("http://192.168.2.21:3000/");
            //client.OnDisconnected += async (sender, e) =>
            //{

            //};
            //client.On("volume", response =>
            //{
            //    // You can print the returned data first to decide what to do next.
            //    // output: ["hi client"]
            //    Console.WriteLine(response);

            //    string text = response.GetValue<string>();

            //    // The socket.io server code looks like this:
            //    // socket.emit('hi', 'hi client');
            //});
            //client.On("connection", response =>
            //{
            //    // You can print the returned data first to decide what to do next.
            //    // output: ["hi client"]
            //    Console.WriteLine(response);

            //    string text = response.GetValue<string>();

            //    // The socket.io server code looks like this:
            //    // socket.emit('hi', 'hi client');
            //});

            //client.On("bringmepizza", response =>
            //{
            //    // You can print the returned data first to decide what to do next.
            //    // output: ["hi client"]
            //    Console.WriteLine(response);

            //    string text = response.GetValue<string>();

            //    // The socket.io server code looks like this:
            //    // socket.emit('hi', 'hi client');
            //});

            //client.On("next", response =>
            //{
            //    // You can print the returned data first to decide what to do next.
            //    // output: ["ok",{"id":1,"name":"tom"}]
            //    Console.WriteLine(response);

            //    // Get the first data in the response
            //    string text = response.GetValue<string>();
            //    // Get the second data in the response
            //    //var dto = response.GetValue<TestDTO>(1);

            //    // The socket.io server code looks like this:
            //    // socket.emit('hi', 'ok', { id: 1, name: 'tom'});
            //});

            //client.OnConnected += async (sender, e) =>
            //{
            //    // Emit a string
            //    await client.EmitAsync("hi", "socket.io");

            //    // Emit a string and an object
            //    //var dto = new TestDTO { Id = 123, Name = "bob" };
            //   // await client.EmitAsync("register", "source", dto);
            //};
            //await client.ConnectAsync();


        }

        public async Task VolumeToMax()
        {
            //var client = new SocketIO("http://192.168.2.21:3000/");
            //await client.ConnectAsync();

            await _client.EmitAsync("volume", 90);
        }
    }
}
