using Game;
using SharpTestsEx;
using Xunit;

namespace Tests
{
    public class BoardTests
    {
        private readonly Board _board;
        private bool _hasWinner;

        public BoardTests()
        {
            _board = new Board();
            _hasWinner = false;
        }

        [Fact]
        public void CheckForWinnerVertical()
        {
            _board.HasWinner += OnHasWinner;

            _board.SelectTile(1, 73);
            _board.SelectTile(1, 74);
            _board.SelectTile(1, 75);
            _board.SelectTile(1, 76);
            _board.SelectTile(1, 77);

            _hasWinner.Should().Be.True();
        }

        [Fact]
        public void CheckForNonWinnerVertical()
        {
            _board.HasWinner += OnHasWinner;

            _board.SelectTile(1, 73);
            _board.SelectTile(1, 74);
            _board.SelectTile(1, 76);
            _board.SelectTile(1, 77);
            _board.SelectTile(1, 81);

            _hasWinner.Should().Be.False();
        }


        [Fact]
        public void CheckForWinnerHorizontal()
        {
            _board.HasWinner += OnHasWinner;

            _board.SelectTile(1, 23);
            _board.SelectTile(1, 12);
            _board.SelectTile(1, 1);
            _board.SelectTile(1, 4);
            _board.SelectTile(1, 5);

            _hasWinner.Should().Be.True();
        }

        [Fact]
        public void CheckForNonWinnerHorizontal()
        {
            _board.HasWinner += OnHasWinner;

            _board.SelectTile(1, 80);
            _board.SelectTile(1, 26);
            _board.SelectTile(1, 28);
            _board.SelectTile(1, 30);
            _board.SelectTile(1, 42);

            _hasWinner.Should().Be.False();
        }

        [Fact]
        public void CheckForWinnerLeftDiagnol()
        {
            _board.HasWinner += OnHasWinner;

            _board.SelectTile(1, 60);
            _board.SelectTile(1, 18);
            _board.SelectTile(1, 16);
            _board.SelectTile(1, 34);
            _board.SelectTile(1, 40);

            _hasWinner.Should().Be.True();
        }

        [Fact]
        public void CheckForNonWinnerLeftDiagnol()
        {
            _board.HasWinner += OnHasWinner;

            _board.SelectTile(1, 77);
            _board.SelectTile(1, 25);
            _board.SelectTile(1, 27);
            _board.SelectTile(1, 47);
            _board.SelectTile(1, 87);

            _hasWinner.Should().Be.False();
        }

        [Fact]
        public void CheckForWinnerRightDiagnol()
        {
            _board.HasWinner += OnHasWinner;

            _board.SelectTile(1, 95);
            _board.SelectTile(1, 41);
            _board.SelectTile(1, 31);
            _board.SelectTile(1, 45);
            _board.SelectTile(1, 87);

            _hasWinner.Should().Be.True();
        }

        [Fact]
        public void CheckForNonWinnerRightDiagnol()
        {
            _board.HasWinner += OnHasWinner;

            _board.SelectTile(1, 62);
            _board.SelectTile(1, 14);
            _board.SelectTile(1, 12);
            _board.SelectTile(1, 52);
            _board.SelectTile(1, 80);

            _hasWinner.Should().Be.False();
        }

        private void OnHasWinner(object sender, int e)
        {
            _hasWinner = true;
        }
    }
}