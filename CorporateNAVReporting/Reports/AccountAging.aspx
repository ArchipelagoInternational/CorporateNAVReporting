<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AccountAging.aspx.cs" Inherits="CorporateNAVReporting.Reports.AccountAging" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="x_panel">
                <p></p>

                <div class="x_title">
                    <h2>Aged Account Receivable</h2>
                </div>

                <div class="x_content">

                    <!-- content starts here -->

                    <asp:UpdatePanel ID="upReport" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                        <ContentTemplate>


                            <table border="0">
                                <tr>
                                    <td>Posting Date From 
                                    </td>
                                    <td>:
                                        <asp:TextBox ID="txtFromDate" runat="server" TextMode="Date"></asp:TextBox>&nbsp;
                            To :
                                        <asp:TextBox ID="txtToDate" runat="server" TextMode="Date"></asp:TextBox>&nbsp;
                           
                            
                             
                                    </td>
                                </tr>
                                <tr>
                                    <td>Account No</td>
                                    <td>:
                                        <asp:DropDownList ID="comboAccount" runat="server" OnSelectedIndexChanged="comboAccount_SelectedIndexChanged">
                                            <asp:ListItem Text="11201002 - AR CLEARANCE" Value="11201002"></asp:ListItem>
                                            <asp:ListItem Text="11201002 - AR CLEARANCE - Email Account" Value="11201002E"></asp:ListItem>
                                            <asp:ListItem Text="11201002 - AR CLEARANCE - Cloud Server" Value="11201002C"></asp:ListItem>
                                            <asp:ListItem Text="11201002 - AR CLEARANCE - Google Ads" Value="11201002G"></asp:ListItem>
                                            <asp:ListItem Text="21501001 - DEPOSIT TECHNICAL" Value="21501001"></asp:ListItem>
                                            <asp:ListItem Text="41101001 - Management Fee" Value="41101001"></asp:ListItem>
                                            <asp:ListItem Text="41101002 - Sales & Marketing Fee" Value="41101002"></asp:ListItem>
                                            <asp:ListItem Text="41101003 - Incentive Fee" Value="41101003"></asp:ListItem>
                                            <asp:ListItem Text="41101007 - Others" Value="41101007"></asp:ListItem>
                                            <asp:ListItem Text="60312003 - OPERATIONS - PRINTING & STATIONARY" Value="60312003"></asp:ListItem>
                                            <asp:ListItem Text="60912019 - GA - REIMBURSE PMS, SERVERS & ECOMM " Value="60912019"></asp:ListItem>
                                            <asp:ListItem Text="60912099 - GA - OTHERS" Value="60912099"></asp:ListItem>
                                            <asp:ListItem Text="61011001 - IT - SALARY & WAGES" Value="61011001"></asp:ListItem>
                                            <asp:ListItem Text="61512016 - SM - REIMBURSHMENT PROMOTIONAL PROGRAM " Value="61512016"></asp:ListItem>
                                        </asp:DropDownList>

                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                         Aged as of this date 
                                    </td>
                                    <td>
                                        :
                                        <asp:TextBox ID="txtAgingDate" runat="server" TextMode="Date"></asp:TextBox>&nbsp;
                                        <asp:Button ID="btnReload" CssClass="btn btn-info btn-sm" runat="server"
                                 Text="View" OnClick="btnReload_Click" />
                                    </td>
                                </tr>
                            </table>

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


