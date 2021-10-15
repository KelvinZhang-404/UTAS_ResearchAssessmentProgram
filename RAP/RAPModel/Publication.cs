using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAPWPF.RAPModel
{
    public enum Mode { Conference, Journal, Other };
    class Publication
    {
        public string Title { get; set; }
        public int Year { get; set; }
        public Mode Mode { get; set; }
        public DateTime Certified { get; set; }
        public string DOI { get; set; }
        public string Authors { get; set; }
        public string CiteAs { get; set; }
        public int Age { get { return GetAge(); } set { } }
        public bool loaded = false;

        private int GetAge()
        {
            return (DateTime.Now - Certified).Days;
        }
        public override string ToString()
        {
            return "Year: " + Year + ", Title: " + Title +
                ", Type: " + Mode + ", Certified: " + Certified.ToString("yyyy/MM/dd");
        }
    }
}
