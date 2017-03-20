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
            List<CountReport> countReportCollection = new List<CountReport>();
            List<DailyReportConfigModel> dailyReport = DailyReportConfigModel.DailyReportConfigModelFactory();
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
            foreach (var report in dailyReport)
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
                countReportCollection.Add(countReport);
            }
        }
    }
}
