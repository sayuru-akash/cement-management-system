using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace airline_reservation_system
{
    public partial class RescheduleResScreen : Form
    {
        public RescheduleResScreen()
        {
            InitializeComponent();
        }


        private void validateButton_Click(object sender, EventArgs e)
        {
            if (textBoxOID.Text != "")
            {
                int orderID = int.Parse(textBoxOID.Text);
                int orderNum = 0;
                FunctionsClass functions = new FunctionsClass();

                try
                {
                    SqlConnection connection = new SqlConnection(functions.connectionString);
                    connection.Open();
                    string query1 = @"SELECT OrderID FROM OrderTable WHERE orderID LIKE '" + orderID + "'";
                    try
                    {
                        using (SqlCommand command1 = new SqlCommand(query1, connection))
                        {
                            using (SqlDataReader reader = command1.ExecuteReader())
                            {
                                reader.Read();
                                orderNum = reader.GetInt32(0);
                                reader.Close();
                            }
                        }
                        if (orderNum != 0)
                        {
                            new PopupMessage("Order Validated Successfully!").ShowDialog();
                        }
                        connection.Close();
                    }
                    catch
                    {
                        new PopupMessage("Order is invalid!").ShowDialog();
                    }
                }
                catch (Exception ex)
                {
                    new PopupMessage("Validation Error!" + ex).ShowDialog();
                }
            }
            else
            {
                new PopupMessage("Enter Order ID!").ShowDialog();
            }
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            int orderID = int.Parse(textBoxOID.Text);
            string cementType = cementTypeList.Text;
            int quantity = int.Parse(textBoxQuantity.Text);

            FunctionsClass functions = new FunctionsClass();

            if (textBoxOID.Text != "" && cementTypeList.Text != "")
            {
                    try
                    {
                        SqlConnection connection = new SqlConnection(functions.connectionString);
                        string query2 = @"UPDATE OrderTable SET CementType = '" + cementType + "', Quantity= '" + quantity + "' WHERE OrderID = '" + orderID + "'";
                        SqlCommand command = new SqlCommand(query2, connection);
                        connection.Open();
                        command.ExecuteNonQuery();
                        new PopupMessage("Order Update Success!").ShowDialog();
                    }
                    catch (Exception ex)
                    {
                        new PopupMessage("Order Update Error!" + ex).ShowDialog();
                    }
                
            }
            else
            {
                new PopupMessage("Enter all required information!").ShowDialog();
            }
        }
    }
}
