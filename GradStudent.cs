﻿// Alexandria D'Oro-Gilbert
// T INFO 200A, Student Database
///////////////////////////////////////////////////////////////////////////////////////////////////
// CHANGE HISTORY
// DATE         DEVELOPER           CHANGE HISTORY
// 02-27-2025   adorog12@uw.edu     Create new Grad Student that inherits from Student. Contains 
//                                  TuitionCredit for TAing and FacultyAdvisor access information.
//                                  Override ToString() method.
// 03-01-2025   adorog12@uw.edu     Add an empty case for Grad students.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 in addition to being a student,
--
 */
namespace StudentDb
{
    class GradStudent : Student
    {
        // Pay Grad students with TuitionCredit (TA)
        public decimal TuitionCredit { get; set; }
        // Help them work on thesis/capstone
        public string FacultyAdvisor { get; set; }


        public GradStudent(string first, string last, double gpa, string email, decimal credit, 
            string faculty) : base(first, last, gpa, email) 
        {
            TuitionCredit = credit;
            FacultyAdvisor = faculty;
        }

        // Empty GradStudent to cast information into.
        public GradStudent() { }


        public override string ToString()
        {
            return base.ToString() + $"Credit: {TuitionCredit}\nFaculty Advisor: {FacultyAdvisor}";
        }

        public override string ToStringForOutputFile()
        {
            string str = this.GetType().FullName + "\n";
            str += base.ToStringForOutputFile() + "\n";
            str += $"{TuitionCredit}\n";
            str += $"{FacultyAdvisor}\n";
            return str;
        }
    }
}
