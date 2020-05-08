using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CorporateNAVReporting.Helpers
{
    public class AccountBL
    {

        public static DataSet GetAccountPerformance(DateTime FromDate, DateTime ToDate, String AccountNo)
        {
            try
            {
                using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CorporateNAVDataExchangeConnection"].ConnectionString))
                {
                    con.Open();

                    #region Get data
                    //also include data from master accounts and direct invoices

                    SqlDataAdapter LogDataAdapter = new SqlDataAdapter("[ARCH_SummaryProgram]", con);
                    LogDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                    SqlParameter paramFromPostingDate = new SqlParameter("@FromPostingDate", SqlDbType.VarChar);
                    paramFromPostingDate.Value = FromDate.ToString("MM-dd-yyyy");
                    LogDataAdapter.SelectCommand.Parameters.Add(paramFromPostingDate);

                    SqlParameter paramToPostingDate = new SqlParameter("@ToPostingDate", SqlDbType.VarChar);
                    paramToPostingDate.Value = ToDate.ToString("MM-dd-yyyy");
                    LogDataAdapter.SelectCommand.Parameters.Add(paramToPostingDate);

                    SqlParameter paramAccountNo = new SqlParameter("@AccountNo", SqlDbType.VarChar);
                    paramAccountNo.Value = AccountNo;
                    LogDataAdapter.SelectCommand.Parameters.Add(paramAccountNo);


                    DataSet LogDataSet = new DataSet();
                    LogDataAdapter.Fill(LogDataSet);

                    //DataTable inventoryTable = new DataTable("AccountTable");
                    //inventoryTable = LogDataSet.Tables[0];

                    return LogDataSet;
                    #endregion


                }
            }
            catch (Exception)
            {

                return null;
            }


        }

        public static DataTable GetVATDetail(DateTime FromDate, DateTime ToDate, String AccountNo)
        {
            try
            {
                using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CorporateNAVDataExchangeConnection"].ConnectionString))
                {
                    con.Open();

                    #region Get data
                    //also include data from master accounts and direct invoices

                    SqlDataAdapter LogDataAdapter = new SqlDataAdapter("[ARCH_VATDetail]", con);
                    LogDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                    SqlParameter paramFromPostingDate = new SqlParameter("@FromPostingDate", SqlDbType.VarChar);
                    paramFromPostingDate.Value = FromDate.ToString("MM-dd-yyyy");
                    LogDataAdapter.SelectCommand.Parameters.Add(paramFromPostingDate);

                    SqlParameter paramToPostingDate = new SqlParameter("@ToPostingDate", SqlDbType.VarChar);
                    paramToPostingDate.Value = ToDate.ToString("MM-dd-yyyy");
                    LogDataAdapter.SelectCommand.Parameters.Add(paramToPostingDate);

                    SqlParameter paramAccountNo = new SqlParameter("@AccountNo", SqlDbType.VarChar);
                    paramAccountNo.Value = AccountNo;
                    LogDataAdapter.SelectCommand.Parameters.Add(paramAccountNo);


                    DataSet LogDataSet = new DataSet();
                    LogDataAdapter.Fill(LogDataSet);

                    DataTable inventoryTable = new DataTable("AccountTable");
                    inventoryTable = LogDataSet.Tables[0];

                    return inventoryTable;
                    #endregion


                }
            }
            catch (Exception)
            {

                return null;
            }


        }

        public static DataSet GetExpenseDetail(DateTime FromDate, DateTime ToDate, String AccountNo)
        {
            try
            {
                using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CorporateNAVDataExchangeConnection"].ConnectionString))
                {
                    con.Open();

                    #region Get data
                    //also include data from master accounts and direct invoices

                    SqlDataAdapter LogDataAdapter = new SqlDataAdapter("[ARCH_ExpenseDetail]", con);
                    LogDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                    SqlParameter paramFromPostingDate = new SqlParameter("@FromPostingDate", SqlDbType.VarChar);
                    paramFromPostingDate.Value = FromDate.ToString("MM-dd-yyyy");
                    LogDataAdapter.SelectCommand.Parameters.Add(paramFromPostingDate);

                    SqlParameter paramToPostingDate = new SqlParameter("@ToPostingDate", SqlDbType.VarChar);
                    paramToPostingDate.Value = ToDate.ToString("MM-dd-yyyy");
                    LogDataAdapter.SelectCommand.Parameters.Add(paramToPostingDate);

                    SqlParameter paramAccountNo = new SqlParameter("@AccountNo", SqlDbType.VarChar);
                    paramAccountNo.Value = AccountNo;
                    LogDataAdapter.SelectCommand.Parameters.Add(paramAccountNo);


                    DataSet LogDataSet = new DataSet();
                    LogDataAdapter.Fill(LogDataSet);


                    return LogDataSet;
                    #endregion


                }
            }
            catch (Exception)
            {

                return null;
            }


        }

        public static DataSet GetSOA(String HotelNo)
        {
            try
            {
                using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CorporateNAVDataExchangeConnection"].ConnectionString))
                {
                    con.Open();

                    #region Get data
                    //also include data from master accounts and direct invoices

                    SqlDataAdapter LogDataAdapter = new SqlDataAdapter("[CustomerStatementOfAccounts]", con);
                    LogDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                    if (HotelNo.Length > 0)
                    {
                        SqlParameter paramAccountNo = new SqlParameter("@HotelNo", SqlDbType.VarChar);
                        paramAccountNo.Value = HotelNo;
                        LogDataAdapter.SelectCommand.Parameters.Add(paramAccountNo);
                    }

                    DataSet LogDataSet = new DataSet();
                    LogDataAdapter.Fill(LogDataSet);

                    return LogDataSet;
                    #endregion


                }
            }
            catch (Exception)
            {

                return null;
            }


        }

        public static DataSet GetAccountAging(DateTime FromDate, DateTime ToDate, DateTime AgingDate, String AccountNo, String AccountFilter)
        {
            try
            {
                using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CorporateNAVDataExchangeConnection"].ConnectionString))
                {
                    con.Open();

                    #region Get data
                    //also include data from master accounts and direct invoices

                    SqlDataAdapter LogDataAdapter = new SqlDataAdapter("[ARCH_AccountAging]", con);
                    LogDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                    SqlParameter paramFromPostingDate = new SqlParameter("@FromPostingDate", SqlDbType.VarChar);
                    paramFromPostingDate.Value = FromDate.ToString("MM-dd-yyyy");
                    LogDataAdapter.SelectCommand.Parameters.Add(paramFromPostingDate);

                    SqlParameter paramToPostingDate = new SqlParameter("@ToPostingDate", SqlDbType.VarChar);
                    paramToPostingDate.Value = ToDate.ToString("MM-dd-yyyy");
                    LogDataAdapter.SelectCommand.Parameters.Add(paramToPostingDate);

                    SqlParameter paramAgingDate = new SqlParameter("@AgingDate", SqlDbType.VarChar);
                    paramAgingDate.Value = AgingDate.ToString("MM-dd-yyyy");
                    LogDataAdapter.SelectCommand.Parameters.Add(paramAgingDate);

                    SqlParameter paramAccountNo = new SqlParameter("@AccountNo", SqlDbType.VarChar);
                    paramAccountNo.Value = AccountNo;
                    LogDataAdapter.SelectCommand.Parameters.Add(paramAccountNo);

                    SqlParameter paramAccountFilter = new SqlParameter("@AccountFilter", SqlDbType.VarChar);
                    paramAccountFilter.Value = AccountFilter;
                    LogDataAdapter.SelectCommand.Parameters.Add(paramAccountFilter);


                    DataSet LogDataSet = new DataSet();
                    LogDataAdapter.Fill(LogDataSet);


                    return LogDataSet;
                    #endregion


                }
            }
            catch (Exception)
            {

                return null;
            }


        }

        public static DataSet GetExpensesAccountList()
        {
            try
            {
                using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CorporateNAVDataExchangeConnection"].ConnectionString))
                {
                    con.Open();

                    #region Get data
                    //also include data from master accounts and direct invoices

                    SqlDataAdapter LogDataAdapter = new SqlDataAdapter("[ARCH_GetExpensesAccountList]", con);
                    LogDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                    DataSet LogDataSet = new DataSet();
                    LogDataAdapter.Fill(LogDataSet);

                    return LogDataSet;
                    #endregion


                }
            }
            catch (Exception)
            {

                return null;
            }


        }

        public static DataSet GetHotelListForSOA()
        {
            try
            {
                using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CorporateNAVDataExchangeConnection"].ConnectionString))
                {
                    con.Open();

                    #region Get data
                    //also include data from master accounts and direct invoices

                    SqlDataAdapter LogDataAdapter = new SqlDataAdapter("[ARCH_GetHotelListForSOA]", con);
                    LogDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                    DataSet LogDataSet = new DataSet();
                    LogDataAdapter.Fill(LogDataSet);

                    return LogDataSet;
                    #endregion


                }
            }
            catch (Exception)
            {

                return null;
            }


        }

    }
}