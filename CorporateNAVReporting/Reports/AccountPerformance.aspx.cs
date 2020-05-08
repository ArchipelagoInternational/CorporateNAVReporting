using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using System.IO;
using System.Data;
using NPOI.SS.Util;

namespace CorporateNAVReporting.Reports
{
    public partial class AccountPerformance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                txtFromDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
                txtToDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
                this.comboAccount.SelectedIndex = 0;
            }
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            DateTime reportFromDate;
            DateTime.TryParse(this.txtFromDate.Text, out reportFromDate);

            DateTime reportToDate;
            DateTime.TryParse(this.txtToDate.Text, out reportToDate);

            String accountNo = string.Empty;
            if (this.comboAccount.SelectedIndex != -1)
            {
                accountNo = this.comboAccount.SelectedValue;
            }

            UpdateExcel(reportFromDate, reportToDate, accountNo);
        }

        private void UpdateExcel(DateTime fromDate, DateTime toDate, String accountNo)
        {
            //https://www.c-sharpcorner.com/blogs/export-to-excel-using-npoi-dll-library
            //http://www.zachhunter.com/2010/05/npoi-excel-template/

            var tableDataSet = Helpers.AccountBL.GetAccountPerformance(fromDate, toDate, accountNo);

            DataTable dataTable = new DataTable("DataSummaryTable");
            dataTable = tableDataSet.Tables[0];

            DataTable accountTable = new DataTable("AccountTable");
            accountTable = tableDataSet.Tables[1];
            String accountName = String.Empty;
            if (accountTable != null && accountTable.Rows.Count > 0)
                accountName = accountTable.Rows[0]["AccountName"].ToString();

            UpdateExcelWithNPOI(dataTable, fromDate, toDate, accountNo, accountName);
        }

        public void UpdateExcelWithNPOI(DataTable dt, DateTime fromDate, DateTime toDate, String accountNo, String accountName)
        {
            // Open Template
            FileStream fs = new FileStream("C:\\DRRTemplate\\CorporateFinance\\Corporate_Account_Performance_Master.xlsx", FileMode.Open, FileAccess.Read);
            //FileStream fs = new FileStream(Server.MapPath(@"\Reports\DRR_Grogol_Master.xlsx"), FileMode.Open, FileAccess.Read);
            // Load the template into a NPOI workbook
            XSSFWorkbook templateWorkbook = new XSSFWorkbook(fs);

            // Load the sheet you are going to use as a template into NPOI
            ISheet sheetMacro = templateWorkbook.GetSheet("Macro");

            #region Cell Style
            IDataFormat format = templateWorkbook.CreateDataFormat();

            var fontBold = templateWorkbook.CreateFont();
            fontBold.FontHeightInPoints = 10;
            fontBold.FontName = "Calibri";
            fontBold.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;

            var fontSmall = templateWorkbook.CreateFont();
            fontSmall.FontHeightInPoints = 10;
            fontSmall.FontName = "Calibri";
            //fontSmall.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Normal;


            ICellStyle dateStyle = templateWorkbook.CreateCellStyle();
            dateStyle.DataFormat = format.GetFormat("dd/MM/yyyy");
            dateStyle.Alignment = HorizontalAlignment.Right;
            dateStyle.VerticalAlignment = VerticalAlignment.Center;
            dateStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            dateStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            dateStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            dateStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            dateStyle.SetFont(fontSmall);


            ICellStyle amountStyle = templateWorkbook.CreateCellStyle();
            amountStyle.DataFormat = format.GetFormat("#,##0");  // #.00
            amountStyle.Alignment = HorizontalAlignment.Right;
            amountStyle.VerticalAlignment = VerticalAlignment.Center;
            amountStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            amountStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            amountStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            amountStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            amountStyle.SetFont(fontSmall);

            ICellStyle percentageStyle = templateWorkbook.CreateCellStyle();
            percentageStyle.DataFormat = format.GetFormat("#,##0.00");  // #.00
            percentageStyle.Alignment = HorizontalAlignment.Right;
            percentageStyle.VerticalAlignment = VerticalAlignment.Center;
            percentageStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            percentageStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            percentageStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            percentageStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            percentageStyle.SetFont(fontSmall);

            ICellStyle normalStyleLeft = templateWorkbook.CreateCellStyle();
            normalStyleLeft.Alignment = HorizontalAlignment.Left;
            normalStyleLeft.VerticalAlignment = VerticalAlignment.Center;
            normalStyleLeft.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            normalStyleLeft.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            normalStyleLeft.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            normalStyleLeft.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            normalStyleLeft.SetFont(fontSmall);

            ICellStyle normalStyleCenter = templateWorkbook.CreateCellStyle();
            normalStyleCenter.Alignment = HorizontalAlignment.Center;
            normalStyleCenter.VerticalAlignment = VerticalAlignment.Center;
            normalStyleCenter.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            normalStyleCenter.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            normalStyleCenter.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            normalStyleCenter.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            normalStyleCenter.SetFont(fontSmall);           
            
            var CenterBoldStyle = templateWorkbook.CreateCellStyle();
            CenterBoldStyle.VerticalAlignment = VerticalAlignment.Center;
            CenterBoldStyle.Alignment = HorizontalAlignment.Center;
            CenterBoldStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            CenterBoldStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            CenterBoldStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            CenterBoldStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            CenterBoldStyle.SetFont(fontBold);

            #endregion
            
            #region Merged Column Header
            
            //make merged header row for month and year name
            int mergedRowNumber = 5;
            IRow rowMonthName = sheetMacro.CreateRow(mergedRowNumber);

            //create merged column based on number of month
            int totalColumn = dt.Columns.Count;
            int numberOfColumnInOneMonth = 4;
            int numberOfMonth = (totalColumn - 2) / numberOfColumnInOneMonth;//exclude 2 first column (CustNo and CustName)
           
            int startCol = 2, endCol = 5;
            for (int j = 0; j < numberOfMonth; j++)
            {
                var cra = new NPOI.SS.Util.CellRangeAddress(mergedRowNumber, mergedRowNumber, startCol, endCol);
                sheetMacro.AddMergedRegion(cra);

                ICell cell = rowMonthName.CreateCell(startCol);
                cell.SetCellType(CellType.String);

                //add border in merged cells
                RegionUtil.SetBorderBottom(1, cra, sheetMacro, templateWorkbook);
                RegionUtil.SetBorderTop(1, cra, sheetMacro, templateWorkbook);
                RegionUtil.SetBorderLeft(1, cra, sheetMacro, templateWorkbook);
                RegionUtil.SetBorderRight(1, cra, sheetMacro, templateWorkbook);

                cell.CellStyle = CenterBoldStyle;
                
                cell.SetCellValue(fromDate.AddMonths(j).ToString("MMM yyyy"));

                startCol += numberOfColumnInOneMonth;
                endCol += numberOfColumnInOneMonth;


                //to remove 
                //<start>
                //var cra1 = new NPOI.SS.Util.CellRangeAddress(5, 5, 2, 5);
                //var cra2 = new NPOI.SS.Util.CellRangeAddress(5, 5, 6, 9);
                //sheetMacro.AddMergedRegion(cra1);
                //sheetMacro.AddMergedRegion(cra2);

                //ICell cell1 = rowMonthName.CreateCell(2);
                ////ICell cell1 = sheetMacro.GetRow(5).GetCell(2);
                //cell1.SetCellType(CellType.String);
                //cell1.SetCellValue("Month 1");

                //ICell cell2 = rowMonthName.CreateCell(6);
                ////ICell cell2 = sheetMacro.GetRow(5).GetCell(6);
                //cell2.SetCellType(CellType.String);
                //cell2.SetCellValue("Month 2");
                //<end>
            }

            #endregion

            #region Column Name

            //make a header row  for colmun name
            //starting row 6 (zero based index=6) > 
            //CustNo, CustName, InvoiceM1, InvoiceM2,..InvoiceM[n], PaymentM1, PaymentM2,..PaymentM[n], LastPostingDateM1,LastPostingDateM2,..LastPostingDateM[n]

            IRow row1 = sheetMacro.CreateRow(6);

            for (int j = 0; j < dt.Columns.Count; j++)
            {
                ICell cell = row1.CreateCell(j);

                cell.CellStyle = CenterBoldStyle;
               
                String columnName = dt.Columns[j].ToString().Split('-')[0].ToString();
                if (columnName == "CustNo")
                    columnName = "Customer No.";
                if (columnName == "CustName")
                    columnName = "Name";
                else if (columnName == "Invoice")
                    columnName= "Actual";
                else if (columnName == "Payment")
                    columnName= "Collection";
                else if (columnName == "LastPostingDate")
                    columnName = "Col Date";
                else if (columnName == "Percentage")
                    columnName = "%";
                cell.SetCellValue(columnName);
            }

            #endregion

            #region Data
            
            //loops through data  
            //get column lastPostingDate
            int dateColumnStart = 4, percentageColumnStart=5;
            int numberofColumnPerMonth = 4;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow row = sheetMacro.CreateRow(i + 7);//starting row 7, for data
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    //set decimal for invoice and payment
                    //date for last posting date
                    //text for custNo and custName
                    if (j <= 1)
                    {
                        //text
                        ICell cell = row.CreateCell(j);
                        String columnName = dt.Columns[j].ToString();
                        cell.SetCellType(CellType.String);
                        cell.CellStyle = normalStyleLeft;
                        cell.SetCellValue(dt.Rows[i][columnName].ToString());
                    }
                    else if ((j - dateColumnStart) % numberofColumnPerMonth == 0)//column 4 8 12 ..
                    {
                        //date
                        ICell cell = row.CreateCell(j);
                        String columnName = dt.Columns[j].ToString();
                        cell.SetCellType(CellType.String);
                        cell.CellStyle = dateStyle;
                        if (dt.Rows[i][columnName] != DBNull.Value)
                            cell.SetCellValue(Convert.ToDateTime(dt.Rows[i][columnName]));
                    }
                    else if ((j - percentageColumnStart) % numberofColumnPerMonth == 0) //column 5 9 13 ..
                    {
                        //percentage
                        ICell cell = row.CreateCell(j);
                        String columnName = dt.Columns[j].ToString();
                        cell.SetCellType(CellType.Numeric);
                        cell.CellStyle = percentageStyle;
                        if (dt.Rows[i][columnName] != DBNull.Value)
                            cell.SetCellValue(Convert.ToDouble(dt.Rows[i][columnName]));
                        else
                            cell.SetCellValue(0);
                    }
                    else
                    {
                        //decimal amount
                        ICell cell = row.CreateCell(j);
                        String columnName = dt.Columns[j].ToString();
                        cell.SetCellType(CellType.Numeric);
                        cell.CellStyle = amountStyle;

                        if (dt.Rows[i][columnName] != DBNull.Value)
                            cell.SetCellValue(Convert.ToDouble(dt.Rows[i][columnName]));
                        else
                            cell.SetCellValue(0);
                    }

                }

            }

            #endregion


            //Auto size column
            for (int j = 2; j < dt.Columns.Count; j++)
            {
                sheetMacro.AutoSizeColumn(j);
            }

            #region Parameter
            
            //set parameter value
            int columnParameter = 1;
            IRow rowFromDate = sheetMacro.GetRow(1);
            IRow rowToDate = sheetMacro.GetRow(2);
            IRow rowAccountNo = sheetMacro.GetRow(3);
            IRow rowAccountName = sheetMacro.GetRow(4);
            //cellstyle 
                //rowFromDate.GetCell(columnParameter).CellStyle
            rowFromDate.GetCell(columnParameter).SetCellValue(fromDate);
            rowToDate.GetCell(columnParameter).SetCellValue(toDate);
            rowAccountNo.GetCell(columnParameter).SetCellValue(accountNo);
            rowAccountName.GetCell(columnParameter).SetCellValue(accountName);


            #endregion

            #region Save to File
            

            XSSFFormulaEvaluator.EvaluateAllFormulaCells(templateWorkbook);
            using (var exportData = new MemoryStream())
            {
                Response.Clear();
                templateWorkbook.Write(exportData);
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", "Corporate_AccountSummary_" + fromDate.ToString("ddMMMyyyy")+"_" + toDate.ToString("ddMMMyyyy") + ".xlsx"));
                Response.BinaryWrite(exportData.ToArray());
                Response.End();
            }

            #endregion
        }

        protected void comboAccount_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
     
    }
}





//The main reason that you are not seeing the changes is because you are writing the workbook to a MemoryStream instead of writing back to the file. What you should be doing is this:

//using a FileStream in Read mode to read the spreadsheet file completely;
//making the changes you want, then
//using a FileStream in Write mode to write back to the file (or optionally write to a different file if you don't want to destroy the original -- this may be better for testing).
//Also note it is good practice to use using statements when working with classes that implement IDisposable (all Streams do). This will ensure that the file is closed and all resources used by the stream are cleaned up properly.

//There is another problem I see in your code, and that is you are apparently trying to use an HSSFWorkbook with an .xlsx file. That won't work. HSSFWorkbook (and all HSSF classes) are for .xls files. If you need to work with .xlsx files, then you should be using XSSFWorkbook and related XSSF classes instead. Note that both flavors of classes implement common interfaces like IWorkbook, ISheet, IRow, etc. to help reduce code duplication should you need to support both types of files. I recommend using them where possible. But you may find you still need to downcast occasionally to access certain features that are not covered by the interfaces.

//One other thing I should mention: if a particular row x contains no cells in the original workbook, then GetRow(x) will return null. Similarly, GetCell(y) will return null if cell y is empty. If you want to be able to set the value of the cell regardless, you will need to check for nulls and use CreateRow(x) and/or CreateCell(y) as appropriate to ensure each respective entity exists.

//Here is the revised code:

//string pathSource = @"C:\Users\mvmurthy\Downloads\VOExportTemplate.xlsx";

//IWorkbook templateWorkbook;
//using (FileStream fs = new FileStream(pathSource, FileMode.Open, FileAccess.Read))
//{
//    templateWorkbook = new XSSFWorkbook(fs);
//}

//string sheetName = "ImportTemplate";
//ISheet sheet = templateWorkbook.GetSheet(sheetName) ?? templateWorkbook.CreateSheet(sheetName);
//IRow dataRow = sheet.GetRow(4) ?? sheet.CreateRow(4);
//ICell cell = dataRow.GetCell(1) ?? dataRow.CreateCell(1);
//cell.SetCellValue("foo");

//using (FileStream fs = new FileStream(pathSource, FileMode.Create, FileAccess.Write))
//{
//    templateWorkbook.Write(fs);
//}