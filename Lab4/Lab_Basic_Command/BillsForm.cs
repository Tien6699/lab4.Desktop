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
    public partial class BillsForm : Form
    {
        public BillsForm()
        {
            InitializeComponent();
        }

        private void BillsForm_Load(object sender, EventArgs e)
        {
            dtpTuNgay.Value = DateTime.Now.AddDays(-7);
            dtpDenNgay.Value = DateTime.Now;
            LoadBills();
        }

        private void LoadBills()
        {
            string connectionString = "Server=DALATLAPTOP\\SQLEXPRESS;Database=RestaurantManagement;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Bills WHERE CheckoutDate BETWEEN @FromDate AND @ToDate";

                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.SelectCommand.Parameters.AddWithValue("@FromDate", dtpTuNgay.Value.Date);
                adapter.SelectCommand.Parameters.AddWithValue("@ToDate", dtpDenNgay.Value.Date.AddDays(1));

                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dgvBills.DataSource = dt;

                // Tính tổng
                int totalAmount = 0;
                int totalDiscount = 0;

                foreach (DataRow row in dt.Rows)
                {
                    totalAmount += Convert.ToInt32(row["Amount"]);
                    totalDiscount += (int)(Convert.ToDouble(row["Discount"]) * Convert.ToInt32(row["Amount"]));
                }

                lblTongTien.Text = totalAmount.ToString("N0");
                lblGiamGia.Text = totalDiscount.ToString("N0");
                lblThucThu.Text = (totalAmount - totalDiscount).ToString("N0");
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            LoadBills();
        }

        private void dgvBills_DoubleClick(object sender, EventArgs e)
        {
            if (dgvBills.SelectedRows.Count > 0)
            {
                int billID = Convert.ToInt32(dgvBills.SelectedRows[0].Cells["ID"].Value);
                BillDetailsForm detailsForm = new BillDetailsForm();
                detailsForm.LoadBillDetails(billID);
                detailsForm.ShowDialog();
            }
        }


    }
}
