using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataSet_DataTable_ADO_New
{
    public partial class Form1 : Form
    {
            string connectionString = ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString;
        public Form1()
        {
            InitializeComponent();
           
            SqlConnection conn = new SqlConnection(connectionString);
            CreateTabPages(conn);
          //  dataGridView1.DataSource = table;
            if (conn != null)
                conn.Close();
        }
         
        private DataTable FillTable(DataTable table, SqlConnection conn)
        {
            SqlCommand command = new SqlCommand($"Select * from {tabControl1.SelectedTab.Text}", conn);

            try
            {
                conn.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        do
                        {
                            int line = 0;
                            while (reader.Read())
                            {
                                if (line == 0)
                                {
                                    for (int i = 0; i < reader.FieldCount; i++)
                                    {

                                        table.Columns.Add(reader.GetName(i));
                                    }
                                    line++;
                                }
                                DataRow r = table.NewRow();
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    r[i] = reader[i];
                                }
                                table.Rows.Add(r);
                            }
                        } while (reader.NextResult());
                    }

                }
            }
            catch (SqlException e)
            { }
                return table;
        }

        private void CreateTabPages(SqlConnection connection)
        {
            

           tabControl1.TabPages.Clear();
            tabControl1.TabPages.Add("Hello in our App");
            Label l = new Label();
            l.Text = "Hello in our App";
            l.Size = new Size(300, 100);
            l.TextAlign = ContentAlignment.MiddleCenter;
            l.BackColor = System.Drawing.Color.Green;
          
            


            tabControl1.TabPages[0].Controls.Add(l);
            try
            {
                    connection.Open();

                    SqlCommand command = new SqlCommand();
                    command.CommandText = "SELECT name FROM sys.tables";
                    command.Connection = connection;

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {

                            tabControl1.TabPages.Add(reader[0].ToString());
                            
                        }
                    }
                    reader.Close();
                connection.Close();

            }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            
        }
        private void Form1_Load(object sender, EventArgs e)
        {
           
        }
        DataTable tableX;
            int flag = 0;
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
                DataGridView dataGridView = new DataGridView();
                Size s = new Size(775, 365);
                dataGridView.Size = s;
         tableX = new DataTable();

            tableX.TableName = tabControl1.SelectedTab.Text;

                SqlConnection conn = new SqlConnection(connectionString);
            if (flag == 0)
            {
                // tableX = new DataTable();

                tableX = FillTable(tableX, conn);
                dataGridView.DataSource = tableX;
                tabControl1.SelectedTab.Controls.Add(dataGridView);
                if (conn != null)
                    conn.Close();
                flag++;
            }
            else
            {
                tableX = FillTable(tableX, conn);
                dataGridView.DataSource = tableX;
                tabControl1.SelectedTab.Controls.Add(dataGridView);
                if (conn != null)
                    conn.Close();
            }

        }

        private void btnBook_Click(object sender, EventArgs e) //Delete
        {
           // if (dataGridView1.SelectedIndex == -1)
             //   return;
        }

        private void btnSaveToXml_Click(object sender, EventArgs e)
        {
           tableX.WriteXml(tableX.TableName +".xml");

        }
    }
}
