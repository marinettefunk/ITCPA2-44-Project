/*
 Student Management System - Task 5
*/

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class Student
{
    // Declare student fields
    public string StudentID;
    public string Name;
    public int[] Grades;

    // Static field to track number of students
    public static int studentCount;

    // Static constructor
    static Student()
    {
        studentCount = 0;
    }

    // Instance constructor with validation
    public Student(string id, string name, int[] grades)
    {
        // Validate ID using regex pattern S12345
        if (!Regex.IsMatch(id, @"^S\d{5}$"))
        {
            throw new ArgumentException("Invalid Student ID! Must follow format S12345");
        }

        StudentID = id;
        Name = name;
        Grades = grades;
        studentCount++;
    }

    // Destructor
    ~Student()
    {
        Console.WriteLine($"Student object {StudentID} is being destroyed.");
    }

    // Method to calculate average grade
    public double CalculateAverage()
    {
        if (Grades.Length == 0) return 0;
        double total = 0;
        foreach (int grade in Grades)
        {
            total += grade;
        }
        return total / Grades.Length;
    }

    // Method to display student information in formatted table
    public void DisplayStudentInfoFormatted()
    {
        string gradesStr = Grades.Length > 0 ? string.Join(", ", Grades) : "None";
        Console.WriteLine("{0,-10} {1,-20} {2,-20} {3,10:F2}", StudentID, Name, gradesStr, CalculateAverage());
    }
}

class Program
{
    // Collection to store multiple students
    static List<Student> students = new List<Student>();

    static void Main(string[] args)
    {
        bool exit = false;

        // Main menu loop
        while (!exit)
        {
            Console.WriteLine("STUDENT MANAGEMENT SYSTEM");
            Console.WriteLine("1. Add Student");
            Console.WriteLine("2. Remove Student by ID");
            Console.WriteLine("3. Display All Students");
            Console.WriteLine("4. Search Student by ID");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine() ?? "";  // Null-safe
            Console.WriteLine();

            // Switch statement to handle menu choices
            switch (choice)
            {
                case "1":
                    AddStudent();
                    break;
                case "2":
                    RemoveStudent();
                    break;
                case "3":
                    DisplayAllStudents();
                    break;
                case "4":
                    SearchStudent();
                    break;
                case "5":
                    Console.WriteLine("Exiting program... Goodbye!");
                    exit = true; // Exit the loop and program
                    break;
                default:
                    Console.WriteLine("Invalid option. Try again.\n");
                    break;
            }
        }
    }

    // Method to add a new student with full input validation
    static void AddStudent()
    {
        string id;
        string name;
        int[] grades;

        // Validate Student ID
        while (true)
        {
            Console.Write("Enter Student ID (S12345): ");
            id = Console.ReadLine() ?? "";
            if (Regex.IsMatch(id, @"^S\d{5}$"))
                break;
            else
                Console.WriteLine("Error: Invalid Student ID! Must follow format S12345.");
        }

        // Validate Name (letters and spaces only)
        while (true)
        {
            Console.Write("Enter Name: ");
            name = Console.ReadLine() ?? ""; 
            if (Regex.IsMatch(name, @"^[A-Za-z ]+$"))
                break;
            else
                Console.WriteLine("Error: Name must contain only letters and spaces.");
        }

        // Validate grades (non-negative integers separated by space)
        while (true)
        {
            Console.Write("Enter grades separated by space: ");
            string inputGrades = Console.ReadLine() ?? "";
            string[] gradesInput = inputGrades.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            try
            {
                grades = Array.ConvertAll(gradesInput, s =>
                {
                    if (!int.TryParse(s, out int grade))
                        throw new FormatException($"Invalid grade: '{s}'. Enter only numbers.");
                    if (grade < 0)
                        throw new FormatException($"Invalid grade: '{s}'. Grades cannot be negative.");
                    return grade;
                });
                break; // Exit loop if all grades are valid
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        // Create student and add to collection
        try
        {
            Student newStudent = new Student(id, name, grades);
            students.Add(newStudent);
            Console.WriteLine("Student added successfully!\n");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine("Validation Error: " + ex.Message + "\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Unexpected Error: " + ex.Message + "\n");
        }
    }

    // Method to remove a student by ID
    static void RemoveStudent()
    {
        Console.Write("Enter Student ID to remove: ");
        string id = Console.ReadLine() ?? "";

        Student? studentToRemove = students.Find(s => s.StudentID == id);

        if (studentToRemove != null)
        {
            students.Remove(studentToRemove); // Remove from collection
            Student.studentCount--;
            Console.WriteLine("Student removed successfully!\n");
        }
        else
        {
            Console.WriteLine("Student not found.\n");
        }
    }

    // Method to display all students with total count at the end
    static void DisplayAllStudents()
    {
        if (students.Count == 0)
        {
            Console.WriteLine("No students found.\n");
            return;
        }

        // Display header
        Console.WriteLine("{0,-10} {1,-20} {2,-20} {3,10}", "StudentID", "Name", "Grades", "Average");
        Console.WriteLine(new string('-', 70));

        // Loop through collection and display each student
        foreach (Student s in students)
        {
            s.DisplayStudentInfoFormatted();
        }

        // Display total students once after listing all
        Console.WriteLine($"\nTotal Students so far: {Student.studentCount}\n");
    }

    // Method to search a student by ID
    static void SearchStudent()
    {
        Console.Write("Enter Student ID to search: ");
        string id = Console.ReadLine() ?? "";
        Student? student = students.Find(s => s.StudentID == id);

        if (student != null)
        {
            // Display header
            Console.WriteLine("{0,-10} {1,-20} {2,-20} {3,10}", "StudentID", "Name", "Grades", "Average");
            Console.WriteLine(new string('-', 70));
            student.DisplayStudentInfoFormatted();
        }
        else
        {
            Console.WriteLine("Student not found.\n");
        }
    }
}
