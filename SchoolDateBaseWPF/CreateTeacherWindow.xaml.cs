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
using System.Data.Sql;
using System.Data.SqlClient;

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

            if (textBoxFirstName.Text != "" && textBoxLastName.Text != "" && textBoxClassRoom.Text != "")
            {
                string sql = "INSERT INTO TableTeachers" +
               $"(FullName,Subject,ClassRoom) Values(@FullName,@Subject, @ClassRoom)";
                connection.Open();

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@FullName", $"{ textBoxFirstName.Text}" + " " + $"{textBoxLastName.Text}");
                    cmd.Parameters.AddWithValue("@Subject", $"{ textBoxSubject.Text}");
                    cmd.Parameters.AddWithValue("@ClassRoom", $"{ Convert.ToInt32(textBoxClassRoom.Text)}");
                    cmd.ExecuteNonQuery();
                }

                labelCreateError.Visibility = Visibility;
     


            }
            connection.Close();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            Close();
        }
    }
}
