using System;
using System.Collections.Generic;
using System.Linq;

namespace Game
{
    public class Game
    {
        private readonly Deck _deck;
        private readonly Board _board;
        private readonly Dictionary<int, Player> _players;

        public event EventHandler<int> PlayerTurnChanged;
        public event EventHandler<int> PlayerWon;
        public event EventHandler<PlayerHandChangedEventArgs> PlayerHandChanged;
        public event EventHandler<PlayerSelectedTileEventArgs> PlayerSelectedTile;

        private bool _canAddPlayers = true;
        private int _currentPlayerId;

        public Game()
        {
            _deck = new Deck();
            _deck.OutOfCards += OnOutOfCards;
            _board = new Board();
            _board.HasWinner += OnWinner;
            _players = new Dictionary<int, Player>();
        }

        private void OnOutOfCards(object sender, EventArgs e)
        {
            RaiseEvent(PlayerWon, -1);
        }

        public int AddPlayer()
        {
            if (!_canAddPlayers)
                throw new InvalidOperationException("You cannot add players after the game is started.");

            var playerId = _players.Count + 1;
            _players[playerId] = new Player(playerId);
            return playerId;
        }

        public void StartGame()
        {
            if (_players.Count < 2) 
                throw new InvalidOperationException("Not enough players to start the game.");

            _canAddPlayers = false;
            
            //Deal cards
            for (int i = 0; i < 5; i++)
            {
                _players.Values.Each(player =>
                {
                    var card = _deck.DrawCard();
                    player.Hand.Add(card);
                    RaisePlayerHandChanged(player.Id, card, true);
                });
            }
            //Tell the first player it's their turn.
            _currentPlayerId = _players.Keys.First();
            RaiseEvent(PlayerTurnChanged, _currentPlayerId);
        }

        public void SelectTile(int playerId, int tile, int card)
        {
            if (playerId != _currentPlayerId)
                throw new InvalidOperationException("It's not your turn.");

            var player = _players[playerId];

            if (!player.Hand.Contains(card))
                throw new InvalidOperationException("You don't have that card in your hand.");

            if (tile < card)
                throw new InvalidOperationException("You can't select a tile with a card that is higher than the tile selected.");

            player.Hand.Remove(card);
            _board.SelectTile(playerId, tile);
            RaisePlayerHandChanged(playerId, card, false);
            RaisePlayerSelectedTile(playerId, card);

            _currentPlayerId = GetNextPlayer(playerId);
            RaiseEvent(PlayerTurnChanged, _currentPlayerId);
        }

        private int GetNextPlayer(int playerId)
        {
            var players = _players.Keys.ToList();
            var playerIndex = players.IndexOf(playerId);
            if (playerIndex < players.Count-1)
            {
                return players[playerIndex + 1];
            }
            return 1;
        }

        private void OnWinner(object sender, int playerId)
        {
            RaiseEvent(PlayerWon, playerId);
        }

        public void DiscardCard(int playerId, int card)
        {
            _deck.DiscardCard(card);
            _players[playerId].Hand.Remove(card);
            RaisePlayerHandChanged(playerId, card, false);
        }

        public void DrawCard(int playerId)
        {
            var playersHand = _players[playerId].Hand;
            if (playersHand.Count == 5)
                throw new InvalidOperationException("You can only have a 5 cards.");

            var newCard = _deck.DrawCard();
            _players[playerId].Hand.Add(newCard);
            RaisePlayerHandChanged(playerId, newCard, true);
        }

        private void RaiseEvent(EventHandler<int> ev, int args)
        {
            var handler = ev;
            if (handler != null)
            {
                handler(this, args);
            }
        }

        private void RaisePlayerHandChanged(int playerId, int card, bool added)
        {
            var handler = PlayerHandChanged;
            if (handler != null)
            {
                handler(this, new PlayerHandChangedEventArgs(playerId, card, added));
            }
        }

        private void RaisePlayerSelectedTile(int playerId, int card)
        {
            var handler = PlayerSelectedTile;
            if (handler != null)
            {
                handler(this, new PlayerSelectedTileEventArgs(playerId, card));
            }
        }
    }
}