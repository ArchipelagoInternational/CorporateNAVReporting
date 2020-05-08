using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;  

namespace CorporateNAVReporting.Reports
{
    public partial class AccountAging : System.Web.UI.Page
    {
        //https://www.sqlchick.com/entries/2011/8/20/repeating-column-headers-on-every-page-in-ssrs-doesnt-work-o.html
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                txtFromDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
                txtToDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
                txtAgingDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
                this.comboAccount.SelectedIndex = 0;
            }

        }

        protected void btnReload_Click(object sender, EventArgs e)
        {
            DateTime reportFromDate;
            DateTime.TryParse(this.txtFromDate.Text, out reportFromDate);

            DateTime reportToDate;
            DateTime.TryParse(this.txtToDate.Text, out reportToDate);

            DateTime agingDate;
            DateTime.TryParse(this.txtAgingDate.Text, out agingDate);

            String accountNo = string.Empty;
            String accountFilter = String.Empty;
            if (this.comboAccount.SelectedIndex != -1)
            {
                accountNo = this.comboAccount.SelectedValue;
                if (accountNo == "11201002E")
                {
                    accountNo = "11201002";
                    accountFilter = "Email";
                }
                else if (accountNo == "11201002C")
                {
                    accountNo = "11201002";
                    accountFilter = "Cloud";
                }
                else if (accountNo == "11201002G")
                {
                    accountNo = "11201002";
                    accountFilter = "Google";
                }
                else
                    accountFilter = "All";
            }

            LoadAccountAging(reportFromDate, reportToDate, agingDate, accountNo, accountFilter);
        }

        private void LoadAccountAging(DateTime fromDate, DateTime toDate, DateTime AgingDate, String AccountNo, string accountFilter)
        {
            var tableDataSet = Helpers.AccountBL.GetAccountAging(fromDate, toDate, AgingDate, AccountNo, accountFilter);

            DataTable agingTable = new DataTable("AgingTable");
            agingTable = tableDataSet.Tables[0];

            DataTable accountTable = new DataTable("AccountTable");
            accountTable = tableDataSet.Tables[1];
            String accountName = String.Empty;
            if (accountTable != null && accountTable.Rows.Count > 0)
                accountName = accountTable.Rows[0]["AccountName"].ToString();

            this.ReportViewer1.Reset();
            this.ReportViewer1.LocalReport.ReportPath = Server.MapPath("rvAccountAging.rdlc");

            //// Must match the DataSet in the RDLC
            ReportDataSource rds = new ReportDataSource("dsAccountAging", agingTable);
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(rds);


            ReportParameter[] param = new ReportParameter[3];
            param[0] = new ReportParameter("mPostingDate", fromDate.ToString("dd MMM yyyy") + " - " + toDate.ToString("dd MMM yyyy"));
            param[1] = new ReportParameter("mAccountNo", AccountNo + " - " + accountName);
            param[2] = new ReportParameter("mAgingDate", AgingDate.ToString("dd MMM yyyy"));
            ReportViewer1.LocalReport.SetParameters(param);

            this.ReportViewer1.DataBind();
            this.ReportViewer1.ShowPageNavigationControls = true;
            this.ReportViewer1.ShowPrintButton = true;
            this.ReportViewer1.LocalReport.Refresh();

        }

        protected void comboAccount_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


   
    }
}