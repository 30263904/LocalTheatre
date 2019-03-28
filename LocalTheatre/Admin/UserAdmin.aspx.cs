using LocalTheatre.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LocalTheatre.Admin
{/// <summary>
/// user admin inherits from page
/// </summary>
    public partial class UserAdmin : System.Web.UI.Page
    {
        /// <summary>
        /// 
        ///GridView user is a global attribute object
        /// </summary>
        public object GridViewUser { get; set; }
        /// <summary>
        /// 
        /// the page is loaded via this method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param> event is page loading
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridView();
                //if not post back
                //bing grid view
            }
        }
        /// <summary>
        /// new db context
        /// new list of users
        /// setting data source of grid view ot user list
        /// data source pointed at users
        /// bind the grid
        /// </summary>
        private void BindGridView()
        {
            //new db context 
            ApplicationDbContext db = new ApplicationDbContext();
            //new list of users
            var users = db.Users.ToList();
            //setting data source of grid view to be users list
            gridViewUser.DataSource = users;
            //data source pointed at users
            gridViewUser.DataBind();
            //bind data
        }

            protected void GridViewUser_RowDataBound(object sender, GridViewRowEventArgs e)
            {
                //new db context 
                ApplicationDbContext db = new ApplicationDbContext();
                //get the row index
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    int rowIndex = e.Row.RowIndex;
                    //getting the user's id 
                    String userId = (String)gridViewUser.DataKeys[rowIndex]["Id"];
                    ApplicationUser u = db.Users.Find(userId);
                    //creating a new user manager
                    var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                    //using that to get the roles associated with user 
                    var roles = um.GetRoles(userId);
                    //conversion to a list of strings
                    List<String> lstRoles = roles.ToList();
                    //finding the nested gridview 
                    GridView gridview = (GridView)e.Row.Cells[0].FindControl("gridViewRoles");
                    //making second gridview data source be the list of roles
                    gridview.DataSource = lstRoles;
                    if (lstRoles.Count > 0)
                    {
                        gridview.ShowHeader = false;
                    }

                    //hiding the buttons initially 
                    gridview.DataBind();
                }

            }
        /// <summary>
        /// row editing method allows for rows to be selected and changed
        /// bind grid
        /// </summary>
        /// <param name="sender"></param> button
        /// <param name="e"></param>allow editing
            protected void GridViewUser_RowEditing(object sender, GridViewEditEventArgs e)
            {
                gridViewUser.EditIndex = -1;
               
                BindGridView();
        }
        /// <summary>
        /// updating the gried view of users
        /// allowing the user to change their email
        /// as per the GDPR
        /// using a new instance of a database db
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
            protected void GridViewUser_RowUpdating(object sender, GridViewUpdateEventArgs e)
            {
                ApplicationDbContext db = new ApplicationDbContext();
                GridView gridViewUser = (GridView)sender;
                String id = gridViewUser.DataKeys[e.RowIndex].Value.ToString();
                String username = ((TextBox)gridViewUser.Rows[e.RowIndex].Cells[1].Controls[0]).Text;
                String email = ((TextBox)gridViewUser.Rows[e.RowIndex].Cells[2].Controls[0]).Text;

                ApplicationUser eUser = db.Users.Find(id);
                eUser.Email = email;
                eUser.UserName = username;
                db.SaveChanges();
                gridViewUser.EditIndex = -1;
                BindGridView();
        }
        /// <summary>
        /// I need a method for deleting rows out of the grid view user.
        /// 
        /// ApplicationUser instance user is instantiated
        /// user is found using Find(userId)
        /// save changes
        /// bind grid
        /// </summary>
        /// <param name="sender"></param> is grid view
        /// <param name="e"></param> event is deleting a row

        protected void GridViewUser_RowDeleting(object sender, GridViewDeleteEventArgs e)
            {
                ApplicationDbContext db = new ApplicationDbContext();
                string userId = gridViewUser.DataKeys[e.RowIndex].Value.ToString();
                ApplicationUser user = db.Users.Find(userId);

                db.Users.Remove(user);
                db.SaveChanges();
                BindGridView();

        }


        /// <summary>
        /// in case user does not wish to edit
        /// </summary>
        /// <param name="sender"></param> cancel button pressed
        /// <param name="e"></param> event to cancel button pressed
        protected void GridViewUser_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
            {
                gridViewUser.EditIndex = -1;
                BindGridView();
            }
        /// <summary>
        /// getting context
        /// getting userid
        /// getting user from row
        /// getting userid from row
        /// create a user manager
        /// use user manager to get new roles
        /// get rid of old roles
        /// get index from role
        /// find the control by calling row
        /// get selected row value
        /// add new role
        /// save changed and bind grid
        /// </summary>
        /// <param name="sender"></param> is the grid view
        /// <param name="e"></param> is the event
            protected void GridViewRoles_RowUpdating(object sender, GridViewUpdateEventArgs e)
            {
                ApplicationDbContext db = new ApplicationDbContext();
                GridView grdRoles = (GridView)sender;
                GridViewRow userRow = (GridViewRow)grdRoles.Parent.Parent;
                int index = userRow.RowIndex;
                string userId = gridViewUser.DataKeys[index]["Id"].ToString();
                var user = db.Users.Find(userId);
                var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                var roles = um.GetRoles(userId);
                um.RemoveFromRoles(userId, roles.ToArray());
                GridViewRow row = grdRoles.Rows[e.RowIndex];
                DropDownList ddl = (DropDownList)row.FindControl("DropDownList1");
                string role = ddl.SelectedValue.ToString();
                
                
                um.RemoveFromRoles(userId, roles.ToArray());
                um.AddToRole(userId, role.ToString());

                db.SaveChanges();
                

                BindGridView();
        }
        /// <summary>
        /// Add role to the user
        /// get rowIndex of user row
        /// put grid view in edit mode
        /// get user row from outer gridview
        /// use that row to get user id
        /// get user id from data keys collection
        /// //get user manager
        /// //ger user roles
        /// //set data source
        /// //bind the grid view
        /// grind is bound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
            protected void GridViewRoles_RowEditing(object sender, GridViewEditEventArgs e)
            {
            ApplicationDbContext db = new ApplicationDbContext();
            GridView grv = (GridView)sender;
            grv.EditIndex = e.NewEditIndex;
            GridViewRow userRow = (GridViewRow)grv.Parent.Parent;
            int index = userRow.RowIndex;
            string userId = gridViewUser.DataKeys[e.NewEditIndex]["Id"].ToString();
            var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var roles = um.GetRoles(userId);
            grv.DataSource = roles;
            grv.DataBind();

            }
        /// <summary>
        /// deleting a row in grid view roles is implemented here
        /// 
        /// </summary>
        /// <param name="sender"></param> is gridViewRoles
        /// <param name="e"></param>event is deleting the roles
            protected void GridViewRoles_RowDeleting(object sender, GridViewDeleteEventArgs e)
            {
                ApplicationDbContext db = new ApplicationDbContext();

                //get user id from first grid view 
                string id = gridViewUser.DataKeys[gridViewUser.SelectedIndex].Value.ToString();
                //get role from the second grid view 
                GridView grd2 = ((GridView)sender);

                string txtRole = grd2.Rows[e.RowIndex].Cells[2].Text;
                //creating a new user manager
                var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

                um.RemoveFromRole(id, txtRole);
                db.SaveChanges();
                grd2.DataBind();

            }
        /// <summary>
        /// Panel Button is clicked event is handled here
        /// new instance of database db
        /// selcted index is found and for the grid view user and its value is sent to string
        /// and embodied by string is
        /// UserManager is set up
        /// Button is the sender
        /// Panel is found using cells and controls [0]
        /// drop down list is found by find control
        /// 
        /// </summary>
        /// 
        /// <param name="sender"></param> Panel Button is sender
        /// <param name="e"></param>Panel Button is clicked event 
        protected void PanelButton_Click(object sender, EventArgs e)
            {
                //new application db context

                ApplicationDbContext db = new ApplicationDbContext();

                //get user id from first gridview
                string id = gridViewUser.DataKeys[gridViewUser.SelectedIndex].Value.ToString();
               
            //user manager
            var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                Button btn = (Button)sender;
                GridViewRow row = (GridViewRow)btn.NamingContainer;
                Panel pnl1 = (Panel)row.Cells[0].Controls[0].FindControl("Panel1");

                DropDownList ddl = (DropDownList)row.Cells[0].Controls[0].FindControl("DropDownList2");
                string role = ddl.SelectedValue;
                um.AddToRole(id, role);

                GridView grd2 = (GridView)row.Parent.Parent;
                grd2.EditIndex = -1;
                pnl1.Visible = false;
                BindGridView();
        }
        }
    }

