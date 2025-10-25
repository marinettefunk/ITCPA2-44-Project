using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class Student
{
    public string StudentID;
    public string Name;
    public int[] Grades;
    public static int StudentCount;

    static Student() => StudentCount = 0;

    public Student(string id, string name, int[] grades)
    {
        try
        {
            if (!Regex.IsMatch(id, @"^S\d{5}$"))
                throw new Exception("Error: Invalid Student ID format.");

            StudentID = id;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            StudentID = "INVALID";
        }

        Name = name;
        Grades = grades ?? new int[0];
        StudentCount++;
    }

    public double CalculateAverage()
    {
        if (Grades.Length == 0) return 0;
        double total = 0;
        foreach (int g in Grades) total += g;
        return total / Grades.Length;
    }

    public void DisplayStudent()
    {
        Console.WriteLine($"ID: {StudentID} | Name: {Name} | Average: {CalculateAverage():F2}");
    }

    ~Student()
    {
        Console.WriteLine($"Student object '{Name}' (ID: {StudentID}) is destroyed.");
    }
}

class Program
{
    static List<Student> students = new List<Student>();

    static void Main()
    {
        Console.WriteLine("Student Management System Initialized.\n");

        int choice;
        do
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Add Student");
            Console.WriteLine("2. Remove Student");
            Console.WriteLine("3. Display All Students");
            Console.WriteLine("4. Search for Student");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");

            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid input. Enter a number between 1-5.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    AddStudent();
                    break;
                case 2:
                    RemoveStudent();
                    break;
                case 3:
                    DisplayAllStudents();
                    break;
                case 4:
                    SearchStudent();
                    break;
                case 5:
                    Console.WriteLine("Exiting program...");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Select 1-5.");
                    break;
            }

        } while (choice != 5);
    }

    static void AddStudent()
    {
        Console.Write("Enter Student ID (Format S12345): ");
        string? id = Console.ReadLine();

        if (!Regex.IsMatch(id, @"^S\d{5}$"))
        {
            Console.WriteLine("Error: Invalid Student ID format.");
            return;
        }

        Console.Write("Enter Name: ");
        string? name = Console.ReadLine();

        Console.Write("How many grades? ");
        if (!int.TryParse(Console.ReadLine(), out int numGrades) || numGrades < 0)
        {
            Console.WriteLine("Invalid number of grades.");
            return;
        }

        int[] grades = new int[numGrades];
        for (int i = 0; i < numGrades; i++)
        {
            Console.Write($"Enter grade {i + 1}: ");
            if (!int.TryParse(Console.ReadLine(), out grades[i]))
            {
                Console.WriteLine("Invalid grade input.");
                return;
            }
        }

        students.Add(new Student(id, name, grades));
        Console.WriteLine("Student added successfully.\n");
    }

    static void RemoveStudent()
    {
        Console.Write("Enter Student ID to remove: ");
        string? id = Console.ReadLine();
        Student? student = students.Find(s => s.StudentID == id);

        if (student != null)
        {
            students.Remove(student);
            Console.WriteLine("Student removed successfully.\n");
        }
        else
        {
            Console.WriteLine("Student not found.\n");
        }
    }

    static void SearchStudent()
    {
        Console.Write("Enter Student ID to search: ");
        string? id = Console.ReadLine();
        Student? student = students.Find(s => s.StudentID == id);

        if (student != null)
        {
            Console.WriteLine("\n--- Student List ---");
            student.DisplayStudent();
            Console.WriteLine();
        }
        else
        {
            Console.WriteLine("Student not found.\n");
        }
    }

    static void DisplayAllStudents()
    {
        Console.WriteLine("\n--- Student List ---");
        foreach (Student s in students)
        {
            s.DisplayStudent();
        }
        Console.WriteLine();
    }
}
