using System;
using System.Windows;
using System.Windows.Media;
using System.Data.SqlClient;
using System.Data.Linq;
using System.Text.RegularExpressions;

namespace SchoolDateBaseWPF
{
    public partial class CreateTeacherWindow : Window
    {
        SqlConnection connection;
        public CreateTeacherWindow(SqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {      
            textBoxFirstNameInCreateTeacherWindow.BorderBrush = (textBoxFirstNameInCreateTeacherWindow.Text != "") ? Brushes.Black : Brushes.Red;
            textBoxLastNameInCreateTeacherWindow.BorderBrush = (textBoxLastNameInCreateTeacherWindow.Text != "") ? Brushes.Black : Brushes.Red;
            textBoxClassRoomInCreateTeacherWindow.BorderBrush = (textBoxClassRoomInCreateTeacherWindow.Text != "") ? Brushes.Black : Brushes.Red;        

            if (textBoxFirstNameInCreateTeacherWindow.Text != "" && textBoxLastNameInCreateTeacherWindow.Text != "" && textBoxClassRoomInCreateTeacherWindow.Text != "" && comboBoxSubgectOnCreateTeacher.Text != "")
            {
                DataContext dataContext = new DataContext(connection);

                //Table<MainWindow.TableTeachers> tableStudents = dataContext.GetTable<MainWindow.TableTeachers>();

                MainWindow.TableTeachers newTeacher = new MainWindow.TableTeachers
                {
                    FullName = textBoxFirstNameInCreateTeacherWindow.Text + " "
                    + textBoxLastNameInCreateTeacherWindow.Text,
                    Classroom = Convert.ToInt32(textBoxClassRoomInCreateTeacherWindow.Text),
                    Subject = comboBoxSubgectOnCreateTeacher.Text
     
                };

                dataContext.GetTable<MainWindow.TableTeachers>().InsertOnSubmit(newTeacher);
                dataContext.SubmitChanges();

                labelCreateErrorTeacher.Visibility = Visibility;
                labelCreateErrorTeacher.Content = "teacher successfully added";
            }
            else
            {
                labelCreateErrorTeacher.Visibility = Visibility;
                labelCreateErrorTeacher.Content = "need to fill in all fields";
            }
        }
       
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            Close();
        }

        private void textBoxFirstNameInCreateTeacherWindow_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            string inputSymbol = e.Text.ToString();

            if (!Regex.Match(inputSymbol, @"[а-я],[a-z]|\.|,").Success)
            {
                e.Handled = true;
            }
        }

        private void textBoxLastNameInCreateTeacherWindow_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            string inputSymbol = e.Text.ToString();

            if (!Regex.Match(inputSymbol, @"[а-я][a-z]|\.|,").Success)
            {
                e.Handled = true;
            }
        }

        private void textBoxClassRoomInCreateTeacherWindow_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            string inputSymbol = e.Text.ToString();

            if (!Regex.Match(inputSymbol, @"[0-9]|\.|,").Success)
            {
                e.Handled = true;
            }
        }
    }
}
