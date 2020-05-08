<%@ Page Title="Archipelago International" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CorporateNAVReporting._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>AII - Corporate NAV Reporting</h1>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Sales & Revenue</h2>
            <p>
                <asp:HyperLink runat="server" NavigateUrl="~/Reports/VAT.aspx" Text="VAT Detail"></asp:HyperLink><br />
                <asp:HyperLink runat="server" NavigateUrl="~/Reports/AccountAging.aspx" Text="Aged Account Receivable"></asp:HyperLink><br />
            </p>
        </div>
        <div class="col-md-4">
            <h2>Purchase & Expense</h2>
            <p>
                <asp:HyperLink runat="server" NavigateUrl="~/Reports/ExpenseDetail.aspx" Text="Expense Account Detail"></asp:HyperLink><br />
            </p>
        </div>
        <div class="col-md-4">
            <h2>Account Performance</h2>
            <p>
                <asp:HyperLink runat="server" NavigateUrl="~/Reports/AccountPerformance.aspx" Text="Account Summary Performance"></asp:HyperLink><br />
                <asp:HyperLink runat="server" NavigateUrl="~/Reports/SOA.aspx" Text="Customer Statement of Account"></asp:HyperLink><br />
            </p>
        </div>
    </div>

</asp:Content>
