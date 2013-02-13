using System;
using System.Collections.Generic;
using System.Linq;

namespace Game
{
    internal class Deck
    {
        private readonly List<int> _cards = new List<int>();
        private readonly List<int> _discardedCards = new List<int>();

        public event EventHandler OutOfCards;

        public Deck()
        {
            _cards = Enumerable.Range(1, 99).ToList();
            _cards.Shuffle();
        }

        public int DrawCard()
        {
            if (_cards.Count == 0)
            {
                if (_discardedCards.Count > 0)
                    Reshuffle();
                else
                {
                    var handler = OutOfCards;
                    if (handler != null)
                    {
                        handler(this, EventArgs.Empty);
                    }
                    return -1;
                }
            }

            var card = _cards.First();
            _cards.Remove(card);
            return card;
        }

        public void DiscardCard(int card)
        {
            _discardedCards.Add(card);
        }

        private void Reshuffle()
        {
            _cards.AddRange(_discardedCards);
            _cards.Shuffle();
            _discardedCards.Clear();
        }
    }
}