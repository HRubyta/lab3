using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//reference our db model
using comp2084_lesson10.Models;

namespace comp2084_lesson10
{
    public partial class course_details : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if loading for the first time, load department dropdown
                GetDepartments();

                //check for course id, if found populate the selected course
                if(!String.IsNullOrEmpty(Request.QueryString["CourseID"]))
                {
                    GetCourse();
                    pnlEnrollments.Visible = true;
                }
                else
                {
                    pnlEnrollments.Visible = false;
                }
            }
        }

        protected void GetCourse()
        {
            //connect
            using (DefaultConnection db = new DefaultConnection())
            {
                //get selected courseID from the url
                Int32 CourseID = Convert.ToInt32(Request.QueryString["CourseID"]);

                //query the db
                Course objc = (from c in db.Courses
                               where c.CourseID == CourseID
                               select c).FirstOrDefault();

                //populate the form
                txtTitle.Text = objc.Title;
                txtCredits.Text = objc.Credits.ToString();
                ddlDepartment.SelectedValue = objc.DepartmentID.ToString();

                //populate student enrollments grid
                var Enrollments = from en in db.Enrollments
                                  where en.CourseID == CourseID
                                  orderby en.Student.LastName, en.Student.FirstMidName 
                                  select en;

                //bind the grid
                grdEnrollments.DataSource = Enrollments.ToList();
                grdEnrollments.DataBind();

            }
        }

        protected void GetDepartments()
        {
            using (DefaultConnection db = new DefaultConnection())
            {
                var Departments = from d in db.Departments
                                  orderby d.Name
                                  select d;

                //bind data dropdown list
                ddlDepartment.DataSource = Departments.ToList();
                ddlDepartment.DataBind();

                //add a default option
                ListItem DefaultItem = new ListItem("Select", "0");
                ddlDepartment.Items.Insert(0, DefaultItem);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //connect
            using (DefaultConnection db = new DefaultConnection())
            {
                //create new course and fills the properties
                Course objc = new Course();

                objc.Title = txtTitle.Text;
                objc.Credits = Convert.ToInt32(txtCredits.Text);
                objc.DepartmentID = Convert.ToInt32(ddlDepartment.SelectedValue);

                //save
                db.Courses.Add(objc);
                db.SaveChanges();

                //redirect
                Response.Redirect("courses.aspx");
            }
        }
    }
}