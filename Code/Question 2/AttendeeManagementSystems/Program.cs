using System;

// Class to run main program
class Program
{
    static void Main()
    {
        // Welcome message
        Console.WriteLine("Techcon Global Attendee Managemnet System");
        Menu();
    }

    public static void Menu()
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("1. Add an Attendee");
            Console.WriteLine("2. Display Attendees");
            Console.WriteLine("3. Exit");

            int choice = ValidateInput.GetPositiveInt("Enter your choice: ");

            switch (choice)
            {
                case 1:
                    Console.WriteLine("Adding a new attendee...");
                    AddAttendee(); // Call the method to add an attendee
                    break;
                case 2:
                    Console.WriteLine("Displaying attendees...");
                    conference.DisplayAttendees(); // Show attendees per hall
                    break;
                case 3:
                    Console.WriteLine("Goodbye!");
                    return; // exits the loop and returns to Main (ends program)
                default:
                    Console.WriteLine("Invalid input: Please enter a positive whole number from 1-3.");
                    break;
            }
        }
    }

    // Create one ConferenceCenter object shared by the program
    static ConferenceCenter conference = new ConferenceCenter(3, 4); // 3 halls, each with capacity 4

    // Method to handle adding an attendee
    static void AddAttendee()
    {
        Console.WriteLine("\n--- Add New Attendee ---");

        // Ask for attendee's name
        Console.Write("Enter attendee's name: ");
        string name = Console.ReadLine() ?? "Unknown";

        // Ask for attendee's age (use input validation)
        int age = ValidateInput.GetPositiveInt("Enter attendee's age: ");

        // Ask for area of interest
        Console.Write("Enter area of interest (e.g., AI, Cybersecurity, Robotics): ");
        string interestArea = Console.ReadLine() ?? "General";

        // Create the new Attendee object
        Attendee attendee = new Attendee(name, age, interestArea);

        // Keep asking for a valid hall number until attendee is successfully added
        while (true)
        {
            int hallNumber = ValidateInput.GetPositiveInt("Enter hall number (1–3): ");

            // Check that hall number is within range
            if (hallNumber < 1 || hallNumber > 3)
            {
                Console.WriteLine("Invalid hall number. Please enter a number between 1 and 3.");
                continue; // Ask again
            }

            // Try to add the attendee to the chosen hall
            bool added = conference.AddAttendeeToHall(attendee, hallNumber - 1);

            if (added)
            {
                // Successfully added — break out of loop
                Console.WriteLine($"Attendee '{name}' successfully added to Hall {hallNumber}.");
                break;
            }
            else
            {
                // Hall is full — ask for another hall number again
                Console.WriteLine($"Hall {hallNumber} is full. Please choose another hall.");
            }
        }
    }
}

// Class to validate input
public static class ValidateInput
{
    public static int GetPositiveInt(string prompt)
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

// Class to represent each attendee
class Attendee
{
    // Properties hold attendee details
    public string Name { get; set; }
    public int Age { get; set; }
    public string InterestArea { get; set; }

    // Constructor to set up new attendee with name, age, and interest
    public Attendee(string name, int age, string interestArea)
    {
        Name = name;
        Age = age;
        InterestArea = interestArea;
    }

    // Method that returns a readable string version of an attendee
    public override string ToString()
    {
        return $"{Name} (Age: {Age}, Interest: {InterestArea})";
    }
}

// Class to manage attendee data in conference halls
class ConferenceCenter
{
    // 2D array to store attendees by hall and seat
    private Attendee[,] halls;

    // Variables to store how many halls and how many seats per hall
    private int numberOfHalls;
    private int capacityPerHall;

    // Constructor creates the halls array and initializes sizes
    public ConferenceCenter(int numberOfHalls, int capacityPerHall)
    {
        this.numberOfHalls = numberOfHalls;
        this.capacityPerHall = capacityPerHall;

        // Initialize 2D array for halls
        halls = new Attendee[numberOfHalls, capacityPerHall];
    }

    // Method to add attendee to a specific hall
    public bool AddAttendeeToHall(Attendee attendee, int hallIndex)
    {
        // Check if hall index is valid
        if (hallIndex < 0 || hallIndex >= numberOfHalls)
        {
            Console.WriteLine("Invalid hall number.");
            return false;
        }

        // Loop through seats to find an empty one
        for (int i = 0; i < capacityPerHall; i++)
        {
            if (halls[hallIndex, i] == null) // Empty seat
            {
                halls[hallIndex, i] = attendee; // Place attendee here
                return true; // Success
            }
        }

        // If no empty seat found, hall is full
        return false;
    }

    // Method to display all attendees in all halls
    public void DisplayAttendees()
    {
        Console.WriteLine("\n--- Attendee List by Hall ---");

        for (int h = 0; h < numberOfHalls; h++)
        {
            Console.WriteLine($"\nHall {h + 1}:");
            bool empty = true;

            for (int i = 0; i < capacityPerHall; i++)
            {
                if (halls[h, i] != null)
                {
                    // Display each attendee in the hall
                    Console.WriteLine($"  Seat {i + 1}: {halls[h, i]}");
                    empty = false;
                }
            }

            // If no attendees found, show message
            if (empty)
            {
                Console.WriteLine("  (No attendees registered yet)");
            }
        }
    }
}
