using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sample.Test
{
    public partial class WebForm1 : Page
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyDBConnectionString"].ConnectionString;
        int taxex = 0;
        int id = 1;
        private DataTable DataTable
        {
            get
            {
                if (ViewState["DataTable"] == null)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Customer");
                    dt.Columns.Add("Product");
                    dt.Columns.Add("Quantity");
                    dt.Columns.Add("Tax");
                    dt.Columns.Add("Price");
                    dt.Columns.Add("TaxExc");
                    ViewState["DataTable"] = dt;
                }
                return (DataTable)ViewState["DataTable"];
            }
            set { ViewState["DataTable"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Populate dropdowns on initial load
                PopulateCustomerDropdown();
                PopulateProductDropdown();
            }
        }

        //protected void PopulateCustomerDropdown()
        //{
        //    string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyDBConnectionString"].ConnectionString; string query = "SELECT Id, Name FROM Employees";
          
        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {

        //        SqlCommand cmd = new SqlCommand(query, conn);
        //        conn.Open();

        //        SqlDataReader reader = cmd.ExecuteReader();
        //        DropDownList1.DataSource = reader;
        //        DropDownList1.DataTextField = "Name";
        //        DropDownList1.DataValueField = "Id";
        //        DropDownList1.DataBind();

        //        reader.Close();
        //    }
        //}


        protected void PopulateCustomerDropdown()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyDBConnectionString"].ConnectionString; string query = "SELECT Id, Name FROM Customers";

            // -- added postgresql
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {

                NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
                conn.Open();

                NpgsqlDataReader reader = cmd.ExecuteReader();
                DropDownList1.DataSource = reader;
                DropDownList1.DataTextField = "Name";
                DropDownList1.DataValueField = "Id";
                DropDownList1.DataBind();

                reader.Close();
            }
        }

        protected void PopulateProductDropdown()
        {
             string query = "SELECT pid, pname FROM Products";

            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
                conn.Open();

                NpgsqlDataReader reader = cmd.ExecuteReader();
                DropDownList2.DataSource = reader;
                DropDownList2.DataTextField = "pname";
                DropDownList2.DataValueField = "pid";
                DropDownList2.DataBind();

                reader.Close();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string selectedValue1 = DropDownList1.SelectedItem.Text;
            string selectedValue2 = DropDownList2.SelectedItem.Text;
            string textBox1Value = TextBox1.Text;
            string textBox2Value = TextBox2.Text;
            string textBox3Value = TextBox3.Text;

             taxex = Convert.ToInt32(TextBox1.Text) * Convert.ToInt32(TextBox3.Text);
            // Add the values to the DataTable
            DataRow row = DataTable.NewRow();
            row["Customer"] = selectedValue1;
            row["Product"] = selectedValue2;
            row["Quantity"] = textBox1Value;
            row["Tax"] = textBox2Value;
            row["Price"] = textBox3Value;
            row["TaxExc"] = taxex;
            DataTable.Rows.Add(row);

            // Bind the DataTable to the GridView
            BindGridView();
        }

        private void BindGridView()
        {
            GridView1.DataSource = DataTable;
            GridView1.DataBind();

            
            string que = "SELECT MAX(id) FROM Invoicebill";
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                NpgsqlCommand cmd = new NpgsqlCommand(que, conn);
                conn.Open();
                NpgsqlDataReader reader = cmd.ExecuteReader();
                if(id == 1)
                {
                    id = 1;
                }
                else
                {
                    id = id + 1;

                }
                reader.Close();
            }

            string qry = "insert into Invoicebill(id,Custid,pid,qty,price,taxexcl)values(" + id + "," + DropDownList1.SelectedValue + "," + DropDownList2.SelectedValue + "," + TextBox1.Text + "," + TextBox3.Text + "," + taxex + ")";
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                NpgsqlCommand cmd = new NpgsqlCommand(qry, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            CalculateAndDisplaySums(DataTable);
        }

        private void CalculateAndDisplaySums(DataTable dt)
        {
            // Initialize sums for each column
            int untaxed = 0;
            int tax = 0;
            int total = 0;
            

            // Iterate through each row in the DataTable to calculate sums
            foreach (DataRow row in dt.Rows)
            {
                untaxed += Convert.ToInt32(row["TaxExc"]);
                
            }

            tax = untaxed * 15 / 100;
            total = untaxed + tax;

            // Display or use the sums as needed
            Label1.Text = "Untaxed Amount: " + untaxed.ToString();
            Label2.Text = "Tax 15% : " + tax.ToString();
            Label3.Text = "Total : " + total.ToString();


            string qryyy = "insert into InvoiceMaster(id,tax,untaxedamt,totaltax,totalamount)values(" + id + ",15,"+ untaxed + "," + tax + "," + total + ")";
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                NpgsqlCommand cmd = new NpgsqlCommand(qryyy, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
            //Label1.Text = "";
            //Label2.Text = "";
            //Label3.Text = "";
        
        }
    }
}