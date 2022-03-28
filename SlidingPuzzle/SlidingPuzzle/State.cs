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
				currentCellNumber = fixedStartSetup[i] //TODO: Change to shuffled here after getting scripts to work
			};
			x++;
		}
	}

	private bool IsIllegalState
	{
		get
		{
			if (predecessors.Contains(this))
			{
				return true;
			}
			
			return false;
		}
	}

	private Cell GetCellValue(int x, int y)
	{
		return Grid[x + y*GridWidth];
	}

	private Cell GetCellValue(int index)
	{
		return Grid[index];
	}

	private int GetIndexOfCell(int value)
	{
		for (var i = 0; i < Grid.Length; i++)
		{
			if (value == Grid[i].currentCellNumber)
			{
				return i;
			}
		}

		return default;
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
		foreach (var cell in Grid)
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
		var slotAboveIndex = emptySlotPosition - GridWidth;
		var slotBelowIndex = emptySlotPosition + GridWidth;
		var slotRightIndex = emptySlotPosition + 1;
		var slotLeftIndex = emptySlotPosition - 1;
		
		var newState = new State
		{
			Grid = Grid,
			GridWidth = GridWidth,
			predecessors = predecessors.Concat
			(
				new[]
				{
					this
				}
			).ToList()
		};

		if (newState.IsIllegalState)
		{
			yield break;
		}

		if (LegalBoardPosition(slotAboveIndex)) // inside board
		{
			// if (GetCellValue(emptySlotPosition).currentCellNumber == -1)
			// {
				SwapElementsInState(newState, slotAboveIndex, emptySlotPosition);
				
				yield return newState;
			// }

		}

		if (LegalBoardPosition(slotBelowIndex)) // inside board
		{

			// if (GetCellValue(emptySlotPosition).currentCellNumber == -1)
			// {
				SwapElementsInState(newState, slotBelowIndex, emptySlotPosition);

				yield return newState;
			// }

		}

		if (LegalBoardPosition(slotLeftIndex)) // inside board
		{
			// var newState = new State
			// {
			// 	Grid = Grid,
			// 	GridWidth = GridWidth
			// };
			// if (GetCellValue(emptySlotPosition).currentCellNumber == -1)
			// {
				SwapElementsInState(newState, slotLeftIndex, emptySlotPosition);

				yield return newState;
			// }
		}

		if (LegalBoardPosition(slotRightIndex)) // inside board
		{
			// var newState = new State
			// {
			// 	Grid = Grid,
			// 	GridWidth = GridWidth
			// };

			// if (GetCellValue(emptySlotPosition).currentCellNumber == -1)
			// {
				SwapElementsInState(newState, slotRightIndex, emptySlotPosition);

				yield return newState;
			// }
		}
	}

	private void SwapElementsInState(State newState, int checkDir, int originalEmptyPos)
	{
		(newState.Grid[checkDir].currentCellNumber, newState.Grid[originalEmptyPos].currentCellNumber) = (newState.Grid[originalEmptyPos].currentCellNumber, newState.Grid[checkDir].currentCellNumber);
	}

	private bool LegalBoardPosition(int pos)
	{
		return pos >= 0 && pos < Grid.Length - 1;
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
}
