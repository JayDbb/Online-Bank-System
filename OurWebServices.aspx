<%@ Page Title="Our Web Services" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OurWebServices.aspx.cs" Inherits="Online_Bank_System.OurWebServices" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container mt-5">

        <div class="d-flex justify-content-center">
            <div class="card" style="border-color: #7c071c; width: 100%; max-width: 600px;">
                <div class="card-header text-white" style="background-color: #c93853;">
                    <h5 class="mb-0">Account Management</h5>
                </div>
                <div class="card-body">
                    
                    <!-- Check Account Section -->
                    <div class="form-group mb-3">
                        <h3 class="text-danger">Check if Account Exists</h3>
                        <asp:Label ID="lblCheckAccountResult" runat="server" Text="" CssClass="text-danger mb-2 d-block"></asp:Label>
                        <asp:TextBox ID="txtAccountID" runat="server" placeholder="Enter Account ID" CssClass="form-control" />
                        <asp:Button ID="btnCheckAccount" runat="server" Text="Check Account" OnClick="btnCheckAccount_Click" CssClass="btn btn-danger w-100 mt-3" />
                    </div>

                    <hr />

                    <!-- Add Account Section -->
                    <div class="form-group mb-3">
                        <h3 class="text-danger">Add Account</h3>
                        <asp:Label ID="lblAddAccountResult" runat="server" Text="" CssClass="text-danger mb-2 d-block"></asp:Label>
                        <asp:TextBox ID="txtAddAccountID" runat="server" placeholder="Enter Account ID" CssClass="form-control" />
                        <asp:TextBox ID="txtPassword" runat="server" placeholder="Enter Password" TextMode="Password" CssClass="form-control mt-2" />
                        <asp:Button ID="btnAddAccount" runat="server" Text="Add Account" OnClick="btnAddAccount_Click" CssClass="btn btn-danger w-100 mt-3" />
                    </div>

                    <hr />

                    <!-- Submit Payment Section -->
                    <div class="form-group mb-3">
                        <h3 class="text-danger">Submit Payment</h3>
                        <asp:Label ID="lblSubmitPaymentResult" runat="server" Text="" CssClass="text-danger mb-2 d-block"></asp:Label>
                        <asp:TextBox ID="txtSenderAccountID" runat="server" placeholder="Sender Account ID" CssClass="form-control" />
                        <asp:TextBox ID="txtReceiverAccountID" runat="server" placeholder="Receiver Account ID" CssClass="form-control mt-2" />
                        <asp:TextBox ID="txtAmount" runat="server" placeholder="Amount" CssClass="form-control mt-2" />
                        <asp:TextBox ID="txtSenderPassword" runat="server" placeholder="Sender Password" TextMode="Password" CssClass="form-control mt-2" />
                        <asp:Button ID="btnSubmitPayment" runat="server" Text="Submit Payment" OnClick="btnSubmitPayment_Click" CssClass="btn btn-danger w-100 mt-3" />
                    </div>

                </div>
            </div>
        </div>

    </div>

</asp:Content>
