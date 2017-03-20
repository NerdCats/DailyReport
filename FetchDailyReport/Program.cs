using FetchDailyReport.Model;
using FetchDailyReport.Model;
using FetchDailyReport.Utility;
using System;
using System.Collections.Generic;

namespace FetchDailyReport
{
    class Program
    {
        static void Main(string[] args)
        {            
            List<DailyReport> dailyReports = new List<DailyReport>();
            List<DailyReportConfigModel> dailyReportConfig = DailyReportConfigModel.DailyReportConfigModelFactory();
            string auth_token;
            bool reportHasBeenSentOneAlreadyForToday = false;
            while (true)
            {
                if (DateTime.UtcNow.Hour == 17 && DateTime.UtcNow.Minute > 50 && !reportHasBeenSentOneAlreadyForToday)                
                {
                    reportHasBeenSentOneAlreadyForToday = true;
                    #region Report generation and email send                    
                    try
                    {
                        auth_token = new HttpRequest().getAuthToken();
                    }
                    catch (Exception e)
                    {
                        try
                        {
                            auth_token = new HttpRequest().getAuthToken();
                        }
                        catch (Exception e2)
                        {
                            try
                            {
                                auth_token = new HttpRequest().getAuthToken();
                            }
                            catch (Exception e3)
                            {
                                MailUtility.SendEmailReport("Server error while sending Daily Fetch Report",
                                        "Sorry, today after trying several times, the daily report couldn't be generated from the server!");
                                continue;
                            }
                        }
                    }

                    var currentTime = new TimeString();
                    foreach (var report in dailyReportConfig)
                    {
                        CountReport countReport;
                        var reportUrl = report.ReportUrl.Replace("{startDate}", currentTime.StartTimeISO).Replace("{endDate}", currentTime.EndTimeISO);
                        try
                        {
                            countReport = new HttpRequest().getReport(reportUrl, auth_token);
                        }
                        catch (Exception e)
                        {
                            try
                            {
                                countReport = new HttpRequest().getReport(reportUrl, auth_token);
                            }
                            catch (Exception e2)
                            {
                                try
                                {
                                    countReport = new HttpRequest().getReport(reportUrl, auth_token);
                                }
                                catch (Exception ex3)
                                {
                                    MailUtility.SendEmailReport("Server error while sending Daily Fetch Report",
                                        "Sorry, today after trying several times, the daily report couldn't be generated from the server!");
                                    continue;
                                }
                            }
                        }
                        var dailyReport = new DailyReport();
                        dailyReport.ReportName = report.ReportName;
                        dailyReport.TotalCount = countReport.pagination.Total.ToString();
                        dailyReports.Add(dailyReport);
                    }
                    var reportText = DailyReportGenerator.generateDailyReport(dailyReports);
                    var reportFilePath = DailyReportGenerator.generateDailyReportCSV(dailyReports);
                    MailUtility.SendEmailReport("Daily Fetch Report", reportText, reportFilePath);
                    #endregion
                }
                if (DateTime.UtcNow.Hour == 18 && reportHasBeenSentOneAlreadyForToday)
                {
                    reportHasBeenSentOneAlreadyForToday = true;
                }
            }            
        }
    }
}
