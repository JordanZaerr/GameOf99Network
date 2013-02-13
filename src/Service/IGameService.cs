using System.ServiceModel;
using Service.Contracts;

namespace Service
{
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IGameServiceEvents))]
    public interface IGameService
    {
        /// <summary>
        /// Adds the player to the game.
        /// </summary>
        /// <param name="player">The player to add to the game.</param>
        /// <returns>The players id in the game.</returns>
        int Join(Player player);

        /// <summary>
        /// Draws a card from the deck.
        /// </summary>
        /// <returns>The card that was drawn</returns>
        void DrawCard();

        /// <summary>
        /// Discards the selected card from the players hand.
        /// </summary>
        /// <param name="card"></param>
        void DiscardCard(int card);

        /// <summary>
        /// Selects the tile with a given card.
        /// </summary>
        /// <param name="tile">The selected tile.</param>
        /// <param name="card">The card that was played to select the tile.</param>
        void SelectTile(int tile, int card);

        /// <summary>
        /// Starts the game with the current players.
        /// </summary>
        void StartGame();
    }
}