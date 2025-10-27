/*
 Student Management System - Task 2
*/

using System;

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

    // Instance constructor
    public Student(string id, string name, int[] grades)
    {
        StudentID = id;
        Name = name;
        Grades = grades;
        studentCount++;
    }

    // Method to display student information
    public void DisplayStudentInfo()
    {
        Console.WriteLine($"Student ID: {StudentID}");
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine("Grades: " + string.Join(", ", Grades));
        Console.WriteLine();
    }

}

class Program()
{
    static void Main(string[] args)
    {
        // Create two Student objects
        Student student1 = new Student("S001", "John Doe", new int[] { 85, 90, 82 });
        Student student2 = new Student("S002", "Jane Doe", new int[] { 78, 88, 91 });

        // Display info
        Console.WriteLine("STUDENT INFORMATION\n");
        student1.DisplayStudentInfo();
        student2.DisplayStudentInfo();

        // Show number of students created
        Console.WriteLine($"Total Students: {Student.studentCount}");
    }
}