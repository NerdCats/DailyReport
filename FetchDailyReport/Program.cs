using FetchDailyReport.Model;
using FetchDailyReport.Utility;
using System;
using System.Collections.Generic;
using System.Threading;

namespace FetchDailyReport
{
    class Program
    {
        static void Main(string[] args)
        {            
            string auth_token;
            bool reportHasBeenSentOneAlreadyForToday = false;
            
            while (true)
            {

                if (DateTime.UtcNow.Hour == 19 && !reportHasBeenSentOneAlreadyForToday)
                //if (DateTime.UtcNow.Hour > 6 && !reportHasBeenSentOneAlreadyForToday)
                {
                    List<DailyReport> dailyReports = new List<DailyReport>();
                    List<DailyReportConfigModel> dailyReportConfig = DailyReportConfigModel.DailyReportConfigModelFactory();
                    
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
                        CountReport countReport = null;
                        string reportUrl = "";
                        if(report.ReportUrl != null)
                        {
                            if (report.ReportName.Contains("Pending") || report.ReportName.Contains("In Progress"))
                            {
                                reportUrl = report.ReportUrl.Replace("{endDate}", currentTime.GetNewTimeString(report.EndTimeHourOffsetFromLastHourOfTheDay));
                            }
                            else
                            {
                                reportUrl = report.ReportUrl.Replace("{startDate}", currentTime.StartTimeISO).Replace("{endDate}", currentTime.EndTimeISO);
                            }
                            Console.WriteLine(report.ReportName + ":\n\n" + reportUrl + "\n\n\n\n");

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
                        }
                        var dailyReport = new DailyReport();
                        dailyReport.ReportName = report.ReportName;
                        dailyReport.TotalCount = countReport!=null? countReport.pagination.Total: 0;
                        dailyReport.NewLine = report.NewLine;
                        dailyReports.Add(dailyReport);
                    }
                    var reportText = DailyReportGenerator.generateDailyReport(dailyReports);
                    var reportFilePath = DailyReportGenerator.generateDailyReportCSV(reportText);
                    MailUtility.SendEmailReport("Daily Fetch Report", reportText, reportFilePath);
                    reportHasBeenSentOneAlreadyForToday = true; 
                    #endregion
                }
                if (DateTime.UtcNow.Hour > 21 && reportHasBeenSentOneAlreadyForToday)
                {
                    reportHasBeenSentOneAlreadyForToday = false;
                }
                Thread.Sleep(100000);
            }
        }
    }
}