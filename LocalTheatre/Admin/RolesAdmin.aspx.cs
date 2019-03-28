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
/// partial class role admin inherits from page
/// </summary>
    public partial class RolesAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindGridView();
            }

        }
        /// <summary>
        /// The method to bind the grid view.
        /// New instance of the database db
        /// the data source for the grid view of roles
        /// is set to database table Roles to List
        /// data is bound
        /// </summary>
        private void BindGridView()
        {
            //need new instance of database db
            ApplicationDbContext db = new ApplicationDbContext();

            //update datasource to take data from the roles grid

            grdRoles.DataSource = db.Roles.ToList();
            //sent the roles to list after pointing the data source to db.roles
            grdRoles.DataBind();
        }
        /// <summary>
        /// The method to create new roles.
        /// new Role manager instance of RoleManager instantiated
        /// sRole string holds the string value of txtRole
        /// if role is not empty, a new role is created
        /// the role is stored in the database
        /// User is prompted that it was successful
        /// </summary>
        /// <param name="sender"></param> button is the sender
        /// <param name="e"></param> creating new roles is the event
        protected void btnRole_Click(object sender, EventArgs e)
        {
            //new instance of application db context
            ApplicationDbContext db = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            //new inastance of role manager 

            String sRole = txtRole.Text;
            //strRole is storing the role input

            if(!sRole.Equals(""))
            {
                //if not empty
                if(!roleManager.RoleExists(sRole))
                //if exists
                {
                    //store the role inside of the database
                    IdentityRole newRole = new IdentityRole();
                    newRole.Name = sRole;

                    //boolean to return true if successful
                    bool successful = roleManager.Create(newRole).Succeeded;
                    if(successful)
                    {
                        lblErrorMessage.Text = "The role has been added";
                        BindGridView();
                        //bind the grid if successful
                        //otherwise prompt user about what went wrong

                    }
                    else
                    {
                        lblErrorMessage.Text = "There was an error.No new role added";
                    }
                }
                else
                {
                    lblErrorMessage.Text = "Role already in existence";
                }
            }
            else
            {
                lblErrorMessage.Text = "Enter input before attempting to add a new role.";
            }

        }
        /// <summary>
        /// Created to give user the option not to edit the role
        /// in case they change their mind
        /// sender is button
        /// event is as above
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void grdRoles_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            //in case user does not 
            //wish to edit any rows

            grdRoles.EditIndex = -1;
            //changes are not made
            BindGridView();
            //grid view is bound

        }
        /// <summary>
        /// 
        /// This enables deleting the row
        /// via new instance of db context
        /// capture the row index
        /// sent the role id to string
        ///after retrieving it from rowId of grid view grdRoles

        /// </summary>
        /// <param name="sender"></param> is button
        /// <param name="e"></param> event is delete
        protected void grdRoles_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //new instance of db context
            ApplicationDbContext db = new ApplicationDbContext();

            int rowId = e.RowIndex;
            //capture the row index

            String roleId = grdRoles.DataKeys[rowId].Value.ToString();
            //sent the role id to string
            //after retrieving it from rowId of grid view grdRoles

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            //new role manager intiated

            var role = roleManager.FindById(roleId);
            //role gets the stored role found by id

            roleManager.Delete(role);
            //delete the role
            BindGridView();
            //bind the grid


        }
        /// <summary>
        /// method to enable row deleting
        /// new index is established for grdRoles
        /// the grd view is bound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdRoles_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //this method enables row editing
            grdRoles.EditIndex = e.NewEditIndex;

            BindGridView();
            //update the grid by binding it to the table
        }
        /// <summary>
        /// row is updated via this method
        /// </summary>
        /// <param name="sender"></param> is button update
        /// <param name="e"></param> e is event to delete
        protected void grdRoles_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            //new instance of db context

            int rowId = e.RowIndex;
            //get the row index 

            String roleId = grdRoles.DataKeys[rowId]["Id"].ToString();
            //roleId is obtained

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
            var role = roleManager.FindById(roleId);
            //roles is stored
            String roleName = ((TextBox)grdRoles.Rows[rowId].Cells[1].Controls[0]).Text;
            //the roleName from the textbox is stored in the database
            role.Name = roleName;
            //the name is set to roleNumber
            roleManager.Update(role);
            db.SaveChanges();
            //changes saved to the database 
            grdRoles.EditIndex = -1;

            BindGridView();
            //the grid view is bound
        }
    }
}