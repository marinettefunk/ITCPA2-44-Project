using System;

// Class to run main program
class Program
{
    static void Main()
    {
        // Welcome message
        Console.WriteLine("Techcon Global Attendee Managemnet System");
        Console.WriteLine();
    }

    public static void Menu()
    {
        Console.WriteLine("1. Add an Attendee");
        Console.WriteLine("2. Display Attendees");
        Console.WriteLine("3. Exit");
    }

}

// Class to validate input
public static class ValidateInput
{
    public static int GetMenuInt (string prompt)
    {
        int result; // Variable to hold the converted integer

        while (true)
        {
            Console.Write(prompt); // Ask user for input
            string? input = Console.ReadLine(); // Read user input from console (nullable string to handle Enter without typing anything)

            /* 
             * Try to convert (parse) the input string to an integer.
             * - int.TryParse() returns true if conversion succeeds.
             * - If successful, the parsed value is stored in 'result'.
             * - Check that result > 0 and <=3 to ensure it's positive and a valid input for the menu.
             */
            if (int.TryParse(input, out result) && result > 0 && result <= 3)
            {
                // If input is valid (a positive integer), return it to the caller
                return result;
            }
            else
            {
                // If input was invalid (not a number or <= 0), show an error and repeat loop
                Console.WriteLine("Invalid input: Please enter a positive whole number from 1-3.");
            }
        }
    }
}

// Class to represent each attendee
class Attendee
{

}

// Class to manage attendee data in conference halls
class ConferenceCenter
{

}