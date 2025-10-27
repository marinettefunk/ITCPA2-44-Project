using System;

public static class ValidateInput
{
    // Validate positive integers
    public static int GetPositiveInt(string prompt)
    {
        int value;
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
                Console.WriteLine("Invalid input. Please enter a positive integer.");
            }
        }
    }

    // Validate menu input (1–3)
    public static int GetMenuInt(string prompt)
    {
        int value;
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine();
            if (int.TryParse(input, out value) && value >= 1 && value <= 3)
            {
                return value;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter an integer between 1 and 3.");
            }
        }
    }

    // Validate string input
    public static string GetValidString(string prompt)
    {
        string? value;
        while (true)
        {
            Console.Write(prompt);
            value = Console.ReadLine()?.Trim();
            if (!string.IsNullOrEmpty(value))
            {
                return value;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a non-empty string.");
            }
        }
    }

    // Prompt for a hall number that exists, is not full, and display available halls
    public static int GetAvailableHallNumber(string prompt, int numberOfHalls, ConferenceCenter center)
    {
        int hallNumber;
        while (true)
        {
            // Display available halls with free seats
            Console.Write("Available halls: ");
            bool anyAvailable = false;
            for (int i = 0; i < numberOfHalls; i++)
            {
                if (!center.IsHallFull(i))
                {
                    Console.Write($"{i + 1} ");
                    anyAvailable = true;
                }
            }
            if (!anyAvailable)
            {
                Console.WriteLine("\nAll halls are full. Cannot add more attendees.");
                return -1; // Signal that no hall is available
            }
            Console.WriteLine();

            hallNumber = GetPositiveInt(prompt);
            if (hallNumber >= 1 && hallNumber <= numberOfHalls)
            {
                if (!center.IsHallFull(hallNumber - 1))
                {
                    return hallNumber;
                }
                else
                {
                    Console.WriteLine($"Hall {hallNumber} is full. Please choose a different hall.");
                }
            }
            else
            {
                Console.WriteLine($"Invalid hall number. Please enter a number between 1 and {numberOfHalls}.");
            }
        }
    }
}

public class Attendee
{
    // Properties for attendee details
    public string Name;
    public int Age;
    public string InterestArea;

    // Constructor
    public Attendee(string name, int age, string interestArea)
    {
        Name = name;
        Age = age;
        InterestArea = interestArea;
    }

    // Readable string output
    public override string ToString()
    {
        return $"{Name} (Age: {Age}, Interest: {InterestArea})";
    }
}

class AttendeeManager
{
    public static Attendee AddAttendee()
    {
        Console.WriteLine("\nADD NEW ATTENDEE");

        string name = ValidateInput.GetValidString("Enter attendee's name: ");
        int age = ValidateInput.GetPositiveInt("Enter attendee's age: ");
        string interestArea = ValidateInput.GetValidString("Enter area of interest (e.g., AI, Cybersecurity, Robotics): ");

        // Create and return the new Attendee
        Attendee attendee = new Attendee(name, age, interestArea);

        Console.WriteLine("\nAttendee added successfully:");
        Console.WriteLine(attendee);

        return attendee;
    }
}

public class ConferenceCenter
{
    private Attendee[,] halls;
    private int numberOfHalls;
    private int capacityPerHall;

    public ConferenceCenter(int numberOfHalls, int capacityPerHall)
    {
        this.numberOfHalls = numberOfHalls;
        this.capacityPerHall = capacityPerHall;
        halls = new Attendee[numberOfHalls, capacityPerHall];
    }

    public bool AddAttendeeToHall(Attendee attendee, int hallIndex)
    {
        if (hallIndex < 0 || hallIndex >= numberOfHalls)
        {
            Console.WriteLine("Invalid hall number.");
            return false;
        }

        for (int i = 0; i < capacityPerHall; i++)
        {
            if (halls[hallIndex, i] == null)
            {
                halls[hallIndex, i] = attendee;
                Console.WriteLine($"Attendee added to Hall {hallIndex + 1}, Seat {i + 1}");
                return true;
            }
        }

        Console.WriteLine("Hall is full. Could not add attendee.");
        return false;
    }

    // Check if a hall is full
    public bool IsHallFull(int hallIndex)
    {
        for (int i = 0; i < capacityPerHall; i++)
        {
            if (halls[hallIndex, i] == null) return false;
        }
        return true;
    }

    public void DisplayAttendees()
    {
        Console.WriteLine("\nATTENDEE LIST");

        for (int h = 0; h < numberOfHalls; h++)
        {
            Console.WriteLine($"\nHall {h + 1}:");
            bool empty = true;

            for (int i = 0; i < capacityPerHall; i++)
            {
                if (halls[h, i] != null)
                {
                    Console.WriteLine($"  Seat {i + 1}: {halls[h, i]}");
                    empty = false;
                }
            }

            if (empty)
            {
                Console.WriteLine("  (No attendees registered yet)");
            }
        }
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("TECHCON GLOBAL ATTENDEE MANAGEMENT SYSTEM");

        ConferenceCenter conferenceCenter = new ConferenceCenter(3, 4); // 3 halls, 4 seats each

        while (true)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Add an Attendee");
            Console.WriteLine("2. Display Attendees");
            Console.WriteLine("3. Exit");

            int menuChoice = ValidateInput.GetMenuInt("Enter your choice: ");

            switch (menuChoice)
            {
                case 1:
                    Attendee newAttendee = AttendeeManager.AddAttendee();
                    int hallIndex = ValidateInput.GetAvailableHallNumber("Enter hall number (1–3): ", 3, conferenceCenter) - 1;
                    if (hallIndex != -1) // Only add if a hall is available
                    {
                        conferenceCenter.AddAttendeeToHall(newAttendee, hallIndex);
                    }
                    break;

                case 2:
                    conferenceCenter.DisplayAttendees();
                    break;

                case 3:
                    Console.WriteLine("Exiting program. Goodbye!");
                    return;
            }
        }
    }
}