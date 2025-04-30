using Staffportal.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using Staffportal.NAVWS;
using System.Web.UI.WebControls;
using System.Globalization;

namespace Staffportal
{
    public class HRHelper
    {
        private static Staffportall webportals = Components.ObjNav;
        private static string[] strLimiters = new string[] { "::" };
        private static string[] strLimiters2 = new string[] { "[]" };
        private static string[] strLimiters3 = new string[] { "|" };
        public static List<HumanResource> GetLeaveRequests(string username)
        {
            var list = new List<HumanResource>();
            try
            {
                string leaveRequests = webportals.GetMyleaveApplications(username);
                if (!string.IsNullOrEmpty(leaveRequests))
                {
                    string[] leaveRequestsArr = leaveRequests.Split(strLimiters2, StringSplitOptions.RemoveEmptyEntries);
                    Array.Reverse(leaveRequestsArr);
                    foreach (string leaveRequest in leaveRequestsArr)
                    {
                        string[] responseArr = leaveRequest.Split(strLimiters, StringSplitOptions.None);

                        DateTime parsedDate, parsedStartDate, parsedEndDate, parsedReturnDate;
                        string dateFormat = "MM/dd/yy"; // Adjust based on your actual date format

                        bool isDateValid = DateTime.TryParseExact(responseArr[3], dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate);
                        bool isStartDateValid = DateTime.TryParseExact(responseArr[4], dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedStartDate);
                        bool isEndDateValid = DateTime.TryParseExact(responseArr[5], dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedEndDate);
                        bool isReturnDateValid = DateTime.TryParseExact(responseArr[6], dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedReturnDate);

                        list.Add(new HumanResource()
                        {
                            
                            LeaveNo = responseArr[0],
                            LeaveType = responseArr[1],
                            AppliedDays = string.IsNullOrEmpty(responseArr[2]) ? 0 : Convert.ToDecimal(responseArr[2]),
                            Date = isDateValid ? parsedDate : DateTime.Now,
                            StartDate = isStartDateValid ? parsedStartDate : DateTime.Now,
                            EndDate = isEndDateValid ? parsedEndDate : DateTime.Now,
                            ReturnDate = isReturnDateValid ? parsedReturnDate : DateTime.Now,
                            Status = responseArr[7],
                            StatusCls = Components.StatusClass(responseArr[7])
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
        public static List<Config> GetPeriodYears()
        {
            var list = new List<Config>();
            try
            {
                string payslipYears = webportals.GetPayslipYears();
                if (!string.IsNullOrEmpty(payslipYears))
                {
                    string[] yearsArr = payslipYears.Split(strLimiters2, StringSplitOptions.RemoveEmptyEntries);

                    // Directly add all unique years to the dropdown
                    foreach (string year in yearsArr.Distinct()) // Remove duplicates if any
                    {
                        list.Add(new Config()
                        {
                            Year = yearsArr[0],
                            
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
        public static List<Config> GetPeriodMonths(int Year)
        {
            var list = new List<Config>();
            try
            {
                string payslipMonths = webportals.GetPayslipMonths(Year);
                if (!string.IsNullOrEmpty(payslipMonths))
                {
                    string[] monthsArr = payslipMonths.Split(strLimiters2, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string months in monthsArr)
                    {

                        string[] responseArr = months.Split(strLimiters, StringSplitOptions.None);
                        if (responseArr.Length == 2)
                        {
                            list.Add(new Config()
                            {
                                monthNumber = responseArr[0],
                                monthName = responseArr[1]

                            });
                           
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }

            list.Sort((a, b) => a.monthNumber.CompareTo(b.monthName));
            return list;

        }
        public static List<Config> GetLeaveTypes(string gender)
        {
            var list = new List<Config>();
            try
            {
                string leaveRequests = webportals.GetLeaveTypes(gender);
                if (!string.IsNullOrEmpty(leaveRequests))
                {
                    string[] leaveRequestsArr = leaveRequests.Split(strLimiters2, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string leaveRequest in leaveRequestsArr)
                    {
                        string[] responseArr = leaveRequest.Split(strLimiters, StringSplitOptions.None);
                        list.Add(new Config()
                        {
                            Code = responseArr[0],
                            Description = responseArr[1]
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
            list.Sort((a, b) => a.Description.CompareTo(b.Description));
            return list;
        }
       

        public static string ValidateLeaveStartDate(DateTime startDate)
        {
            string valid = string.Empty;
            try
            {
               
                valid = webportals.ValidateStartDate(startDate);
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
            return valid;
        }

        public static HumanResource CalculateDates(DateTime startDate, int appliedDays, string leaveType)
        {
            HumanResource dates = new HumanResource();
            try
            {
                string endDate = webportals.CalcEndDate(startDate, appliedDays, leaveType).ToString();
                string returnDate = webportals.CalcReturnDate(Convert.ToDateTime(endDate), leaveType).ToString();
                dates.EndingDate = endDate;
                dates.ReturningDate = returnDate;
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
            return dates;
        }

        public static string LoadLeaveBalance(string username, string leaveType)
        {
            string balance = string.Empty;
            try
            {
                if (leaveType == "ANNUAL") balance = webportals.AvailableLeaveDays(username, leaveType);
                else balance = webportals.GetDefaultDays(leaveType);
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
            return balance == "0" ? "Not Available" : balance;
        }

        public static List<HumanResource> GetLeaveTransactions(string username)
        {
            var list = new List<HumanResource>();
            try
            {
                string leaveRequests = webportals.GetLeaveTransactions(username);
                if (!string.IsNullOrEmpty(leaveRequests))
                {
                    string[] leaveRequestsArr = leaveRequests.Split(strLimiters2, StringSplitOptions.RemoveEmptyEntries);
                    Array.Reverse(leaveRequestsArr);
                    foreach (string leaveRequest in leaveRequestsArr)
                    {
                        string[] responseArr = leaveRequest.Split(strLimiters, StringSplitOptions.None);
                        list.Add(new HumanResource()
                        {
                            LeaveType = responseArr[0],
                            TransactionDate = Convert.ToDateTime(responseArr[1]),
                            TransactionType = responseArr[2],
                            NoOfDays = responseArr[3],
                            TransactionDescription = responseArr[4]
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
    }
}