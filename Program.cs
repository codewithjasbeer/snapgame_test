namespace SnapTest                   
{
    class Program                    
    {
        // Random number generator for shuffling and snap winner
        static Random randomNum = new Random(); 

        // Enum for the types of matching rules
        enum MatchType { Face, Suit, Both }

        class Card
        {
            public string Face { get; }   // Face value (A, 2, ..., K)
            public string Suit { get; }   // Suit (Hearts, Diamonds, etc.)

            // Card constructor
            public Card(string face, string suit) 
            {
                Face = face;
                Suit = suit;
            }

            // String representation of the card
            public override string ToString()     
            {
                return $"{Face} of {Suit}";
            }
        }

        // Function to create a deck (or multiple decks) of cards
        static List<Card> CreateDeck(int numPacks)
        {
            // Face - Ace, numbers and Others
            var faces = new[] { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
            var suits = new[] { "Hearts", "Diamonds", "Clubs", "Spades" }; // All suits
            var deck = new List<Card>();

            for (int n = 0; n < numPacks; n++) // For each pack
            {
                foreach (var face in faces)      // For each face value
                    foreach (var suit in suits)  // For each suit
                        deck.Add(new Card(face, suit)); // Add card to deck
            }
            return deck; // Return the complete deck
        }

        // Reshuffle using Random generator
        static void Shuffle<T>(IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = randomNum.Next(n + 1); // Pick a random index
                T value = list[k];
                list[k] = list[n]; // Swap
                list[n] = value;
            }
        }

        // Function to check if two cards match according to the selected rule
        static bool IsMatch(Card a, Card b, MatchType matchType)
        {
            switch (matchType)
            {
                case MatchType.Face: return a.Face == b.Face; // Match by face value
                case MatchType.Suit: return a.Suit == b.Suit; // Match by suit
                case MatchType.Both: return a.Face == b.Face && a.Suit == b.Suit; // Match by both
                default: return false; // Should not happen
            }
        }

        // Function to prompt the user for the matching rule
        static MatchType MatchCardType()
        {
            Console.WriteLine("Select matching condition:");
            Console.WriteLine("1. Face value (e.g., 7 of Spades and 7 of Diamonds)");
            Console.WriteLine("2. Suit (e.g., 3 of Diamonds and 9 of Diamonds)");
            Console.WriteLine("3. Both (e.g., Q of Hearts and Q of Hearts)");
            while (true)
            {
                Console.Write("Enter 1, 2, or 3: "); 
                string input = Console.ReadLine();
                if (input == "1") return MatchType.Face; 
                if (input == "2") return MatchType.Suit;
                if (input == "3") return MatchType.Both; 
                Console.WriteLine("Invalid input. Try again.");            }
            }

        // Function to prompt user for number of packs
        static int NumPacks()
        {
            while (true)
            {
                // Ask for number of packs
                Console.Write("Enter number of packs to use (N): ");
                // Parse and check > 0
                if (int.TryParse(Console.ReadLine(), out int n) && n > 0) 
                    return n; // Valid input
                // Invalid input, repeat
                Console.WriteLine("Invalid input. Please enter a positive integer."); 
            }
        }

        // Main function where the game runs
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Snap! Card Game");

            //Prompt for number of packs and match type
            int numPacks = NumPacks();
            MatchType matchType = MatchCardType(); 

            var pile = CreateDeck(numPacks); // Create and combine decks
            // Shuffle the cards
            Shuffle(pile); 

            int player1Cards = 0; // Counter for cards won by player 1
            int player2Cards = 0; // Counter for cards won by player 2
            List<Card> playedCards = new List<Card>(); // Cards played since last snap

            Card previousCard = null; // Track previous card for matching

            while (pile.Count > 0) // While there are cards left in the pile
            {
                Card currentCard = pile[0]; // Take the top card
                pile.RemoveAt(0);           // Remove it from the pile
                playedCards.Add(currentCard); // Add to played cards

                if (previousCard != null && IsMatch(previousCard, currentCard, matchType)) // Check for match
                {
                    // Snap
                    int winner = randomNum.Next(2) + 1; // Randomly pick 1 or 2
                    Console.WriteLine($"SNAP! {previousCard} and {currentCard} match. Player {winner} wins {playedCards.Count} cards.");
                    if (winner == 1)
                        player1Cards += playedCards.Count; // Give cards to player 1
                    else
                        player2Cards += playedCards.Count; // Give cards to player 2
                    playedCards.Clear(); // Reset played cards
                    previousCard = null; // Reset previous card
                }
                else
                {
                    previousCard = currentCard; // Update previous card
                }
            }

            // If there are any cards left in play that weren't won, discard them
            if (playedCards.Count > 0)
                Console.WriteLine($"{playedCards.Count} cards remaining in play are discarded.");

            // Show results
            Console.WriteLine($"\nRESULTS:");
            Console.WriteLine($"Player 1: {player1Cards} cards");
            Console.WriteLine($"Player 2: {player2Cards} cards");
            if (player1Cards > player2Cards)
                Console.WriteLine("Player 1 wins!");
            else if (player2Cards > player1Cards)
                Console.WriteLine("Player 2 wins!");
            else
                Console.WriteLine("It's a draw!");

            Console.WriteLine("\nPress Enter to exit."); // Wait for user before closing
            Console.ReadLine();
        }
    }
}