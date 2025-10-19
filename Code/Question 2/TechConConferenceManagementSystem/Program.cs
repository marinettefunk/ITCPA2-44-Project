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
}

class Attendee
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
        Console.WriteLine("\n--- Add New Attendee ---");

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

class ConferenceCenter
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
        Console.WriteLine("TECHCON GLOBAL ATTENDEE MANAGEMENT SYSTEM\n");

        ConferenceCenter conferenceCenter = new ConferenceCenter(3, 4); // 3 halls, 4 seats each

        while (true)
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Add an Attendee");
            Console.WriteLine("2. Display Attendees");
            Console.WriteLine("3. Exit");

            int menuChoice = ValidateInput.GetMenuInt("Enter your choice: ");

            switch (menuChoice)
            {
                case 1:
                    Attendee newAttendee = AttendeeManager.AddAttendee();
                    int hallIndex = ValidateInput.GetPositiveInt("Enter hall number (1–3): ") - 1;
                    conferenceCenter.AddAttendeeToHall(newAttendee, hallIndex);
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