using Staffportal.Models;
using Staffportal.NAVWS;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Staffportal
{
    public class Helper
    {
        private static Staffportall webportals = Components.ObjNav;
        private static string[] strLimiters = new string[] { "::" };
        private static string[] strLimiters2 = new string[] { "[]" };

        public static List<Config> GetJobs()
        {
            var list = new List<Config>();
            try
            {
                //string result = webportals.GetJobs();
                //if (!string.IsNullOrEmpty(result))
                //{
                //    string[] resultsArr = result.Split(strLimiters2, StringSplitOptions.RemoveEmptyEntries);
                //    foreach (string str in resultsArr)
                //    {
                //        string[] responseArr = str.Split(strLimiters, StringSplitOptions.None);
                //        list.Add(new Config()
                //        {
                //            Code = responseArr[0],
                //            Description = responseArr[1]
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
        public static List<Config> GetCustomers()
        {
            var list = new List<Config>();
            try
            {
                string result = webportals.GetCustomers();
                if (!string.IsNullOrEmpty(result))
                {
                    string[] resultsArr = result.Split(strLimiters2, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string str in resultsArr)
                    {
                        string[] responseArr = str.Split(strLimiters, StringSplitOptions.None);
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
            return list;
        }


        public static List<Config> GetResponsibilityCenters(string grouping)
        {
            var list = new List<Config>();
            try
            {
                string result = webportals.GetDocResponsibilityCentres(grouping);
                if (!string.IsNullOrEmpty(result))
                {
                    string[] resultsArr = result.Split(strLimiters2, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string str in resultsArr)
                    {
                        string[] responseArr = str.Split(strLimiters, StringSplitOptions.None);
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
            return list;
        }

        public static List<Config> GetClientCodes()
        {
            var list = new List<Config>();
            string dimensionCode = "CUSTOMER";
            try
            {
                string dimensions = webportals.GetDimensions(dimensionCode);
                if (!string.IsNullOrEmpty(dimensions))
                {
                    string[] dimensionsArray = dimensions.Split(strLimiters2, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string dimension in dimensionsArray)
                    {
                        string[] responseArr = dimension.Split(strLimiters, StringSplitOptions.None);
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
            return list;
        }
        public static List<Config> GetJobOrderCodes()
        {
            var list = new List<Config>();
            string dimensionCode = "JOB ORDER";
            try
            {
                string dimensions = webportals.GetDimensions(dimensionCode);
                if (!string.IsNullOrEmpty(dimensions))
                {
                    string[] dimensionsArray = dimensions.Split(strLimiters2, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string dimension in dimensionsArray)
                    {
                        string[] responseArr = dimension.Split(strLimiters, StringSplitOptions.None);
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
            return list;
        }
        
        public static Config GetDepartmentDetails(string username)
        {
            var departments = new Config();
            try
            {
                string result = webportals.GetStaffDepartmentDetails(username);
                if (!string.IsNullOrEmpty(result))
                {
                    string[] responseArr = result.Split(strLimiters, StringSplitOptions.None);
                    departments.Directorate = responseArr[1];
                    departments.Department = responseArr[2];
                }
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
            return departments;
        }

        public static List<Config> GetDirectorates()
        {
            var list = new List<Config>();
            try
            {
                //string result = webportals.GetDirectorates();
                //if (!string.IsNullOrEmpty(result))
                //{
                //    string[] resultsArr = result.Split(strLimiters2, StringSplitOptions.RemoveEmptyEntries).ToArray();
                //    foreach (string str in resultsArr)
                //    {
                //        string[] responseArr = str.Split(strLimiters, StringSplitOptions.None);
                //        list.Add(new Config()
                //        {
                //            Code = responseArr[0],
                //            Description = responseArr[1]
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

        public static List<Config> GetDepartments()
        {
            var list = new List<Config>();
            try
            {
            //    string result = webportals.GetDepartments();
            //    if (!string.IsNullOrEmpty(result))
            //    {
            //        string[] resultsArr = result.Split(strLimiters2, StringSplitOptions.RemoveEmptyEntries);
            //        foreach (string str in resultsArr)
            //        {
            //            string[] responseArr = str.Split(strLimiters, StringSplitOptions.None);
            //            list.Add(new Config()
            //            {
            //                Code = responseArr[0],
            //                Description = responseArr[1]
            //            });
            //        }
            //    }
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
            return list;
        }

        public static List<Config> GetProjects()
        {
            var list = new List<Config>();
            try
            {
                //string result = webportals.GetProjects();
                //if (!string.IsNullOrEmpty(result))
                //{
                //    string[] resultsArr = result.Split(strLimiters2, StringSplitOptions.RemoveEmptyEntries);
                //    foreach (string str in resultsArr)
                //    {
                //        string[] responseArr = str.Split(strLimiters, StringSplitOptions.None);
                //        list.Add(new Config()
                //        {
                //            Code = responseArr[0],
                //            Description = $"{responseArr[0]} => {responseArr[1]}"
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

        public static List<Config> GetActivities(string project)
        {
            var list = new List<Config>();
            try
            {
            //    string result = webportals.GetActivities(project);
            //    if (!string.IsNullOrEmpty(result))
            //    {
            //        string[] resultsArr = result.Split(strLimiters2, StringSplitOptions.RemoveEmptyEntries);
            //        foreach (string str in resultsArr)
            //        {
            //            string[] responseArr = str.Split(strLimiters, StringSplitOptions.None);
            //            list.Add(new Config()
            //            {
            //                Code = responseArr[0],
            //                Description = responseArr[1]
            //            });
            //        }
               // }
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
            return list;
        }

        public static List<Config> GetGlAccounts()
        {
            var list = new List<Config>();
            try
            {
                string result = webportals.GetGLAccounts();
                if (!string.IsNullOrEmpty(result))
                {
                    string[] resultsArr = result.Split(strLimiters2, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string str in resultsArr)
                    {
                        string[] responseArr = str.Split(strLimiters, StringSplitOptions.None);
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
            return list;
        }

        public static List<Config> GetItems()
        {
            var list = new List<Config>();
            try
            {
            //    string result = webportals.GetItems();
            //    if (!string.IsNullOrEmpty(result))
            //    {
            //        string[] resultsArr = result.Split(strLimiters2, StringSplitOptions.RemoveEmptyEntries);
            //        foreach (string str in resultsArr)
            //        {
            //            string[] responseArr = str.Split(strLimiters, StringSplitOptions.None);
            //            list.Add(new Config()
            //            {
            //                Code = responseArr[0],
            //                Description = responseArr[1]
            //            });
            //        }
            //    }
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
            return list;
        }

        public static List<Config> GetFixedAssets()
        {
            var list = new List<Config>();
            try
            {
                //string result = webportals.GetFixedAssets();
                //if (!string.IsNullOrEmpty(result))
                //{
                //    string[] resultsArr = result.Split(strLimiters2, StringSplitOptions.RemoveEmptyEntries);
                //    foreach (string str in resultsArr)
                //    {
                //        string[] responseArr = str.Split(strLimiters, StringSplitOptions.None);
                //        list.Add(new Config()
                //        {
                //            Code = responseArr[0],
                //            Description = responseArr[1]
                //        });
                //    }
              //  }
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
            return list;
        }

        public static List<Config> GetIssuingStores()
        {
            var list = new List<Config>();
            try
            {
                //string result = webportals.GetLocations();
                //if (!string.IsNullOrEmpty(result))
                //{
                //    string[] resultsArr = result.Split(strLimiters2, StringSplitOptions.RemoveEmptyEntries);
                //    foreach (string str in resultsArr)
                //    {
                //        string[] responseArr = str.Split(strLimiters, StringSplitOptions.None);
                //        list.Add(new Config()
                //        {
                //            Code = responseArr[0],
                //            Description = responseArr[1]
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

        public static List<Config> GetRelievers(string username)
        {
            var list = new List<Config>();
            try
            {
                string result = webportals.GetEmployees();
                if (!string.IsNullOrEmpty(result))
                {
                    string[] resultsArr = result.Split(strLimiters2, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string str in resultsArr)
                    {
                        string[] responseArr = str.Split(strLimiters, StringSplitOptions.None);
                        if (responseArr[0] == username) continue;
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

        public static List<Config> GetMemoRegions(string grade)
        {
            var list = new List<Config>();
            try
            {
                //string result = webportals.GetMemoRegions(grade);
                //if (!string.IsNullOrEmpty(result))
                //{
                //    string[] resultsArr = result.Split(strLimiters2, StringSplitOptions.RemoveEmptyEntries);
                //    foreach (string str in resultsArr)
                //    {
                //        string[] responseArr = str.Split(strLimiters, StringSplitOptions.None);
                //        list.Add(new Config()
                //        {
                //            Code = responseArr[0],
                //            Description = responseArr[1]
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

        public static List<Config> GetImprestTypes1()
        {
            var list = new List<Config>();
            try
            {
            //    string result = webportals.GetImprestTypes();
            //    if (!string.IsNullOrEmpty(result))
            //    {
            //        string[] resultsArr = result.Split(strLimiters2, StringSplitOptions.RemoveEmptyEntries);
            //        foreach (string str in resultsArr)
            //        {
            //            string[] responseArr = str.Split(strLimiters, StringSplitOptions.None);
            //            list.Add(new Config()
            //            {
            //                Code = responseArr[0],
            //                Description = responseArr[1]
            //            });
            //        }
            //    }
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
            return list;
        }
        public static List<Config> GetImprestTypes()
        {
            var list = new List<Config>();
            try
            {
                string result = webportals.GetAdvancetype(3);
                if (!string.IsNullOrEmpty(result))
                {
                    string[] resultsArr = result.Split(strLimiters2, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string str in resultsArr)
                    {
                        string[] responseArr = str.Split(strLimiters, StringSplitOptions.None);
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
            return list;
        }
        public static List<Config> GetMyAttachments(string documentNo)
        {
            var list = new List<Config>();
            try
            {
                string documents = webportals.GetMyAttachments(documentNo);
                if (!string.IsNullOrEmpty(documents))
                {
                    string[] documentsArray = documents.Split(strLimiters2, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string document in documentsArray)
                    {
                        string[] responseArr = document.Split(strLimiters, StringSplitOptions.None);
                        list.Add(new Config()
                        {
                            DocumentNo = responseArr[0],
                            FileName = responseArr[1],
                            FileExtension = responseArr[2],
                            //CreatedAt = Convert.ToDateTime(responseArr[3]),
                            CreatedAt = DateTime.ParseExact(responseArr[3], "MM/dd/yy hh:mm tt", CultureInfo.InvariantCulture),

                            SystemId = responseArr[4]
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

        public static List<Config> GetMyImprestReceipts(string username)
        {
            var list = new List<Config>();
            try
            {
                //string documents = webportals.GetImprestReceipts(username);
                //if (!string.IsNullOrEmpty(documents))
                //{
                //    string[] documentsArray = documents.Split(strLimiters2, StringSplitOptions.RemoveEmptyEntries);
                //    foreach (string document in documentsArray)
                //    {
                //        string[] responseArr = document.Split(strLimiters, StringSplitOptions.None);
                //        list.Add(new Config()
                //        {
                //            Code = responseArr[0],
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

        public static List<Config> GetClaimTypes()
        {
            var list = new List<Config>();
            try
            {
                string documents = webportals.GetClaimTypes();
                if (!string.IsNullOrEmpty(documents))
                {
                    string[] documentsArray = documents.Split(strLimiters2, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string document in documentsArray)
                    {
                        string[] responseArr = document.Split(strLimiters, StringSplitOptions.None);
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
            return list;
        }

        public static List<Config> GetPettyCashTypes()
        {
            var list = new List<Config>();
            try
            {
                //string documents = webportals.GetPettyCashTypes();
                //if (!string.IsNullOrEmpty(documents))
                //{
                //    string[] documentsArray = documents.Split(strLimiters2, StringSplitOptions.RemoveEmptyEntries);
                //    foreach (string document in documentsArray)
                //    {
                //        string[] responseArr = document.Split(strLimiters, StringSplitOptions.None);
                //        list.Add(new Config()
                //        {
                //            Code = responseArr[0],
                //            Description = responseArr[1]
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

        public static List<Config> GetPettyCashReceipts(string username)
        {
            var list = new List<Config>();
            try
            {
                //string documents = webportals.GetPettyCashReceipts(username);
                //if (!string.IsNullOrEmpty(documents))
                //{
                //    string[] documentsArray = documents.Split(strLimiters2, StringSplitOptions.RemoveEmptyEntries);
                //    foreach (string document in documentsArray)
                //    {
                //        string[] responseArr = document.Split(strLimiters, StringSplitOptions.None);
                //        list.Add(new Config()
                //        {
                //            Code = responseArr[0],
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
    }
}