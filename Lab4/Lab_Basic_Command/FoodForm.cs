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
    public partial class FoodForm : Form
    {
        public FoodForm()
        {
            InitializeComponent();
        }

        public void LoadFood(int categoryID)
        {
            string connectionString = "Server=DALATLAPTOP\\SQLEXPRESS;Database=RestaurantManagement;Integrated Security=True";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = sqlConnection.CreateCommand();

            // Thiết lập lệnh truy vấn cho đối tượng Command
            sqlCommand.CommandText = "SELECT Name FROM Category WHERE ID = " + categoryID;

            sqlConnection.Open();

            // Gán tên nhóm sản phẩm cho tiêu đề
            string catName = sqlCommand.ExecuteScalar().ToString();
            this.Text = "Danh sách các món ăn thuộc nhóm: " + catName;

            sqlCommand.CommandText = "SELECT * FROM Food WHERE FoodCategoryID = " + categoryID;

            // Tạo đối tượng DataAdapter
            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);

            // Tạo DataTable để chứa dữ liệu
            DataTable dt = new DataTable("Food");
            da.Fill(dt);

            dgvFood.Columns["ID"].DataPropertyName = "ID";
            dgvFood.Columns["FoodName"].DataPropertyName = "Name";
            dgvFood.Columns["Unit"].DataPropertyName = "Unit";
            dgvFood.Columns["FoodCategoryID"].DataPropertyName = "FoodCategoryID";
            dgvFood.Columns["Prices"].DataPropertyName = "Price";
            dgvFood.Columns["Notes"].DataPropertyName = "Notes";

            // Hiển thị danh sách món ăn lên Form
            dgvFood.DataSource = dt;


            // Đóng kết nối và giải phóng bộ nhớ
            sqlConnection.Close();
            sqlConnection.Dispose();
            da.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=DALATLAPTOP\\SQLEXPRESS;Database=RestaurantManagement;Integrated Security=True";
            DataTable dataTable = (DataTable)dgvFood.DataSource;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Food", connection);
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

                adapter.Update(dataTable);
                MessageBox.Show("Dữ liệu đã được lưu thành công!");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvFood.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa món ăn này?", "Xác nhận xóa",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    foreach (DataGridViewRow row in dgvFood.SelectedRows)
                    {
                        dgvFood.Rows.Remove(row);
                    }

                    MessageBox.Show("Đã xóa thành công! Nhấn Save để lưu thay đổi.");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn món ăn cần xóa!");
            }
        }



    }
}
