
using SlidingPuzzle;

State state = new();

// Console.WriteLine(state.GetCell(1,0).targetCellNumber);

int x = 0, y = 0;
for (int i = 0;       i < state.grid.Length;   i++)
{
	if (x > 2)
	{
		y++;
		x = 0;
	}

	Console.WriteLine(state.GetCell(x,y).targetCellNumber);
	x++;


}
