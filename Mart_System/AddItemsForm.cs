using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Mart_System
{
    public partial class AddItemsForm : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;

        public AddItemsForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtotemname.Text) == true)
            {
                txtotemname.Focus();
                errorProvider1.SetError(this.txtotemname, "Please Enter Item Name");
            }
            else if (string.IsNullOrEmpty(txtitemprice.Text) == true)
            {
                txtitemprice.Focus();

                errorProvider2.SetError(this.txtitemprice, "Please Enter Item Price");
            }
            else
            if (string.IsNullOrEmpty(txtitemdiscount.Text) == true)
            {
                txtitemdiscount.Focus();

                errorProvider3.SetError(this.txtitemdiscount, "Please Enter Item Discount");
            }
            else
            {
                if (CheckItemNameExistInDataBase() == true)
                {
                    MessageBox.Show("Item Already  Exist\nPlease Change Item Name","Failure",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    txtotemname.Focus();
                }
                else
                {
                    SqlConnection con = new SqlConnection(cs);
                    string query = "insert into item_tbl values(@itemname,@itemprice,@itemdiscount)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@itemname", txtotemname.Text);
                    cmd.Parameters.AddWithValue("@itemprice", txtitemprice.Text);
                    cmd.Parameters.AddWithValue("@itemdiscount", txtitemdiscount.Text);
                    con.Open();
                    int a = cmd.ExecuteNonQuery();
                    if (a > 0)
                    {
                        MessageBox.Show("Inserted SuccessFully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ResetControl();

                    }
                    else
                    {
                        MessageBox.Show("Insertion Failed", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    con.Close();
                }
            }

        }
        void ResetControl()
        {
            txtotemname.Focus();
            txtotemname.Clear();
            txtitemprice.Clear();
            txtitemdiscount.Clear();
        }

        bool CheckItemNameExistInDataBase()
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "select  item_name from item_tbl where item_name='"+txtotemname.Text+"'";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows == true)
            {
                return true;
            }
            return false;
        }


        //  Validation ABount Accepting Only Number And Letter

        #region VAlidations
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
            }  else if (ch == 32)
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

        private void txtitemdiscount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();

            }
        }



        #endregion

        // Leave Events

        #region LEave_Envents
        private void txtotemname_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtotemname.Text) == true)
            {
                txtotemname.Focus();
                errorProvider1.SetError(this.txtotemname, "Please Enter Item Name");
            }
            else
            {
                errorProvider1.Clear();

            }
        }

        private void txtitemprice_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtitemprice.Text) == true)
            {
                txtitemprice.Focus();

                errorProvider2.SetError(this.txtitemprice, "Please Enter Item Price");
            }
            else
            {
                errorProvider2.Clear();

            }
        }

        private void txtitemdiscount_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtitemdiscount.Text) == true)
            {
                txtitemdiscount.Focus();

                errorProvider3.SetError(this.txtitemdiscount, "Please Enter Item Discount");
            }
            else
            {
                errorProvider3.Clear();

            }
        }
        #endregion





    }
}
