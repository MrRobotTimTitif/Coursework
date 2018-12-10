using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Data.SqlClient;
using System.Data;
using System.Data.Linq;
using System.Text.RegularExpressions;

namespace SchoolDateBaseWPF
{
    public partial class CreateStudentWindow : Window
    {       
        SqlConnection connection; 
        public CreateStudentWindow(SqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
               
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            Close();
        }


        private void buttonCreateStudent_Click(object sender, RoutedEventArgs e)
        {
            textBoxFirstNameInCreateStudentWindow.BorderBrush = (textBoxFirstNameInCreateStudentWindow.Text != "") ? Brushes.Black : Brushes.Red;
            textBoxLastNameInCreateStudentWindow.BorderBrush = (textBoxFirstNameInCreateStudentWindow.Text != "") ? Brushes.Black : Brushes.Red;
          

            if (textBoxFirstNameInCreateStudentWindow.Text != "" && textBoxLastNameInCreateStudentWindow.Text != "")
            {

                DataContext dataContext = new DataContext(connection);
            
                Table<MainWindow.TableStudents> tableStudents = dataContext.GetTable<MainWindow.TableStudents>();

                MainWindow.TableStudents newStudent = new MainWindow.TableStudents
                {
                    FullName = textBoxFirstNameInCreateStudentWindow.Text + " "
                    + textBoxLastNameInCreateStudentWindow.Text,
                    Class = comboBoxClassOnCreateStudent.Text
                };
               
                dataContext.GetTable<MainWindow.TableStudents>().InsertOnSubmit(newStudent);
                dataContext.SubmitChanges();

                var id = from o in dataContext.GetTable<MainWindow.TableStudents>()
                         where o.FullName== textBoxFirstNameInCreateStudentWindow.Text + " " + textBoxLastNameInCreateStudentWindow.Text 
                         && o.Class== comboBoxClassOnCreateStudent.Text
                         select o.Id;
              
                int s = 0;
                foreach (var o in id)
                {
                    s = Convert.ToInt32(o);
                }
           
                Table<MainWindow.TablePerformance> tablePerformance = dataContext.GetTable<MainWindow.TablePerformance>();

                MainWindow.TablePerformance newPerformance = new MainWindow.TablePerformance
                {
                    Id = s,
                    NameStudent = textBoxFirstNameInCreateStudentWindow.Text + " "
                    + textBoxLastNameInCreateStudentWindow.Text,
                    Maths = 0,
                    Physics = 0,
                    Biology = 0
                   
                   
                };

                dataContext.GetTable<MainWindow.TablePerformance>().InsertOnSubmit(newPerformance);
                dataContext.SubmitChanges();

                labelCreateError.Visibility = Visibility;
                labelCreateError.Content = "student successfully added";
            }
            else
            {             
                labelCreateError.Visibility = Visibility;
                labelCreateError.Content = "need to fill in all fields";
              
            }
            connection.Close();

            MainWindow mw = new MainWindow();

            mw.ShowStudents();   
            mw.ShowPerformance();
        }

        private void textBoxFirstNameInCreateStudentWindow_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            string inputSymbol = e.Text.ToString();

            if (!Regex.Match(inputSymbol, @"[а-я]|\.|,").Success)
            {
                e.Handled= true;
            }
           
        }

        private void textBoxLastNameInCreateStudentWindow_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            string inputSymbol = e.Text.ToString();

            if (!Regex.Match(inputSymbol, @"[а-я]|\.|,").Success)
            {
                e.Handled = true;
            }
        }
    }
}
