using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Game;
using SharpTestsEx;
using Xunit;

namespace Tests
{
    public class GameTests
    {
        private readonly  Game.Game _game;
        private int _currentPlayerId;
        private int _winningPlayerId;
        private int _playerToSelectTile;
        private int _lastTileSelected;
        private readonly ConcurrentDictionary<int, List<int>> _playersHands;

        public GameTests()
        {
            _playersHands = new ConcurrentDictionary<int, List<int>>(); 
            _game = new Game.Game();
            _game.PlayerTurnChanged += OnPlayerTurnChanged;
            _game.PlayerHandChanged += OnPlayerHandChanged;
            _game.PlayerWon += OnPlayerWon;
            _game.PlayerSelectedTile += OnPlayerSelectedTile;
        }

        [Fact]
        public void CantStartGamePrematurely()
        {
            Executing.This(() => _game.StartGame()).Should()
                .Throw<InvalidOperationException>();
        }

        [Fact]
        public void CanAddPlayer()
        {
            var playerId = _game.AddPlayer();
            playerId.Should().Be(1);
        }

        [Fact]
        public void CanAddMutiplePlayer()
        {
            var player1 = _game.AddPlayer();
            var player2 = _game.AddPlayer();
            player1.Should().Be(1);
            player2.Should().Be(2);
        }

        [Fact]
        public void CanStartGame()
        {
            _game.AddPlayer();
            _game.AddPlayer();

            _game.StartGame();

            _currentPlayerId.Should().Be(1);
        }

        [Fact]
        public void CantAddPlayerAfterGameStart()
        {
            StartGame();

            Executing.This(() => _game.AddPlayer()).Should()
                .Throw<InvalidOperationException>();
        }

        [Fact]
        public void PlayersHandShouldHave5CardsAtGameStart()
        {
            StartGame();

            _playersHands[1].Count.Should().Be(5);
            _playersHands[2].Count.Should().Be(5);
        }

        [Fact]
        public void PlayerShouldLooseCardOnDiscard()
        {
            const int playerId = 1;
            StartGame();

            var card = _playersHands[playerId].First();
            _game.DiscardCard(playerId, card);

            _playersHands[playerId].Count.Should().Be(4);
            _playersHands[playerId].Should().Not.Contain(card);
        }

        [Fact]
        public void PlayerCantHaveMoreThanFiveCards()
        {
            StartGame();

            Executing.This(() => _game.DrawCard(1)).Should()
                .Throw<InvalidOperationException>();
        }

        [Fact]
        public void PlayerCanDrawMoreCards()
        {
            const int playerId = 1;
            StartGame();

            var playersOriginalHand = _playersHands[playerId].ToList();

            _game.DiscardCard(playerId, playersOriginalHand.First());
            _playersHands[playerId].Count.Should().Be(4);

            _game.DrawCard(playerId);
            var playersNewHand = _playersHands[playerId].ToList();

            playersNewHand.Count.Should().Be(5);
            var newCards = playersNewHand.Except(playersOriginalHand).ToList();

            newCards.Count.Should().Be(1);
            playersOriginalHand.Should().Not.Contain(newCards.First());
        }

        [Fact]
        public void PlayerCanSelectTile()
        {
            _lastTileSelected = -1;
            StartGame();
            var card = _playersHands[1].First();
            _game.SelectTile(1, card, card);

            _lastTileSelected.Should().Be(card);
            _playerToSelectTile.Should().Be(1);
        }

        [Fact]
        public void PlayerCanSelectTileThatIsHigherThanCard()
        {
            _lastTileSelected = -1;
            StartGame();
            var card = _playersHands[1].First();
            _game.SelectTile(1, card + 1, card);

            _lastTileSelected.Should().Be(card);
        }

        [Fact]
        public void PlayerCantSelectTileThatIsLowerThanCard()
        {
            _lastTileSelected = -1;
            StartGame();
            var card = _playersHands[1].First();
            Executing.This(() => 
                _game.SelectTile(1, card - 1, card))
                .Should().Throw<InvalidOperationException>();
            _lastTileSelected.Should().Be(-1);
        }

        [Fact]
        public void PlayerCantSelectTileWhenItIsNotTheirTurn()
        {
            _lastTileSelected = -1;
            StartGame();
            var card = _playersHands[2].First();
            Executing.This(() =>
                _game.SelectTile(2, card - 1, card))
                .Should().Throw<InvalidOperationException>();

            _lastTileSelected.Should().Be(-1);
        }


        [Fact]
        public void PlayerCantSelectTileWithCardNotInTheirHand()
        {
            _lastTileSelected = -1;
            StartGame();

            //Wrong players hand
            var card = _playersHands[2].First();
            Executing.This(() =>
                _game.SelectTile(1, card, card))
                .Should().Throw<InvalidOperationException>();

            _lastTileSelected.Should().Be(-1);
        }

        [Fact]
        public void SelectingATileRemovesTheCardFromYourHand()
        {
            _lastTileSelected = -1;
            StartGame();
            List<int> playersHand = _playersHands[1];
            var card = playersHand.First();
            _game.SelectTile(1, card, card);

            playersHand.Count.Should().Be(4);
            playersHand.Should().Not.Contain(card);
        }

        [Fact]
        public void SelectingATileAdvancesThePlayersTurn()
        {
            _lastTileSelected = -1;
            StartGame();

            _currentPlayerId.Should().Be(1);

            //Player 1 moves
            var card = _playersHands[1].First();
            _game.SelectTile(1, card, card);
            _currentPlayerId.Should().Be(2);

            //Player 2 moves
            card = _playersHands[2].First();
            _game.SelectTile(2, card, card);
            _currentPlayerId.Should().Be(1);

            //Player 1 moves
            card = _playersHands[1].First();
            _game.SelectTile(1, card, card);
            _currentPlayerId.Should().Be(2);
        }

        private void OnPlayerHandChanged(object sender, PlayerHandChangedEventArgs e)
        {
            _playersHands.AddOrUpdate(e.PlayerId,
                hand => new List<int> {e.Card}, 
                (key, hand) => 
                {
                    if (e.CardAdded)
                        hand.Add(e.Card);
                    else
                        hand.Remove(e.Card);
                    return hand;
                });
        }

        private void OnPlayerSelectedTile(object sender, PlayerSelectedTileEventArgs e)
        {
            _playerToSelectTile = e.PlayerId;
            _lastTileSelected = e.Card;
        }

        private void OnPlayerTurnChanged(object sender, int playerId)
        {
            _currentPlayerId = playerId;
        }

        private void OnPlayerWon(object sender, int playerId)
        {
            _winningPlayerId = playerId;
        }

        private void StartGame()
        {
            _game.AddPlayer();
            _game.AddPlayer();
            _game.StartGame();
        }
    }
}