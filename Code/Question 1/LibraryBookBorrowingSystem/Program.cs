using System;

// Class running main program
class Program
{
    static void Main()
    {
        // Welcome message
        Console.WriteLine("Library Book Borrowing Tracker\n");

        // Prompt user to enter the number of categories (validated input)
        int categories = InputHelper.GetPositiveInt("Enter the number of categories: ");
        // Prompt user to enter number of borrow records per category (validated input)
        int borrow = InputHelper.GetPositiveInt("Enter the number of borrow records per category: ");
        Console.WriteLine(); // Blank line

        // Create 2D array to store borrow data
        int[,] records = new int[categories, borrow];

        // Input borrowing data
        for (int i = 0; i < categories; i++) // Loop through categories
        {
            Console.WriteLine($"Enter borrowing records for Category {i + 1}:");

            for (int j = 0; j < borrow; j++) // Loop through records
            {
                // Ask how many books were borrowed for this record
                records[i, j] = InputHelper.GetPositiveInt($"   Record {j + 1}: ");
            }

            Console.WriteLine(); // Blank line after each category for spacing
        }

        // Display borrowing records

        Console.WriteLine("Borrowing Data");
        for (int i = 0; i < categories; i++)
        {
            // Start category line
            Console.Write($"Category {i + 1}: ");

            // Print all borrow records on one line
            for (int j = 0; j < borrow; j++)
            {
                Console.Write(records[i, j] + " "); // just the number
            }

            Console.WriteLine(); // move to next line after category
        }


        // Borrowing analysis print message
        Console.WriteLine();
        Console.WriteLine("Borrowing analysis completed.");
    }
}

// Class for input validation
public static class InputHelper
{
    // A method that asks the user for input and only returns when they enter a valid positive integer
    public static int GetPositiveInt(string prompt)
    {
        int result; // variable to hold the converted integer

        // Loop to continue prompting until user enters valid input
        while (true)
        {
            // Display the prompt message
            Console.Write(prompt);

            // Read user input from console (nullable string to handle Enter without typing anything)
            string? input = Console.ReadLine();

            /* 
             * Try to convert (parse) the input string to an integer.
             * - int.TryParse() returns true if conversion succeeds.
             * - If successful, the parsed value is stored in 'result'.
             * - Check that result > 0 to ensure it's positive.
             */
            if (int.TryParse(input, out result) && result > 0)
            {
                // If input is valid (a positive integer), return it to the caller
                return result;
            }
            else
            {
                // If input was invalid (not a number or <= 0), show an error and repeat loop
                Console.WriteLine("Invalid input: Please enter a positive whole number.");
            }
        }
    }
}
