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
    public partial class VAT : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                txtFromDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
                txtToDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
            }

        }

        protected void btnReload_Click(object sender, EventArgs e)
        {
            DateTime reportFromDate;
            DateTime.TryParse(this.txtFromDate.Text, out reportFromDate);

            DateTime reportToDate;
            DateTime.TryParse(this.txtToDate.Text, out reportToDate);

            string accountNo = this.txtAccountNo.Text;

            LoadVATDetail(reportFromDate, reportToDate, accountNo);
        }

        private void LoadVATDetail(DateTime fromDate, DateTime toDate, String AccountNo)
        {
            var tableData = Helpers.AccountBL.GetVATDetail(fromDate, toDate, AccountNo);
            this.ReportViewer1.Reset();
            this.ReportViewer1.LocalReport.ReportPath = Server.MapPath("rvVATDetail.rdlc");

            //// Must match the DataSet in the RDLC
            DataTable accountTable = new DataTable("AccountTable");
            accountTable = tableData;


            ReportDataSource rds = new ReportDataSource("dsVATDetail", accountTable);
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(rds);


            ReportParameter[] param = new ReportParameter[2];
            param[0] = new ReportParameter("mPostingDate", fromDate.ToString("dd MMM yyyy") + " - " + toDate.ToString("dd MMM yyyy"));
            param[1] = new ReportParameter("mAccountNo", AccountNo);
            ReportViewer1.LocalReport.SetParameters(param);

            this.ReportViewer1.DataBind();
            this.ReportViewer1.ShowPageNavigationControls = true;
            this.ReportViewer1.ShowPrintButton = true;
            this.ReportViewer1.LocalReport.Refresh();

        }
    }
}