using Microsoft.Win32;
using System;
using System.IO;
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
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {
        private BitmapImage imageSource;

        private Personnel currentPersonnel = new Personnel();
        public AddEditPage(Personnel personnel)
        {
            InitializeComponent();

            var UnitsTypes = KPIsmagilovEntities.GetContext().Units.Select(p => p.ID_Units).ToList();


            foreach (var UnitsType in UnitsTypes)
            {
                UnitsTypeComboBox.Items.Add(UnitsType);
            }

            if (personnel == null) {
                DeleteButton.Visibility = Visibility.Collapsed;
                SaveButton.Margin = new Thickness(0);
                //imageSource = new BitmapImage(new Uri("C:\\Users\\prod_upal\\Desktop\\KP\\KPIsmagilov\\Resources\\default_photo.png"));
            }
            else
            {
                currentPersonnel = personnel;
                YearOfBirthDPicker.SelectedDate = currentPersonnel.YearOfBirth;
                YearOfEntryIntoServiceDPicker.SelectedDate = currentPersonnel.YearOfEntryIntoService;
                if (personnel.Photo != null)
                {
                    try
                    {
                        imageSource = new BitmapImage(new Uri(personnel.Photo));
                        UserImage.Source = imageSource;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("не удалось загрузить фотографию, либо она отсутствует");
                    }
                }
                else
                {
                    MessageBox.Show("не удалось загрузить фотографию, либо она отсутствует");
                    //imageSource = new BitmapImage(new Uri("C:\\Users\\prod_upal\\Desktop\\KP\\KPIsmagilov\\Resources\\default_photo.png"));
                }
            }

            DataContext = currentPersonnel;
        }

        private void ChoisePhoto_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    string selectedFilePath = openFileDialog.FileName;

                    BitmapImage image = new BitmapImage(new Uri(selectedFilePath));
                    UserImage.Source = image;
                    PhotoTBox.Text = selectedFilePath;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при добавлении фото: {ex.Message}", "Ошибка");
                }

            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime date2 = (DateTime)YearOfBirthDPicker.SelectedDate;
            DateTime date1 = (DateTime)YearOfEntryIntoServiceDPicker.SelectedDate;
            int res = (int)date1.Year - date2.Year;

            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(PhotoTBox.Text))
                errors.AppendLine("Добавьте фотографию военнослужащего");
            if (UnitsTypeComboBox.SelectedItem == null)
                errors.AppendLine("Укажите ID части");
            if (string.IsNullOrWhiteSpace(currentPersonnel.Surname))
                errors.AppendLine("Укажите фамилию военнослужащего");
            if (string.IsNullOrWhiteSpace(currentPersonnel.Name))
                errors.AppendLine("Укажите имя военнослужащего");
            if (string.IsNullOrWhiteSpace(currentPersonnel.Patronymic))
                errors.AppendLine("Укажите отчество военнослужащего");
            if (string.IsNullOrWhiteSpace(currentPersonnel.Position))
                errors.AppendLine("Укажите звание военнослужащего");
            if (YearOfBirthDPicker.SelectedDate == new DateTime(1, 1, 1))
                errors.AppendLine("Укажите дату рождения военнослужащего");
            if (YearOfEntryIntoServiceDPicker.SelectedDate == new DateTime(1, 1, 1))
                errors.AppendLine("Укажите дату вступления в службу военнослужащего");
            if (currentPersonnel.LengthOfService == 0)
                errors.AppendLine("Укажите выслугу лет военнослужащего");
            if (YearOfBirthDPicker.SelectedDate > DateTime.Now)
            {
                errors.AppendLine("Дата рождения военнослужащего не может быть позднее текущей даты");
            }
            else if (YearOfBirthDPicker.SelectedDate > YearOfEntryIntoServiceDPicker.SelectedDate &&
                !(YearOfBirthDPicker.SelectedDate == new DateTime(1, 1, 1) && YearOfEntryIntoServiceDPicker.SelectedDate == new DateTime(1, 1, 1)))
                errors.AppendLine("дата рождения военнослужащего не может быть позднее года вступления в службу");
             if (YearOfEntryIntoServiceDPicker.SelectedDate > DateTime.Now)
            {
                errors.AppendLine("Дата вступления в службу военнослужащего не может быть позднее текущей даты");
            } 
            else if (res < 18 &&
                !(YearOfBirthDPicker.SelectedDate == new DateTime(1, 1, 1) || YearOfEntryIntoServiceDPicker.SelectedDate == new DateTime(1, 1, 1)))
                errors.AppendLine("Вступление на службу моложе 18 лет невозможно");
           

            currentPersonnel.YearOfBirth = date2;
            currentPersonnel.YearOfEntryIntoService = date1;
            currentPersonnel.Photo = PhotoTBox.Text;

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (currentPersonnel.ID_serviceman == 0)
            {
                KPIsmagilovEntities.GetContext().Personnel.Add(currentPersonnel);
            }
            try
            {
                KPIsmagilovEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void YearOfEntryIntoServiceDPicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime date = (DateTime)YearOfEntryIntoServiceDPicker.SelectedDate;
            int res = (int)date.Year;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var currentPersonnel = (sender as Button).DataContext as Personnel;

            if (MessageBox.Show("Вы точно хотите выполнить удаление?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    KPIsmagilovEntities.GetContext().Personnel.Remove(currentPersonnel);
                    KPIsmagilovEntities.GetContext().SaveChanges();
                    Manager.MainFrame.GoBack();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            
        }
    }
}
