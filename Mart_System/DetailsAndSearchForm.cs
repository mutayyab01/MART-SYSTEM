using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Mart_System
{
    public partial class DetailsAndSearchForm : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;

        public DetailsAndSearchForm()
        {
            InitializeComponent();
            BindGridView();
        }
        void BindGridView()
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "sp_getBothTableData";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = cmd;
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        private void searchbtn_Click(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(cs);
            string query = "sp_getBothTableDataBYInvoiceID";
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@InvoiceID", searchtextBox.Text);
            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = cmd;
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count>0)
            {
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[9].Visible = false;
                FinaltextBox1.Text = dataGridView1.Rows[0].Cells[9].Value.ToString();

            }
            else
            {
                MessageBox.Show("No ID Found", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                searchtextBox.Focus();

            }


        }

        private void searchwithdatebutton_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "sp_getBothTableDataByDateTime";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@firstdate", dateTimePicker1.Value);
            cmd.Parameters.AddWithValue("@seconddate", dateTimePicker2.Value);
            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = cmd;
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void resetbutton_Click(object sender, EventArgs e)
        {
            BindGridView();
            searchtextBox.Clear();
            searchtextBox.Focus();
            FinaltextBox1.Clear();

        }

        
    }
}
