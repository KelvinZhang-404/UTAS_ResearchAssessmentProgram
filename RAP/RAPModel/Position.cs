using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAPWPF.RAPModel
{
    class Position
    {
        public int ID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Level Level { get; set; }

        public override string ToString()
        {
            string end = EndDate == null ? "Null" : EndDate?.ToString("yyyy/MM/dd");
            return "ID: " + ID + ", StartDate: " + StartDate.ToString("yyyy/MM/dd") + ", EndDate: " + end + ", Level: " + Level;
        }
    }
}
