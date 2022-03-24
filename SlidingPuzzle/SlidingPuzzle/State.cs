namespace SlidingPuzzle;

public class State
{
	public Cell[] grid = new Cell[9]; 
	public int gridWidth = 3;

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
			grid[i].currentCellNumber = random.Next(1, 9); //TODO: change for array so duplicates cant be selected
			x++;

		}
		
		
		
	}

}