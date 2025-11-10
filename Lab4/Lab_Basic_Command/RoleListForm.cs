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
    public partial class RoleListForm : Form
    {
        public RoleListForm()
        {
            InitializeComponent();
        }

        public void LoadRoles(string accountName)
        {
            string connectionString = "Server=DALATLAPTOP\\SQLEXPRESS;Database=RestaurantManagement;Integrated Security=True";

            string query = "SELECT r.RoleName, ra.Actived FROM RoleAccount ra, Role r WHERE ra.RoleID = r.ID AND ra.AccountName = '" + accountName + "'";

            SqlDataAdapter adapter = new SqlDataAdapter(query, connectionString);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            dgvRoles.DataSource = dt;
            this.Text = "Vai trò của: " + accountName;
        }

    }
}
