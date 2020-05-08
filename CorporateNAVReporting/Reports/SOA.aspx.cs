using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;
using System.Data;
using System.IO;
using System.Drawing.Printing;
using System.Text;
using System.Drawing.Imaging;
using System.Drawing;  

namespace CorporateNAVReporting.Reports
{
    public partial class SOA : System.Web.UI.Page
    {
        private static List<Stream> m_streams;
        private static int m_currentPageIndex = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                txtReportDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
                PopulateCustomerList();
            }

        }

        protected void btnReload_Click(object sender, EventArgs e)
        {
            ReloadData();
        }

        private void ReloadData()
        {
            DateTime reportDate;
            DateTime.TryParse(this.txtReportDate.Text, out reportDate);

            string hotelNo = string.Empty;

            if (this.comboHotel.SelectedIndex == -1)
                return;
            else if (this.comboHotel.SelectedIndex == 0)//all hotels
                hotelNo = "";
            else
                hotelNo = this.comboHotel.SelectedItem.Value;

            
            LoadSOA(reportDate, hotelNo);
        }

        private void LoadSOA(DateTime reportDate,  String hotelNo)
        {
            var tableDataSet = Helpers.AccountBL.GetSOA(hotelNo);

            DataTable soaTable = new DataTable("SOATable");
            soaTable = tableDataSet.Tables[0];

            DataTable bankTable = new DataTable("BankTable");
            bankTable = tableDataSet.Tables[1];
            String bankName = String.Empty, bankNo = String.Empty, bankBranch = String.Empty;

            if (bankTable != null && bankTable.Rows.Count > 0)
            {
                bankName = bankTable.Rows[0]["BankName"].ToString();
                bankNo = bankTable.Rows[0]["BankNo"].ToString();
                bankBranch = bankTable.Rows[0]["BankBranch"].ToString();
            }

            this.ReportViewer1.Reset();
            this.ReportViewer1.LocalReport.ReportPath = Server.MapPath("rvStatementOfAccounts.rdlc");

            //// Must match the DataSet in the RDLC
            ReportDataSource rds = new ReportDataSource("dsStatementOfAccount", soaTable);
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(rds);


            ReportParameter[] param = new ReportParameter[4];
            param[0] = new ReportParameter("mReportDate", reportDate.ToString("dd MMM yyyy"));
            param[1] = new ReportParameter("mBankName", bankName);
            param[2] = new ReportParameter("mBankNo", bankNo);
            param[3] = new ReportParameter("mBankBranch", bankBranch);
            ReportViewer1.LocalReport.SetParameters(param);

            this.ReportViewer1.DataBind();
            this.ReportViewer1.ShowPageNavigationControls = true;
            this.ReportViewer1.ShowPrintButton = true;
            this.ReportViewer1.LocalReport.Refresh();

            //PrintToPrinter(this.ReportViewer1.LocalReport);

        }

        //https://ashproghelp.blogspot.com/2017/01/how-to-print-directly-without-showing.html
        //https://www.youtube.com/watch?v=uTzwk6zBdQs
        //==========================

        //  LocalReport report = new LocalReport();
        //          string path = rptName;
        //          report.ReportPath = path;
        //          report.DataSources.Add(new ReportDataSource(dsName, ds.table[0]));
        //          report.SetParameters(parameters);
        //          PrintToPrinter(report);
        ////========================================================

        #region Print direct to default printer
        
        public static void PrintToPrinter(LocalReport report)
        {
            Export(report);

        }

        public static void Export(LocalReport report, bool print = true)
        {
            string deviceInfo =
             @"<DeviceInfo>
                <OutputFormat>EMF</OutputFormat>
                <PageWidth>21cm</PageWidth>
                <PageHeight>29.7cm</PageHeight>
                <MarginTop>1cm</MarginTop>
                <MarginLeft>0.5cm</MarginLeft>
                <MarginRight>0.5cm</MarginRight>
                <MarginBottom>1cm</MarginBottom>
            </DeviceInfo>";
            Warning[] warnings;
            m_streams = new List<Stream>();
            report.Render("Image", deviceInfo, CreateStream, out warnings);
            foreach (Stream stream in m_streams)
                stream.Position = 0;

            if (print)
            {
                Print();
            }
        }

        public static void Print()
        {
            if (m_streams == null || m_streams.Count == 0)
                throw new Exception("Error: no stream to print.");
            PrintDocument printDoc = new PrintDocument();
            if (!printDoc.PrinterSettings.IsValid)
            {
                throw new Exception("Error: cannot find the default printer.");
            }
            else
            {
                printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
                m_currentPageIndex = 0;
                printDoc.Print();
            }
        }

        public static Stream CreateStream(string name, string fileNameExtension, Encoding encoding, string mimeType, bool willSeek)
        {
            Stream stream = new MemoryStream();
            m_streams.Add(stream);
            return stream;
        }

        public static void PrintPage(object sender, PrintPageEventArgs ev)
        {
            Metafile pageImage = new
               Metafile(m_streams[m_currentPageIndex]);

            // Adjust rectangular area with printer margins.
            Rectangle adjustedRect = new Rectangle(
                ev.PageBounds.Left - (int)ev.PageSettings.HardMarginX,
                ev.PageBounds.Top - (int)ev.PageSettings.HardMarginY,
                ev.PageBounds.Width,
                ev.PageBounds.Height);

            // Draw a white background for the report
            ev.Graphics.FillRectangle(Brushes.White, adjustedRect);

            // Draw the report content
            ev.Graphics.DrawImage(pageImage, adjustedRect);

            // Prepare for the next page. Make sure we haven't hit the end.
            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }

        public static void DisposePrint()
        {
            if (m_streams != null)
            {
                foreach (Stream stream in m_streams)
                    stream.Close();
                m_streams = null;
            }
        }

        #endregion

        void PopulateCustomerList()
        {
            var tableDataSet = Helpers.AccountBL.GetHotelListForSOA();

            DataTable accountList = new DataTable("CustomerList");
            accountList = tableDataSet.Tables[0];

            List<System.Collections.Generic.KeyValuePair<string, string>> listAccounts = new List<System.Collections.Generic.KeyValuePair<string, string>>();

            for (int i = 0; i < accountList.Rows.Count; i++)
            {
                //string theValue = source.Rows[i].ItemArray[0].ToString();
                string theValue = accountList.Rows[i]["CustName"].ToString();
                string theKey = accountList.Rows[i]["CustNo"].ToString();
                listAccounts.Add(new KeyValuePair<string, string>(theKey, theValue));
            }


            this.comboHotel.DataSource = listAccounts;
            this.comboHotel.DataTextField = "Value";
            this.comboHotel.DataValueField = "Key";
            this.comboHotel.DataBind();
            comboHotel.Items.Insert(0, "--All Hotel--");
        }

        protected void comboHotel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboHotel.SelectedIndex == -1)
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