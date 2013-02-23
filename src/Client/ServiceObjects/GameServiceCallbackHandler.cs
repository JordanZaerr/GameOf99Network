using Client.GameService;
using Service.Contracts;

namespace Client.ServiceObjects
{
    public class GameServiceCallbackHandler : IGameServiceCallback
    {
        public void PlayerSelectedTile(int playerId, Color color)
        {
            
        }

        public void PlayerJoinedGame(int playerid, string playerName, Color color)
        {
            
        }

        public void YourTurn()
        {
            
        }

        public void PlayerWon(int playerId, string playerName)
        {
            
        }

        public void CardRemoved(int card)
        {
            
        }

        public void CardAdded(int card)
        {
            
        }
    }
}