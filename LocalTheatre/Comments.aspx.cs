using LocalTheatre.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LocalTheatre
{
    public partial class Comments : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnLeaveAComment_Click(object sender, EventArgs e)
        {
            Panel1.Visible = true;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Comment com = new Comment { UserId = User.Identity.Name, Date = DateTime.Now, Text = tbComment.Text };
            ApplicationDbContext context = new ApplicationDbContext();
            context.Comments.Add(com);
            context.SaveChanges();
            BindData();
            Panel1.Visible = false;
        }



        private void BindData()
        {
            GridView1.DataBind();
        }


    }
}