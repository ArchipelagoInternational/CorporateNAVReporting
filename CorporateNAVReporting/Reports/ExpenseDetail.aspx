<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ExpenseDetail.aspx.cs" Inherits="CorporateNAVReporting.Reports.ExpenseDetail" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <div class="row">
        <div class="col-md-12">
            <div class="x_panel">
                <p></p>

                <div class="x_title">
                    <h2>Account Trial Balance</h2>
                </div>

                <div class="x_content">

                    <!-- content starts here -->

                    <asp:UpdatePanel ID="upReport" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                        <ContentTemplate>
                            
                            Account :
                              <asp:DropDownList ID="comboAccounts" runat="server"  AutoPostBack="true" 
                                        onselectedindexchanged="comboAccounts_SelectedIndexChanged">
                                    </asp:DropDownList>

                              From : <asp:TextBox ID="txtFromDate" runat="server" TextMode="Date"></asp:TextBox>&nbsp;
                            To : <asp:TextBox ID="txtToDate" runat="server" TextMode="Date"></asp:TextBox>&nbsp;
                            
                             <asp:Button ID="btnReload" CssClass="btn btn-info btn-sm" runat="server"
                                 Text="View" OnClick="btnReload_Click" />
                            <hr />

                             <asp:Panel runat="server" Width="100%" ScrollBars="Auto">
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                    <ProgressTemplate>
                                        Processing... please wait
                                        <asp:Image ID="aspImg1" runat="server" ImageUrl="~/Images/ajax_loader.gif" />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>

                                   <rsweb:ReportViewer ID="ReportViewer1" runat="server"
                                    ProcessingMode="Local" AsyncRendering="false"
                                    WaitMessageFont-Names="Verdana"
                                    WaitMessageFont-Size="14pt"
                                    KeepSessionAlive="true"
                                    ShowBackButton="true"
                                    ShowPageNavigationControls="true"
                                    ShowToolBar="true"
                                    ShowPrintButton="true"
                                    ShowZoomControl="true"
                                    ShowExportControls="true"
                                    ShowRefreshButton="true"
                                    SizeToReportContent='True'>
                                </rsweb:ReportViewer>

                            </asp:Panel>


                        </ContentTemplate>
                        <Triggers>
                        </Triggers>
                    </asp:UpdatePanel>


                    <!-- content ends here -->
                </div>
            </div>
        </div>

    </div>

</asp:Content>

