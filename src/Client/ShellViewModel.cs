using Caliburn.Micro;
using Client.Framework;
using Client.Views.Interfaces;

namespace Client
{
    public class ShellViewModel : Conductor<IScreen>, IShell, IHandle<IOpenScreen>
    {
        private readonly IEventAggregator _eventAggregator;

        public ShellViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
            DisplayName = "Game of 99";
        }

        public void Handle(IOpenScreen message)
        {
            ActivateItem(message.Screen);
        }

        protected override void OnActivate()
        {
            //Default screen on load, there is probably a better way to do this.
            //But this works for my purposes for right now.
            _eventAggregator.Publish(new OpenScreen<IServerChoiceViewModel>());
        }

        public override void ActivateItem(IScreen item)
        {
            base.ActivateItem(item);
            if (item is IHandle)
            {
                _eventAggregator.Subscribe(item);
            }
        }

        public override void DeactivateItem(IScreen item, bool close)
        {
            base.DeactivateItem(item, close);
            if (item is IHandle)
            {
                _eventAggregator.Unsubscribe(item);
            }
        }
    }
}