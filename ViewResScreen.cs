using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace airline_reservation_system
{
    public partial class ViewResScreen : Form
    {
        public ViewResScreen()
        {
            InitializeComponent();
            loadDataGrid();
        }

        private void loadDataGrid()
        {
            try
            {
                FunctionsClass functions = new FunctionsClass();

                SqlConnection connection = new SqlConnection(functions.connectionString);
                connection.Open();

                string query = "SELECT * FROM OrderTable";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);

                DataSet dataSet = new DataSet();

                dataAdapter.Fill(dataSet, "OrderTable");
                dataGridView1.DataSource = dataSet.Tables["OrderTable"].DefaultView;

                connection.Close();
            }
            catch (Exception ex)
            {
                new PopupMessage("Data fetch unsuccessful" + ex).ShowDialog();
            }
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            if (textBoxName.Text != "")
            {
                string name = textBoxName.Text;

                FunctionsClass functions = new FunctionsClass();

                try
                {
                    SqlConnection connection = new SqlConnection(functions.connectionString);
                    connection.Open();

                    string query2 = "SELECT * FROM OrderTable WHERE Name='" + name + "'";
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query2, connection);

                    DataSet dataSet = new DataSet();

                    dataAdapter.Fill(dataSet, "OrderTable");
                    dataGridView1.DataSource = dataSet.Tables["OrderTable"].DefaultView;

                    connection.Close();
                }
                catch(Exception ex)
                {
                    new PopupMessage("Search unsuccessful!" + ex).ShowDialog();
                }
                finally
                {
                    textBoxName.Text = "";
                }
            }
            else
            {
                new PopupMessage("Enter your name!").ShowDialog();
            }
        }

        private void refreshAllButton_Click(object sender, EventArgs e)
        {
            loadDataGrid();
        }
    }
}
