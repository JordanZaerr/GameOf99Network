using System;
using System.Collections.Generic;
using Game;
using SharpTestsEx;
using Xunit;

namespace Tests
{
    public class DeckTests
    {
        private readonly Deck _deck;
        private bool _outOfCards;

        public DeckTests()
        {
            _deck = new Deck();
            _deck.OutOfCards += OnOutOfCards;
        }

        private void OnOutOfCards(object sender, EventArgs e)
        {
            _outOfCards = true;
        }

        [Fact]
        public void RaisesOutOfCards()
        {
            for (int i = 0; i < 100; i++)
            {
                _deck.DrawCard();
            }
            _outOfCards.Should().Be.True();
        }

        [Fact]
        public void ReturnsNegOneForNoMorecards()
        {
            var lastCard = 0;
            for (int i = 0; i < 100; i++)
            {
               lastCard = _deck.DrawCard();
            }
            lastCard.Should().Be(-1);
        }

        [Fact]
        public void Shuffles()
        {
            _deck.DrawCard().Should().Not.Be(1);
            _deck.DrawCard().Should().Not.Be(2);
            _deck.DrawCard().Should().Not.Be(3);
        }

        [Fact]
        public void ReshufflesDiscards()
        {
            var cards = new List<int>();
            for (int i = 0; i < 95; i++)
            {
                var card = _deck.DrawCard();
                if (card%9 == 0)
                {
                    cards.Add(card);
                    _deck.DiscardCard(card);
                }
            }

            var shuffledDiscards = new List<int>();
            for (int i = 0; i < cards.Count + 4; i++)
            {
                if (i > 3)
                {
                    var card = _deck.DrawCard();
                    card.Satisfy(x => x%9 == 0);
                    shuffledDiscards.Add(card);
                }
                else
                    _deck.DrawCard();
            }
            cards.Should().Not.Have.SameSequenceAs(shuffledDiscards);
        }
    }
}