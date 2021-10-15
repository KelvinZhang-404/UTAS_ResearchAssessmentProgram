using RAPWPF.RAPController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAPWPF.RAPModel
{
    public enum Type { Student, Staff };
    public enum Campus { Hobart, Launceston, CradleCoast };
    public enum Level { Null, A, B, C, D, E };

    class Researcher
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public Type Type { get; set; }
        public Level Level { get; set; }
        public Campus Campus { get; set; }
        public string Title { get; set; }
        public string Unit { get; set; }
        public string Email { get; set; }
        public string Photo { get; set; }
        public string JobTitle { get; set; }
        public DateTime CommencedIns { get; set; }
        public DateTime CommencedPos { get; set; }
        public double Tenure { get; set; }
        private bool loaded = false;
        public bool Loaded { get { return loaded; } set { } }
        public int PublicationCount { get { return publications.Count; } set { } }
        public List<Tuple<string, int>> CumulativeCounts { get; set; }

        public List<Publication> publications = new List<Publication>();

        public void CumulativePublication()
        {
            PublicationController pc = new PublicationController();
            pc.DisplayCumulativeByYear(this);
        }

        public override string ToString()
        {
            //return "ID: " + ID + ", Name: " + Name + " (" + Title + "), Type: " + Type + ", Level: " + Level;
            return Name + " (" + Title + ")";
        }
    }
}
