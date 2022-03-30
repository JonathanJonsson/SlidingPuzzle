namespace SlidingPuzzle;

public class Pathfinder
{
	private State finalState;
	private Queue<State> stateQueue = new();
	private List<State> visitedStates = new List<State>();
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
				finalState.PrintCurrentState();
				break;
			}
			
			// foreach (var neighbour in currentState.GetNeighbour())
			// {
			// 	neighbour.PrintCurrentState();
			// 	if (visitedStates.Contains(neighbour))
			// 	{
			// 		continue;
			// 	}
			// 	
			// 	visitedStates.Add(neighbour);
			// 	stateQueue.Enqueue(neighbour);
			//
			// }
		}

		return finalState;
	}
}
