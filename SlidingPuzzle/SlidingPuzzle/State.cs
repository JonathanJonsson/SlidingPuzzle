using System.Numerics;

namespace SlidingPuzzle;

public class State //Node!
{
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

	private Cell[,] grid = new Cell[3,3];
	private int gridWidth = 3;
 
	public State PreviousState;
 
	public State()
	{
		var targetNumber = 1;
		var i = 0;
		var shuffledNumbers = numberPool.OrderBy(a => Guid.NewGuid()).ToList();
		for (int x = 0; x <= grid.Rank; x++)
		{
			for (int y = 0; y <= grid.Rank; y++)
			{
				grid[x, y] = new Cell()
				{
					targetCellNumber = targetNumber,
					currentCellNumber = fixedStartSetup[i],
					gridPosition = new Vector2(x,y)
				};
				i++;
				targetNumber++;
			}
		}
	}
	

	public Cell GetCellValue(int x, int y)
	{
		return grid[x,y];
	}

	 

	private Vector2 GetIndexOfCell(int value)
	{
		foreach (var cell in grid)
		{
			if (value == cell.currentCellNumber)
			{
				return cell.gridPosition;
			}
		}

		return default;
	}

	public void PrintCurrentState()
	{
		Console.WriteLine();
		Console.WriteLine();
		Console.WriteLine("Current state (-1 = empty slot): ");

		for (int x = 0; x <= grid.Rank; x++)
		{
			for (int y = 0; y <= grid.Rank; y++)
			{
				Console.Write(GetCellValue(x, y).currentCellNumber + " | ");

			}
			Console.WriteLine();
			Console.Write("-------------");
			Console.WriteLine();
		}
		
		 
	}

	public void PrintTargetState()
	{
		Console.WriteLine();
		Console.WriteLine("Target state (-1 = empty slot): ");
	
		for (int x = 0; x <= grid.Rank; x++)
		{
			for (int y = 0; y <= grid.Rank; y++)
			{
				if (x == grid.Rank && y == grid.Rank)
					grid[x, y].targetCellNumber = -1;
				
				Console.Write(GetCellValue(x, y).targetCellNumber + " | ");

			}
			Console.WriteLine();
			Console.Write("-------------");
			Console.WriteLine();
		}
	}

	public bool IsEndNode()
	{
		foreach (var cell in grid)
		{
			if (cell.currentCellNumber != cell.targetCellNumber)
			{
				return false;
			}
		}

		return true;
	}

	public IEnumerable<State> GetNeighbour()
	{
		//get cardinal directions in relation to empty slot (=-1)
		var emptySlotPosition = GetIndexOfCell(-1);
		var slotAbove = new Vector2(emptySlotPosition.X-1, emptySlotPosition.Y);
		var slotBelow = new Vector2(emptySlotPosition.X+1, emptySlotPosition.Y);
		var slotRight = new Vector2(emptySlotPosition.X, emptySlotPosition.Y+1);
		var slotLeft = new Vector2(emptySlotPosition.X, emptySlotPosition.Y-1);
		
		
		
 
		if (LegalBoardPosition(slotAbove)) // inside board
		{
			
			var newState = new State 
			{
				grid = grid,
				gridWidth = gridWidth,
				PreviousState = this,
			};
			
				SwapElementsInState( slotAbove, emptySlotPosition);
				
				yield return newState;
			
	
		}
	
		if (LegalBoardPosition(slotBelow))
		{
			var newState = new State 
			{
				grid = grid,
				gridWidth = gridWidth,
				PreviousState = this
			};
	
 
				SwapElementsInState( slotBelow, emptySlotPosition);
	
				yield return newState;
			
	
		}
	
		if (LegalBoardPosition(slotLeft)) 
		{
			var newState = new State 
			{
				grid = grid,
				gridWidth = gridWidth,
				PreviousState = this
			};
	
	
				SwapElementsInState( slotLeft, emptySlotPosition);
	
				yield return newState;
			
		}
	
		if (LegalBoardPosition(slotRight)) 
		{
			var newState = new State 
			{
				grid = grid,
				gridWidth = gridWidth,
				PreviousState = this
			};
	
				SwapElementsInState( slotRight, emptySlotPosition);
	
				yield return newState;
			
		}
	}

	private void SwapElementsInState( Vector2 slotPosition, Vector2 EmptyPos)
	{
		var temp = this.grid[(int)slotPosition.X, (int)slotPosition.Y];
		this.grid[(int) slotPosition.X, (int) slotPosition.Y] = this.grid[(int) EmptyPos.X, (int) EmptyPos.Y];
		this.grid[(int) EmptyPos.X, (int) EmptyPos.Y] = temp;


	}

	private bool LegalBoardPosition(Vector2 pos)
	{
		if (pos.X > grid.Rank || pos.X<0 || pos.Y> grid.Rank || pos.Y<0)
		{
			return false;
		}
		
		return true;
	}
 
}
