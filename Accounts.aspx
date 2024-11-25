<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Accounts.aspx.cs" Inherits="Online_Bank_System.Accounts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div style="">
         <div class="form-group mb-3">
     <h3 class="text-danger">Check if Account Exists</h3>
     <asp:Label ID="lblCheckAccountResult" runat="server" Text="" CssClass=" mb-2 d-block"></asp:Label>
     <asp:TextBox ID="txtAccountID" runat="server" placeholder="Enter Account ID" CssClass="form-control m-0" />
     <asp:Button ID="btnCheckAccount" runat="server" Text="Check Account" OnClick="btnCheckAccount_Click" CssClass="btn btn-danger w-100 mt-3 m-0" />
 </div>
    </div>
    <hr />
    <h1>Account List</h1>
    <div class="d-flex flex-wrap">

   <asp:Repeater ID="AccRepeater" runat="server" OnItemDataBound="AccRepeater_ItemDataBound">
    <ItemTemplate>
        <div class="col-md-5 " style="margin-right: 20px;">
            <div class="card" style="border: 1px solid #7c071c; border-radius:8px">
                <div class="mx-3 mt-2 d-flex justify-content-between">
                    <div>
                        <span class="d-flex" style="align-items: baseline;">
                            <h3><%# Eval("Type") %></h3>
                            <p class="text-muted mx-1">Account #<%# Eval("ID") %></p>
                        </span>
                        <p>Balance: <%# String.Format("{0:C}", Eval("Balance")) %></p>
                    </div>
                    <asp:Button ID="iwhat" class="py-2 my-2 text-white btn btn-danger"
                        CommandArgument='<%# Eval("ID") %>' runat="server" Text="Remove"
                        OnClick="SaveChangesDel" />
                </div>
            </div>
        </div>

     
    </ItemTemplate>
</asp:Repeater>

    </div>

</asp:Content>
