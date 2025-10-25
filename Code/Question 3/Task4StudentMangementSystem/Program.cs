/* 
 Student Management System - Task 4
 ---------------------------------
 Features implemented:
 - Stores multiple students in a List<Student>
 - Add a new student
 - Remove a student by ID
 - Display all students
 - Search for a student by ID
 - Uses loops and control statements (switch, foreach)
*/

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class Student
{
    public string StudentID;
    public string Name;
    public int[] Grades;
    public static int StudentCount;

    // Static constructor runs once
    static Student() => StudentCount = 0;

    // Instance constructor with ID validation
    public Student(string id, string name, int[] grades)
    {
        try
        {
            if (!Regex.IsMatch(id, @"^S\d{5}$"))
                throw new Exception("Invalid Student ID! Must be in format: S12345");

            StudentID = id;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            StudentID = "INVALID";
        }

        Name = name;
        Grades = grades;
        StudentCount++;
    }

    // Calculate average grade
    public double CalculateAverage()
    {
        if (Grades.Length == 0) return 0;
        double total = 0;
        foreach (int mark in Grades) total += mark;
        return total / Grades.Length;
    }

    // Display student details
    public void DisplayStudent()
    {
        Console.WriteLine($"ID: {StudentID}; Name: {Name}; Grades: {string.Join(", ", Grades)}; Average: {CalculateAverage():F2}");
    }
}

class Program
{
    // Collection to store students
    static List<Student> students = new List<Student>();

    static void Main(string[] args)
    {
        // Add sample students
        students.Add(new Student("S10001", "Jane Doe", new int[] { 85, 90, 78 }));
        students.Add(new Student("S10002", "John Smith", new int[] { 70, 65, 80 }));

        int choice;

        do
        {
            // Menu for user
            Console.WriteLine("\n===== STUDENT MANAGEMENT MENU =====");
            Console.WriteLine("1. Add Student");
            Console.WriteLine("2. Remove Student by ID");
            Console.WriteLine("3. Search Student by ID");
            Console.WriteLine("4. Display All Students");
            Console.WriteLine("5. Exit");
            Console.Write("Enter choice: ");

            // Read user input and validate
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid input. Enter a number between 1-5.");
                continue;
            }

            // Perform action based on choice
            switch (choice)
            {
                case 1:
                    AddStudent();
                    break;
                case 2:
                    RemoveStudent();
                    break;
                case 3:
                    SearchStudent();
                    break;
                case 4:
                    DisplayAllStudents();
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

    // Add a new student
    static void AddStudent()
    {
        Console.Write("Enter Student ID (S12345): ");
        string id = Console.ReadLine();
        Console.Write("Enter Name: ");
        string name = Console.ReadLine();
        Console.Write("Enter Grades separated by commas: ");
        string[] gradesInput = Console.ReadLine().Split(',');
        List<int> gradeList = new List<int>();

        foreach (string g in gradesInput)
            if (int.TryParse(g.Trim(), out int grade)) gradeList.Add(grade);

        students.Add(new Student(id, name, gradeList.ToArray()));
        Console.WriteLine("Student added successfully!");
    }

    // Remove a student by ID
    static void RemoveStudent()
    {
        Console.Write("Enter Student ID to remove: ");
        string id = Console.ReadLine();
        Student student = students.Find(s => s.StudentID == id);

        if (student != null)
        {
            students.Remove(student);
            Console.WriteLine("Student removed successfully.");
        }
        else
        {
            Console.WriteLine("Student not found.");
        }
    }

    // Search a student by ID
    static void SearchStudent()
    {
        Console.Write("Enter Student ID to search: ");
        string id = Console.ReadLine();
        Student student = students.Find(s => s.StudentID == id);

        if (student != null)
        {
            Console.WriteLine("\n--- STUDENT FOUND ---");
            student.DisplayStudent();
        }
        else
        {
            Console.WriteLine("Student not found.");
        }
    }

    // Display all students
    static void DisplayAllStudents()
    {
        Console.WriteLine("\n--- ALL STUDENTS ---");
        foreach (Student s in students)
        {
            s.DisplayStudent();
        }
    }
}
