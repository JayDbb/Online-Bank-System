<%@ Page Title="Deposit Funds" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Deposit.aspx.cs" Inherits="Online_Bank_System.Deposit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Deposit Section -->
    <div class="container my-5">
        <h2 class="text-center mb-4 text-danger">Deposit Funds into Your Account</h2>

        <!-- Deposit Form -->
        <div class=" d-flex justify-content-center">
            <div class="">
                                        <!-- Success/Error Message -->
                        <asp:Label ID="lblMessage" runat="server" CssClass="mb-3 d-block text-center text-danger" />

                        <!-- Amount Input Field -->
                        <div class="form-group">
                            <label for="txtAmount" class="font-weight-bold">Enter Amount to Deposit:</label>
                            <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control m-0 mt-2" placeholder="Amount" type="number" min="0" step="0.01" />
                        </div>

                        <!-- Deposit Button -->
                        <asp:Button ID="btnDeposit" runat="server" Text="Deposit" CssClass="btn btn-danger w-100 m-0 mt-3" OnClick="btnDeposit_Click" />

                   
            </div>
        </div>
    </div>

    <!-- Account Balance Section -->
    <div class="container my-5">
        <h3 class="text-center">Your Current Account Balance</h3>
        <div class="text-center">
            <asp:Label ID="Balance" runat="server"></asp:Label>
        </div>
    </div>

</asp:Content>
