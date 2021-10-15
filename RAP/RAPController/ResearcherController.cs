using RAPWPF.RAPModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAPWPF.RAPController
{
    class ResearcherController
    {
        public void GetPositionHistory(Researcher researcher)
        {
            (researcher as Staff).Positions = Agency.LoadPositions(researcher.ID);
            //positions.ForEach(Console.WriteLine);
        }

        public void GetPublications(Researcher researcher)
        {
            researcher.publications = Agency.LoadPublications(researcher.ID);
            //researcher.publications.ForEach(Console.WriteLine);
        }

        public void GetPrimaryDetails(Researcher researcher)
        {
            Agency.LoadPrimaryDetails(researcher);
        }

        public void GetOtherDetails(Researcher researcher)
        {
            Agency.LoadOtherDetails(researcher);

            if(researcher.Type == RAPModel.Type.Student)
            {
                Agency.LoadSupervisor(researcher as Student);
            }
            else
            {
                int currentYear = Int32.Parse(DateTime.Now.ToString("yyyy"));
                (researcher as Staff).ThreeYearAvg = Math.Truncate((Agency.YearRangePublicationCount(researcher, currentYear - 3, currentYear) * 100 / 3.0) * 100) / 100;

                string level = researcher.Level.ToString();
                double division = 0.0;
                switch (level)
                {
                    case "A":
                        division = 0.5;
                        break;
                    case "B":
                        division = 1;
                        break;
                    case "C":
                        division = 2;
                        break;
                    case "D":
                        division = 3.2;
                        break;
                    case "E":
                        division = 4;
                        break;
                    default:
                        break;
                }
                double threeYearAvg = (researcher as Staff).ThreeYearAvg;
                double result = threeYearAvg / division;
                if (result <= 70) (researcher as Staff).Performance = "Poor";
                else if (result > 70 && result < 110) (researcher as Staff).Performance = "Below Expectations";
                else if (result >= 110 && result < 200) (researcher as Staff).Performance = "Meeting Minimum";
                else (researcher as Staff).Performance = "Star Performers";
                (researcher as Staff).Supervise();
            }
        }


    }
}
