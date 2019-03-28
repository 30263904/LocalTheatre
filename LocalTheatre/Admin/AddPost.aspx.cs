using LocalTheatre.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Add Post is added into the Admin folder
/// </summary>
namespace LocalTheatre.Admin
{
    /// <summary>
    /// AddPost inherits from Page
    /// 
    /// </summary>
    public partial class AddPost : System.Web.UI.Page
    {
        /// <summary>
        /// on Page load
        /// the grid is bound
        /// </summary>
        /// <param name="sender"></param> 
        /// <param name="e"></param> event is page loading
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
           
        }

 
        /// <summary>
        /// The method to bind grid
        /// new instance of the database is created
        /// post with category created and linked
        /// using linq query with the category in another table
        /// postid,text,date,categid and comment list and type 
        /// are filled in using the constructor
        /// data souirce is pointed at post and category var
        /// and sent to list
        /// data is bound
        /// </summary>
        private void BindGrid()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            //new instance of db context with users bound to the grid

            var postandcategory =
                from post in db.Posts
                join categ in db.Categories on post.CategoryId equals categ.CategoryId
                orderby post.Date descending
                select new
                {
                post.PostId,
                post.Text,
                post.Title,
                post.Date,
                post.CategoryId,
                post.CommentList,
                Type = categ.Type};
            grdPosts.DataSource = postandcategory.ToList();
            grdPosts.DataBind();

        }
        /// <summary>
        /// The post is added.
        /// //new instance of application db
        ///initialisation and declaration of username string
        ///input in the title to text
        ///input in the text to text
        ///input from the category box's selected value
        ///parsed into an int and printed to string
        ///new Post constructed
        ///if statement to see if user logged in
        ///username of the logged in User if authenticated is their name
        ///in my case admin@admin.com
        ///blog's text sent to text and so on
        ///changes saved
        ///data bound
        /// </summary>
        /// <param name="sender"></param> is button
        /// <param name="e"></param> e is the event of adding post
        protected void AddAPost_Click(object sender, EventArgs e)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            //new instance of application db

            string uname = "anon";
            //initialisation and declaration of username string

            var Title = tbTitle.Text;
            //input in the title to text
            var Text = tbPost.Text;
            //input in the text to text
            int catid = Int32.Parse(ddlCategories.SelectedValue.ToString());
            //input from the category box's selected value
            //parsed into an int and printed to string

            var blogpost = new Post();
            //new Post constructed

            if (User.Identity.IsAuthenticated)
                //if statement to see if user logged in
            {
                uname = User.Identity.Name;
                //username of the logged in User if authenticated is their name
                //in my case admin@admin.com
                

               
            }
            else
            {
              
                uname = "guest";
                //otherwise post as guest
            }


            blogpost.Text = Text;
            blogpost.Title = Title;
            blogpost.UserId = uname;
            //ddl            
             blogpost.CategoryId = catid;


            db.Posts.Add(blogpost);
            db.SaveChanges();
            //save
            BindGrid();
            //bind grid


        }
        /// <summary>
        /// The method to delete a row of the grid view
        /// new instance of database created
        /// row found using grid view row cast as grid view
        /// inside Rows
        /// post id parsed as an int, inside data keys of name "Postid"
        /// toString()
        /// posts are found using Find(postid) in Posts
        /// posts is also found
        /// post is removed using Remove
        /// comments associated with the post
        /// are also deleted
        /// </summary>
        /// <param name="sender"></param> delete button
        /// 
        /// <param name="e"></param> row deleting
        protected void GrdPosts_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //Row row = (Row)sender;
            ApplicationDbContext db = new ApplicationDbContext();
            GridViewRow row = (GridViewRow)grdPosts.Rows[e.RowIndex];
            Label lbldeleteid = (Label)row.FindControl("lblpostID");

            int postid = Int32.Parse(grdPosts.DataKeys[e.RowIndex]["PostId"].ToString());

            var post = db.Posts.Find(postid);

            db.Posts.Remove(post);

            var commentsToDelete = from c in db.Comments
                                   where c.PostId == postid
                                   select c;

            db.Comments.RemoveRange(commentsToDelete);

            db.SaveChanges();


        }

        protected void GrdPosts_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //Row row = (Row)sender;
            ApplicationDbContext db = new ApplicationDbContext();
            GridView grdPosts = (GridView) sender;
            grdPosts.EditIndex = e.NewEditIndex;

            int rowId = e.NewEditIndex;
            //  (GridViewRow)grdPosts.Rows[e.RowIndex];
            

            int postId = Int32.Parse(grdPosts.DataKeys[rowId]["PostId"].ToString());

            var postfound = db.Posts.Find(postId);

            postId = postfound.PostId;

            var posts = from c in db.Posts
                                   where c.PostId == postId
                                   select c;

            grdPosts.DataSource = posts.ToList();
            grdPosts.DataBind();
            //bind data to grid

          


        }
        /// <summary>
        /// Updating row method
        /// new instance of database db is created
        /// rowId is found with rowIndex
        /// int postId is found using DataKeys and its name in the table
        /// the text from the post.Text value is sent to string
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GrdPosts_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            
            ApplicationDbContext db = new ApplicationDbContext();
            //new instance of database
            GridView grdPosts = (GridView)sender;

            int rowId = e.RowIndex;
            //  get row index


            int postId = (int) grdPosts.DataKeys[rowId]["PostId"];

            var postfound = db.Posts.Find(postId);
            //the post is found by postId
            postfound.Text = e.NewValues[3].ToString();
            postfound.Title = e.NewValues[4].ToString();
            //postfound.Text = ((TextBox)grdPosts.Rows[rowId].Cells[3].Controls[0]).Text;
            //postfound.Title =((TextBox)grdPosts.Rows[rowId].Cells[4].Controls[0]).Text;
            //the text from the post.Text value is sent to string
          
            //postfound.Text = ;
            //eUser.UserName = username;




            db.SaveChanges();
            BindGrid();

        }

        protected void GrdPosts_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
        /// <summary>
        /// Row cancelling edit allows user
        /// to change their mind and not edit the rows
        /// without errors
        /// the grid view is the sender
        /// editIndex is set to be -1
        /// go back and bind
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdPosts_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView grdPosts = (GridView)sender;
            //the grid view posts is the sender
            //must be cased as it is in the nested grid view
            grdPosts.EditIndex = -1;
            //if user chooses to cancel, go back to prior index
            BindGrid();
            //bind 
        }
    }
}