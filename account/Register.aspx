<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Online_Bank_System.account.Register" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <main aria-labelledby="title">
        <h2 id="title"><%: Title %>.</h2>
            <style>
main {
    margin: auto;
    max-width: 500px;
    border: 1px solid #dcdcdc; /* Light gray border */
    border-radius: 10px;
    padding: 20px;
    box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1); /* Subtle shadow */
}

h2, h4 {
    color: #b71c1c; /* Red header text */
}

hr {
    border: none;
    border-top: 1px solid #b71c1c; /* Red horizontal line */
}

label {
    font-weight: bold;
    color: #333; /* Dark gray for labels */
}

.form-control {
    border: 1px solid #ccc; /* Light gray border */
    border-radius: 5px;
    padding: 10px;
    margin-top: 5px; /* Adds space between the label and textbox */
    margin-bottom: 15px; /* Adds space below the textbox */
}

.btn {
    background-color: #b71c1c; /* Red button background */
    color: #fff; /* White text */
    border: none;
    padding: 10px 20px;
    border-radius: 5px;
}

.btn:hover {
    background-color: #8b1a1a; /* Darker red for hover */
}

.text-danger {
    color: #d9534f; /* Bootstrap danger color */
}

p {
    margin-top: 10px;
}

a {
    color: #b71c1c; /* Red links */
    text-decoration: none;
}

a:hover {
    text-decoration: underline;
}
</style>
        <p class="text-danger">
            <asp:Literal runat="server" ID="ErrorMessage" />
        </p>
        <hr />
        <asp:ValidationSummary runat="server" CssClass="text-danger" />
        <div class="row">
            <asp:Label runat="server" AssociatedControlID="Name"  CssClass="col-md-2 col-form-label">Name</asp:Label>
            <div class="col-md-10">

                <asp:TextBox ID="Username" runat="server" CssClass="form-control" />
        
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Name"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="The Name field is required." />
            </div>
        </div>
        <div class="row">
            <asp:Label runat="server" AssociatedControlID="Email"  CssClass="col-md-2 col-form-label">Email</asp:Label>
            <div class="col-md-10">

                <asp:TextBox ID="Email" runat="server" CssClass="form-control"  TextMode="Email" />
                
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Email"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="The email field is required." />
            </div>
        </div>
        <div class="row">
            <asp:Label runat="server" AssociatedControlID="Password" CssClass="col-md-2 col-form-label">Password</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Password"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="The password field is required." />
            </div>
        </div>
        <div class="row">
            <asp:Label runat="server" AssociatedControlID="ConfirmPassword" CssClass="col-md-2 col-form-label">Confirm password</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmPassword"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="The confirm password field is required." />
                <asp:CompareValidator runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="The password and confirmation password do not match." />
            </div>
        </div>
        <div class="row">
            <div class="col-md-10">
                <asp:Button runat="server" OnClick="CreateUser_Click" Text="Register" CssClass="m-0 btn btn-outline-dark" />
            </div>
        </div>
    </main>
</asp:Content>
