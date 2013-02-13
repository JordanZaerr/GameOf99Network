using System;

namespace Game
{
    public class PlayerHandChangedEventArgs : EventArgs
    {
        public PlayerHandChangedEventArgs(int playerId, int card, bool cardAdded)
        {
            PlayerId = playerId;
            Card = card;
            CardAdded = cardAdded;
        }

        public int PlayerId { get; private set; }
        public int Card { get; private set; }
        public bool CardAdded { get; private set; }
    }
}