using Caliburn.Micro;
using Client.Framework;
using Client.Views.Interfaces;

namespace Client.Views
{
    public class ServerChoiceViewModel : Screen, IServerChoiceViewModel
    {
        private readonly IEventAggregator _eventAggregator;

        public ServerChoiceViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void ConnectToAServer()
        {
            _eventAggregator.Publish(new OpenScreen<IConnectToAServerViewModel>());
        }

        public void HostAServer()
        {
            _eventAggregator.Publish(new OpenScreen<IHostAServerViewModel>());
        } 
    }
}