using LoginScreen;
using Mart_System;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;


namespace Shopping_Mart
{
    public partial class DashBoard : Form
    {

        int serial_no = 0;
        int finalCost = 0;
        int tax = 0;
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        DataRow dtr;

        public DashBoard()
        {
            this.Icon = Mart_System.Properties.Resources.dashboard;
            InitializeComponent();
            GetItem();
            getInvoiceID();
            usertextBox2.Text = Form1.username;

        }


        //Getting Item From Database To ComboBox 
        void GetItem()
        {
            if (selectitemcomboBox1.SelectedItem == null)
            {
                selectitemcomboBox1.Items.Insert(0, "Please select any Item");
                selectitemcomboBox1.SelectedIndex = 0;
            }
            else
            {

                selectitemcomboBox1.Items.Clear();

                SqlConnection con = new SqlConnection(cs);
                string query = "select * from item_tbl";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    //dtr.ItemArray = new object[] {0,"---Select Item ---" };
                    //selectitemcomboBox1.Items.Insert(0,dtr);
                    //selectitemcomboBox1.DisplayMember = "item_name";
                    string itemname = dr.GetString(1);
                    selectitemcomboBox1.Items.Add(itemname);
                }
                con.Close();

            }

        }
        //End

        //getting price when we select comboBox item and display the particular price in Unit Price Textbox
        void getprice()
        {
            if (selectitemcomboBox1.SelectedItem == null)
            {

            }
            else
            {
                int price = 0;
                SqlConnection con = new SqlConnection(cs);
                string query = "select item_price from item_tbl where item_name=@name";
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                sda.SelectCommand.Parameters.AddWithValue("@name", selectitemcomboBox1.SelectedItem.ToString());
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    price = Convert.ToInt32(dt.Rows[0]["item_price"]);
                }
                unittextBox3.Text = price.ToString();
            }

        }
        //end

        //getting Discount price when we select comboBox item and display the particular Discounted price in Discounted Per Item Textbox

        void getdiscount()
        {
            if (selectitemcomboBox1.SelectedItem == null)
            {

            }
            else
            {

                int discountprice = 0;
                SqlConnection con = new SqlConnection(cs);
                string query = "select item_discount from item_tbl where item_name=@name";
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                sda.SelectCommand.Parameters.AddWithValue("@name", selectitemcomboBox1.SelectedItem.ToString());
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    discountprice = Convert.ToInt32(dt.Rows[0]["item_discount"]);
                }
                discountedtextBox4.Text = discountprice.ToString();

            }

        }
        //end

        private void selectitemcomboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            itemerror.Clear();
            getprice();


            getdiscount();
            quantitytextBox5.Enabled = true;
        }

        private void quantitytextBox5_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(quantitytextBox5.Text) == true)
            {
            }
            else
            {

                errorProvider1.Clear();
                int price = Convert.ToInt32(unittextBox3.Text);
                int discount = Convert.ToInt32(discountedtextBox4.Text);
                int quantity = Convert.ToInt32(quantitytextBox5.Text);
                int subtotal = price * (quantity);
                subtotal = subtotal - (discount * quantity);
                subtotaltextBox6.Text = subtotal.ToString();
            }


        }

        private void subtotaltextBox6_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(subtotaltextBox6.Text) == true)
            {

            }
            else
            {

                int subtotal = Convert.ToInt32(subtotaltextBox6.Text);
                if (subtotal >= 10000)
                {
                    tax = (int)(subtotal * 0.15);
                    taxtextBox7.Text = tax.ToString();

                }
                else if (subtotal >= 6000)
                {
                    tax = (int)(subtotal * 0.10);
                    taxtextBox7.Text = tax.ToString();

                }
                else if (subtotal >= 3000)
                {
                    tax = (int)(subtotal * 0.07);
                    taxtextBox7.Text = tax.ToString();

                }
                else if (subtotal >= 1000)
                {
                    tax = (int)(subtotal * 0.05);
                    taxtextBox7.Text = tax.ToString();

                }
                else
                {
                    tax = (int)(subtotal * 0.03);
                    taxtextBox7.Text = tax.ToString();
                }
            }
        }

        private void taxtextBox7_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(taxtextBox7.Text) == true)
            {

            }
            else
            {

                int subtotal = Convert.ToInt32(subtotaltextBox6.Text);
                int tax = Convert.ToInt32(taxtextBox7.Text);
                int totalcost = subtotal + tax;
                totalcosttextBox8.Text = totalcost.ToString();
            }
        }

        void AddDataToGridview(string sr, string itemname, string itemprice, string discount, string quantity, string subtotal, string tax, string totalcost)
        {
            string[] row = { sr, itemname, itemprice, discount, quantity, subtotal, tax, totalcost };
            dataGridView1.Rows.Add(row);


        }

        //Reset Contorl Functionalities 
        void resetControl()
        {
            selectitemcomboBox1.SelectedItem = null;
            unittextBox3.Clear();
            discountedtextBox4.Clear();
            quantitytextBox5.Clear();
            subtotaltextBox6.Clear();
            taxtextBox7.Clear();
            totalcosttextBox8.Clear();
            finalcosttextBox9.Clear();
            amountpaidtextBox10.Clear();
            changetextBox11.Clear();
            quantitytextBox5.Enabled = false;

        }

        //ToolBar Calculate The Final Amount From The DataGridView And Display To Final TextBox 
        void CalculateFinalCost()
        {
            finalCost = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                finalCost += Convert.ToInt32(dataGridView1.Rows[i].Cells[7].Value);
            }
            finalcosttextBox9.Text = finalCost.ToString();
        }
        //End

        private void addbtn_Click(object sender, EventArgs e)
        {
            if (selectitemcomboBox1.SelectedItem != null)
            {

                //selectitemcomboBox1.Items.Insert(0, "Please select any value");
                //selectitemcomboBox1.SelectedIndex = 0;

                if (string.IsNullOrEmpty(quantitytextBox5.Text) == false)
                {
                    errorProvider1.Clear();
                    AddDataToGridview((++serial_no).ToString(), selectitemcomboBox1.SelectedItem.ToString(), unittextBox3.Text, discountedtextBox4.Text, quantitytextBox5.Text, subtotaltextBox6.Text, taxtextBox7.Text, totalcosttextBox8.Text);
                    resetControl();
                    CalculateFinalCost();
                }
                else
                {
                    errorProvider1.SetError(this.quantitytextBox5, "Please Enter Qunatity");
                    quantitytextBox5.Focus();
                }
            }
            else
            {
                itemerror.SetError(this.selectitemcomboBox1, "Select Item");
                //itemerror.Clear();
            }
        }

        private void resetbtn_Click(object sender, EventArgs e)
        {
            resetControl();
        }

        private void amountpaidtextBox10_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(amountpaidtextBox10.Text) == true)
            {

            }
            else
            {

                int FCost = int.Parse(finalcosttextBox9.Text);
                int amount = int.Parse(amountpaidtextBox10.Text);
                int change = amount - FCost;
                changetextBox11.Text = change.ToString();
            }


        }

        // Clear DataGrid View

        #region Not_In_Use
        private void Cleardatagrid_Click(object sender, EventArgs e)
        {
            //dataGridView1.Rows.Clear();
            //serial_no = 0;
        } 
        #endregion


        void getInvoiceID()
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "select invoice_Id from order_master ";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count < 1)
            {
                invoicetextBox1.Text = "1";
            }
            else
            {
                string query2 = "select max(invoice_Id) from order_master ";
                SqlCommand cmd = new SqlCommand(query2, con);
                con.Open();
                int max = Convert.ToInt32(cmd.ExecuteScalar());
                max = max + 1;
                invoicetextBox1.Text = max.ToString();
                con.Close();

            }
        }

        private void insertbtn_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "insert into order_master values(@invoiceID,@username,@date,@finalcost)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@invoiceID", invoicetextBox1.Text);
            cmd.Parameters.AddWithValue("@username", usertextBox2.Text);
            cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
            cmd.Parameters.AddWithValue("@finalcost", finalcosttextBox9.Text);
            con.Open();
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                MessageBox.Show("Inserted SuccessFully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                getInvoiceID();
                resetControl();
            }
            else
            {
                MessageBox.Show("Insertion Failed", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            con.Close();
            InsertIntoOrderDetails_Table();
            dataGridView1.Rows.Clear();
            serial_no = 0;
        }


        int GetLastInvoiceID()
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "select max(invoice_Id) from order_master";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int MaxInvoiceID = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
            return MaxInvoiceID;
        }

        void InsertIntoOrderDetails_Table()
        {
            int a = 0;
            SqlConnection con = new SqlConnection(cs);
            try
            {

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    string query = "insert into order_details values(@invoiceID,@itemname,@price,@discount,@quantity,@subtotal,@tax,@finalcost)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@invoiceID", GetLastInvoiceID());
                    cmd.Parameters.AddWithValue("@itemname", dataGridView1.Rows[i].Cells[1].Value.ToString());
                    cmd.Parameters.AddWithValue("@price", dataGridView1.Rows[i].Cells[2].Value);
                    cmd.Parameters.AddWithValue("@discount", dataGridView1.Rows[i].Cells[3].Value);
                    cmd.Parameters.AddWithValue("@quantity", dataGridView1.Rows[i].Cells[4].Value);
                    cmd.Parameters.AddWithValue("@subtotal", dataGridView1.Rows[i].Cells[5].Value);
                    cmd.Parameters.AddWithValue("@tax", dataGridView1.Rows[i].Cells[6].Value);
                    cmd.Parameters.AddWithValue("@finalcost", dataGridView1.Rows[i].Cells[7].Value);
                    con.Open();
                    a = a + cmd.ExecuteNonQuery();

                    con.Close();
                }

            }

            catch
            {


            }
            if (a > 0)
            {
                MessageBox.Show("Data Added In Order Details Table", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                getInvoiceID();
                resetControl();
            }
            else
            {
                MessageBox.Show("Data Not Added Failed In Order Details Table", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        // Key Press Events 

        #region Key_Press_Events

        private void quantitytextBox5_KeyPress(object sender, KeyPressEventArgs e)
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

        private void amountpaidtextBox10_KeyPress(object sender, KeyPressEventArgs e)
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



        //Print Document Preview
        private void printpreviewbtn_Click(object sender, EventArgs e)
        {
            printPreviewDialog.Document = printDocument;
            printPreviewDialog.ShowDialog();
        }

        // Print Function Code

        private void printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap brm = Mart_System.Properties.Resources.martlogo;
            Image img = brm;
            e.Graphics.DrawImage(img, 315, 10, 200, 200);
            e.Graphics.DrawString("Invoice ID   :  " + invoicetextBox1.Text, new Font("Arial", 16, FontStyle.Bold), Brushes.Black, new Point(60, 250));
            e.Graphics.DrawString("User Name : " + usertextBox2.Text, new Font("Arial", 16, FontStyle.Bold), Brushes.Black, new Point(60, 280));
            e.Graphics.DrawString("Date            : " + DateTime.Now.ToString("dd-MM-yyyy"), new Font("Arial", 16, FontStyle.Bold), Brushes.Black, new Point(60, 310));
            e.Graphics.DrawString("Time           : " + DateTime.Now.ToLongTimeString(), new Font("Arial", 16, FontStyle.Bold), Brushes.Black, new Point(60, 340));
            e.Graphics.DrawString("---------------------------------------------------------------------------------------------------", new Font("Arial", 16, FontStyle.Bold), Brushes.Black, new Point(60, 370));
            e.Graphics.DrawString("ITEM", new Font("Arial", 16, FontStyle.Bold), Brushes.Black, new Point(60, 400));
            e.Graphics.DrawString("PRICE", new Font("Arial", 16, FontStyle.Bold), Brushes.Black, new Point(310, 400));
            e.Graphics.DrawString("QUANTITY", new Font("Arial", 16, FontStyle.Bold), Brushes.Black, new Point(450, 400));
            e.Graphics.DrawString("DISCOUNT", new Font("Arial", 16, FontStyle.Bold), Brushes.Black, new Point(660, 400));
            e.Graphics.DrawString("---------------------------------------------------------------------------------------------------", new Font("Arial", 16, FontStyle.Bold), Brushes.Black, new Point(60, 430));
            // For Item Name
            int gap = 460;
            if (dataGridView1.Rows.Count > 0)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    try
                    {

                        e.Graphics.DrawString(dataGridView1.Rows[i].Cells[1].Value.ToString(), new Font("Arial", 16, FontStyle.Bold), Brushes.Black, new Point(60, gap));
                        gap += 30;
                    }
                    catch
                    {


                    }
                }
            }
            // For Item Price
            int gap_orice = 460;
            if (dataGridView1.Rows.Count > 0)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    try
                    {

                        e.Graphics.DrawString(dataGridView1.Rows[i].Cells[2].Value.ToString(), new Font("Arial", 16, FontStyle.Bold), Brushes.Black, new Point(320, gap_orice));
                        gap_orice += 30;
                    }
                    catch
                    {


                    }
                }
            }
            // For Item Quantity
            int gap_quantity = 460;
            if (dataGridView1.Rows.Count > 0)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    try
                    {

                        e.Graphics.DrawString(dataGridView1.Rows[i].Cells[4].Value.ToString(), new Font("Arial", 16, FontStyle.Bold), Brushes.Black, new Point(480, gap_quantity));
                        gap_quantity += 30;
                    }
                    catch
                    {


                    }
                }
            }
            // For Item Discount
            int gap_discount = 460;
            if (dataGridView1.Rows.Count > 0)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    try
                    {

                        e.Graphics.DrawString(dataGridView1.Rows[i].Cells[3].Value.ToString(), new Font("Arial", 16, FontStyle.Bold), Brushes.Black, new Point(685, gap_discount));
                        gap_discount += 30;
                    }
                    catch
                    {


                    }
                }
            }
            // Sub Total 
            int subtotalprint = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                subtotalprint += Convert.ToInt32(dataGridView1.Rows[i].Cells[5].Value);
            }
            e.Graphics.DrawString("---------------------------------------------------------------------------------------------------", new Font("Arial", 16, FontStyle.Bold), Brushes.Black, new Point(60, gap_discount + 50));
            e.Graphics.DrawString("SUB-TOTAL      :  " + subtotalprint.ToString(), new Font("Arial", 16, FontStyle.Bold), Brushes.Black, new Point(60, gap_discount + 80));
            // TAX TOTAL
            int tax_total_print = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                tax_total_print += Convert.ToInt32(dataGridView1.Rows[i].Cells[6].Value);
            }
            e.Graphics.DrawString("---------------------------------------------------------------------------------------------------", new Font("Arial", 16, FontStyle.Bold), Brushes.Black, new Point(60, gap_discount + 50));
            e.Graphics.DrawString("TAX-TOTAL      :  " + tax_total_print.ToString(), new Font("Arial", 16, FontStyle.Bold), Brushes.Black, new Point(60, gap_discount + 110));
            e.Graphics.DrawString("FINAL COST     :  " + finalcosttextBox9.Text, new Font("Arial", 16, FontStyle.Bold), Brushes.Black, new Point(60, gap_discount + 140));
            e.Graphics.DrawString("--------------------▬-------------------------------------------------------------------------------", new Font("Arial", 16, FontStyle.Bold), Brushes.Black, new Point(60, gap_discount + 170));
            e.Graphics.DrawString("AMOUNT PAID  :  " + amountpaidtextBox10.Text, new Font("Arial", 16, FontStyle.Bold), Brushes.Black, new Point(60, gap_discount + 200));
            e.Graphics.DrawString("CHANGE            :  " + changetextBox11.Text, new Font("Arial", 16, FontStyle.Bold), Brushes.Black, new Point(60, gap_discount + 230));
            // 
            Bitmap mutayyab = Mart_System.Properties.Resources.mutayyab;
            Image images = mutayyab;
            e.Graphics.DrawImage(images, 215, gap_discount + 260, 385, 139);

        }




        private void printbtn_Click(object sender, EventArgs e)
        {
            printDocument.Print();
        }



        private void aDDITEMSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddItemsForm additem = new AddItemsForm();
            additem.ShowDialog();
        }

        private void DashBoard_Activated(object sender, EventArgs e)
        {
            if (selectitemcomboBox1.SelectedItem == null)
            {
                itemerror.GetError(this.selectitemcomboBox1);
            }
            else
            {
                GetItem();
            }
        }

        private void eXITToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void eDITITEMSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EdititemForm edit = new EdititemForm();
            edit.ShowDialog();
        }

        private void vIEWDATAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewDataForm view = new ViewDataForm();
            view.ShowDialog();
        }

        private void detailsAndSearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DetailsAndSearchForm detailsandsearchform = new DetailsAndSearchForm();
            detailsandsearchform.ShowDialog();
        }

        // If You Press Enter Button On Quantity Textbox Then Add Button Will Automatically Clicked

        #region Key_Down_Event
        private void quantitytextBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                addbtn.PerformClick();
            }
        }


        #endregion

        private void sIGNOUTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.login.Show();


        }
    }
}
