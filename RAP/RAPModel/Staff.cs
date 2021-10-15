using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAPWPF.RAPModel
{
    
    class Staff: Researcher
    {
        //private List<Position> positions = new List<Position>();
        private List<Student> students = new List<Student>();
        public double ThreeYearAvg { get; set; }
        public string Performance { get; set; }
        public int Supervisions { get { return students.Count; } set { } }
        public List<Student> Students { get { return students; } set { } }
        public List<Position> Positions { get; set; }
        public void Supervise()
        {
            students = Agency.LoadSupervision(this);
            //students.ForEach(Console.WriteLine);
        }

        
    }
}
