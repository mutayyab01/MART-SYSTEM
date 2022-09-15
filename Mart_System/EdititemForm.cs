using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Mart_System
{
    public partial class EdititemForm : Form
    {

        int i;
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;

        public EdititemForm()
        {
            this.Icon = Mart_System.Properties.Resources.edititemform;

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

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            IDtextBox.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            txtotemname.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            txtitemprice.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            txtitemdiscount.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
        }

        private void updatebtn_Click(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(cs);
            string query = "update  item_tbl set item_name=@name,item_price=@price,item_discount=@discount where item_Id=@id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", IDtextBox.Text);
            cmd.Parameters.AddWithValue("@name", txtotemname.Text);
            cmd.Parameters.AddWithValue("@price", txtitemprice.Text);
            cmd.Parameters.AddWithValue("@discount", txtitemdiscount.Text);
            con.Open();
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                MessageBox.Show("Update SuccessFully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BindGridView();
                ResetControl();

            }
            else
            {
                MessageBox.Show("Update Failed", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            con.Close();
        }

        void ResetControl()
        {
            txtotemname.Focus();
            IDtextBox.Clear();
            txtotemname.Clear();
            txtitemprice.Clear();
            txtitemdiscount.Clear();
        }

        private void deletebtn_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "delete from  item_tbl where item_Id=@id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", IDtextBox.Text);
            con.Open();
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                MessageBox.Show("Delete SuccessFully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BindGridView();
                ResetControl();

            }
            else
            {
                MessageBox.Show("Delete Failed", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            con.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {



            





        }
        //  Validation ABount Accepting Only Number And Letter

        #region Validations
        private void txtotemname_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (char.IsLetterOrDigit(ch) == true)
            {
                e.Handled = false;
            }
            else if (ch == 8)
            {
                e.Handled = false;
            }
            else if (ch == 32)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtitemprice_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (char.IsDigit(ch) == true)
            {
                e.Handled = false;
            }
            else if (ch == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtitemdiscount_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (char.IsDigit(ch) == true)
            {
                e.Handled = false;
            }
            else if (ch == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        } 
        #endregion



    }
}
