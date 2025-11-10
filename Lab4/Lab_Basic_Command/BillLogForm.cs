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
    public partial class BillLogForm : Form
    {
        public BillLogForm()
        {
            InitializeComponent();
            LoadBillLog();
        }

        private void LoadBillLog()
        {
            string connectionString = "Server=DALATLAPTOP\\SQLEXPRESS;Database=RestaurantManagement;Integrated Security=True";

            string query = @"SELECT b.ID, b.CheckoutDate, b.Account, b.Amount, b.Discount 
                       FROM Bills b 
                       WHERE b.CheckoutDate IS NOT NULL";

            SqlDataAdapter adapter = new SqlDataAdapter(query, connectionString);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            dgvBillLog.DataSource = dt;

            // Tính tổng
            int totalBills = dt.Rows.Count;
            int totalAmount = 0;
            int totalDiscount = 0;

            foreach (DataRow row in dt.Rows)
            {
                totalAmount += Convert.ToInt32(row["Amount"]);
                totalDiscount += (int)(Convert.ToDouble(row["Discount"]) * Convert.ToInt32(row["Amount"]));
            }

            lblTongHoaDon.Text = totalBills.ToString();
            lblTongTien.Text = totalAmount.ToString("N0");
            lblGiamGia.Text = totalDiscount.ToString("N0");
        }

    }
}
