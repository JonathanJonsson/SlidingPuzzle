using System.Numerics;

namespace SlidingPuzzle;

public class State //Node!
{
    private readonly List<int> fixedStartSetup = new()
    {
        7,
        2,
        4,
        5,
        8,
        3,
        1,
        6,
        -1
    };

    private readonly List<int> numberPool = new()
    {
        1,
        2,
        3,
        4,
        5,
        6,
        7,
        8,
        -1
    };

    private Cell[,] grid = new Cell[3, 3];
    private int gridWidth = 3;

    private State previousState;

    public State()
    {
        var targetNumber = 1;
        var i = 0;
        var shuffledNumbers = numberPool.OrderBy(a => Guid.NewGuid()).ToList();
        for (var x = 0; x <= grid.Rank; x++)
        for (var y = 0; y <= grid.Rank; y++)
        {
            grid[x, y] = new Cell
            {
                TargetCellNumber = targetNumber,
                CurrentCellNumber = fixedStartSetup[i],
                GridPosition = new Vector2(x, y)
            };
            i++;
            targetNumber++;
        }
    }

    private Cell GetCellValue(int x, int y)
    {
        return grid[x, y];
    }
    
    private Vector2 GetIndexOfCell(int value)
    {
        foreach (var cell in grid)
            if (value == cell.CurrentCellNumber)
                return cell.GridPosition;

        return default;
    }

    public void PrintState()
    {
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("Current state (-1 = empty slot): ");

        for (var x = 0; x <= grid.GetLength(0)-1; x++)
        {
            for (var y = 0; y <= grid.GetLength(1)-1; y++) 
                Console.Write(GetCellValue(x, y).CurrentCellNumber + " | ");
            
            Console.WriteLine();
            Console.Write("-------------");
            Console.WriteLine();
        }
    }

    public void PrintTargetState()
    {
        Console.WriteLine();
        Console.WriteLine("Target state (-1 = empty slot): ");

        for (var x = 0; x <= grid.GetLength(0)-1; x++) // change to getLength(dim)
        {
            for (var y = 0; y <= grid.GetLength(1)-1; y++)
            {
                if (x == grid.GetLength(1)-1 && y == grid.GetLength(1)-1)
                    grid[x, y].TargetCellNumber = -1;

                Console.Write(GetCellValue(x, y).TargetCellNumber + " | ");
            }

            Console.WriteLine();
            Console.Write("-------------");
            Console.WriteLine();
        }
    }

    public bool IsEndNode() //TODO: This needs to be changed when moving targetCellNumber from Cell.cs later
    {
        foreach (var cell in grid)
            if (cell.CurrentCellNumber != cell.TargetCellNumber)
                return false;

        return true;
    }

    private bool Equals(State other) // checking each cell - if not equal we know that the states are different --> return false. If all are same --> return true
    {
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                if (other.grid[i,j] != grid[i,j])
                {
                    return false;
                }
            }
        }

        return true;
    }

    public override bool Equals(object? obj) // Equals for Objects - do we need it if we only pass in state as per above. This method seems to type cast into state and return Equals() above?
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((State) obj); //Type cast to state - since we passes it into Equals, do we need this override really? I guess it is a safety-check if we pass in a null value?
    }

    public override int GetHashCode() //To compare if a state instance is same as previous state instance. If so, skip it - in GetNeighbour()?
    {
        return grid.GetHashCode();
    }

    public IEnumerable<State> GetNeighbour()
    {
        //get cardinal directions in relation to empty slot (=-1)
        var emptySlotPosition = GetIndexOfCell(-1);
        var slotAbove = new Vector2(emptySlotPosition.X - 1, emptySlotPosition.Y);
        var slotBelow = new Vector2(emptySlotPosition.X + 1, emptySlotPosition.Y);
        var slotRight = new Vector2(emptySlotPosition.X, emptySlotPosition.Y + 1);
        var slotLeft = new Vector2(emptySlotPosition.X, emptySlotPosition.Y - 1);
        
        if (LegalBoardPosition(slotAbove))
        {
            var newState = new State
            {
                grid = this.grid,
                gridWidth = this.gridWidth,
                previousState = this
            };

            SwapElementsInState(newState,slotAbove, emptySlotPosition);

            yield return newState;
        }

        if (LegalBoardPosition(slotBelow))
        {
            var newState = new State
            {
                grid = this.grid,
                gridWidth = this.gridWidth,
                previousState = this
            };
            
            SwapElementsInState(newState,slotBelow, emptySlotPosition);

            yield return newState;
        }

        if (LegalBoardPosition(slotLeft))
        {
            var newState = new State
            {
                grid = this.grid,
                gridWidth = this.gridWidth,
                previousState = this
            };
            
            SwapElementsInState(newState,slotLeft, emptySlotPosition);

            yield return newState;
        }

        if (LegalBoardPosition(slotRight))
        {
            var newState = new State
            {
                grid = this.grid,
                gridWidth = this.gridWidth,
                previousState = this
            };

            SwapElementsInState(newState,slotRight, emptySlotPosition);

            yield return newState;
        }
    }

    private static void SwapElementsInState(State newState, Vector2 slotPosition, Vector2 EmptyPos) // swapping elements in new state
    {
        (newState.grid[(int) slotPosition.X, (int) slotPosition.Y], newState.grid[(int) EmptyPos.X, (int) EmptyPos.Y]) = (newState.grid[(int) EmptyPos.X, (int) EmptyPos.Y], newState.grid[(int) slotPosition.X, (int) slotPosition.Y]);
    }

    private bool LegalBoardPosition(Vector2 pos)
    {
        return !(pos.X > grid.Rank) && !(pos.X < 0) && !(pos.Y > grid.Rank) && !(pos.Y < 0);
    }
}