using LoginScreen;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Mart_System
{
    public partial class SignUpForm : Form
    {
        string emailpattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
        string passwordpattern = @"(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$";
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;

        public SignUpForm()
        {
            this.Icon = Mart_System.Properties.Resources.signup;

            InitializeComponent();
        }



        //Form Validation With Error Provider In Textboxes
        #region Validation_With_TextBoxes

        private void txtfullname_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtfullname.Text) == true)
            {
                txtfullname.Focus();
                errorProvider1.SetError(this.txtfullname, "Please Enter Full Name");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void txtusername_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtusername.Text) == true)
            {
                txtusername.Focus();
                errorProvider2.SetError(this.txtusername, "Please Enter User Name");
            }
            else
            {
                errorProvider2.Clear();
            }
        }

        private void comboBoxGender_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBoxGender.Text) == true)
            {
                comboBoxGender.Focus();
                errorProvider3.SetError(this.comboBoxGender, "Please Enter Your Gender");
            }
            else
            {
                errorProvider3.Clear();
            }
        }

        private void numericUpDown_Leave(object sender, EventArgs e)
        {
            if (numericUpDown.Value == 0)
            {
                numericUpDown.Focus();
                errorProvider4.SetError(this.numericUpDown, "Please Enter Your Age");
            }
            else
            {
                errorProvider4.Clear();
            }
        }

        private void txtemail_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtemail.Text) == true)
            {
                txtemail.Focus();
                errorProvider5.SetError(this.txtemail, "Please Enter Your Valid Email");
            }
            else
            {
                if (Regex.IsMatch(txtemail.Text, emailpattern) == false)
                {
                    txtemail.Focus();
                    errorProvider5.SetError(this.txtemail, "Please Enter Your Valid Email");
                }
                else
                {
                    errorProvider5.Clear();

                }
            }
        }

        private void txtpassword_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtpassword.Text) == true)
            {
                txtpassword.Focus();
                errorProvider6.SetError(this.txtpassword, "Password Must Contain 1 UpperCase,1 Special Character,And Atleast 1 Number");
            }
            else
            {
                if (Regex.IsMatch(txtpassword.Text, passwordpattern) == false)
                {
                    txtpassword.Focus();
                    errorProvider6.SetError(this.txtpassword, "Password Must Contain 1 UpperCase,LowerCase,And Atleast 1 Number,1 Special Character");
                }
                else
                {
                    errorProvider6.Clear();

                }
            }
        }

        private void txtcpassword_Leave(object sender, EventArgs e)
        {
            if (txtpassword.Text != txtcpassword.Text)
            {
                txtcpassword.Focus();
                errorProvider7.SetError(this.txtcpassword, "Password Is Not Identical");
            }
            else
            {
                errorProvider7.Clear();
            }
        }
        #endregion





        // Validation To Check Only Letter And Number▬
        #region Validations_To_Check_Only_Letter_&_Number2

        private void txtfullname_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (Char.IsLetter(ch))
            {
                e.Handled = false;
            }
            else if (ch == 8) //For BackSpace Key Enumeration
            {
                e.Handled = false;
            }
            else if (ch == 32) //For SpaceBar Key Enumeration
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }

        }



        private void txtusername_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (Char.IsLetterOrDigit(ch))
            {
                e.Handled = false;
            }
            else if (ch == 8) //For BackSpace Key Enumeration
            {
                e.Handled = false;
            }
            else if (ch == 32) //For SpaceBar Key Enumeration
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }









        #endregion

        private void signupbutton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtfullname.Text) == true)
            {
                txtfullname.Focus();
                errorProvider1.SetError(this.txtfullname, "Please Enter Full Name");
            }
            else if (string.IsNullOrEmpty(txtusername.Text) == true)
            {
                txtusername.Focus();
                errorProvider2.SetError(this.txtusername, "Please Enter User Name");
            }
            else if (string.IsNullOrEmpty(comboBoxGender.Text) == true)
            {
                comboBoxGender.Focus();
                errorProvider3.SetError(this.comboBoxGender, "Please Enter Your Gender");
            }
            else if (numericUpDown.Value == 0)
            {
                numericUpDown.Focus();
                errorProvider4.SetError(this.numericUpDown, "Please Enter Your Age");
            }
            else if (string.IsNullOrEmpty(txtemail.Text) == true || Regex.IsMatch(txtemail.Text, emailpattern) == false)
            {
                txtemail.Focus();
                errorProvider5.SetError(this.txtemail, "Please Enter Your Valid Email");

            }
            else if (string.IsNullOrEmpty(txtpassword.Text) == true || Regex.IsMatch(txtpassword.Text, passwordpattern) == false)
            {
                txtpassword.Focus();
                errorProvider6.SetError(this.txtpassword, "Password Must Contain 1 UpperCase,LowerCase,And Atleast 1 Number,1 Special Character");
            }
            else if (txtpassword.Text != txtcpassword.Text)
            {
                txtcpassword.Focus();
                errorProvider7.SetError(this.txtcpassword, "Password Is Not Identical");
            }
            else
            {
                string userName = "";
                string userPassword = "";
                SqlConnection con = new SqlConnection(cs);
                string query = "insert into signup_tbl values(@fullname,@username,@gender,@age,@email,@password)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@fullname", txtfullname.Text);
                cmd.Parameters.AddWithValue("@username", txtusername.Text);
                cmd.Parameters.AddWithValue("@gender", comboBoxGender.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@age", numericUpDown.Value);
                cmd.Parameters.AddWithValue("@email", txtemail.Text);
                cmd.Parameters.AddWithValue("@password", txtcpassword.Text);
                con.Open();
                int a = cmd.ExecuteNonQuery();
                if (a > 0)
                {
                    MessageBox.Show("Registered SuccessFully ", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MessageBox.Show("Your Username is  " + txtusername.Text + "\n\nYour Password Is  " + txtcpassword.Text + " ", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form1 login = new Form1();
                    this.Hide();
                    login.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Registered Failed ", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                con.Close();


            }
        }

        // When User Press Enter On Confirm PassWord TextBox The Signup Button Will Clicked Automatically
        // With The HElp Of This Event

        private void txtcpassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                signupbutton.PerformClick();
            }
        }

        private void resetbutton_Click(object sender, EventArgs e)
        {
            txtfullname.Clear();
            txtusername.Clear();
            comboBoxGender.SelectedItem = null;
            numericUpDown.Value = 0;
            txtemail.Clear();
            txtpassword.Clear();
            txtcpassword.Clear();



        }





    }
}
