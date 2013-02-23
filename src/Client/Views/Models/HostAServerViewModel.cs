using System;
using Caliburn.Micro;
using Client.ServiceObjects;
using Client.Views.Interfaces;

namespace Client.Views
{
    public class HostAServerViewModel : Screen, IHostAServerViewModel
    {
        private readonly IHostingService _hostingService;
        private readonly IClient _client;
        private readonly IWindowManager _windowManager;

        public HostAServerViewModel(IHostingService hostingService, IClient client, IWindowManager windowManager)
        {
            _hostingService = hostingService;
            _client = client;
            _windowManager = windowManager;
        }

        public int? Port { get; set; }
        public string Name { get; set; }
        public System.Windows.Media.Color Color { get; set; }

        public void StartServer()
        {
            try
            {
                if (Port == null)
                {
                    throw new InvalidOperationException("Port cannot be null.");
                }
                _hostingService.StartHost(Port.Value);
            }
            catch (Exception ex)
            {
                ShowError("Couldn't start the server...", ex);
                return;
            }

            try
            {
                _client.ConnectToClient("localhost", Port.Value, Name, Color);
            }
            catch (Exception ex)
            {
                ShowError("Couldn't connect to the client.", ex);
            }
        }

        private void ShowError(string title, Exception ex)
        {
            _windowManager.ShowDialog(new ErrorDialogViewModel
            {
                DisplayName = title,
                Exception = ex
            });
        }
    }
}