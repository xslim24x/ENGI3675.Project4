<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="unsafeinsert.aspx.cs" Inherits="Assign_4.UnsafeInsert" validateRequest="false" %>
    
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Springfield University GPA Inserter</title>
</head>
<body>
    <form id="unsafeinsert" runat="server">
    <div>
        <asp:Table ID="tblUnsafeAdd" runat="server" BorderWidth="1">
            <asp:TableHeaderRow>
                <asp:TableHeaderCell>Name</asp:TableHeaderCell>
                <asp:TableHeaderCell>GPA</asp:TableHeaderCell>
            </asp:TableHeaderRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:TextBox ID="sname" runat="server" />
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="sgpa" runat="server" />
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:Button ID="InsertStudent" runat="server" OnClick="InsertStudent_Click" Text="Add Entry" />
    </div>
    </form>
</body>
</html>
