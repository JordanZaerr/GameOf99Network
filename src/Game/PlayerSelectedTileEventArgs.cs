namespace Game
{
    public class PlayerSelectedTileEventArgs
    {
        public PlayerSelectedTileEventArgs(int playerId, int card)
        {
            PlayerId = playerId;
            Card = card;
        }

        public int PlayerId { get; private set; }
        public int Card { get; private set; }
    }
}