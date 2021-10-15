using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAPWPF.RAPModel;

namespace RAPWPF.RAPController
{
    class StaffSupervisionController
    {
        public void LoadSupervisions(Staff staff)
        {
            List<Student> students = new List<Student>();
            students = Agency.LoadSupervision(staff);
            students.ForEach(Console.WriteLine);
            //return students;
        }
    }
}
