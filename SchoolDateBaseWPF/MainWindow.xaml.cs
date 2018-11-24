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
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Sql;
using System.Data;


namespace SchoolDateBaseWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// 
    /// 
    /*
    Требуется разработать программную систему, предназначенную для завуча школы. Она должна обеспечивать хранение сведений о каждом учителе, о предметах, которые он преподает, номере закрепленного за ним кабинета. Об учениках должны храниться следующие сведения: фамилия и имя, в каком классе учится, какую оценку имеет в текущей четверти по каждому предмету. Завуч должен иметь возможность добавить сведения о новом учителе или ученике, внести в базу данных четвертные оценки учеников каждого класса по каждому предмету, удалить данные об уволившемся учителе и отчисленном из школы ученике.  
    Учитель Ученик  Успеваемость
    Ключ    Ключ    Ключ
    ФИО     Ученик  Ученик
    Предмет     Класс Предмет 1 
    № кабинета Предмет 2 
           Предмет 3 
   Успеваемость задается числами от 2 до 5. Количество предметов может быть увеличено.
   Должны быть созданы обобщенные списки: 
•	Сведения об учителях.
•	Сведения об учениках.
•	Сведения об успеваемости.
   Завучу могут потребоваться следующие сведения о текущем состоянии успеваемости: 
•	Успеваемость по заданному предмету.
•	Количество неуспевающих учеников по всем классам. 
•	У какого учителя самая низкая успеваемость. 
•	Средняя оценка по всем предметам в каждом классе. 
•	Класс с самой высокой успеваемостью по всем предметам. 
•	Класс с самой низкой успеваемостью по всем предметам. 
   Завуч может выполнять следующие операции: 
•	Записать в школу нового ученика.
•	Отчислить из школы ученика. 


   /// */

    /// </summary>
    public partial class MainWindow : Window
    {

  
        SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=SchoolDataBase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        public MainWindow()
        {
            InitializeComponent();
        
        }

        private void createStudent_Click(object sender, RoutedEventArgs e)
        {
            CreateWindow createWindow = new CreateWindow(connection);
            createWindow.Show();
            Close();
        }

        #region testinOneWindow
        //  if (textBoxFirstName.Text != "" && textBoxLastName.Text != "" && textBoxClass.Text != "")
        //  {
        //string sqlcommand = $"INSERT INTO TableStudents (FullName,Class) VALUES (N'{textBoxFirstName.Text + " " + textBoxLastName.Text}',N'{textBoxClass.Text}');";
        // using 
        //string sqlcommand = $"INSERT INTO TableStudents (FullName,Class) VALUES ('Rick','Hentai');";
        //using (connection = new SqlConnection(connectionString))
        //{
        //        connection.Open();
        //        SqlCommand sqlCommand = new SqlCommand(sqlcommand, connection);
        //        sqlCommand.ExecuteNonQuery();
        //        connection.Close();
        //}
        //   labelCreateError.Visibility = Visibility;
        //  labelCreateError.Content = "student successfully added";
        //  }
        //  else
        //  {
        //     labelCreateError.Visibility = Visibility;
        //     labelCreateError.Content = "need to fill in all fields";
        //}


        //CreateWindow createwindow = new CreateWindow(connectionString);
        //createwindow.Show();
        //Close();

        #endregion

        private void buttonShowStudents_Click(object sender, RoutedEventArgs e)
        {
            gridRightContentStudents.Visibility = Visibility.Visible;
            gridRightContentTeachers.Visibility = Visibility.Hidden;
            gridRightContentPerformance.Visibility = Visibility.Hidden;

            using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM TableStudents", connection))
            {

                connection.Open();
                DataTable tableStudets = new DataTable();
                adapter.Fill(tableStudets);
                dataGridRigthContentStudents.ItemsSource = tableStudets.DefaultView;             
                connection.Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void buttonShowTeacher_Click(object sender, RoutedEventArgs e)
        {
            gridRightContentStudents.Visibility = Visibility.Hidden;
            gridRightContentTeachers.Visibility = Visibility.Visible;
            gridRightContentPerformance.Visibility = Visibility.Hidden;

            using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM TableTeachers", connection))

            {
                connection.Open();
                DataTable tableTeachers = new DataTable();
                adapter.Fill(tableTeachers);              
                dataGridRigthContentTechers.ItemsSource = tableTeachers.DefaultView;
                connection.Close();
            }
        }

        private void buttonShowPerformance_Click(object sender, RoutedEventArgs e)
        {
            gridRightContentStudents.Visibility = Visibility.Hidden;
            gridRightContentTeachers.Visibility = Visibility.Hidden;
            gridRightContentPerformance.Visibility = Visibility.Visible;
            using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM TablePerformance", connection))
            {
                connection.Open();
                DataTable tablePerformance = new DataTable();
                adapter.Fill(tablePerformance);
                dataGridRigthContentPerformance.ItemsSource = tablePerformance.DefaultView;
                connection.Close();

            }
        }

        

        private void buttonDeleteStudent_Click(object sender, RoutedEventArgs e)
        {


            bool success = Int32.TryParse(textBoxIdDelete.Text, out int number);


            if (textBoxIdDelete.Text != " " && success == true)
            {

                string sql = "DELETE  FROM TableStudents WHERE Id=@ID";
                connection.Open();
                //using (connection = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@Id", textBoxIdDelete.Text);
                    cmd.ExecuteNonQuery();
              
                }
                sql = "DELETE  FROM TablePerformance WHERE Id=@ID";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@Id", textBoxIdDelete.Text);
                    cmd.ExecuteNonQuery();
                 
                }
                connection.Close();
                testlabel.Content = "";
            }
            else
            {
                testlabel.Content = "Error!!!";
            }
                    
        }

        private void buttonCreateTeacher_Click(object sender, RoutedEventArgs e)
        {
            CreateTeacherWindow createTeacherWindow = new CreateTeacherWindow(connection);
            createTeacherWindow.Show();
            Close();
        }

        private void buttonDeleteTeacher_Click(object sender, RoutedEventArgs e)
        {
            bool success = Int32.TryParse(textBoxIdDeleteTeacher.Text, out int number);


            if (textBoxIdDeleteTeacher.Text != " " && success == true)
            {

                string sql = "DELETE  FROM TableTeachers WHERE Id=@ID";
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {


                    cmd.Parameters.AddWithValue("@Id", textBoxIdDeleteTeacher.Text);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
                connection.Close();
                testlabelteacher.Content = "";
            }
            else
            {
                testlabelteacher.Content = "Error!!!";
            }
          
        }

        private void buttonSetMark_Click(object sender, RoutedEventArgs e)
        {
            string sql = "UPDATE TablePerformance SET Subject1 = @Maths,Subject2 = @Physics,Subject3=@Biology WHERE Id=@ID";
            connection.Open();
            using (SqlCommand cmd = new SqlCommand(sql, connection))
            {


                cmd.Parameters.AddWithValue("@Id", textBoxIdStudent.Text);

                cmd.Parameters.AddWithValue("@Maths", textBoxSetMarkMaths.Text);
                cmd.Parameters.AddWithValue("@Physics", textBoxSetMarkPhysics.Text);
                cmd.Parameters.AddWithValue("@Biology", textBoxSetMarkBiology.Text);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            connection.Close();
            testlabelteacher.Content = "";
        }
    }
}
