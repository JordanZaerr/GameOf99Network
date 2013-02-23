namespace Client.ServiceObjects
{
    public interface IHostingService
    {
        void StartHost(int port);
        void StopHost();
    }
}