# Snap! Card Game Simulation
This is a simple C# console program that simulates a two-player game of Snap! using one or more standard decks of playing cards.

# How to Run
Requirements:

.NET 8 SDK (or later)
Setup:

Create a new console project (dotnet new console -n Snap_test)
Replace the contents of Program.cs with the code provided.
Run the Game:

Open a terminal in the project folder.
Type dotnet run and press Enter.
or you can run the application using F5

# How to Play
When prompted, enter:
The number of card packs to use.
The matching condition (face value, suit, or both).
The program shuffles all cards and simulates the game.
When two consecutive cards match the chosen condition, a random player "snaps" and wins those cards.
At the end, the program shows how many cards each player won and announces the winner.

# Notes
Only standard playing card faces and suits are used.
The game is fully automated and runs in the console.
No external libraries are required.
