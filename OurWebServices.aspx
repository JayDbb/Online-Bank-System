<%@ Page Title="Our Web Services" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OurWebServices.aspx.cs" Inherits="Online_Bank_System.OurWebServices" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container mt-5">

        <div class="d-flex justify-content-center">
            <div class="card" style="border-color: #7c071c; width: 100%; max-width: 600px;">
                <div class="card-header text-white" style="background-color: #c93853;">
                    <h5 class="mb-0">Make Payment</h5>
                </div>
                <div class="card-body">

                    <!-- Check Account Section -->




                    <!-- Add Account Section -->


                    <!-- Submit Payment Section -->
                    <div class="form-group mb-3">
                        <h3 class="text-danger"></h3>
                        <asp:Label ID="lblSubmitPaymentResult" runat="server" Text="" CssClass="text-danger mb-2 d-block"></asp:Label>
                        <div class="d-flex align-items-center">
                            <div class="form-group mb-2 mr-3">
                                <label for="ddlRecipientAccount" class="form-label">Select Sender Account</label>

                                <asp:DropDownList
                                    ID="ddlRecipientAccount"
                                    CssClass="form-control m-0"
                                    runat="server"
                                    AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlRecipientAccount_SelectedIndexChanged">
                                </asp:DropDownList>
                                <p class="mt-2 text-muted">Balance:
                                    <asp:Label ID="lblRecipientBalance" runat="server" Text="0.00"></asp:Label></p>
                            </div>

                            <asp:TextBox ID="txtReceiverAccountID" runat="server" placeholder="Receiver Account ID" CssClass="form-control ml-2 mb-4"  Height="38px"/>

                        </div>
                        <div class="d-flex">
                            <asp:TextBox ID="txtAmount" runat="server" placeholder="Amount" CssClass="form-control m-0 mr-2" Width="50%" Height="38px"/>
                            <asp:TextBox ID="txtSenderPassword" runat="server" placeholder="Sender Password" TextMode="Password" CssClass="form-control w-50" />

                        </div>
                        <asp:Button ID="btnSubmitPayment" runat="server" Text="Submit Payment" OnClick="btnSubmitPayment_Click" CssClass="btn btn-danger w-100 m-0 mt-3" />
                    </div>

                </div>
            </div>
        </div>

    </div>

</asp:Content>
