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

	private readonly List<int> staticStartSetup = new()
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

	public Cell[] Grid = new Cell[9];
	public int GridWidth = 3;
	public List<State> predecessors = new();

	public State()
	{
		var x = 1;
		var shuffledNumbers = numberPool.OrderBy(a => Guid.NewGuid()).ToList();

		for (var i = 0; i < Grid.Length; i++)
		{
			Grid[i] = new Cell
			{
				targetCellNumber = x,
				currentCellNumber = staticStartSetup[i] //TODO: Change to shuffled here
			};
			x++;
		}
	}

	public Cell GetCellValue(int x, int y)
	{
		return Grid[x + y*GridWidth];
	}

	public Cell GetCellValue(int index)
	{
		return Grid[index];
	}
	public int GetIndexOfCell(int value)
	{
		for (var i = 0; i < Grid.Length; i++)
		{
			if (value == Grid[i].currentCellNumber)
			{
				return i;
			}
		}

		return 0;
	}

	public void PrintCurrentState()
	{
		Console.WriteLine();
		int x = 0, y = 0;
		Console.WriteLine();
		Console.WriteLine("Current state (-1 = empty slot): ");

		for (var i = 0; i < Grid.Length; i++)
		{
			if (x > 2)
			{
				y++;
				x = 0;
				Console.WriteLine();
				Console.Write("-------------");
				Console.WriteLine();
			}

			Console.Write(GetCellValue(x, y).currentCellNumber + " | ");
			x++;
		}
	}

	public void PrintTargetState()
	{
		int x = 0, y = 0;
		Console.WriteLine();
		Console.WriteLine("Target state (-1 = empty slot): ");

		for (var i = 0; i < Grid.Length; i++)
		{
			if (i == Grid.Length - 1)
			{
				Grid[i].targetCellNumber = -1;
			}

			if (x > 2)
			{
				y++;
				x = 0;
				Console.WriteLine();
				Console.Write("-------------");
				Console.WriteLine();
			}

			Console.Write(GetCellValue(x, y).targetCellNumber + " | ");
			x++;
		}
	}

	public bool IsEndNode()
	{
		foreach (var cell in this.Grid)
		{
			if (cell.currentCellNumber != cell.targetCellNumber)
			{
				return false;
			}
		}

		return true;
	}

	// public void GetNeighbour()
	public IEnumerable<State> GetNeighbour()
	{
		var slotPosition = GetIndexOfCell(-1);
		var checkUp = slotPosition - GridWidth;
		var checkDown = slotPosition + GridWidth;
		var checkRight = slotPosition + 1;
		var checkLeft = slotPosition - 1;

		if (predecessors.Contains(this))
		{
			yield break;
		}
		
		if (LegalBoardPosition(checkUp)) // inside board
		{
			var newState = new State
			{
				Grid = Grid,
				GridWidth = GridWidth
			};
			SwapElements(newState, checkUp);

			yield return newState;
		}

		if (LegalBoardPosition(checkDown)) // inside board
		{
			var newState = new State
			{
				Grid = Grid,
				GridWidth = GridWidth
			};
			SwapElements(newState, checkDown);

			yield return newState;
		}

		if (LegalBoardPosition(checkLeft)) // inside board
		{
			var newState = new State
			{
				Grid = Grid,
				GridWidth = GridWidth
			};
			SwapElements(newState, checkLeft);

			yield return newState;
		}

		if (LegalBoardPosition(checkRight)) // inside board
		{
			var newState = new State
			{
				Grid = Grid,
				GridWidth = GridWidth
			};
			SwapElements(newState, checkRight);

			yield return newState;
		}
	}

	private void SwapElements(State newState, int checkDir)
	{
		var temp = newState.Grid[checkDir].currentCellNumber;
		Grid[GetIndexOfCell(-1)].currentCellNumber = temp;
		Grid[checkDir].currentCellNumber = -1;
	}

	public bool LegalBoardPosition(int pos)
	{
		if (pos >= 0 && pos < Grid.Length - 1)
		{
			return true;
		}

		return false;
	}

	public List<State> ReturnPath()
	{
		var path = new List<State>();

		foreach (var state in predecessors)
		{
			path.Add(state);
		}

		path.Add(this);

		return path;
	}

	#region TempSave
	// var currentHole = GetIndexOfCell(-1);
	// //check if up is allowed
	// var checkPos = currentHole - GridWidth;
	//
	// 	if (checkPos < 0)
	// {
	// 	Console.WriteLine("ERROR: OUTSIDE ARRAY");
	//
	// 	return;
	// }else
	// {
	// 	Console.WriteLine("Below: " +Grid[checkPos].currentCellNumber);
	// 			
	// }
	#endregion
}
