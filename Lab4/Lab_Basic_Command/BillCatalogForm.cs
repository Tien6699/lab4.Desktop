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
    public partial class BillCatalogForm : Form
    {
        public BillCatalogForm()
        {
            InitializeComponent();
        }

        public void LoadBillsByTable(string tableID)
        {
            string connectionString = "Server=DALATLAPTOP\\SQLEXPRESS;Database=RestaurantManagement;Integrated Security=True";

            // Load danh sách ngày vào ListBox - format thành dd/MM/yyyy
            string queryDates = "SELECT DISTINCT CheckoutDate FROM Bills WHERE TableID = " + tableID + " AND CheckoutDate IS NOT NULL";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapterDates = new SqlDataAdapter(queryDates, connection);
                DataTable dtDates = new DataTable();
                adapterDates.Fill(dtDates);

                foreach (DataRow row in dtDates.Rows)
                {
                    // Format thành dd/MM/yyyy để khớp với query
                    DateTime date = Convert.ToDateTime(row["CheckoutDate"]);
                    lbxNgayLap.Items.Add(date.ToString("dd/MM/yyyy"));
                }
            }

            this.Text = "Danh mục hóa đơn - Bàn " + tableID;
        }

        private void lbxNgayLap_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbxNgayLap.SelectedItem != null)
            {
                string selectedDate = lbxNgayLap.SelectedItem.ToString();
                DateTime date = DateTime.ParseExact(selectedDate, "dd/MM/yyyy", null);

                string connectionString = "Server=DALATLAPTOP\\SQLEXPRESS;Database=RestaurantManagement;Integrated Security=True";
                string query = @"SELECT f.Name, bd.Quantity, f.Price, f.Price * bd.Quantity as Amount 
                       FROM Bills b, BillDetails bd, Food f 
                       WHERE b.ID = bd.InvoiceID AND bd.FoodID = f.ID 
                       AND b.CheckoutDate = @SelectedDate";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@SelectedDate", date);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvBillDetails.DataSource = dt;
                }
            }
        }
    }
}
