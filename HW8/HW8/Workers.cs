using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW8
{
    public struct Workers
    {
        public Vacancy vacancy { get; set; }
        public string Name { get; set; }
        public int Salary { get; set; }
        public int[] WorkerData { get; set; }
    }
}
