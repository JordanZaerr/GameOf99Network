using System.ServiceModel;
using Service.Contracts;

namespace Service
{
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IGameServiceEvents))]
    public interface IGameService
    {
        /// <summary>
        /// Discards the selected card from the players hand.
        /// </summary>
        /// <param name="cardToDiscard"></param>
        [OperationContract]
        void DiscardCard(int cardToDiscard);

        /// <summary>
        /// Draws a card from the deck.
        /// </summary>
        /// <returns>
        /// The card that was drawn
        /// </returns>
        [OperationContract]
        void DrawCard();

        /// <summary>
        /// Adds the player to the game.
        /// </summary>
        /// <param name="player">The player information.</param>
        /// <returns>
        /// The players id in the game.
        /// </returns>
        [OperationContract]
        int Join(Player player);

        /// <summary>
        /// Selects the tile with a given card.
        /// </summary>
        /// <param name="tile">The selected tile.</param>
        /// <param name="card">The card that was played to select the tile.</param>
        [OperationContract]
        void SelectTile(int tile, int card);

        /// <summary>
        /// Starts the game with the joined players.
        /// </summary>
        [OperationContract]
        void StartGame();
    }
}