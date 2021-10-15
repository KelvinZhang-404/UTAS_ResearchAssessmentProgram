using RAPWPF.RAPController;
using RAPWPF.RAPModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RAPWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ResearcherListController rlc;
        PublicationController pc;
        private Researcher researcher = null;

        public MainWindow()
        {
            InitializeComponent();
            rlc = (ResearcherListController)FindResource("researcherListContrller");
            pc = new PublicationController();
        }

        private void LevelComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (rlc.VisibleResearchers.Count > 0)
            {
                if(LevelComboBox.SelectedItem == null)
                {
                    ResearcherListBox.ItemsSource = rlc.GetViewableResearchers();
                    return;
                }
                string level = LevelComboBox.SelectedItem.ToString();
                if (level == "All")
                {
                    ResearcherListBox.ItemsSource = rlc.GetViewableResearchers();
                }
                else if (level == "Students")
                {
                    ResearcherListBox.ItemsSource = rlc.FilterByType("Student");
                }
                else
                {
                    ResearcherListBox.ItemsSource = rlc.FilterByLevel(level);
                }
            }
            
        }

        private void NameTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            string name = NameTextBox.Text;
            ResearcherListBox.ItemsSource = rlc.SearchByName(name);
        }

        private void ResearcherListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                ResearcherController rc = new ResearcherController();
                researcher = ResearcherListBox.SelectedItem as Researcher;

                if (!researcher.Loaded)
                {
                    rc.GetPrimaryDetails(researcher);
                    rc.GetOtherDetails(researcher);
                    rc.GetPublications(researcher);
                    if (researcher.Type == RAPModel.Type.Staff) rc.GetPositionHistory(researcher);
                }

                SetResources(researcher);
                SetDefaultStyle(researcher);
            }
        }

        private void SetResources(Researcher researcher)
        {
            PrimaryDetailsPanel.DataContext = researcher;
            PersonalDetailsPanel.DataContext = researcher;

            List<int> yearRange = pc.PubYearRange(researcher);
            FromComboBox.ItemsSource = yearRange;
            ToComboBox.ItemsSource = yearRange;

            PersonalPublicationPanel.DataContext = researcher;
            tblPublications.ItemsSource = researcher.publications;

            if (researcher.Type == RAPModel.Type.Staff)
            {
                List<Position> positions = (researcher as Staff).Positions;
                tblPositions.ItemsSource = positions;
            }
        }

        private void SetDefaultStyle(Researcher researcher)
        {
            PersonalDetailsTabCtrl.Visibility = Visibility.Visible;
            DetailTab.IsSelected = true;

            if (PrimaryDetailsPanel.Visibility == Visibility.Hidden)
            {
                PrimaryDetailsPanel.Visibility = Visibility.Visible;
                PersonalDetailsPanel.Visibility = Visibility.Visible;
            }

            StudentListPanel.Visibility = Visibility.Collapsed;
            OtherDetailsPanel.Visibility = Visibility.Visible;
            CumulativeCountsPanel.Visibility = Visibility.Collapsed;

            if (researcher.Type == RAPModel.Type.Student)
            {
                SupervisonBtn.Visibility = Visibility.Hidden;
                PositionTab.Visibility = Visibility.Collapsed;
            }
            else
            {
                SupervisonBtn.Visibility = Visibility.Visible;
                PositionTab.Visibility = Visibility.Visible;
            }
        }

        private void NameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            LevelComboBox.SelectedItem = null;
        }

        private void SupervisonBtn_Click(object sender, RoutedEventArgs e)
        {
            tblStudents.ItemsSource = (researcher as Staff).Students;
            StudentListPanel.Visibility = Visibility.Visible;
            CumulativeCountsPanel.Visibility = Visibility.Collapsed;
            OtherDetailsPanel.Visibility = Visibility.Collapsed;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CumulativeCountsPanel.Visibility = Visibility.Collapsed;
            StudentListPanel.Visibility = Visibility.Collapsed;
            OtherDetailsPanel.Visibility = Visibility.Visible;
        }

        private void CumulativeBtn_Click(object sender, RoutedEventArgs e)
        {
            if(researcher.CumulativeCounts == null)
            {
                researcher.CumulativePublication();
            }
            tblCumulativeCounts.ItemsSource = researcher.CumulativeCounts;
            StudentListPanel.Visibility = Visibility.Collapsed;
            CumulativeCountsPanel.Visibility = Visibility.Visible;
            OtherDetailsPanel.Visibility = Visibility.Collapsed;
        }

        private void SearchRangeBtn_Click(object sender, RoutedEventArgs e)
        {
            if(FromComboBox.SelectedItem == null || ToComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please specify start year and end year");
            }
            else
            {
                int startYear = Int32.Parse(FromComboBox.SelectedItem.ToString());
                int endYear = Int32.Parse(ToComboBox.SelectedItem.ToString());
                if(startYear > endYear)
                {
                    MessageBox.Show("Invalid year range!");
                }
                else
                {
                    tblPublications.ItemsSource = new PublicationController().PublicationByRange(researcher, startYear, endYear);
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            PubListPanel.Visibility = Visibility.Visible;
            PubDetailsPanel.Visibility = Visibility.Collapsed;
        }

        private void TblPublications_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Publication publication = tblPublications.SelectedItem as Publication;
            if (!publication.loaded)
            {
                pc.GetPublicationDetails(publication);
            }
            // add to item resource
            PubDetailsPanel.DataContext = publication;
            PubListPanel.Visibility = Visibility.Collapsed;
            PubDetailsPanel.Visibility = Visibility.Visible;
        }

    }
}
