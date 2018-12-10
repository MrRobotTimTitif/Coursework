using System;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Data.SqlClient;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Text.RegularExpressions;

namespace SchoolDateBaseWPF
{
    public partial class MainWindow : Window
    {

        SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=SchoolDataBase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        public MainWindow()
        {
            InitializeComponent();         
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ShowStudents();
            ShowTeachers();
            ShowPerformance();
        }

        public void ShowStudents()
        {
            DataContext dataContext = new DataContext(connection);
            Table<TableStudents> tableStudents = dataContext.GetTable<TableStudents>();
            dataGridRigthContentStudents.ItemsSource = tableStudents;
        }
        public void ShowTeachers()
        {
            DataContext dataContext = new DataContext(connection);
            Table<TableTeachers> tableTeachers = dataContext.GetTable<TableTeachers>();
            dataGridRigthContentTechers.ItemsSource = tableTeachers;
        }
        public void ShowPerformance()
        {
            DataContext dataContext = new DataContext(connection);
            Table<TablePerformance> tablePerformance = dataContext.GetTable<TablePerformance>();
            dataGridPerformance.ItemsSource = tablePerformance;
        }

        private void createStudent_Click(object sender, RoutedEventArgs e)
        {
            CreateStudentWindow createWindow = new CreateStudentWindow(connection);
            createWindow.Show();
            Close();
        }
      
        private void buttonDeleteStudent_Click(object sender, RoutedEventArgs e)
        {
            bool success = Int32.TryParse(textBoxIdDelete.Text, out int number);
            if (textBoxIdDelete.Text != " " && success == true)
            {
                DataContext dataContext = new DataContext(connection);
              
                var deleteStudent =from del in dataContext.GetTable<TableStudents>()
                                        where del.Id == Convert.ToInt32(textBoxIdDelete.Text)
                                        select del;
                
                foreach (var detail in deleteStudent)
                {
                    dataContext.GetTable<TableStudents>().DeleteOnSubmit(detail);
                }
                dataContext.SubmitChanges();

                var deletePerformance = from del in dataContext.GetTable<TablePerformance>()
                                         where del.Id == Convert.ToInt32(textBoxIdDelete.Text)
                                         select del;

                foreach (var detail in deletePerformance)
                {
                    dataContext.GetTable<TablePerformance>().DeleteOnSubmit(detail);
                }
                dataContext.SubmitChanges();

                testlabel.Content = "Deleted";
                ShowStudents();
                ShowPerformance();
                #region sqlDeleteStud

                //string sql = "DELETE  FROM TableStudents WHERE Id=@ID";
                //connection.Open();
                //using (SqlCommand cmd = new SqlCommand(sql, connection))
                //{
                //    cmd.Parameters.AddWithValue("@Id", textBoxIdDelete.Text);
                //    cmd.ExecuteNonQuery();

                //}
                //sql = "DELETE  FROM TablePerformance WHERE Id=@ID";
                //using (SqlCommand cmd = new SqlCommand(sql, connection))
                //{
                //    cmd.Parameters.AddWithValue("@Id", textBoxIdDelete.Text);
                //    cmd.ExecuteNonQuery();

                //}
                //connection.Close();
                #endregion

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
                #region sqlTeavherDel
                //string sql = "DELETE  FROM TableTeachers WHERE Id=@ID";
                //connection.Open();
                //using (SqlCommand cmd = new SqlCommand(sql, connection))
                //{
                //    cmd.Parameters.AddWithValue("@Id", textBoxIdDeleteTeacher.Text);
                //    cmd.ExecuteNonQuery();
                //    connection.Close();
                //}
                //connection.Close();
                //testlabelteacher.Content = "";
                #endregion

                DataContext dataContext = new DataContext(connection);

                var deleteTeachers = from del in dataContext.GetTable<TableTeachers>()
                                    where del.Id == Convert.ToInt32(textBoxIdDeleteTeacher.Text)
                                    select del;

                foreach (var detail in deleteTeachers)
                {
                    dataContext.GetTable<TableTeachers>().DeleteOnSubmit(detail);
                }
                dataContext.SubmitChanges();
                testlabelteacher.Content = "Deleted";
                ShowTeachers();
            }
            else
            {
                testlabelteacher.Content = "Error!!!";
            }

        }

        private void buttonSetMark_Click(object sender, RoutedEventArgs e)
        {
            string subjectString1, subjectString2, subjectString3;

            subjectString1 = (textBoxSetMarkMaths.Text != "") ? "Maths = @Maths" : "Maths = Maths";
            subjectString2 = (textBoxSetMarkPhysics.Text != "") ? "Physics = @Physics" : "Physics = Physics";
            subjectString3 = (textBoxSetMarkBiology.Text != "") ? "Biology = @Biology" : "Biology = Biology";


            string sql = $"UPDATE TablePerformance SET {subjectString1},{subjectString2},{subjectString3} WHERE Id=@ID";

            connection.Open();
            using (SqlCommand cmd = new SqlCommand(sql, connection))
            {

                cmd.Parameters.AddWithValue("@Id", textBoxIdStudent.Text);
                cmd.Parameters.AddWithValue("@Maths", textBoxSetMarkMaths.Text);
                cmd.Parameters.AddWithValue("@Physics", textBoxSetMarkPhysics.Text);
                cmd.Parameters.AddWithValue("@Biology", textBoxSetMarkBiology.Text);

                cmd.ExecuteNonQuery();
            }
            connection.Close();
            ShowPerformance(); 
        }

        [Table(Name = "TableStudents")]
        public class TableStudents
        {
            [Column(IsPrimaryKey = true, IsDbGenerated = true)]
            public int Id { get; set; }
            [Column]
            public string FullName { get; set; }
            [Column]
            public string Class { get; set; }
        }      
        [Table(Name = "TableTeachers")]
        public class TableTeachers
        {
            [Column(IsPrimaryKey = true, IsDbGenerated = true)]
            public int Id { get; set; }
            [Column]
            public string FullName { get; set; }
            [Column]
            public string Subject { get; set; }
            [Column]
            public int Classroom { get; set; }

        }
        [Table(Name = "TablePerformance")]
        public class TablePerformance
        {
            [Column(IsPrimaryKey = true)]
            public int Id { get; set; }
            [Column]
            public string NameStudent { get; set; }
            [Column]
            public int Maths { get; set; }
            [Column]
            public int Physics { get; set; }
            [Column]
            public int Biology { get; set; }

        }

        private void buttonPoPredmety_Click(object sender, RoutedEventArgs e)
        {
            DataContext dataContext = new DataContext(connection);          
            if (comboBoxSubgect.Text == "Maths")
            {

                Table<TablePerformance> tablePerformance = dataContext.GetTable<TablePerformance>();
                var result = from x in dataContext.GetTable<TablePerformance>()
                             select new { x.NameStudent, x.Maths };
                dataGridFilterPerformance.ItemsSource = result;
            }
            if (comboBoxSubgect.Text == "Physics")
            {
                Table<TablePerformance> tablePerformance = dataContext.GetTable<TablePerformance>();
               var result = from x in dataContext.GetTable<TablePerformance>()
                         select new { x.NameStudent, x.Physics };
                dataGridFilterPerformance.ItemsSource = result;
            }
            if (comboBoxSubgect.Text == "Biology")
            {
                Table<TablePerformance> tablePerformance = dataContext.GetTable<TablePerformance>();
                var result = from x in dataContext.GetTable<TablePerformance>()
                             select new { x.NameStudent, x.Biology };
                dataGridFilterPerformance.ItemsSource = result;
            }     
        }    
        private void textBoxIdStudent_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
                e.Handled = validationNumber(e);
           
        }

        private void buttonLowSkillOnSubject_Click(object sender, RoutedEventArgs e)
        {
            DataContext dataContext = new DataContext(connection);
          
            Table<TablePerformance> tablePerformance = dataContext.GetTable<TablePerformance>();
            var result = from x in dataContext.GetTable<TablePerformance>()
                         where x.Maths < 3 || x.Physics < 3 || x.Biology < 3
                         select x;
            dataGridFilterPerformance.ItemsSource = result;


        }





        private void textBoxSetMarkMaths_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            setMarkText(e);
        }

        private void textBoxSetMarkPhysics_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            setMarkText(e);
        }

        private void textBoxSetMarkBiology_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            setMarkText(e);
        }

        private void setMarkText(TextCompositionEventArgs e)
        { 
                e.Handled = validationNumber(e);           
        }

        private void buttonLowMarksSubject_Click(object sender, RoutedEventArgs e)
        {
            DataContext dataContext = new DataContext(connection);
   
            Table<TablePerformance> tablePerformance = dataContext.GetTable<TablePerformance>();
            var resultOnMatsh = from x in dataContext.GetTable<TablePerformance>()
                                select x.Maths;
            int marksMaths = 0;
            foreach (var o in resultOnMatsh)
            {
                marksMaths += Convert.ToInt32(o);
            }

            var resultOnPhysics = from x in dataContext.GetTable<TablePerformance>()
                                  select x.Physics;
            int marksPhysics = 0;
            foreach (var o in resultOnPhysics)
            {
                marksPhysics += Convert.ToInt32(o);
            }
            var resultOnBiology = from x in dataContext.GetTable<TablePerformance>()
                                  select x.Biology;
            int marksBiology = 0;
            foreach (var o in resultOnBiology)
            {
                marksBiology += Convert.ToInt32(o);
            }

           
            string subjectVariant = "";
            if (marksMaths < marksPhysics && marksMaths < marksBiology)
            {
                subjectVariant = "Maths";
            };
            if (marksPhysics < marksMaths && marksPhysics < marksBiology)
            {
                subjectVariant = "Physics";
            };
            if (marksBiology < marksMaths && marksBiology < marksPhysics)
            {
                subjectVariant = "Biology";
            };

            var resultTeacher = from x in dataContext.GetTable<TableTeachers>()
                                where x.Subject == Convert.ToString(subjectVariant)
                                select new {x.FullName,x.Subject};

            dataGridFilterPerformance.ItemsSource = resultTeacher;

        }

        private void buttonAverageMarks_Click(object sender, RoutedEventArgs e)
        {
            DataContext dataContext = new DataContext(connection);
           
            var resultA = from x in dataContext.GetTable<TableStudents>()
                          where x.Class == comboBoxAverageMarksInClass.Text
                          select new {x.Id};

            var marksA = from x in dataContext.GetTable<TablePerformance>()
                         select new {x.Id, x.Maths, x.Physics, x.Biology };
            
            double averageA = 0;
            int countA = 0;
            foreach (var a in resultA)
            {
                foreach (var o in marksA)
                {
                    if (o.Id == a.Id)
                    {
                        averageA += Convert.ToDouble(o.Maths + o.Physics + o.Biology) / 3;
                        countA++;
                    }
                }
            }
           
            labelAverageMarksInClass.Content= String.Format("{0,12:F4}", averageA = Convert.ToDouble(averageA / countA));

      
        }

        private void buttonBestClassOnMark_Click(object sender, RoutedEventArgs e)
        {
            GetMarkClass("Best");
        }

        private void buttonTheWorstClassOnMark_Click(object sender, RoutedEventArgs e)
        {
            GetMarkClass("Worst");
        }

        public void GetMarkClass(string variant)
        {
            DataContext dataContext = new DataContext(connection);

            var resultA = from x in dataContext.GetTable<TableStudents>()
                          where x.Class == "A"
                          select new { x.Id };
            var resultB = from x in dataContext.GetTable<TableStudents>()
                          where x.Class == "B"
                          select new { x.Id };
            var resultC = from x in dataContext.GetTable<TableStudents>()
                          where x.Class == "C"
                          select new { x.Id };
            var resultD = from x in dataContext.GetTable<TableStudents>()
                          where x.Class == "D"
                          select new { x.Id };
            var resultF = from x in dataContext.GetTable<TableStudents>()
                          where x.Class == "F"
                          select new { x.Id };


            var marksAll = from x in dataContext.GetTable<TablePerformance>()
                           select new { x.Id, x.Maths, x.Physics, x.Biology };

            int summMarkA = 0;
            int summMarkB = 0;
            int summMarkC = 0;
            int summMarkD = 0;
            int summMarkF = 0;
   
           
            foreach (var a in resultA)
            {
                foreach (var o in marksAll)
                {
                    if (o.Id == a.Id)
                    {
                        summMarkA += o.Maths + o.Physics + o.Biology;
                    }
                }
            }
            foreach (var a in resultB)
            {
                foreach (var o in marksAll)
                {
                    if (o.Id == a.Id)
                    {
                        summMarkB += o.Maths + o.Physics + o.Biology;
                    }
                }
            }
            foreach (var a in resultC)
            {
                foreach (var o in marksAll)
                {
                    if (o.Id == a.Id)
                    {
                        summMarkC += o.Maths + o.Physics + o.Biology;
                    }
                }
            }
            foreach (var a in resultD)
            {
                foreach (var o in marksAll)
                {
                    if (o.Id == a.Id)
                    {
                        summMarkD += o.Maths + o.Physics + o.Biology;
                    }
                }
            }
            foreach (var a in resultF)
            {
                foreach (var o in marksAll)
                {
                    if (o.Id == a.Id)
                    {
                        summMarkF += o.Maths + o.Physics + o.Biology;
                    }
                }
            }
            int[] massMarks = new int[5] { summMarkA, summMarkB, summMarkC, summMarkD, summMarkF };
            string[] massClassNumber = new string[5] { "A", "B", "C", "D", "F" };
           
            int max = massMarks[0];
            int min = massMarks[0];
            if (variant == "Best")
            {
                for (int i = 0; i < 5; i++)
                {
                    if (massMarks[i] > max)
                    {
                        max = massMarks[i];
                        labelBestClassOnMark.Content = massClassNumber[i];
                    }
                }
            }
            if (variant == "Worst")
            {
                for (int i = 0; i < 5; i++)
                {
                    if (massMarks[i] < min)
                    {
                        min = massMarks[i];
                        labelTheWorstClassOnMark.Content = massClassNumber[i];
                    }
                }
            }

        }

        private bool validationNumber(TextCompositionEventArgs e)
        {
            string inputSymbol = e.Text.ToString();

            if (!Regex.Match(inputSymbol, @"[0-9]|\.|,").Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void textBoxIdDelete_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            
                e.Handled = validationNumber(e);
            
        }

        private void textBoxIdDeleteTeacher_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            validationNumber(e);
        }
    }
}
    
