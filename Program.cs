using FetchDailyReport.Config;
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
                        throw;
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
                            throw;
                        }
                    }
                }
                var dailyReport = new DailyReport();
                dailyReport.ReportName = report.ReportName;
                dailyReport.TotalCount = countReport.pagination.Total.ToString();
                dailyReports.Add(dailyReport);
            }
            var reportText = DailyReportGenerator.generateDailyReport(dailyReports);
            MailUtility.SendEmailReport("Daily Fetch Report", reportText);
        }

    }
}
