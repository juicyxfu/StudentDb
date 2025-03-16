// Alexandria D'Oro-Gilbert
// T INFO 200A, Student Database
///////////////////////////////////////////////////////////////////////////////////////////////////
// CHANGE HISTORY
// DATE         DEVELOPER           CHANGE HISTORY
// 02-27-2025   adorog12@uw.edu     Create new Undergrad that inherits from Student. Set enum vals
//                                  of undergrad years. Override ToStringForOutputFile() method 
// 03-01-2025   adorog12@uw.edu     Add an empty case for Undergrad students.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDb
{
    // specified range of values
    // enum - declare various constants
    public enum YearRank
    {
        // comma separate list
        Freshman = 1,
        Sophomore = 2,
        Junior = 3,
        Senior = 4
    }

    class Undergrad : Student
    {
        public YearRank Rank { get; set; }
        public string DegreeMajor { get; set; }
        public Undergrad(string first, string last, double gpa, string email, YearRank year,
                        string major) : base(first, last, gpa, email)
        {
            Rank = year;
            DegreeMajor = major;

        }

        // Empty Undergrad to cast information into.
        public Undergrad() { }


        public override string ToString() 
        {
            return base.ToString() + $" Year: {Rank}\nMajor: {DegreeMajor}\n";
        }

        // Override the original ToStringForOutputFile() method and add Undergrad major and year
        // rnak.
        public override string ToStringForOutputFile()
        {
            string str = this.GetType().FullName + "\n";
            str += base.ToStringForOutputFile() + "\n";
            str += $"{Rank}\n";
            str += $"{DegreeMajor}\n";

            return str;
        }
    }
}
