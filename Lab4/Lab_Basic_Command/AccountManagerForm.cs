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
    public partial class AccountManagerForm : Form
    {
        public AccountManagerForm()
        {
            InitializeComponent();
        }

        private void AccountManagerForm_Load(object sender, EventArgs e)
        {
            LoadAccounts();
        }

        private void LoadAccounts()
        {
            string connectionString = "Server=DALATLAPTOP\\SQLEXPRESS;Database=RestaurantManagement;Integrated Security=True";

            string query = @"SELECT a.AccountName, a.FullName, a.Email, a.DateCreated 
                        FROM Account a";

            SqlDataAdapter adapter = new SqlDataAdapter(query, connectionString);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            dgvAccounts.DataSource = dt;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=DALATLAPTOP\\SQLEXPRESS;Database=RestaurantManagement;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Lấy dòng MỚI NHẤT từ DataGridView (dòng có dấu *)
                DataGridViewRow newRow = dgvAccounts.Rows[dgvAccounts.Rows.Count - 2];

                string accountName = newRow.Cells["AccountName"].Value.ToString();
                string fullName = newRow.Cells["FullName"].Value.ToString();
                string email = newRow.Cells["Email"].Value.ToString();

                string query = "INSERT INTO Account (AccountName, Password, FullName, Email, DateCreated) VALUES (@AccountName, @Password, @FullName, @Email, GETDATE())";

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@AccountName", accountName);
                cmd.Parameters.AddWithValue("@Password", "123456"); // Mật khẩu mặc định
                cmd.Parameters.AddWithValue("@FullName", fullName);
                cmd.Parameters.AddWithValue("@Email", email);

                connection.Open();
                cmd.ExecuteNonQuery();

                MessageBox.Show("Thêm tài khoản thành công!");
                LoadAccounts();
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=DALATLAPTOP\\SQLEXPRESS;Database=RestaurantManagement;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string accountName = dgvAccounts.CurrentRow.Cells["AccountName"].Value.ToString();
                string fullName = dgvAccounts.CurrentRow.Cells["FullName"].Value.ToString();
                string email = dgvAccounts.CurrentRow.Cells["Email"].Value.ToString();

                string query = "UPDATE Account SET FullName = @FullName, Email = @Email WHERE AccountName = @AccountName";

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@FullName", fullName);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@AccountName", accountName);

                connection.Open();
                cmd.ExecuteNonQuery();

                MessageBox.Show("Cập nhật thành công!");
                LoadAccounts();
            }
        }

        private void tsmXoaTaiKhoan_Click(object sender, EventArgs e)
        {
            if (dgvAccounts.SelectedRows.Count > 0)
            {
                string accountName = dgvAccounts.SelectedRows[0].Cells["AccountName"].Value.ToString();

                string connectionString = "Server=DALATLAPTOP\\SQLEXPRESS;Database=RestaurantManagement;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // 1. XÓA khỏi RoleAccount trước (khóa ngoại)
                    string query1 = "DELETE FROM RoleAccount WHERE AccountName = @AccountName";
                    SqlCommand cmd1 = new SqlCommand(query1, connection);
                    cmd1.Parameters.AddWithValue("@AccountName", accountName);
                    cmd1.ExecuteNonQuery();

                    // 2. XÓA khỏi Account (khóa chính)
                    string query2 = "DELETE FROM Account WHERE AccountName = @AccountName";
                    SqlCommand cmd2 = new SqlCommand(query2, connection);
                    cmd2.Parameters.AddWithValue("@AccountName", accountName);
                    cmd2.ExecuteNonQuery();

                    // 3. XÓA khỏi DataGridView
                    dgvAccounts.Rows.Remove(dgvAccounts.SelectedRows[0]);

                    MessageBox.Show("Đã xóa hoàn toàn tài khoản: " + accountName);
                }
            }
        }

        private void tsmXemVaiTro_Click(object sender, EventArgs e)
        {
            if (dgvAccounts.SelectedRows.Count > 0)
            {
                string accountName = dgvAccounts.SelectedRows[0].Cells["AccountName"].Value.ToString();

                RoleListForm roleForm = new RoleListForm();
                roleForm.LoadRoles(accountName);
                roleForm.ShowDialog();
            }
        }

    }
}