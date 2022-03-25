namespace SlidingPuzzle;

public class Pathfinder
{
	private State finalState;
	public Queue<State> stateQueue = new();

	public State CalculateRoute()
	{
		var currentState = stateQueue.Dequeue();

		while (stateQueue.Count > 0)
		{
			foreach (var neighbour in currentState.GetNeighbour())
			{
				if (currentState.IsEndNode())
				{
					finalState = currentState;

					break;
				}

				if (currentState.predecessors.Contains(neighbour))
				{
					continue;
				}
				
				neighbour.predecessors.Add(currentState);
				stateQueue.Enqueue(neighbour);
			}
		}

		return finalState;
	}
}
