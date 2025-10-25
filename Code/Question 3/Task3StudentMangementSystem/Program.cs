/* 
 Student Management System - Task 3
 ---------------------------------
 This version includes:
 - Student average grade calculation method
 - Regular Expression for validating Student ID format
 - try-catch block to handle invalid Student ID errors
 - Displays student details and average score
*/

using System;
using System.Text.RegularExpressions; // Required for Student ID validation

class Student
{
    // Fields that store student details
    public string StudentID;
    public string Name;
    public int[] Grades;

    // Static field to keep track of total students created
    public static int StudentCount;

    // Static constructor (runs once before any student objects are created)
    static Student()
    {
        StudentCount = 0;
    }

    // Constructor to initialize each student object
    public Student(string id, string name, int[] grades)
    {
        try
        {
            // Check if the ID matches format: S followed by 5 digits, e.g. S12345
            if (!Regex.IsMatch(id, @"^S\d{5}$"))
            {
                throw new Exception("Invalid Student ID! Must be in format: S12345");
            }

            StudentID = id; // If valid, set ID
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message); // Display error message
            StudentID = "INVALID"; // Assign default value if invalid
        }

        Name = name;   // Set student name
        Grades = grades; // Set grade list

        StudentCount++; // Count the new student object
    }

    // Method to calculate average grade
    public double CalculateAverage()
    {
        double total = 0;

        // Add all grades together
        foreach (int mark in Grades)
        {
            total += mark;
        }

        return total / Grades.Length; // Return average score
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Create two student objects
        Student student1 = new Student("S10001", "Jane Doe", new int[] { 85, 90, 78 });
        Student student2 = new Student("SABCDE", "John Doe", new int[] { 70, 65, 80 }); // Invalid ID (for testing)

        // Display student information + average marks
        Console.WriteLine("\nSTUDENT DETAILS");
        Console.WriteLine($"ID: {student1.StudentID}; Name: {student1.Name}; Grades: {string.Join(", ", student1.Grades)}; Average: {student1.CalculateAverage():F2}");
        Console.WriteLine($"ID: {student2.StudentID}; Name: {student2.Name}, Grades: {string.Join(", ", student2.Grades)}; Average: {student2.CalculateAverage():F2}");

        // Show total number of student objects created
        Console.WriteLine($"\nTotal Students: {Student.StudentCount}");
    }
}
