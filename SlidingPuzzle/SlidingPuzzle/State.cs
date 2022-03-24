using System.Windows.Input;
namespace SlidingPuzzle;

public class State //Node basically!
{
	public Cell[] Grid = new Cell[9];
	public int GridWidth = 3;

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

	public State()
	{
		var x = 1;
		var shuffledNumbers = numberPool.OrderBy(a => Guid.NewGuid()).ToList();

		for (var i = 0; i < Grid.Length; i++)
		{
			Grid[i] = new Cell
			{
				targetCellNumber = x,
				currentCellNumber = shuffledNumbers[i]
			};
			x++;
		}
	}

	public Cell GetCell(int x, int y)
	{
		return Grid[x + y*GridWidth];
	}

	public void PrintCurrentState()
	{
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

			Console.Write(GetCell(x, y).currentCellNumber + " | ");
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

			Console.Write(GetCell(x, y).targetCellNumber + " | ");
			x++;
		}
	}

	public bool CheckWinState()
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

	public void GetNeighbour()
	// public IEnumerable<State> GetNeighbour()
	{
		var allowed = false;
		var inputKey = new ConsoleKey();
		
		while (!allowed)
		{
			Console.WriteLine("Use arrows to reach targetState by moving to -1 (representing the empty block)");
			inputKey = Console.ReadKey().Key;
			if (inputKey == ConsoleKey.UpArrow || inputKey == ConsoleKey.DownArrow || inputKey == ConsoleKey.LeftArrow || inputKey == ConsoleKey.RightArrow)
				allowed = true;
		}
		
		if (inputKey == ConsoleKey.UpArrow)
		{
			
		}
		if (inputKey == ConsoleKey.DownArrow)
		{
			
		}
		if (inputKey == ConsoleKey.LeftArrow)
		{
			
		}
		if (inputKey == ConsoleKey.RightArrow)
		{
			
		}
		
		
	}
}
