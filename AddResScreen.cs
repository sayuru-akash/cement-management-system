using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace airline_reservation_system
{
    public partial class AddResScreen : Form
    {
        public AddResScreen()
        {
            InitializeComponent();
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            if(textBoxName.Text!="" && textBoxLocation.Text!="" && cementTypeList.Text!="" && textBoxQuantity.Text!="")
            {
                string name = textBoxName.Text;
                string location = textBoxLocation.Text;
                string cementType = cementTypeList.Text;
                string date = datePicker.Value.ToShortDateString();
                int quantity = int.Parse(textBoxQuantity.Text);
                

                FunctionsClass functions = new FunctionsClass();


                    try
                    {
                        SqlConnection connection = new SqlConnection(functions.connectionString);
                        string query = "INSERT INTO OrderTable (Name, Quantity, Location, CementType, DeliveryDate) VALUES ('" + name + "','" + quantity + "','" + location + "','" + cementType + "','" + date + "')";
                        SqlCommand command = new SqlCommand(query, connection);
                        connection.Open();
                        command.ExecuteNonQuery();
                        int orderNum;
                        string query2 = "SELECT MAX(OrderID) FROM OrderTable";
                        using (SqlCommand command2 = new SqlCommand(query2, connection))
                        {
                            using (SqlDataReader reader = command2.ExecuteReader())
                            {
                                reader.Read();
                                orderNum = reader.GetInt32(0);
                                reader.Close();
                            }
                        }
                        new PopupMessage("Order placed successfully! Your Order Number is " + orderNum).ShowDialog();
                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        new PopupMessage("Error making order!" + ex).ShowDialog();
                    }
                    finally
                    {
                        textBoxName.Text = "";
                        textBoxLocation.Text = "";
                        cementTypeList.Text = "";
                        textBoxQuantity.Text = "";
                    }
                
            }
            else
            {
                new PopupMessage("Enter all required information!").ShowDialog();
            }
        }
    }
}