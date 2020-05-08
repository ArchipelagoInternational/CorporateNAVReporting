<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SOA.aspx.cs" Inherits="CorporateNAVReporting.Reports.SOA" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <div class="row">
        <div class="col-md-12">
            <div class="x_panel">
                <p></p>

                <div class="x_title">
                    <h2>Statement of Accounts</h2>
                </div>

                <div class="x_content">

                    <!-- content starts here -->

                    <asp:UpdatePanel ID="upReport" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                        <ContentTemplate>
                            

                         Hotel:  <asp:DropDownList ID="comboHotel" runat="server"  AutoPostBack="true" 
                                        onselectedindexchanged="comboHotel_SelectedIndexChanged">
                                    </asp:DropDownList>

                              Date : <asp:TextBox ID="txtReportDate" runat="server" TextMode="Date"></asp:TextBox> 
                            
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


