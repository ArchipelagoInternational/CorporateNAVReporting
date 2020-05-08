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
    public partial class ExpenseDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                txtFromDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
                txtToDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
                PopulateExpensesAccount();
            }

        }

        protected void btnReload_Click(object sender, EventArgs e)
        {
            ReloadData();
        }

        private void ReloadData()
        {
            DateTime reportFromDate;
            DateTime.TryParse(this.txtFromDate.Text, out reportFromDate);

            DateTime reportToDate;
            DateTime.TryParse(this.txtToDate.Text, out reportToDate);

            string accountNo = string.Empty;

            if (this.comboAccounts.SelectedIndex == -1 || this.comboAccounts.SelectedIndex == 0)
            {
                return;
            }
            else
            {
                accountNo = this.comboAccounts.SelectedItem.Value;
            }

            LoadExpenseDetail(reportFromDate, reportToDate, accountNo);
        }

        private void LoadExpenseDetail(DateTime fromDate, DateTime toDate, String AccountNo)
        {
            var tableDataSet = Helpers.AccountBL.GetExpenseDetail(fromDate, toDate, AccountNo);

           
            DataTable expenseTable = new DataTable("ExpenseTable");
            expenseTable = tableDataSet.Tables[0];

            DataTable accountNameTable = new DataTable("AccountName");
            accountNameTable = tableDataSet.Tables[1];

           
            String accountName = String.Empty;
            if (accountNameTable != null && accountNameTable.Rows.Count > 0)
                accountName = accountNameTable.Rows[0]["AccountName"].ToString();

            this.ReportViewer1.Reset();
            this.ReportViewer1.LocalReport.ReportPath = Server.MapPath("rvExpenseDetail.rdlc");

            //// Must match the DataSet in the RDLC
            ReportDataSource rds = new ReportDataSource("dsExpenseDetail", expenseTable);
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(rds);


            ReportParameter[] param = new ReportParameter[2];
            param[0] = new ReportParameter("mPostingDate", fromDate.ToString("dd MMM yyyy") + " - " + toDate.ToString("dd MMM yyyy"));
            param[1] = new ReportParameter("mAccountNo", AccountNo + " - " + accountName);
            ReportViewer1.LocalReport.SetParameters(param);

            this.ReportViewer1.DataBind();
            this.ReportViewer1.ShowPageNavigationControls = true;
            this.ReportViewer1.ShowPrintButton = true;
            this.ReportViewer1.LocalReport.Refresh();

        }

        void PopulateExpensesAccount()
        {
            var tableDataSet = Helpers.AccountBL.GetExpensesAccountList();

            DataTable accountList = new DataTable("AccountList");
            accountList = tableDataSet.Tables[0];

            List<System.Collections.Generic.KeyValuePair<string, string>> listAccounts = new List<System.Collections.Generic.KeyValuePair<string, string>>();

            for (int i = 0; i < accountList.Rows.Count; i++)
            {
                //string theValue = source.Rows[i].ItemArray[0].ToString();
                string theValue = accountList.Rows[i]["AccountName"].ToString();
                string theKey = accountList.Rows[i]["AccountNo"].ToString();
                listAccounts.Add(new KeyValuePair<string, string>(theKey, theValue));
            }


            this.comboAccounts.DataSource = listAccounts;
            this.comboAccounts.DataTextField = "Value";
            this.comboAccounts.DataValueField = "Key";
            this.comboAccounts.DataBind();
            comboAccounts.Items.Insert(0, "Select Account..");
        }

        protected void comboAccounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboAccounts.SelectedIndex == -1 || this.comboAccounts.SelectedIndex == 0)
            {
                return;
            }
            else
            {
                
                ReloadData();
            }
        }
    }
}