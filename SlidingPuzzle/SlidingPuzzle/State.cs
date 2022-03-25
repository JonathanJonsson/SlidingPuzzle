using System.Windows.Input;
namespace SlidingPuzzle;

public class State //Node!
{
	public Cell[] Grid = new Cell[9];
	public int GridWidth = 3;
	public List<State> predecessors = new List<State>();

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

	
	public int GetIndexOfCell(int value)
	{
 
		for (int i = 0; i < Grid.Length; i++)
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
		foreach (var cell in Grid)
		{
			if (cell.currentCellNumber != cell.targetCellNumber)
			{
				return false;
			}
		}

		return true;
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
		
	// public void GetNeighbour()
	public IEnumerable<State> GetNeighbour()
	{
		  
			yield return new State()
			{
				Grid =  this.Grid,
				GridWidth = this.GridWidth,
				//Do the swap between -1 and other direction
				
				
			};
			
			
		
		




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
