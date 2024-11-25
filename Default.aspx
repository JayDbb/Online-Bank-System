<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Online_Bank_System._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Hero Section -->
    <div class="container-fluid text-white py-5" style="background-color:#c93853; border-radius:8px;border: 2px solid #7c071c ">
        <div class="container text-center">
            <h1 class="display-4">Welcome to the Online Bank System</h1>
            <p class="lead">Manage your bank account, top-up Flow & Sagicor accounts, and more!</p>
            <a href="/account/Register" class="btn btn-light btn-lg mt-3">Get Started</a>
        </div>
    </div>

    <!-- Key Features Section -->
    <div class="container my-5">
        <h2 class="text-center mb-4">Key Features</h2>
        <div class="row">
            <!-- Feature 1 -->
            <div class="col-md-4 text-center mb-4">
                <div class="card shadow-sm">
                    <div class="card-body">
                        <h5 class="card-title text-danger">Register & Log In</h5>
                        <p class="card-text">Create your account and securely log in to access your banking features.</p>
                        <a href="/account/Login" class="btn btn-danger">Learn More</a>
                    </div>
                </div>
            </div>
            <!-- Feature 2 -->
            <div class="col-md-4 text-center mb-4">
                <div class="card shadow-sm">
                    <div class="card-body">
                        <h5 class="card-title text-danger">Bank Account Management</h5>
                        <p class="card-text">View your balance, track transactions, and add funds to your account.</p>
                        <a href="/Accounts" class="btn btn-danger">Manage Account</a>
                    </div>
                </div>
            </div>
            <!-- Feature 3 -->
            <div class="col-md-4 text-center mb-4">
                <div class="card shadow-sm">
                    <div class="card-body">
                        <h5 class="card-title text-danger">Top-Up Flow & Sagicor Accounts</h5>
                        <p class="card-text">Use your bank balance to top-up external Flow and Sagicor accounts.</p>
                        <a href="/Top-Up" class="btn btn-danger">Start Topping Up</a>
                    </div>
                </div>
            </div>
        </div>
    </div>


    
    

</asp:Content>
