using System.Collections.Generic;

namespace Game
{
    public class Player
    {
        public Player(int id)
        {
            Id = id;
            Hand = new List<int>();
        }

        public int Id { get; private set; }
        public List<int> Hand { get; private set; } 
    }
}