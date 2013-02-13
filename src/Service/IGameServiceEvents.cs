using System.ServiceModel;
using Service.Contracts;

namespace Service
{
    [ServiceContract]
    public interface IGameServiceEvents
    {
        [OperationContract(IsOneWay = true)]
        void PlayerSelectedTile(int playerId, Color color);

        [OperationContract(IsOneWay = true)]
        void PlayerJoinedGame(int playerid, string playerName, Color color);

        [OperationContract(IsOneWay = true)]
        void YourTurn();

        [OperationContract(IsOneWay = true)]
        void PlayerWon(int playerId, string playerName);

        [OperationContract(IsOneWay = true)]
        void CardRemoved(int card);

        [OperationContract(IsOneWay = true)]
        void CardAdded(int card);
    }
}