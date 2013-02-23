using Caliburn.Micro;

namespace Client.Framework
{
    public class OpenScreen<T> : IOpenScreen
        where T : IScreen
    {
        public OpenScreen()
        {
            Screen = (IScreen)IoC.GetInstance(typeof(T), null);
        }

        public IScreen Screen { get; set; }
    }
}