<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="safeshowall.aspx.cs" Inherits="Assign_4.SafeShowAll" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Springfield University GPA Table</title>
    </head>
    <body>
            <form id="safeshowall" runat="server">
        <div>
            <asp:Table ID="tblsafeselect" runat="server">
                <asp:TableHeaderRow>
                    <asp:TableHeaderCell>ID</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Name</asp:TableHeaderCell>
                    <asp:TableHeaderCell>GPA</asp:TableHeaderCell>
                </asp:TableHeaderRow>
            </asp:Table>
        </div>
        </form>
    </body>
</html>
