using System;

// Class to validate input
public static class ValidateInput
{
    // Function to validate positive integer input
    public static int GetPositiveInt(string prompt)
    {
        int value; // Variable to store user input
        // Loop until valid input is received
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine();

            if (int.TryParse(input, out value) && value > 0)
            {
                return value;
            }
            else
            {
                Console.WriteLine("Invalid input. Please inter a positive integer.");
            }
        }
    }
    
    // Function to validate character input (yes/no)
    public static char GetValidationChar(string prompt)
    {
        // Loop until valid input is received
        while (true)
        {
            while (true)
    {
        Console.Write(prompt);
        string? input = Console.ReadLine()?.Trim();

        // Make sure input is not null or empty
        if (!string.IsNullOrEmpty(input))
        {
            // Convert first character to lowercase
            char response = char.ToLower(input[0]);

            // Check if it's 'y' or 'n'
            if (response == 'y' || response == 'n')
            {
                return response;
            }
        }

        // Handle invalid input
        Console.WriteLine("Invalid input. Please enter 'y' for yes or 'n' for no.");
    }
        }
    }

}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("LIBRARY BOOK BORROWING TRACKER\n");

        // Prompt user for number of categories
        int category = ValidateInput.GetPositiveInt("Enter the number of categories: ");
        // Prompt user for number of records per category
        int record = ValidateInput.GetPositiveInt("Enter the number of borrow records per category: ");

        // Confirmation and updating of input
        while (true)
        {
            // Show current values
            Console.WriteLine("\nYou entered: ");
            Console.WriteLine($"Categories: {category}");
            Console.WriteLine($"Records per category: {record}");

            // Ask for user confirmation
            char choice = ValidateInput.GetValidationChar("Are these values correct? (y/n): ");

            if (choice == 'y')
            {
                break; // Exit loop if confirmed
            }
            else
            {
                // Ask which value to update
                Console.WriteLine("\nWhich value would you like to change?");
                Console.WriteLine("1. Categories");
                Console.WriteLine("2. Records");

                int inputChoice;
                while (true)
                {
                    inputChoice = ValidateInput.GetPositiveInt("\nEnter your choice (1 or 2): ");
                    if (inputChoice == 1 || inputChoice == 2)
                    {
                        break;
                    }
                    Console.WriteLine("Invalid choice. Please enter 1 or 2.");
                }

                switch (inputChoice)
                {
                    case 1:
                        category = ValidateInput.GetPositiveInt("Enter the new number of categories: ");
                        Console.WriteLine($"Updated Categories: {category}");
                        break;

                    case 2:
                        record = ValidateInput.GetPositiveInt("Enter the new number of borrow records per category: ");
                        Console.WriteLine($"Updated Records: {record}");
                        break;

                    default:
                        // This should never happen because of the validation loop above
                        Console.WriteLine("Invalid choice. No changes made.");
                        break;
                }

                Console.WriteLine("Values updated. Please confirm again.");
            }
        }

        // Final summary
        Console.WriteLine("\nThank you for confirming the following:");
        Console.WriteLine($"Categories: {category}");
        Console.WriteLine($"Records per category: {record}");

        // Create array to store data
        int[,] books = new int[category, record];
        for (int i = 0; i < category; i++)
        {
            Console.WriteLine($"\nEnter the borrowing record for Category {i + 1}:");

            for (int j = 0; j < record; j++)
            {
                // Get validated positive integer for each record
                int result = ValidateInput.GetPositiveInt($"Record {j + 1}: ");
                // Store the value in the array
                books[i, j] = result;
            }
        }

        // Display borrowing records
        Console.WriteLine("\nBorrowing Data");
        for (int i = 0; i < category; i++)
        {
            Console.Write($"Category {i + 1}: ");
            // Print all borrow records on one line
            for (int j = 0; j < record; j++)
            {
                Console.Write(books[i, j] + " ");
            }
            Console.WriteLine();
        }
        // Borrowing analysis message
        Console.WriteLine("\nBorrowing analysis completed.");
    }
}