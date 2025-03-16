// Alexandria D'Oro-Gilbert
// T INFO 200A, Student Database
///////////////////////////////////////////////////////////////////////////////////////////////////
// CHANGE HISTORY
// DATE         DEVELOPER                   CHANGE HISTORY
// 2025-02-13   Alexandria D'Oro-Gilbert    Create new student database class. Create a list of
//                                          students.
// 2025-02-20   Alexandria D'Oro-Gilbert    Update DisplayMenu() to have more user selction options.
//                                          Create additional key options for RunDatabase().
//                                          PrintAllStudentRecirds() and PrintAllStudentRecordKeys()
//                                          are functional display menu options. Made FindStudentRecord()
//                                          functional.
// 02-25-2025   Alexandria D'Oro-Gilbert    Make ReadStudentDataFromInputFile() functional by reading an
//                                          input file of students. Not calling DbAppTestDriver().
// 03-01-2025   Alexandria D'Oro-Gilbert    Made AddStudentRecord() semi-functional, allowing users
//                                          to enter in student information according to their class
//                                          standing (undergrad or grad student). Made a functional
//                                          SaveStudentToFile() method that saves the output file
//                                          to the input file.
// 03-15-2025   adorog12@uw.edu             Nested do-while loops for AddStudentRecord() to ensure 
//                                          valid input for GPA. Created separate method for evaluating
//                                          Whether GPA is valid. Implemented ModifyStudentRecord() to
//                                          be partially functional.

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.ExceptionServices;
namespace StudentDb
{
    internal class DbApp
    {
        // 1: add a record
        // 2: update, modify, & edit a record
        // 3: find, search, & read records already in database
        // 4: delete & remove record from database

        // storage for the student records during the program runtime
        private List<Student> students = new List<Student>();

        public DbApp()
        {
            // some things that will be handled in constructor

            // read in data from permanent storage (file)
            ReadStudentDataFromInputFile();

            // fill list with some sample students
            // reading input file instead, DbAppTestDriver() not needed
            //DbAppTestDriver();

            // run the main loop engine
            RunDatabaseApp();

            // write the data back out to the permanent storage file
            WriteDataToOutputFile();
        }

        // Writes current student data to an output file.
        private string StudentOutputFile = "STUDENT_OUTPUT_FILE.txt";
        // Read current student data from an input file.
        private string StudentInputFile = "STUDENT_INPUT_FILE.txt";

        // Prints out the current student data to an output file.
        private void WriteDataToOutputFile()
        {
            // create an object that attaches to the file on disk
            StreamWriter outFile = new StreamWriter(StudentOutputFile);

            // use the ref to the file above to write the file
            foreach (Student stu in students) 
            {
                outFile.WriteLine(stu);     // can use like a console --> 'Console.WriteLine()'
            }
            // close the resource
            outFile.Close();
            Console.WriteLine("Saved all students to output file.");
        }

        // Inititates the Database loop.
        private void RunDatabaseApp()
        {
            while (!false) 
            {
                DisplayMainMenu();  // give user choices
                char selection = GetUserSelection();    // stores the users menu choice

                switch (char.ToUpper(selection)) 
                { 
                    case 'A':
                        AddStudentRecord();
                        break;
                    case 'F':
                        string email = string.Empty;
                        Student stu = FindStudentRecord(out email);
                        break;
                    case 'M':
                        ModifyStudentRecord();
                        break;
                    case 'D':
                        DeleteStudentRecord();
                        break;
                    case 'P':
                        PrintAllStudentRecords();
                        break;
                    case 'S':
                        WriteDataToOutputFile();
                        //SaveStudentToFile();
                        break;
                    case 'K':
                        PrintAllStudentRecordKeys();
                        break;
                    case 'E':
                        Environment.Exit(0);
                        break;
                    default:
                        Console.Write($"ERROR: {selection} is not a valid INPUT. SELECT AGAIN.");
                        // display the menu again
                        break;
                }
            }
        }

        // Allows user to add in student's first, middle, and last name, grade point average, and
        // email address. Saves to the input file.
        private void AddStudentRecord() {
            Console.WriteLine(
"\nUndergraduate or Graduate student? Enter (U) for Undergraduate or (G) for Graduate student.");
            string standing = Console.ReadLine();

            // GradStudent.
            if (standing.ToUpper() == "G")
            {
                // create new empty student to fill in information
                GradStudent grad = new GradStudent();

                // First + Middle name.
                Console.WriteLine("First and middle name?: ");
                grad.FirstMidName = Console.ReadLine();

                // Last name.
                Console.WriteLine("Last name?: ");
                grad.LastName = Console.ReadLine();

                // store value inside the grad students GPA value
                grad.GradePointAvg = isValidGPA();

                // Email address.
                Console.WriteLine("Email Address? :");
                grad.EmailAddress = Console.ReadLine();
                ;
                Console.WriteLine("Tuition credit amount? :");
                grad.TuitionCredit = int.Parse(Console.ReadLine());
                Console.WriteLine("Faculty Advisor?: ");
                grad.FacultyAdvisor = Console.ReadLine();
                students.Add(grad);
            }

            // Undergrad student.
            else if (standing.ToUpper() == "U")
            {
                // create new empty student to fill in information
                Undergrad undergrad = new Undergrad();

                // First + Middle name.
                Console.WriteLine("First and middle name?: ");
                undergrad.FirstMidName = Console.ReadLine();

                // Last name.
                Console.WriteLine("Last name?: ");
                undergrad.LastName = Console.ReadLine();

                // GPA.
                undergrad.GradePointAvg = isValidGPA();

                // Email address.
                Console.WriteLine("Email Address? :");
                undergrad.EmailAddress = Console.ReadLine();

                //Console.WriteLine("Year ranking? :");
                //undergrad.Rank = Console.ReadLine();
                Console.WriteLine("Degree/Major?: ");
                undergrad.DegreeMajor = Console.ReadLine();
                students.Add(undergrad);
            }
            else { Console.WriteLine("Please enter valid response."); }
            Console.WriteLine("**** Student information successfully entered in. ****");
        }

        // Search for a student according to their email address.
        private Student FindStudentRecord(out string email)
        {
            Console.WriteLine("\nENTER the email (PRIMARY KEY) being searched for: ");
            email = Console.ReadLine();

            foreach (Student stu in students) 
            {
                if (email == stu.EmailAddress)
                {
                    Console.WriteLine($"FOUND email address: {stu.EmailAddress}\n");
                    return stu;
                }
            }
            Console.WriteLine($"{email} NOT FOUND");
            return null;
        }

        // Looks up the student information to be edited based on the email entered into the
        // database. Can modify the first and middle name, last name, GPA, and email address.
        private void ModifyStudentRecord()
        {
            //throw new NotImplementedException();
            Console.WriteLine("What is the email of the student whose information you would like to modify? ");
            string lookupEmail = Console.ReadLine();


            foreach (Student stu in students)
            {
                if (lookupEmail == stu.EmailAddress)
                {
                    Console.WriteLine(stu);
                    Console.WriteLine(@"
What information would you like to edit?
[F]irst name and middle name?
[L]ast name?
[G]PA?
[E]mail address?
When finished, hit [X] to exit the editor.");

                    char editInfo = GetUserSelection();

                    //while (!false)
                    //{
                        switch (char.ToUpper(editInfo))
                        {
                            case 'F':
                                Console.WriteLine("First and middle name? ");
                                stu.FirstMidName = Console.ReadLine();
                                break;
                            case 'L':
                                Console.WriteLine("Last name? ");
                                stu.LastName = Console.ReadLine();
                                break;
                            case 'G':
                                stu.GradePointAvg = isValidGPA();
                                break;
                            case 'E':
                                Console.WriteLine("Email address? ");
                                stu.EmailAddress = Console.ReadLine();
                                break;
                            case 'X':
                                Environment.Exit(0);
                                break;
                            default:
                                Console.WriteLine("Invalid option. Please enter a valid option");
                                break;
                        }
                    //}
                    Console.WriteLine("**** UPDATED STUDENT INFORMAITON ****");
                    Console.WriteLine(stu);
                }

            }
            Console.WriteLine(@"
Either the student is not entered into the database or the email was entered incorrectly. 
Please try again.");
        }

        private void DeleteStudentRecord()
        {
            throw new NotImplementedException();
        }

        // Prints all of the current students in the database.
        private void PrintAllStudentRecords()
        {
            Console.WriteLine("\n********** List all Student Records **********");

            // iterates through the student lists and prints their information.
            foreach (Student stu in students)
            {
                Console.WriteLine(stu);
            }
            Console.WriteLine("********** Done listing all students **********");
        }

        // Print all the email addresses of the current students in the database.
        private void PrintAllStudentRecordKeys()
        {
            Console.WriteLine("********** List all student emails **********");

            foreach (Student stu in students)
            {
                Console.WriteLine(stu.EmailAddress);
            }

            Console.WriteLine("********** Done listing all student emails **********");
        }

        // users can indicate choices with a single key press
        // Returns the keyboard key selected by the user to the main menu.
        private char GetUserSelection()
        {
            ConsoleKeyInfo key = Console.ReadKey();     // reads and return key press
            return key.KeyChar;
        }
        
        // Evaluates whether the entered value is a valid GPA value. Checks fo the case of an non-
        // double is entered and whether the value is between 0.0 and 4.0.
        private double isValidGPA()
        {
            // create temporary double and bool values for do-while loop evaluations
            double tempGPA;
            bool isValid;

            // Check for valid GPA value.
            do      // outer do-while checks if GPA is within correct GPA range
            {
                do  // inner checks if the GPA is a double
                {
                    Console.WriteLine("Grade Point Average?: ");
                    string input = Console.ReadLine();
                    isValid = double.TryParse(input, out tempGPA);

                    if (!isValid)
                    {
                        Console.WriteLine("Invalid input. Please enter a valid GPA.");
                    }
                } while (!isValid);

                if (tempGPA < 0 || tempGPA > 4.0)
                {
                    Console.WriteLine("Please enter a valid GPA value between 0.0 and 4.0.");
                }
            } while (tempGPA < 0 || tempGPA > 4.0);
            return tempGPA;
        }

        // Start-up menu that displays all the user options for viewing and/or altering the student
        // database.
        private void DisplayMainMenu()
        {
            Console.Write(@"
***********************************************************
******************* StudentDatabase App *******************
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
[A]dd a student record       (C in CRUD - Create)
[F]ind a student record      (R in CRUD - Read)
[M]odify student record      (U in CRUD - Update)
[D]elete a student record    (D in CRUD - Delete)
[P]rint all records in current database storage
Print all primary [K]eys (email addresses)
[S]ave data to file without exiting app
[E]xit without saving changes

User Key Selection: ");
        }

        // Reads in the student information from an external storage file (text file in this case).
        private void ReadStudentDataFromInputFile()
        {
            // create a resource in the code to point to the file on disk
            StreamReader inFile = new StreamReader(StudentInputFile);

            string studentType = string.Empty;

            // use the stream object to read in from the file
            while ((studentType = inFile.ReadLine()) != null)
            {
                string first = inFile.ReadLine();
                string last = inFile.ReadLine();
                double gpa = double.Parse(inFile.ReadLine());   // turn string value into double
                string email = inFile.ReadLine();

                // Read the first line to determine student type
                if (studentType == "StudentDb.Undergrad") 
                {
                    YearRank year = (YearRank)Enum.Parse(typeof(YearRank), inFile.ReadLine());
                    string major = inFile.ReadLine();
                    // 1-line code way to create and add student to list
                    students.Add(new Undergrad(first, last, gpa, email, year, major));
                }
                else if (studentType == "StudentDb.GradStudent")
                {
                    decimal credit = decimal.Parse(inFile.ReadLine());
                    string advisor = inFile.ReadLine();
                    // 2-line code way to create and add student to list
                    Student grad = new GradStudent(first, last, gpa, email, credit, advisor);
                    students.Add(grad);
                }
                else
                {
                    Console.WriteLine($"{studentType} is not a vaild student type. Check the input file.");
                }
            }
            // close the file
            inFile.Close();
        }

        // Tester application.
        public void DbAppTestDriver()
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
            stu2 = new Student("Bob Billy", "Bradshaw", 3.8, "bbradshaw.uw.edu");
            stu3 = new Student("Carlos Charlie", "Castaneda", 3.6, "ccastaneda@uw.edu");

            // place all students into the list
            students.Add(stu1);
            students.Add(stu2);
            students.Add(stu3);
            students.Add(stu4);

            //// output can be done individually
            //Console.WriteLine(stu1);
            //Console.WriteLine(stu2);
            //Console.WriteLine(stu3);
            //Console.WriteLine(stu4);

            //WriteDataToOutputFile();

        }

    }
}