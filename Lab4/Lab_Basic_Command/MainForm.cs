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
using System.Xml.Linq;

namespace Lab_Basic_Command
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadTables();
        }
        private void LoadTables()
        {
            string connectionString = "Server=DALATLAPTOP\\SQLEXPRESS;Database=RestaurantManagement;Integrated Security=True";
            string query = "SELECT ID, Name, Status, Capacity FROM [Table]";

            SqlDataAdapter adapter = new SqlDataAdapter(query, connectionString);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            dgvTables.DataSource = dt;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            // Lấy dòng MỚI NHẤT từ DataGridView
            DataGridViewRow newRow = dgvTables.Rows[dgvTables.Rows.Count - 2];

            string name = newRow.Cells["Name"].Value.ToString();
            string status = newRow.Cells["Status"].Value.ToString();
            string capacity = newRow.Cells["Capacity"].Value.ToString();

            string connectionString = "Server=DALATLAPTOP\\SQLEXPRESS;Database=RestaurantManagement;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO [Table] (Name, Status, Capacity) VALUES (@Name, @Status, @Capacity)";

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@Capacity", capacity);

                connection.Open();
                cmd.ExecuteNonQuery();
                
            }

            MessageBox.Show("Đã cập nhật thành công!");
            LoadTables();
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=DALATLAPTOP\\SQLEXPRESS;Database=RestaurantManagement;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM [Table]", connection);
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

                DataTable dt = (DataTable)dgvTables.DataSource;
                adapter.Update(dt);

                MessageBox.Show("Đã thêm thành công!");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvTables.SelectedRows.Count > 0)
            {
                string id = dgvTables.SelectedRows[0].Cells["ID"].Value.ToString();
                string name = dgvTables.SelectedRows[0].Cells["Name"].Value.ToString();

                string connectionString = "Server=DALATLAPTOP\\SQLEXPRESS;Database=RestaurantManagement;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM [Table] WHERE ID = @ID";

                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@ID", id);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show($"Đã xóa bàn: {name}");
                LoadTables();
            }
        }

        private void tsmXoa_Click(object sender, EventArgs e)
        {
            if(dgvTables.SelectedRows.Count > 0)
                btnXoa.PerformClick();
        }

        private void tsmXemDanhMuc_Click(object sender, EventArgs e)
        {
            if (dgvTables.SelectedRows.Count > 0)
            {
                string tableID = dgvTables.SelectedRows[0].Cells["ID"].Value.ToString();
                BillCatalogForm catalogForm = new BillCatalogForm();
                catalogForm.LoadBillsByTable(tableID);
                catalogForm.ShowDialog();
            }
        }

        private void xemNhatKyHoaDon_Click(object sender, EventArgs e)
        {
            BillLogForm logForm = new BillLogForm();
            logForm.ShowDialog();
        }

        private void nhómMónĂnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CategoryForm categoryForm = new CategoryForm();
            categoryForm.ShowDialog();
        }

        private void hóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BillsForm billsForm = new BillsForm();
            billsForm.ShowDialog();
        }

        private void quảnLýTàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AccountManagerForm accountManagerForm = new AccountManagerForm();
            accountManagerForm.ShowDialog();
        }
    }
}
