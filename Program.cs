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
            List<DailyReportConfigModel> dailyReport = DailyReportConfigModel.DailyReportConfigModelFactory();
            var currentTime = new TimeString();
            var url = dailyReport[0].ReportUrl.Replace("{startDate}", currentTime.StartTimeISO).Replace("{endDate}", currentTime.EndTimeISO);


            var auth_token = new HttpRequest().getAuthToken();
            var countReport = new HttpRequest().getReport(url, auth_token);
            
        }
    }
}
