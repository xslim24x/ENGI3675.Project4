//-----------------------------------------------------------------------
// <copyright file="unsafeinsert.aspx.cs" company="LakeheadU">
//     Copyright ENGI-3675. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Assign_4
{
    using System;
    using Assign_4.App_Code.ServerConn;

    /// <summary>
    /// UnsafeInsert class responsible for displaying a webpage with the ability to insert into database
    /// </summary>
    public partial class UnsafeInsert : System.Web.UI.Page
    {
        /// <summary>
        /// Page_Load constructor
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            // Leave empty
        }

        /// <summary>
        /// Contains the code to insert a students and their GPA into the database
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        protected void InsertStudent_Click(object sender, EventArgs e)
        {
            ServerConn.UnsafeAdd(this.sname.Text, float.Parse(this.sgpa.Text));

            this.sname.Text = string.Empty;
            this.sgpa.Text = string.Empty;
        }
    }
}