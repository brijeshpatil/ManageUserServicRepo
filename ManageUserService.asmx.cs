using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApplication1
{
    /// <summary>
    /// Summary description for ManageUserService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ManageUserService : System.Web.Services.WebService
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString);
        SqlCommand cmd; //Insert/Update.Delete
        SqlDataAdapter da; //Select
        DataSet dt; // Data Store

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public string InsertNewUser(string FirstName, string LastName)
        {
            cmd = new SqlCommand("insert into userinfo values(@fname,@lname)", con);
            cmd.Parameters.AddWithValue("@fname", FirstName);
            cmd.Parameters.AddWithValue("@lname", LastName);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return "Record Saved";
        }

        [WebMethod]
        public string UpdateUser(int UserID, string FirstName, string LastName)
        {
            cmd = new SqlCommand("update userinfo set fname=@fname,lname=@lname where userid=@uid", con);
            cmd.Parameters.AddWithValue("@fname", FirstName);
            cmd.Parameters.AddWithValue("@lname", LastName);
            cmd.Parameters.AddWithValue("@uid", UserID);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return "Record Updated";
        }

        [WebMethod]
        public string DeleteUser(int UserID)
        {
            cmd = new SqlCommand("delete from userinfo where userid=@UID", con);
            cmd.Parameters.AddWithValue("@UID", UserID);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return "Record Deleted";
        }

        [WebMethod]
        public DataSet GetAllUsers()
        {
            da = new SqlDataAdapter("select * from userinfo", con);
            dt = new DataSet();
            da.Fill(dt);
            return dt;
        }
    }
}
