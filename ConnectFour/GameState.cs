namespace ConnectFour;

public class GameState
{
    public enum WinState
    {
        No_Winner = 0,
        Player1_Wins = 1,
        Player2_Wins = 2,
        Tie = 3
    }

    public int PlayerTurn => TheBoard.Count(x => x != 0) % 2 + 1;

    public int CurrentTurn => TheBoard.Count(x => x != 0);

    public List<int> TheBoard { get; private set; } = new(new int[42]);

    public void ResetBoard()
    {
        TheBoard = new List<int>(new int[42]);
    }

    public byte PlayPiece(int column)
    {
        if (column is < 0 or > 6)
        {
            throw new ArgumentException("Invalid column");
        }

        if (CheckForWin() != WinState.No_Winner)
        {
            throw new ArgumentException("Game is over");
        }

        var landingSpot = -1;
        for (var row = 0; row < 6; row++)
        {
            var index = row * 7 + column;
            if (TheBoard[index] == 0)
            {
                landingSpot = index;
                break;
            }
        }

        if (landingSpot < 0)
        {
            throw new ArgumentException("Column is full");
        }

        TheBoard[landingSpot] = PlayerTurn;
        return (byte)(landingSpot / 7 + 1);
    }

    public WinState CheckForWin()
    {
        for (var row = 0; row < 6; row++)
        {
            for (var column = 0; column < 7; column++)
            {
                var player = TheBoard[row * 7 + column];
                if (player == 0)
                {
                    continue;
                }

                if (HasLine(row, column, 0, 1, player)
                    || HasLine(row, column, 1, 0, player)
                    || HasLine(row, column, 1, 1, player)
                    || HasLine(row, column, 1, -1, player))
                {
                    return (WinState)player;
                }
            }
        }

        return TheBoard.All(x => x != 0) ? WinState.Tie : WinState.No_Winner;
    }

    private bool HasLine(int row, int column, int rowStep, int columnStep, int player)
    {
        var endRow = row + rowStep * 3;
        var endColumn = column + columnStep * 3;
        if (endRow is < 0 or >= 6 || endColumn is < 0 or >= 7)
        {
            return false;
        }

        for (var offset = 1; offset < 4; offset++)
        {
            var index = (row + rowStep * offset) * 7 + column + columnStep * offset;
            if (TheBoard[index] != player)
            {
                return false;
            }
        }

        return true;
    }
}