<%@ Page Title="Log in" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Online_Bank_System.account.Login" Async="true" %>


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

        <div class="col-md-10">
            <section id="loginForm">
                <div class="row">
                  
                    <hr />
                    <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                        <p class="text-danger">
                            <asp:Literal runat="server" ID="FailureText" />
                        </p>
                    </asp:PlaceHolder>
                    <div class="row">
                        <asp:Label runat="server" AssociatedControlID="Email" CssClass="col-md-2 col-form-label">Email</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="Email" CssClass="form-control" TextMode="Email" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Email"
                                CssClass="text-danger" Display="Dynamic" ErrorMessage="The email field is required." />
                        </div>
                    </div>
                    <div class="row">
                        <asp:Label runat="server" AssociatedControlID="Password" CssClass="col-md-2 col-form-label">Password</asp:Label>
                        <br />
                        <div class="col-md-10">
                           <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" CssClass="text-danger" Display="Dynamic" ErrorMessage="The password field is required." />
                        </div>
                    </div>
                    <div class="row">
                        <div class="offset-md-2 col-md-10">
                            <div class="checkbox">
                                <asp:CheckBox runat="server" ID="RememberMe" />
                                <asp:Label runat="server" AssociatedControlID="RememberMe">Remember me?</asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-offset-md-2 col-md-10">
                            <asp:Button runat="server" OnClick="LogIn" Text="Log in" CssClass="btn m-0 btn-outline-dark" />
                        </div>
                    </div>
                </div>
                <p>
                    <asp:HyperLink runat="server" ID="RegisterHyperLink" ViewStateMode="Disabled">Register as a new user</asp:HyperLink>
                </p>
            </section>
        </div>

    </main>
</asp:Content>
