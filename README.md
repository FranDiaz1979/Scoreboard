Technical Test with only a library made with KISS, TDD and Clean Code and a simple console app for deal with the library 
(without data storage, webapi, etc.)

The entire description of the requirements is in the .pdf file


Developer Notes:
----------------
I've used TDD (tests first), in this case, integration tests.

DDD:
There are 2 Domain Models: Score and ScoreBoard
ScoreBoard is a scores list, without scoreBoard model the code would be easier,
but I like to apply the DDD logical for create models, this is the reason of the ScoreBoard existence.

CheckOnlyOneGoal
    The difference between the last and the new score must be of only one goal
    It's allowed 1 negative goal because in case the client makes a mistake, he can rectify.
    It's allowed a null difference because the client can send n times the same petition.

Clean Code:
In the methods CheckOnlyOneGoal and CheckNotExist, the Clean Code says that his code can be abstracted, 
but I don't like too much anidation.
Console.WriteLine(); can be changed by "\n", but I this this way the code is more readable.
Menu is too long and I could separate the visualization and the logical, but I like this way
