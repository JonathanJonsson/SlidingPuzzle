
using SlidingPuzzle;

State startState = new();

 startState.PrintTargetState();
startState.PrintCurrentState();
var pathfinder = new Pathfinder();

var finalState = pathfinder.CalculateRoute(startState);

finalState.PrintCurrentState();
//
// var path = finalState.ReturnPath();
//
// foreach (var state in path)
// {
//  Console.WriteLine(state);
// }


