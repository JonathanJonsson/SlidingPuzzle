
using SlidingPuzzle;

State startState = new();

 startState.PrintTargetState();
startState.PrintCurrentState();
Pathfinder pathfinder = new Pathfinder();

var finalState = pathfinder.CalculateRoute();

finalState.PrintCurrentState();

var path = finalState.ReturnPath();

foreach (var state in path)
{
 Console.WriteLine(state);
}


