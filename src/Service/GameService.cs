using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Game;
using Player = Service.Contracts.Player;

namespace Service
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Reentrant)]
    public class GameService : IGameService
    {
        private readonly Game.Game _game;
        private readonly Dictionary<int, Player> _players = new Dictionary<int, Player>();

        public GameService()
        {
            _game = new Game.Game();
            _game.PlayerWon += OnPlayerWon;
            _game.PlayerTurnChanged += OnPlayerTurnChanged;
            _game.PlayerHandChanged += OnPlayerHandChanged;
            _game.PlayerSelectedTile += OnPlayerSelectedTile;
        }

        /// <summary>
        /// Discards the selected card from the players hand.
        /// </summary>
        /// <param name="cardToDiscard"></param>
        public void DiscardCard(int cardToDiscard)
        {
            _game.DiscardCard(GetPlayer().Id, cardToDiscard);
        }

        /// <summary>
        /// Draws a card from the deck.
        /// </summary>
        /// <returns>
        /// The card that was drawn
        /// </returns>
        public void DrawCard()
        {
            _game.DrawCard(GetPlayer().Id);
        }

        /// <summary>
        /// Adds the player to the game.
        /// </summary>
        /// <param name="player">The player information.</param>
        /// <returns>
        /// The players id in the game.
        /// </returns>
        public int Join(Player player)
        {
            int playerId = _game.AddPlayer();
            player.Id = playerId;
            player.EventsHandler = OperationContext.Current.GetCallbackChannel<IGameServiceEvents>();
            _players[player.EventsHandler.GetHashCode()] = player;

            IEnumerable<Player> otherPlayers = _players.Values.Where(x => x.Id != playerId);
            foreach (Player otherPlayer in otherPlayers)
            {
                //Alert the other players about the new player.
                otherPlayer.EventsHandler.PlayerJoinedGame(player.Id, player.Name, player.Color);

                //Alert the new player about the current players.
                player.EventsHandler.PlayerJoinedGame(otherPlayer.Id, otherPlayer.Name, otherPlayer.Color);
            }
            return playerId;
        }

        /// <summary>
        /// Selects the tile with a given card.
        /// </summary>
        /// <param name="tile">The selected tile.</param>
        /// <param name="card">The card that was played to select the tile.</param>
        public void SelectTile(int tile, int card)
        {
            _game.SelectTile(GetPlayer().Id, tile, card);
        }

        public void StartGame()
        {
            _game.StartGame();
        }

        private Player GetPlayer()
        {
            var callback = OperationContext.Current.GetCallbackChannel<IGameServiceEvents>();
            return _players[callback.GetHashCode()];
        }

        private Player GetPlayer(int playerId)
        {
            return _players.Values.First(x => x.Id == playerId);
        }

        private void OnPlayerHandChanged(object sender, PlayerHandChangedEventArgs e)
        {
            Player player = GetPlayer(e.PlayerId);
            if (e.CardAdded)
            {
                player.EventsHandler.CardAdded(e.Card);
            }
            else
            {
                player.EventsHandler.CardRemoved(e.Card);
            }
        }

        private void OnPlayerSelectedTile(object sender, PlayerSelectedTileEventArgs e)
        {
            Player playerWhoSelectedTile = GetPlayer(e.PlayerId);
            foreach (Player player in _players.Values)
            {
                player.EventsHandler.PlayerSelectedTile(e.PlayerId, playerWhoSelectedTile.Color);
            }
        }

        private void OnPlayerTurnChanged(object sender, int playerId)
        {
            GetPlayer(playerId).EventsHandler.YourTurn();
        }

        private void OnPlayerWon(object sender, int playerId)
        {
            Player playerWhoWon = GetPlayer(playerId);
            foreach (Player player in _players.Values)
            {
                player.EventsHandler.PlayerWon(playerId, playerWhoWon.Name);
            }
        }
    }
}