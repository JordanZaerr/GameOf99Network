using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using Service;

namespace ServiceTestClient
{
    internal class Program
    {
        private static void Main()
        {
            //You need admin permissions to listen on a port, 
            //run visual studio as an admin if you wish to 
            //run/debug this from within visual studio
            var binding = new WSDualHttpBinding();
            binding.ReceiveTimeout = TimeSpan.FromSeconds(5);
            binding.SendTimeout = TimeSpan.FromSeconds(5);
            binding.HostNameComparisonMode = HostNameComparisonMode.StrongWildcard;
            binding.Security.Mode = WSDualHttpSecurityMode.None;

            var baseAddress = new Uri("http://localhost:4050/");
            var serviceAddress = new Uri("http://localhost:4050/GameService");
            var metadataBehavior = new ServiceMetadataBehavior();
            metadataBehavior.HttpGetEnabled = true;

            // Create a ServiceHost for the GameService type and provide the base address. 
            using (var serviceHost = new ServiceHost(typeof (GameService), baseAddress))
            {
                serviceHost.AddServiceEndpoint(typeof (IGameService), binding, serviceAddress);
                serviceHost.Description.Behaviors.Add(metadataBehavior);

                // Open the ServiceHostBase to create listeners and start listening for messages.
                serviceHost.Open();

                // The service can now be accessed.
                Console.WriteLine("The service is ready.");
                Console.WriteLine("Press <ENTER> to terminate service.");
                Console.WriteLine();
                Console.ReadLine();

                // Close the ServiceHostBase to shutdown the service.
                serviceHost.Close();
            }
        }
    }
}
