using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAPWPF.RAPModel;

namespace RAPWPF.RAPController
{
    class ResearcherListController
    {
        ObservableCollection<Researcher> viewableResearcherList;
        private List<Researcher> researchers;
        List<Staff> staff = new List<Staff>();
        List<Student> students = new List<Student>();
        public ObservableCollection<Researcher> VisibleResearchers { get { return viewableResearcherList; } set { } }
        public ResearcherListController()
        {
            researchers = Agency.LoadResearchers();
            viewableResearcherList = new ObservableCollection<Researcher>(researchers);
            //GenerateList();
        }

        public ObservableCollection<Researcher> GetViewableResearchers()
        {
            return viewableResearcherList;
        }

        public ObservableCollection<Researcher> FilterByType(string type)
        {
            var selected = from Researcher r in viewableResearcherList
                           where r.Type == Agency.ParseEnum<RAPModel.Type>(type)
                           select r;
            return new ObservableCollection<Researcher>(selected);
        }

        public ObservableCollection<Researcher> FilterByLevel(string level)
        {
            var selected = from Researcher r in viewableResearcherList
                           where r.Level == Agency.ParseEnum<Level>(level)
                           select r;
            return new ObservableCollection<Researcher>(selected);
        }

        public ObservableCollection<Researcher> SearchByName(string name)
        {
            var selected = from Researcher r in viewableResearcherList
                           where r.Name.ToUpper().Contains(name.ToUpper())
                           select r;
            return new ObservableCollection<Researcher>(selected);
        }
    }
}
