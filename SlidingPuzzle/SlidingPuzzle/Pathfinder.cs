namespace SlidingPuzzle;

public class Pathfinder
{
	private State finalState;
	private Queue<State> stateQueue = new();
	public State CalculateRoute(State startState)
	{
		stateQueue.Enqueue(startState);
		while (stateQueue.Count > 0)
		{
			var currentState = stateQueue.Dequeue();


			if (currentState.IsEndNode())
			{
				finalState = currentState;
				break;
			}
			
			foreach (var neighbour in currentState.GetNeighbour())
			{
				neighbour.PrintCurrentState();

				neighbour.predecessors.Add(currentState);
				stateQueue.Enqueue(neighbour);

			}
		}

		return finalState;
	}
}
