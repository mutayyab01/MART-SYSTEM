using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace Mart_System
{
    public partial class ViewDataForm : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;

        public ViewDataForm()
        {
            this.Icon = Mart_System.Properties.Resources.view;

            InitializeComponent();
            BindGridView();
        }
        void BindGridView()
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "select * from item_tbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            AddItemsForm additemform = new AddItemsForm();
            additemform.ShowDialog();
        }

        private void updatebtn_Click(object sender, EventArgs e)
        {
            EdititemForm edititemform = new EdititemForm();
            edititemform.ShowDialog();
        }

        private void deletebtn_Click(object sender, EventArgs e)
        {
            EdititemForm deleteitemform = new EdititemForm();
            deleteitemform.ShowDialog();
        }

        private void ViewDataForm_Activated(object sender, EventArgs e)
        {
            BindGridView();
        }

        
    }
}
