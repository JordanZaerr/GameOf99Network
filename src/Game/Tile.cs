using System.Diagnostics;

namespace Game
{
    [DebuggerDisplay("Id = {Id}, OwnerId: {OwnedByPlayerId}")]
    public class Tile
    {
        public int Id { get; set; }
        public int? OwnedByPlayerId { get; set; }
    }
}