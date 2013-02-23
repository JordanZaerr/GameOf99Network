using System;
using Caliburn.Micro;
using Client.ServiceObjects;
using Client.Views.Interfaces;

namespace Client.Views
{
    public class ConnectToAServerViewModel : Screen, IConnectToAServerViewModel
    {
        private readonly IClient _client;
        private readonly IWindowManager _windowManager;

        public ConnectToAServerViewModel(IClient client, IWindowManager windowManager)
        {
            _client = client;
            _windowManager = windowManager;
        }

        public int? Port { get; set; }
        public string HostName { get; set; }
        public string Name { get; set; }
        public System.Windows.Media.Color Color { get; set; }

        public void ConnectToServer()
        {
            try
            {
                _client.ConnectToClient(HostName, Port.Value, Name, Color);
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