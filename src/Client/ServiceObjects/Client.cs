using System;
using System.ServiceModel;
using System.Windows;
using Client.GameService;
using Service.Contracts;
using Color = System.Windows.Media.Color;

namespace Client.ServiceObjects
{
    /// <summary>
    /// This is wrapper around the service client.
    /// </summary>
    public class Client : IClient
    {
        private readonly IGameServiceCallback _callbackHandler;
        private GameServiceClient _client;

        public int PlayerId { get; private set; }

        public Client(IGameServiceCallback callbackHandler)
        {
            _callbackHandler = callbackHandler;
        }

        public async void ConnectToClient(string hostName, int port, string playerName,  Color color)
        {
            var binding = new WSDualHttpBinding(WSDualHttpSecurityMode.None);
            binding.ReceiveTimeout = TimeSpan.FromSeconds(30);
            binding.SendTimeout = TimeSpan.FromSeconds(30);

            //TODO: Wire up the callbackHandler...

            _client = new GameServiceClient(
                new InstanceContext(_callbackHandler), 
                binding,
                new EndpointAddress(String.Format("http://{0}:{1}/GameService", hostName, port)));
            _client.Open();

            //Just calling join here locks up...
            PlayerId = await _client.JoinAsync(new Player
            {
                Name = playerName,
                Color = new Service.Contracts.Color 
                {
                    A = color.A,
                    R = color.R,
                    G = color.G,
                    B = color.B
                }
            });

            //TODO: Remove this once the view goes somewhere else...
            MessageBox.Show(String.Format("Player: {0} joined with id: {1}", playerName, PlayerId));
        }
    }

    public interface IClient
    {
        void ConnectToClient(string hostName, int port, string playerName, Color color);
    }
}