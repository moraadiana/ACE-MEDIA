using Staffportal.Models;
using Staffportal.NAVWS;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Staffportal
{
    public class FinanceHelper
    {
        private static Staffportall webportals = Components.ObjNav;
        private static string[] strLimiters = new string[] { "::" };
        private static string[] strLimiters2 = new string[] { "[]" };

        public static List<Finance> GetMemoPrnLines(string memoNo)
        {
            var list = new List<Finance>();
            try
            {
                //string prnLines = webportals.GetMemoPrnLines(memoNo);
                //if (!string.IsNullOrEmpty(prnLines))
                //{
                //    string[] prnLinesArr = prnLines.Split(strLimiters2, StringSplitOptions.RemoveEmptyEntries);
                //    foreach (string line in prnLinesArr)
                //    {
                //        string[] responseArr = line.Split(strLimiters, StringSplitOptions.None);
                //        list.Add(new Finance()
                //        {
                //            ItemNo = responseArr[0],
                //            Description = responseArr[1],
                //            Quantity = Convert.ToDecimal(responseArr[2]),
                //            TotalAmount = Convert.ToDecimal(responseArr[3]),
                //            SystemId = responseArr[4]
                //        });
                //    }
                //}
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
            return list;
        }

        public static List<Finance> GetMemoDsaLines(string memoNo)
        {
            var list = new List<Finance>();
            try
            {
                //string prnLines = webportals.GetMemoDsaLines(memoNo);
                //if (!string.IsNullOrEmpty(prnLines))
                //{
                //    string[] prnLinesArr = prnLines.Split(strLimiters2, StringSplitOptions.RemoveEmptyEntries);
                //    foreach (string line in prnLinesArr)
                //    {
                //        string[] responseArr = line.Split(strLimiters, StringSplitOptions.None);
                //        list.Add(new Finance()
                //        {
                //            StaffName = responseArr[0],
                //            Region = responseArr[1],
                //            ImprestType = responseArr[2],
                //            GlAccount = responseArr[3],
                //            Rate = Convert.ToDecimal(responseArr[4]),
                //            Days = Convert.ToDecimal(responseArr[5]),
                //            Amount = Convert.ToDecimal(responseArr[6]),
                //            SystemId = responseArr[7],
                //        });
                //    }
                //}
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
            return list;
        }

        public static List<Finance> GetMemoOtherCostLines(string memoNo)
        {
            var list = new List<Finance>();
            try
            {
                //string prnLines = webportals.GetOtherCostLines(memoNo);
                //if (!string.IsNullOrEmpty(prnLines))
                //{
                //    string[] prnLinesArr = prnLines.Split(strLimiters2, StringSplitOptions.RemoveEmptyEntries);
                //    foreach (string line in prnLinesArr)
                //    {
                //        string[] responseArr = line.Split(strLimiters, StringSplitOptions.None);
                //        list.Add(new Finance()
                //        {
                //            StaffName = responseArr[0],
                //            Region = responseArr[1],
                //            ImprestType = responseArr[2],
                //            GlAccount = responseArr[3],
                //            Rate = Convert.ToDecimal(responseArr[4]),
                //            Days = Convert.ToDecimal(responseArr[5]),
                //            Amount = Convert.ToDecimal(responseArr[6]),
                //            SystemId = responseArr[7],
                //        });
                //    }
                //}
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
            return list;
        }

        public static List<Finance> GetImprests(string username)
        {
            var list = new List<Finance>();
            try
            {
                string prnLines = webportals.GetMyImprests(username);
                if (!string.IsNullOrEmpty(prnLines))
                {
                    int counter = 0;
                    string[] prnLinesArr = prnLines.Split(strLimiters2, StringSplitOptions.RemoveEmptyEntries);
                    Array.Reverse(prnLinesArr);
                    foreach (string line in prnLinesArr)
                    {
                        counter++;
                        string[] responseArr = line.Split(strLimiters, StringSplitOptions.None);
                        list.Add(new Finance()
                        {
                            DocumentNo = responseArr[0],
                            StaffName = responseArr[1],
                            Purpose = responseArr[2],
                            //Date = Convert.ToDateTime(responseArr[1]),
                            //StaffName = responseArr[2],
                            //Amount = Convert.ToDecimal(responseArr[3]),
                            Status = responseArr[3],
                            StatusCls = Components.StatusClass(responseArr[3]),
                            Counter = counter
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
            return list;
        }

        public static List<Finance> GetImprestsLines(string documentNo)
        {
            var list = new List<Finance>();
            try
            {
                string prnLines = webportals.GetImprestLines(documentNo);
                if (!string.IsNullOrEmpty(prnLines) && prnLines != "No Imprests lines")
                {
                    int counter = 0;
                    string[] prnLinesArr = prnLines.Split(strLimiters2, StringSplitOptions.RemoveEmptyEntries);
                    Array.Reverse(prnLinesArr);
                    foreach (string line in prnLinesArr)
                    {
                        counter++;
                        string[] responseArr = line.Split(strLimiters, StringSplitOptions.None);
                        list.Add(new Finance()
                        {
                            Counter = counter,
                            AdvanceType = responseArr[1],
                            AccountNo = responseArr[2],
                            AccountName = responseArr[3],
                            Amount = Convert.ToDecimal(responseArr[4]),
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
            return list;
        }

        public static List<Finance> GetPostedImprests(string username)
        {
            var list = new List<Finance>();
            try
            {
                string prnLines = webportals.GetPostedImprests(username);
                if (!string.IsNullOrEmpty(prnLines))
                {
                    int counter = 0;
                    string[] prnLinesArr = prnLines.Split(strLimiters2, StringSplitOptions.RemoveEmptyEntries);
                    Array.Reverse(prnLinesArr);
                    foreach (string line in prnLinesArr)
                    {
                        counter++;
                        string[] responseArr = line.Split(strLimiters, StringSplitOptions.None);
                        list.Add(new Finance()
                        {
                            DocumentNo = responseArr[0],
                            //Date = Convert.ToDateTime(responseArr[1]),
                            Date = DateTime.ParseExact(responseArr[1], "MM/dd/yy", CultureInfo.InvariantCulture),
                            StaffName = responseArr[2],
                            Amount = Convert.ToDecimal(responseArr[3]),
                            Status = responseArr[4],
                            StatusCls = Components.StatusClass(responseArr[4]),
                            Counter = counter
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
            return list;
        }

        public static List<Finance> GetImprestSurrenders(string username)
        {
            var list = new List<Finance>();
            try
            {
                //string prnLines = webportals.GetMyImprestSurrenders(username);
                //if (!string.IsNullOrEmpty(prnLines))
                //{
                //    int counter = 0;
                //    string[] prnLinesArr = prnLines.Split(strLimiters2, StringSplitOptions.RemoveEmptyEntries);
                //    Array.Reverse(prnLinesArr);
                //    foreach (string line in prnLinesArr)
                //    {
                //        counter++;
                //        string[] responseArr = line.Split(strLimiters, StringSplitOptions.None);
                //        list.Add(new Finance()
                //        {
                //            SurrenderNo = responseArr[0],
                //            SurrenderDate = Convert.ToDateTime(responseArr[1]),
                //            StaffName = responseArr[2],
                //            Amount = Convert.ToDecimal(responseArr[3]),
                //            Status = responseArr[4],
                //            StatusCls = Components.StatusClass(responseArr[4]),
                //            Counter = counter,
                //            DocumentNo = responseArr[5],
                //        });
                //    }
                //}
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
            return list;
        }

        public static List<Finance> GetImprestSurrenderDetails(string surrenderNo, string accountNo)
        {
            var list = new List<Finance>();
            try
            {
                string prnLines = webportals.LoadImprestSurrenderLineDetails(surrenderNo, accountNo);
                if (!string.IsNullOrEmpty(prnLines))
                {
                    int counter = 0;
                    string[] prnLinesArr = prnLines.Split(strLimiters2, StringSplitOptions.RemoveEmptyEntries);
                    Array.Reverse(prnLinesArr);
                    foreach (string line in prnLinesArr)
                    {
                        counter++;
                        string[] responseArr = line.Split(strLimiters, StringSplitOptions.None);
                        list.Add(new Finance()
                        {
                            ReceiptNo = responseArr[0],
                            ActualSpent = responseArr[1],
                            CashReturned = responseArr[2],
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
            return list;
        }

        public static List<Finance> GetMyClaims(string username)
        {
            var list = new List<Finance>();
            try
            {
                string prnLines = webportals.GetMyClaims(username);
                if (!string.IsNullOrEmpty(prnLines))
                {
                    int counter = 0;
                    string[] prnLinesArr = prnLines.Split(strLimiters2, StringSplitOptions.RemoveEmptyEntries);
                    Array.Reverse(prnLinesArr);
                    foreach (string line in prnLinesArr)
                    {
                        counter++;
                        string[] responseArr = line.Split(strLimiters, StringSplitOptions.None);
                        list.Add(new Finance()
                        {
                            DocumentNo = responseArr[0],
                            //Date = Convert.ToDateTime(responseArr[1]),
                            Date = DateTime.ParseExact(responseArr[1], "MM/dd/yy", CultureInfo.InvariantCulture),

                            StaffName = responseArr[2],
                            Purpose = responseArr[3],
                            Status = responseArr[4],
                            StatusCls = Components.StatusClass(responseArr[4])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
            return list;
        }

        public static List<Finance> GetMyClaimLines(string claimNo)
        {
            var list = new List<Finance>();
            try
            {
                string prnLines = webportals.GetClaimLines(claimNo);
                if (!string.IsNullOrEmpty(prnLines))
                {
                    int counter = 0;
                    string[] prnLinesArr = prnLines.Split(strLimiters2, StringSplitOptions.RemoveEmptyEntries);
                    Array.Reverse(prnLinesArr);
                    foreach (string line in prnLinesArr)
                    {
                        counter++;
                        string[] responseArr = line.Split(strLimiters, StringSplitOptions.None);
                        list.Add(new Finance()
                        {
                            AdvanceType = responseArr[0],
                            AccountNo = responseArr[1],
                            AccountName = responseArr[2],
                            Amount = Convert.ToDecimal(responseArr[3]),
                            SystemId = responseArr[4],
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
            return list;
        }

        public static List<Finance> GetMyPettyCashListing(string username)
        {
            var list = new List<Finance>();
            try
            {
                string prnLines = webportals.GetMyPettyCashRequests(username);
                if (!string.IsNullOrEmpty(prnLines))
                {
                    int counter = 0;
                    string[] prnLinesArr = prnLines.Split(strLimiters2, StringSplitOptions.RemoveEmptyEntries);
                    Array.Reverse(prnLinesArr);
                    foreach (string line in prnLinesArr)
                    {
                        counter++;
                        string[] responseArr = line.Split(strLimiters, StringSplitOptions.None);
                        list.Add(new Finance()
                        {
                            DocumentNo = responseArr[0],
                            Date = Convert.ToDateTime(responseArr[1]),
                            StaffName = responseArr[2],
                            Amount = Convert.ToDecimal(responseArr[3]),
                            Status = responseArr[4],
                            StatusCls = Components.StatusClass(responseArr[4])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
            return list;
        }

        public static List<Finance> GetMyPettyCashLines(string pettyCashNo)
        {
            var list = new List<Finance>();
            try
            {
                string prnLines = webportals.GetPettyCashLines(pettyCashNo);
                if (!string.IsNullOrEmpty(prnLines))
                {
                    int counter = 0;
                    string[] prnLinesArr = prnLines.Split(strLimiters2, StringSplitOptions.RemoveEmptyEntries);
                    Array.Reverse(prnLinesArr);
                    foreach (string line in prnLinesArr)
                    {
                        counter++;
                        string[] responseArr = line.Split(strLimiters, StringSplitOptions.None);
                        list.Add(new Finance()
                        {
                            AdvanceType = responseArr[0],
                            AccountNo = responseArr[1],
                            AccountName = responseArr[2],
                            Amount = Convert.ToDecimal(responseArr[3]),
                            SystemId = responseArr[4],
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
            return list;
        }

        public static List<Finance> GetMyPettyCashSurrenderListing(string username)
        {
            var list = new List<Finance>();
            try
            {
                string prnLines = webportals.GetMyPettyCashSurrenders(username);
                if (!string.IsNullOrEmpty(prnLines))
                {
                    int counter = 0;
                    string[] prnLinesArr = prnLines.Split(strLimiters2, StringSplitOptions.RemoveEmptyEntries);
                    Array.Reverse(prnLinesArr);
                    foreach (string line in prnLinesArr)
                    {
                        counter++;
                        string[] responseArr = line.Split(strLimiters, StringSplitOptions.None);
                        list.Add(new Finance()
                        {
                            SurrenderNo = responseArr[0],
                            DocumentNo = responseArr[1],
                            Date = Convert.ToDateTime(responseArr[2]),
                            StaffName = responseArr[3],
                            Amount = Convert.ToDecimal(responseArr[4]),
                            Status = responseArr[5],
                            StatusCls = Components.StatusClass(responseArr[5])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
            return list;
        }

        public static List<Finance> GetMyPettyCashSurrenderLines(string surrenderNo)
        {
            var list = new List<Finance>();
            try
            {
                string prnLines = webportals.GetPettyCashSurrenderLines(surrenderNo);
                if (!string.IsNullOrEmpty(prnLines))
                {
                    int counter = 0;
                    string[] prnLinesArr = prnLines.Split(strLimiters2, StringSplitOptions.RemoveEmptyEntries);
                    Array.Reverse(prnLinesArr);
                    foreach (string line in prnLinesArr)
                    {
                        counter++;
                        string[] responseArr = line.Split(strLimiters, StringSplitOptions.None);
                        list.Add(new Finance()
                        {
                            AccountNo = responseArr[0],
                            AccountName = responseArr[1],
                            Amount = Convert.ToDecimal(responseArr[2]),
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
            return list;
        }

        public static Finance GetMyPettyCashSurrenderDetails(string surrenderNo, string accountNo)
        {
            Finance finance = new Finance();
            try
            {
                //string response = webportals.GetPettyCashSurrenderDetails(surrenderNo, accountNo);
                //if (!string.IsNullOrEmpty(response))
                //{
                //    string[] responseArr = response.Split(strLimiters, StringSplitOptions.None);
                //    finance.ActualSpent = responseArr[0];
                //    finance.CashReturned = responseArr[1];
                //    finance.ReceiptNo = responseArr[2];
               // }
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
            return finance;
        }
    }
}