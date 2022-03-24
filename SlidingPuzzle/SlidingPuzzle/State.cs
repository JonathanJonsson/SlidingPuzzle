namespace SlidingPuzzle;

public class State
{
	public Cell[] grid = new Cell[9];
	public int gridWidth = 3;
	private bool winState = false;

	private List<int> numberPool = new List<int>()
	{
		1,
		2,
		3,
		4,
		5,
		6,
		7,
		8
	};

	public Cell GetCell(int x, int y)
	{
		return grid[x + y*gridWidth];
	}

	private Random random = new Random();

	public State()
	{
		int x = 1;

		for (int i = 0; i < grid.Length; i++)
		{
			grid[i] = new Cell();

			grid[i].targetCellNumber = x;
			var randomPoolNumber = random.Next(0, numberPool.Count);
			grid[i].currentCellNumber = numberPool[randomPoolNumber]; //TODO: change for array so duplicates cant be selected
			
			x++;

		}



	}

	public void PrintCurrentState()
	{
		int x = 0, y = 0;
		Console.WriteLine();
		Console.WriteLine("Current state (-1 = empty slot): ");
		for (int i = 0; i < grid.Length; i++)
		{
			 
			if (x > 2)
			{
				y++;
				x = 0;
				Console.WriteLine();
				Console.Write("-------------");
				Console.WriteLine();
			}

			Console.Write(GetCell(x, y).currentCellNumber + " | ");
			x++;
		}	
	}
	
	public void PrintTargetState()
	{
		int x = 0, y = 0;
		Console.WriteLine();
		Console.WriteLine("Target state (-1 = empty slot): ");
		for (int i = 0; i < grid.Length; i++)
		{
			if (i == grid.Length - 1)
			{
				grid[i].targetCellNumber = -1;
				
			}
			if (x > 2)
			{
				y++;
				x = 0;
				Console.WriteLine();
				Console.Write("-------------");
				Console.WriteLine();
			}

			Console.Write(GetCell(x, y).targetCellNumber + " | ");
			x++;
		}
	}

	public bool CheckWinState()
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
}