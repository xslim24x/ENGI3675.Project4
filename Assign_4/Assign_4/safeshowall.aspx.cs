//-----------------------------------------------------------------------
// <copyright file="safeshowall.aspx.cs" company="LakeheadU">
//     Copyright ENGI-3675. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Assign_4
{
    using System;
    using System.Data;
    using System.Net;
    using System.Web.UI.WebControls;
    using Assign_4.App_Code.ServerConn;
    
    /// <summary>
    /// SafeShowAll class responsible for displaying the webpage containing the full database table
    /// </summary>
    public partial class SafeShowAll : System.Web.UI.Page
    {
        /// <summary>
        /// Contains the code to execute SELECT * query and display full table
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable results = ServerConn.MyQuery("SELECT * FROM students");
            foreach (DataRow d in results.Rows)
            {
                TableRow myrow = new TableRow();
                TableCell c1 = new TableCell();
                TableCell c2 = new TableCell();
                TableCell c3 = new TableCell();
                
                c1.Text = WebUtility.HtmlEncode(d.ItemArray[0].ToString());
                c2.Text = WebUtility.HtmlEncode(d.ItemArray[1].ToString());
                c3.Text = WebUtility.HtmlEncode(d.ItemArray[2].ToString()); 

                myrow.Cells.Add(c1);
                myrow.Cells.Add(c2);
                myrow.Cells.Add(c3);

                this.tblsafeselect.Rows.Add(myrow);
            }
        }
    }
}