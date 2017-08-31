using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Task2
{
    public class Student
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Group { get; set; }
        public double Score { get; set; }
        public double FamilyIncome { get; set; }
        public Sex sex { get; set; }
        public FormOfTraining form { get; set; }
    }
}
