using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eLibraryManagementSystem
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            //Response.Write("<script>alert('Button Click');</script>");
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if(con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT * from member_master_tbl where member_id='" + TextBox1.Text.Trim() + "' AND password = '"+ TextBox2.Text.Trim() +"';", con);
                //disconnected architecture 
                //SqlDataAdapter da = new SqlDataAdapter(cmd);
                //DataTable dt = new DataTable();
                SqlDataReader dr = cmd.ExecuteReader(); //to->execute the query
                if (dr.HasRows) //returns true false
                {
                    while (dr.Read())
                    {
                        //session variable -> reamins live till website is running, access globally
                        Response.Write("<script>alert('Successful login');</script>");
                        Session["username"] = dr.GetValue(8).ToString();
                        //Session["username"] = TextBox1.Text.Trim();
                        Session["fullname"] = dr.GetValue(2).ToString();
                        Session["role"] = "user";
                        //Session["status"] = dr.GetValue(10).ToString();
                        Response.Write("<script>alert('" + dr.GetValue(0).ToString() + "');</script>");
                    }
                    Response.Redirect("homepage.aspx");
                }
                else
                {
                    Response.Write("<script>alert('Invalid credentials');</script>");
                }
                
            }
            catch(Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }

        }
    }
}