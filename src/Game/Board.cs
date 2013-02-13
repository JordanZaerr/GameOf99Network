using System;
using System.Collections.Generic;
using System.Linq;

namespace Game
{
    public class Board
    {
        private readonly List<List<Tile>> _collections = new List<List<Tile>>();
        private readonly List<Tile> _tiles = new List<Tile>();

        public Board()
        {
            BuildBoard();
        }

        public event EventHandler<int> HasWinner;

        public void SelectTile(int playerId, int tileId)
        {
            Tile tile = _tiles[tileId -1];
            if (tile.OwnedByPlayerId != null)
            {
                throw new InvalidOperationException("Another player has already claimed this tile.");
            }
            tile.OwnedByPlayerId = playerId;
            CheckForWinner(playerId);
        }

        private void BuildBoard()
        {
            for (int i = 1; i < 100; i++)
            {
                _tiles.Add(new Tile 
                {
                    Id = i
                });
            }
            BuildRows();
            BuildColumns();
            BuildRightDiagnols();
            BuildLeftDiagnols();
        }

        private void BuildRightDiagnols()
        {
            BuildCollections(new List<List<int>> 
            {
                {new List<int>{78,52,26,48,86}},
                {new List<int>{77,53,25,27,47,87}},
                {new List<int>{76,54,24,10,28,46,88}},
                {new List<int>{75,55,23,11,9,29,45,89}},
                {new List<int>{74,56,22,12,2,8,30,44,90}},
                {new List<int>{73,57,21,13,1,3,7,31,43,91}},
                {new List<int>{72,58,20,14,4,6,32,42,92}},
                {new List<int>{71,59,19,15,5,33,41,93}},
                {new List<int>{70,60,18,16,34,40,94}},
                {new List<int>{69,61,17,35,39,95}},
                {new List<int>{68,62,36,38,96}},
            });
        }

        private void BuildLeftDiagnols()
        {
            BuildCollections(new List<List<int>> 
            {
                {new List<int>{69,59,21,55,77}},
                {new List<int>{68,60,20,22,54,78}},
                {new List<int>{67,61,19,13,23,53,79}},
                {new List<int>{66,62,18,14,12,24,52,80}},
                {new List<int>{65,63,17,15,1,11,25,51,81}},
                {new List<int>{64,36,16,4,2,10,26,50,82}},
                {new List<int>{99,37,35,5,3,9,27,49,83}},
                {new List<int>{98,38,34,6,8,28,48,84}},
                {new List<int>{97,39,33,7,29,47,85}},
                {new List<int>{96,40,32,30,46,86}},
                {new List<int>{95,41,31,45,87}},
            });
        }

        private void BuildCollections(IEnumerable<List<int>> collections)
        {
            foreach (var collection in collections)
            {
                _collections.Add(collection.Select(tile => _tiles[tile - 1]).ToList());
            }
        }

        private void BuildColumns()
        {
            BuildCollections(new List<List<int>> 
            {
                {new List<int>{73,74,75,76,77,78,79,80,81,82}},
                {new List<int>{72,57,56,55,54,53,52,51,50,83}},
                {new List<int>{71,58,21,22,23,24,25,26,49,84}},
                {new List<int>{70,59,20,13,12,11,10,27,48,85}},
                {new List<int>{69,60,19,14,1,2,9,28,47,86}},
                {new List<int>{68,61,18,15,4,3,8,29,46,87}},
                {new List<int>{67,62,17,16,5,6,7,30,45,88}},
                {new List<int>{66,63,36,35,34,33,32,31,44,89}},
                {new List<int>{65,64,37,38,39,40,41,42,43,90}},
                {new List<int>{99,98,97,96,95,94,93,92,91}},
            });
        }

        private void BuildRows()
        {
            BuildCollections(new List<List<int>> 
            {
                {new List<int>{73,72,71,70,69,68,67,66,65}},
                {new List<int>{74,57,58,59,60,61,62,63,64,99}},
                {new List<int>{75,56,21,20,19,18,17,36,37,98}},
                {new List<int>{76,55,22,13,14,15,16,35,38,97}},
                {new List<int>{77,54,23,12,1,4,5,34,39,96}},
                {new List<int>{78,53,24,11,2,3,6,33,40,95}},
                {new List<int>{79,52,25,10,9,8,7,32,41,94}},
                {new List<int>{80,51,26,27,28,29,30,31,42,93}},
                {new List<int>{81,50,49,48,47,46,45,44,43,92}},
                {new List<int>{82,83,84,85,86,87,88,89,90,91}},
            });
        }

        private void CheckForWinner(int playerId)
        {
            
            foreach (List<Tile> col in _collections)
            {
                int consecutive = 0;
                foreach (var tile in col)
                {
                    if (tile.OwnedByPlayerId == playerId)
                        consecutive++;
                    else
                        consecutive = 0;

                    if (consecutive == 5)
                    {
                        EventHandler<int> copy = HasWinner;
                        if (HasWinner != null)
                        {
                            copy(this, playerId);
                        }
                        break;
                    }
                }
            }
        }
    }
}