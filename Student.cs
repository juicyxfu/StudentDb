// Alexandria D'Oro-Gilbert
// T INFO 200A, Student Database
///////////////////////////////////////////////////////////////////////////////////////////////////
// CHANGE HISTORY
// DATE         DEVELOPER                   CHANGE HISTORY
// 2025-02-13   Alexandria D'Oro-Gilbert    Create new Student class. Defined student, defined ways
//                                          to create a student, defined ways to print student.
// 2025-02-20   Alexandria D'Oro-Gilbert    Create ToStringForOutputFile() to print student information
//                                          to an output file.
// 02-27-2025   Alexandria D'Oro-Gilbert    Create new Student class. Change ToStringOutputFile to
//                                          virtual string method. Format the GPA with 'F1'.
//                                          

using System.Runtime.CompilerServices;

namespace StudentDb
{
    internal class Student  // : object
    {
        public string FirstMidName { get; set; }
        public string LastName { get; set; }
        public double GradePointAvg { get; set; }
        public string EmailAddress { get; set; }

        // constructor
        // full specified constructor definition
        public Student(string first, string last, double gpa, string email)
        {
            FirstMidName = first;
            LastName = last;
            GradePointAvg = gpa;
            EmailAddress = email;
        }

        // create new instance of student
        public Student() {}

        // need an easy way to output student object data
        public override string ToString()
        {
            string str = "********** Student Record **********\n";
            str += $"First: {FirstMidName}\n";
            str += $" Last: {LastName}\n";
            str += $"  GPA: {GradePointAvg:F1}\n";
            str += $"Email: {EmailAddress}\n";

            return str;
        }

        // need an easy way to output student object data ot the data file for the persistence
        // storing the data between instances of the program execution
        public virtual string ToStringForOutputFile()
        {
            string str = "********** Student Record **********\n";
            str += $"{FirstMidName}\n";
            str += $"{LastName}\n";
            str += $"{GradePointAvg:F1}\n";
            str += $"{EmailAddress}\n";

            return str;
        }
    }
}