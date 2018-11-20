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
using System.Windows.Shapes;
using System.Data.SqlClient;



namespace SchoolDateBaseWPF
{
    public partial class CreateWindow : Window
    {
       // string connectionString;
        SqlConnection connection; 
        public CreateWindow(SqlConnection connection)
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            if (textBoxFirstName.Text != "" && textBoxLastName.Text != "" && textBoxClass.Text != "")
            {               
                string sql = "INSERT INTO TableStudents" +
               $"(FullName,Class) Values(@FullName, @Class)";
                connection.Open();
             
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {               
                    cmd.Parameters.AddWithValue("@FullName", $"{ textBoxFirstName.Text}" +" "+ $"{textBoxLastName.Text}");
                    cmd.Parameters.AddWithValue("@Class", $"{ textBoxClass.Text}");
                    cmd.ExecuteNonQuery();
                }                 
                
                labelCreateError.Visibility = Visibility;
                labelCreateError.Content = "student successfully added";


                sql = "INSERT INTO TablePerformance" +
                $"(NameStudent) Values(@NameStudent)";
              

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@NameStudent", $"{ textBoxFirstName.Text}" + " " + $"{textBoxLastName.Text}");

                    cmd.ExecuteNonQuery();
                }
            }
            else
            {
                labelCreateError.Visibility = Visibility;
                labelCreateError.Content = "need to fill in all fields";
            }
            connection.Close();


        }
    }
}
