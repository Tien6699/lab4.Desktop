using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_Basic_Command
{
    public partial class BillDetailsForm : Form
    {
        public BillDetailsForm()
        {
            InitializeComponent();
        }

        public void LoadBillDetails(int billID)
        {
            string connectionString = "Server=DALATLAPTOP\\SQLEXPRESS;Database=RestaurantManagement;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"SELECT f.Name as FoodName, bd.Quantity, f.Price, 
                            f.Price * bd.Quantity as Amount
                     FROM BillDetails bd, Food f 
                     WHERE bd.FoodID = f.ID AND bd.InvoiceID = " + billID;

                SqlDataAdapter adapter = new SqlDataAdapter(query, connectionString);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dgvBillDetails.DataSource = dt;
                this.Text = "Chi tiết hóa đơn #" + billID;
            }
        }


    }
}
