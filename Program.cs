// Alexandria D'Oro-Gilbert
// T INFO 200A, Student Database
///////////////////////////////////////////////////////////////////////////////////////////////////
// CHANGE HISTORY
// DATE         DEVELOPER                   CHANGE HISTORY
// 2025-02-13   Alexandria D'Oro-Gilbert    Create new student instances.
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDb
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //MainTestDriver();

            // using the singlton pattern
            // DB application will be a single object
            DbApp app = new DbApp();
            app.DbAppTestDriver();
        }
        // for now this is a test driver
        // later on this will be a launching point for application
        static void MainTestDriver()
        {
            // object creation, no args
            Student stu1 = new Student();
            Student stu2 = new Student();
            Student stu3 = new Student();

            // does not scale well
            stu1.FirstMidName = "Alice Amy";
            stu1.LastName = "Anderson";
            stu1.GradePointAvg = 4.0;
            stu1.EmailAddress = "aanderson@uw.edu";

            // need a way to make students like this
            Student stu4 = new Student("David Deacon", "Davis", 3.7, "ddavis@uw.edu");
            stu2 = new Student("Bob Billy", "Bradshaw", 3.8, "bbradshaw@uw.edu");
            stu3 = new Student("Carlos Charlie", "Castaneda", 3.6, "ccastaneda@uw.edu");

            // output can be done individually
            Console.WriteLine(stu1);
            Console.WriteLine(stu2);
            Console.WriteLine(stu3);
            Console.WriteLine(stu4);
        }
    }
}
