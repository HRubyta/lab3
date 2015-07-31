using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//reference db model for connecting to sql server
using comp2084_lesson10.Models;

namespace comp2084_lesson10
{
    public partial class student : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if loading for the first time, check for url
            if (!IsPostBack)
            {
                //check ID in seleceted record
                if (!String.IsNullOrEmpty(Request.QueryString["StudentID"]))
                {
                    GetStudent();
                }
            }

        }

        protected void GetStudent()
        {
            //store id from the url in a variable
            Int32 StudentID = Convert.ToInt32(Request.QueryString["StudentID"]);

            //look up selected student and fill the form
            using (DefaultConnection db = new DefaultConnection())
            {    

                //populate student instance
                Student stud = (from d in db.Students
                                where d.StudentID == StudentID
                                select d).FirstOrDefault();

                if(stud != null)
                {
                    //pre-populate the form fields
                    txtFirstName.Text = stud.FirstMidName;
                    txtLastName.Text = stud.LastName;
                    txtEnrollDate.Text = stud.EnrollmentDate.ToString("yyyy-mm-dd");
                }
                
                //populate enrollments
                var enroll = (from e in db.Enrollments
                              join c in db.Courses on e.CourseID equals c.CourseID
                              join d in db.Departments on c.DepartmentID equals d.DepartmentID
                              where e.StudentID == StudentID
                              select new { e.EnrollmentID, e.Grade, c.Title, d.Name });

                grdCourses.DataSource = enroll.ToList();
                grdCourses.DataBind();

                //show course panel
                pnlCourses.Visible = true;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //connect db
            using (DefaultConnection db = new DefaultConnection())
            {
                //create a new department in memory
                Student stud = new Student();

                Int32 StudentID = 0;

                //check for a url
                if (!String.IsNullOrEmpty(Request.QueryString["StudentID"]))
                {
                    //get id from the url
                    StudentID = Convert.ToInt32(Request.QueryString["StudentID"]);

                    //look up the student
                    stud = (from d in db.Students n
                            where d.StudentID == StudentID
                            select d).FirstOrDefault();
                }

                //fill properties of the new student
                stud.FirstMidName = txtFirstName.Text;
                stud.LastName = txtLastName.Text;
                stud.EnrollmentDate = Convert.ToDateTime(txtEnrollDate.Text);

                //add if we have no id in the url
                if (StudentID == 0)
                {
                    db.Students.Add(stud);
                }

                //save new student
                db.SaveChanges();

                //redirect to student list page
                Response.Redirect("students.aspx");
            }
        }
    }
}