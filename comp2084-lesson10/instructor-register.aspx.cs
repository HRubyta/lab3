using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using comp2084_lesson10.Models;

//crytography reference
using System.Security.Cryptography;

namespace comp2084_lesson10
{
    public partial class instructor_register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetDepartment();
            }
            
        }

        protected void GetDepartment()
        {

            using (DefaultConnection db = new DefaultConnection())
            {
                var deps = from d in db.Departments
                           orderby d.Name
                           select d;

                ddlDepartment.DataSource = deps.ToList();
                ddlDepartment.DataBind();

                ListItem default_item = new ListItem("select", "0");
                ddlDepartment.Items.Insert(0, default_item);

            }
        }

        private static string CreateSalt(int size)
        {
            // Generate a cryptographic random number using the cryptographic 
            // service provider
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);
            // Return a Base64 string representation of the random number
            return Convert.ToBase64String(buff);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            using (DefaultConnection db = new DefaultConnection())
            {
                Instructor objI = new Instructor();

                objI.FirstName = txtFirstName.Text;
                objI.LastName = txtLastName.Text;
                objI.Username = txtUsername.Text;
                objI.DepartmentID = Convert.ToInt32(ddlDepartment.SelectedValue);

                String password = txtPassword.Text;
                String salt = CreateSalt(8);
                String pass_and_salt = password + salt;

                // Create a new instance of the hash crypto service provider.
                HashAlgorithm hashAlg = new SHA256CryptoServiceProvider();
                // Convert the data to hash to an array of Bytes.
                byte[] bytValue = System.Text.Encoding.UTF8.GetBytes(pass_and_salt);
                // Compute the Hash. This returns an array of Bytes.
                byte[] bytHash = hashAlg.ComputeHash(bytValue);
                // Optionally, represent the hash value as a base64-encoded string, 
                // For example, if you need to display the value or transmit it over a network.
                string base64 = Convert.ToBase64String(bytHash);

                objI.Password = base64;
                objI.Salt = salt;

                db.Instructors.Add(objI);
                db.SaveChanges();
            }
        }
    }
}