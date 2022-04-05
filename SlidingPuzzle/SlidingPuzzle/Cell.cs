using System.Numerics;

namespace SlidingPuzzle;

public class Cell
{
	public Vector2 GridPosition = new(0, 0);
	public int TargetCellNumber =0; //TODO: Cell does not need to know abut its target state --> move out later
	public int CurrentCellNumber=0;

}
