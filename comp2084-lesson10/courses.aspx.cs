using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//entity framework reference
using comp2084_lesson10.Models;

namespace comp2084_lesson10
{
    public partial class courses : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GetCourses();
        }

        protected void GetCourses()
        {
            using (DefaultConnection db = new DefaultConnection())
            {
                var cours = from d in db.Courses
                             select d;

                //bind the cours query result to our grid
                grdCourses.DataSource = cours.ToList();
                grdCourses.DataBind();
            }

            
        }
    }
}