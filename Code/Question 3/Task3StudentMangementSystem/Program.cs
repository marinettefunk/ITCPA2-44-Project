/*
 Student Management System - Task 3
*/

using System;
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
            throw new ArgumentException("Invalid Student ID! Must follow format: S12345");
        }

        StudentID = id;
        Name = name;
        Grades = grades;
        studentCount++;
    }

    // Method to calculate average grade
    public double CalculateAverage()
    {
        double total = 0;
        foreach (int grade in Grades)
        {
            total += grade;
        }
        return total / Grades.Length;
    }

    // Method to display student information
    public void DisplayStudentInfo()
    {
        Console.WriteLine($"Student ID: {StudentID}");
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine("Grades: " + string.Join(", ", Grades));
        Console.WriteLine($"Average Grade: {CalculateAverage():F2}");
        Console.WriteLine();
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("STUDENT INFORMATION\n");

        try
        {
            Student student1 = new Student("S12345", "John Doe", new int[] { 85, 90, 82 });
            student1.DisplayStudentInfo();

            // Intentionally incorrect ID to test validation and error handling
            Student student2 = new Student("12345", "Jane Doe", new int[] { 78, 88, 91 });
            student2.DisplayStudentInfo();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }

        Console.WriteLine($"Total Students: {Student.studentCount}");
    }
}