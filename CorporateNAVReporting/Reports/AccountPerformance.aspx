<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AccountPerformance.aspx.cs" Inherits="CorporateNAVReporting.Reports.AccountPerformance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <div class="row">
        <div class="col-md-12">
            <div class="x_panel">
                <p></p>

                <div class="x_title">
                    <h2>Account Performance Summary</h2>
                </div>

                <div class="x_content">

                    <!-- content starts here -->

                 
                    Account No : <asp:DropDownList ID="comboAccount" runat="server" OnSelectedIndexChanged="comboAccount_SelectedIndexChanged">
                                            <asp:ListItem Text="11201002 - AR CLEARANCE" Value="11201002"></asp:ListItem>
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

                             Posting Date From : <asp:TextBox ID="txtFromDate" runat="server" TextMode="Date"></asp:TextBox>&nbsp;
                            To : <asp:TextBox ID="txtToDate" runat="server" TextMode="Date"></asp:TextBox>&nbsp;
                            
                             <asp:Button ID="btnDownload" CssClass="btn btn-info btn-sm" runat="server"
                                 Text="Download" OnClick="btnDownload_Click" />
                            <hr />

                           

                  


                    <!-- content ends here -->
                </div>
            </div>
        </div>

    </div>

</asp:Content>

