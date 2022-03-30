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
		-1,
		3,
		1,
		6,
		8
	};

	private Cell[,] grid = new Cell[3,3];
	private int gridWidth = 3;
	private Vector2 cellPos = default; 
 
	public State PreviousState;
 
	public State()
	{
		var targetNumber = 1;
		var i = 0;
		var shuffledNumbers = numberPool.OrderBy(a => Guid.NewGuid()).ToList();
		Console.WriteLine(grid.Rank);
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

	// public IEnumerable<State> GetNeighbour()
	// {
	// 	//get cardinal directions in relation to empty slot (=-1)
	// 	////TODO: Need x & y --> int vector
	// 	var emptySlotPosition = GetIndexOfCell(-1);
	// 	var slotAboveIndex = new Vector2(emptySlotPosition.X, emptySlotPosition.Y + 1);
	// 	var slotBelowIndex = new Vector2(emptySlotPosition.X, emptySlotPosition.Y -1);
	// 	var slotRightIndex = new Vector2(emptySlotPosition.X+1, emptySlotPosition.Y);
	// 	var slotLeftIndex = new Vector2(emptySlotPosition.X-1, emptySlotPosition.Y);;
	// 	
	// 	
	// 	
 //
	// 	if (LegalBoardPosition(slotAboveIndex)) // inside board
	// 	{
	// 		
	// 		var newState = new State //TODO: in each statement below.
	// 		{
	// 			grid = grid,
	// 			gridWidth = gridWidth,
	// 			PreviousState = this,
	// 		};
	// 		
	// 			// SwapElementsInState(newState, slotAboveIndex, emptySlotPosition);
	// 			
	// 			yield return newState;
	// 		
	//
	// 	}
	//
	// 	if (LegalBoardPosition(slotBelowIndex)) // inside board
	// 	{
	// 		var newState = new State //TODO: in each statement below.
	// 		{
	// 			grid = grid,
	// 			gridWidth = gridWidth,
	// 			PreviousState = this
	// 		};
	//
 //
	// 			// SwapElementsInState(newState, slotBelowIndex, emptySlotPosition);
	//
	// 			yield return newState;
	// 		
	//
	// 	}
	//
	// 	if (LegalBoardPosition(slotLeftIndex)) // inside board
	// 	{
	// 		var newState = new State //TODO: in each statement below.
	// 		{
	// 			grid = grid,
	// 			gridWidth = gridWidth,
	// 			PreviousState = this
	// 		};
	//
	//
	// 			// SwapElementsInState(newState, slotLeftIndex, emptySlotPosition);
	//
	// 			yield return newState;
	// 		
	// 	}
	//
	// 	if (LegalBoardPosition(slotRightIndex)) // inside board
	// 	{
	// 		var newState = new State //TODO: in each statement below.
	// 		{
	// 			grid = grid,
	// 			gridWidth = gridWidth,
	// 			PreviousState = this
	// 		};
	//
	// 			// SwapElementsInState(newState, slotRightIndex, emptySlotPosition);
	//
	// 			yield return newState;
	// 		
	// 	}
	// }

	private void SwapElementsInState(State newState, Vector2 checkDir, Vector2 originalEmptyPos)
	{
		// (newState.grid[checkDir].currentCellNumber, newState.grid[originalEmptyPos].currentCellNumber) = (newState.grid[originalEmptyPos].currentCellNumber, newState.grid[checkDir].currentCellNumber);
		
		
		
	}

	// private bool LegalBoardPosition(Vector2 pos)
	// {
	// 	if (x + y*gridWidth > grid.Length && x + y*gridWidth < 0)
	// 	{
	// 		return false;
	// 	}
	// 	return true;
	// }
 
}
