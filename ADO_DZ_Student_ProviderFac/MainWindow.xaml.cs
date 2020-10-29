using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
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

namespace ADO_DZ_Student
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string connectionString = ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString;
        static string provider = ConfigurationManager.AppSettings["provider"];
        static DbProviderFactory factory = DbProviderFactories.GetFactory(provider);

        private static ObservableCollection<Student> students_list = new ObservableCollection<Student>();

        public MainWindow()
        {
            InitializeComponent();

            DownloadDate();

        }

        public void DownloadDate()
        {
            string sql = "SELECT * FROM Student";
            using (DbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();
             
                DbCommand command = factory.CreateCommand();
                command.CommandText = "SELECT * FROM Student";
                command.Connection = connection;


                DbDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        students_list.Add(new Student
                        {
                            Id = (int)reader["Id"],
                            Name = (string)reader["Name"],
                            Surname = (string)reader["Surname"],
                            IdGroup = reader["IdGroup"] as Nullable<int>


                        });

                    }
                }

                reader.Close();
                lb_Students.ItemsSource = students_list;

            }
        }


        private void Button_Click(object sender, RoutedEventArgs e) //Add
        {

            Window1 w1 = new Window1();
            w1.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) //Delete
        {
            DeleteStudent();
        }

        private void DeleteStudent()
        {
             int index = lb_Students.SelectedIndex;
             int ID= students_list[index].Id;
            students_list.Remove(students_list[index]);
            using (DbConnection conn = factory.CreateConnection())
            {
                conn.ConnectionString = connectionString;
                string cmdText = $@"Delete from Student where Id={ID}";
              //  SqlCommand command = new SqlCommand(cmdText, conn);

                DbCommand command = factory.CreateCommand();
                command.CommandText = cmdText;
                command.Connection = conn;

                conn.Open();

                int rows = command.ExecuteNonQuery();

            }

        }

        private void Button_Click_2(object sender, RoutedEventArgs e) //Update
        {
          
        }

        private void Button_Click_3(object sender, RoutedEventArgs e) //Update List
        {
            students_list.Clear();
            DownloadDate();
        }
    }
}
