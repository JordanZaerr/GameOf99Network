using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace Client.ServiceObjects
{
    public class HostingService : IHostingService
    {
        private ServiceHost _serviceHost;

        public void StartHost(int port)
        {
            var binding = new WSDualHttpBinding();
            binding.ReceiveTimeout = TimeSpan.FromSeconds(5);
            binding.SendTimeout = TimeSpan.FromSeconds(5);
            binding.HostNameComparisonMode = HostNameComparisonMode.StrongWildcard;
            binding.Security.Mode = WSDualHttpSecurityMode.None;

            var baseAddress = new Uri(String.Format("http://localhost:{0}/", port));
            var serviceAddress = new Uri(String.Format("http://localhost:{0}/GameService", port));
            var metadataBehavior = new ServiceMetadataBehavior();
            metadataBehavior.HttpGetEnabled = true;

            // Create a ServiceHost for the GameService type and provide the base address. 
            _serviceHost = new ServiceHost(typeof(Service.GameService), baseAddress);
            _serviceHost.AddServiceEndpoint(typeof(Service.IGameService), binding, serviceAddress);
            _serviceHost.Description.Behaviors.Add(metadataBehavior);

            // Open the ServiceHostBase to create listeners and start listening for messages.
            _serviceHost.Open();
        }

        public void StopHost()
        {
            if (_serviceHost != null)
            {
                _serviceHost.Close();
            }
        }
    }
}