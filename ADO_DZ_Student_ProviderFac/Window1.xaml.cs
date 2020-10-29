using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
using System.Windows.Shapes;

namespace ADO_DZ_Student
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        //  static string connectionString = ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString;
       static string provider = ConfigurationManager.AppSettings["provider"];
        string connectionString = ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString;
        static    DbProviderFactory factory = DbProviderFactories.GetFactory(provider);

        public Window1()
        {
            InitializeComponent();
            List<int?> list_IdGroup = new List<int?>();

            using (DbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();

               
               
                DbCommand command = factory.CreateCommand();
                command.CommandText = "SELECT IdGroup FROM Student";
                command.Connection = connection;

                DbDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (!list_IdGroup.Exists(x => x == reader["IdGroup"] as Nullable<int>))
                            list_IdGroup.Add(reader["IdGroup"] as Nullable<int>);

                    }
                }
                reader.Close();


                cb_IdGroup.ItemsSource = list_IdGroup;

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Student newStudent = new Student()
            {
                Name = txtName.Text,
                Surname = txtSurname.Text,
                IdGroup = Convert.ToInt32(cb_IdGroup.Text)
            };
            using (DbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();

          
               // connection.ConnectionString = connectionString;
              //  connection.Open();
                AddStudent(newStudent, connection);
            }
            this.Close();
        }

       

        static void AddStudent(Student student, DbConnection conn)
        {
           // string cmdText = @"insert Student values (@name, @surname, @idGroup)";

            DbCommand command = factory.CreateCommand();
            command.CommandText = @"insert into Student values (@name, @surname, @idGroup)";
            command.Connection = conn;

            IDataParameter dp;
                dp = command.CreateParameter();
                dp.ParameterName = "@name";
                dp.Value = student.Name;
                command.Parameters.Add(dp);

            IDataParameter dp1;
            dp1 = command.CreateParameter();
            dp1.ParameterName = "@surname";
            dp1.Value = student.Surname;
            command.Parameters.Add(dp1);

            IDataParameter dp2;
            dp2 = command.CreateParameter();
            dp2.ParameterName = "@idGroup";
            dp2.Value = student.IdGroup;
            command.Parameters.Add(dp2);

          
            int rows = command.ExecuteNonQuery();
        }
    }
}
