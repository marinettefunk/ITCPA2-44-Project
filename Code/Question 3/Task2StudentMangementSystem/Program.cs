/* 
 Student Management System - Task 2
 ---------------------------------
 This task includes:
 - A Student class with fields for Student ID, Name, and Grades.
 - A static constructor that tracks how many Student objects are created.
 - An instance constructor to initialize student data.
 - Creation of at least two Student objects in the Main method.
 - Output to display student information and total number of students.
*/

using System;

class Student
    {
        public string StudentID;
        public string Name;
        public int[] Grades;

        // Static field to track number of students
        public static int StudentCount;

        // Static constructor (runs once)
        static Student()
        {
            StudentCount = 0;
        }

        // Instance constructor to initialize fields
        public Student(string id, string name, int[] grades)
        {
            StudentID = id;
            Name = name;
            Grades = grades;

            StudentCount++; // Track number of created students
        }
    }

class Program
    {
        static void Main(string[] args)
        {
            // Create 2 student objects
            Student student1 = new Student("S10001", "Jane Doe", new int[] { 85, 90, 78 });
            Student student2 = new Student("S10002", "John Doe", new int[] { 70, 65, 80 });

            // Print the student details
            Console.WriteLine("\nSTUDENT DETAILS");
            Console.WriteLine($"ID: {student1.StudentID}, Name: {student1.Name}, Grades: {string.Join(", ", student1.Grades)}");
            Console.WriteLine($"ID: {student2.StudentID}, Name: {student2.Name}, Grades: {string.Join(", ", student2.Grades)}");

            // Display total students
            Console.WriteLine("\nStudents created successfully!");
            Console.WriteLine($"Total number of students: {Student.StudentCount}");
        }
    }