<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="View_Account.aspx.cs" Inherits="Online_Bank_System.View_Account" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!DOCTYPE html>
    <html xmlns="http://www.w3.org/1999/xhtml">
    <body>
        <div>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="AccountName" HeaderText="Account Name" />
                    <asp:BoundField DataField="Balance" HeaderText="Balance" />
                    <asp:BoundField DataField="Type" HeaderText="Type" />
                </Columns>
            </asp:GridView>
        </div>
    </body>
    </html>
</asp:Content>
