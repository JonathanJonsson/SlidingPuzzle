namespace SlidingPuzzle;

public class Pathfinder
{
	private State finalState = new();
	private Queue<State> stateQueue = new();
	private HashSet<State> visitedStates = new HashSet<State>();
	public State CalculateRoute(State startState)
	{
		stateQueue.Enqueue(startState);
		while (stateQueue.Count > 0)
		{
			var currentState = stateQueue.Dequeue();


			if (currentState.IsEndNode())
			{
				finalState = currentState;
				Console.WriteLine("Found final state: ");
				finalState.PrintState();
				break;
			}
			
			foreach (var neighbour in currentState.GetNeighbour())
			{
				
				// if (currentState.PreviousState.Equals(neighbour)) // can one do this comparison?
				// {
				// 	continue;
				// }
				
				if (visitedStates.Contains(neighbour))
				{
					continue;
				}
				
				neighbour.PrintState();

				visitedStates.Add(neighbour);
				stateQueue.Enqueue(neighbour);
			
			}
		}

		return finalState;
	}
}
