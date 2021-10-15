using RAPWPF.RAPModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAPWPF.RAPController
{
    class PublicationController
    {
        public void DisplayCumulativeByYear(Researcher researcher)
        {
            researcher.CumulativeCounts = Agency.PublicationCumulativeCount(researcher);
            
        }

        public List<int> PubYearRange(Researcher researcher)
        {
            List<int> range = new List<int>();

            var minYear = (from Publication p in researcher.publications
                           orderby (p.Year)
                           select p.Year).First();
            var maxYear = (from Publication p in researcher.publications
                           orderby (p.Year)
                           select p.Year).Last();
            for(int i = minYear; i <= maxYear; i++)
            {
                range.Add(i);
            }
            return range;
        }

        public List<Publication> PublicationByRange(Researcher researcher, int startYear, int endYear)
        {
            var publications = from Publication p in researcher.publications
                               where p.Year >= startYear && p.Year <= endYear
                               select p;
            return new List<Publication>(publications);
        }

        public Publication GetPublicationDetails(Publication publication)
        {
            Agency.LoadPublicationDetails(publication);
            return publication;
        }
    }
}
