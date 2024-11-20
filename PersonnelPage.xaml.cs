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

namespace KPIsmagilov
{
    /// <summary>
    /// Логика взаимодействия для PersonnelPage.xaml
    /// </summary>
    public partial class PersonnelPage : Page
    {
        public PersonnelPage()
        {
            InitializeComponent();

            var currentPersonnel = KPIsmagilovEntities.GetContext().Personnel.ToList();
            PersonnelListView.ItemsSource = currentPersonnel;

            ProdAll.Text = Convert.ToString(currentPersonnel.Count);
            ProdAtTheMoment.Text = Convert.ToString(currentPersonnel.Count);
        }

        private void UpdatePersonnel()
        {
            var currentPersonnel = KPIsmagilovEntities.GetContext().Personnel.ToList();

            currentPersonnel = currentPersonnel.Where(p => p.Surname.ToLower().Contains(TBSearch.Text.ToLower()) ||
                                                        p.Name.ToLower().Contains(TBSearch.Text.ToLower()) ||
                                                        p.Patronymic.ToLower().Contains(TBSearch.Text.ToLower()) ||
                                                        p.Position.ToLower().Contains(TBSearch.Text.ToLower()) ||
                                                        p.PersonnelAwardsDisplay.ToLower().Contains(TBSearch.Text.ToLower()) ||
                                                        (p.LengthOfService).ToString().ToLower().Contains(TBSearch.Text.ToLower()) ||
                                                        (p.YearOfBirth).ToString().ToLower().Contains(TBSearch.Text.ToLower()) ||
                                                        (p.YearOfEntryIntoService).ToString().ToLower().Contains(TBSearch.Text.ToLower())).ToList();

            if (SortCombo.SelectedIndex == 0)
            {
                currentPersonnel = currentPersonnel.OrderBy(p => p.LengthOfService).ToList();
            }

            if (SortCombo.SelectedIndex == 1)
            {
                currentPersonnel = currentPersonnel.OrderByDescending(p => p.LengthOfService).ToList();
            }


            ProdAtTheMoment.Text = Convert.ToString(currentPersonnel.Count);

            if (CostUp.IsChecked.Value)
            {
                currentPersonnel = currentPersonnel.Where(p => p.Awards != null).ToList();
                ProdAtTheMoment.Text = Convert.ToString(currentPersonnel.Count);
            }
            if (CostDown.IsChecked.Value)
            {
                currentPersonnel = currentPersonnel.Where(p => p.Awards == null).ToList();
                ProdAtTheMoment.Text = Convert.ToString(currentPersonnel.Count);
            }

            PersonnelListView.ItemsSource = currentPersonnel;
        }

        private void TBSearch_SelectionChanged(object sender, RoutedEventArgs e)
        {
            UpdatePersonnel();
        }

        private void CostUp_Checked(object sender, RoutedEventArgs e)
        {
                UpdatePersonnel();
        }

        private void CostDown_Checked(object sender, RoutedEventArgs e)
        {
            UpdatePersonnel();
        }

        private void SortCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdatePersonnel();
        }

        private void EditContextButton_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage((sender as Button).DataContext as Personnel));
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage(null));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var currentPersonnel = KPIsmagilovEntities.GetContext().Personnel.ToList();

            PersonnelListView.ItemsSource = currentPersonnel;
        }

        private void ResetFilter_Click(object sender, RoutedEventArgs e)
        {
            CostUp.IsChecked = false;
            CostDown.IsChecked = false;
            UpdatePersonnel();
        }
    }
}
