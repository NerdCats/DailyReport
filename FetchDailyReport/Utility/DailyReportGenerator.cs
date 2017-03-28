using FetchDailyReport.Model;
using System;
using System.Collections.Generic;
using System.IO;

namespace FetchDailyReport.Utility
{
    class DailyReportGenerator
    {
        public static string generateDailyReport(List<DailyReport> dailyReports)
        {
            var currentTime = new TimeString();

            string reportText = "The following Report is calculated based on this timeline:\n From " + currentTime.StartBDTime + " to " + currentTime.EndBDTime + "\n\n";
                        
            var B2BTotalDispatchedIndex = dailyReports.FindLastIndex(x => x.ReportName == "B2B Total Dispatched");
            var B2CTotalDispatchedIndex = dailyReports.FindLastIndex(x => x.ReportName == "B2C Total Dispatched");
            foreach (var item in dailyReports)
            {
                if (item.ReportName.Contains("B2B Total Completed") || item.ReportName.Contains("B2B Total Attempted") || item.ReportName.Contains("B2B Total Returned"))
                {
                    dailyReports[B2BTotalDispatchedIndex].TotalCount += item.TotalCount;                                        
                }

                if (item.ReportName.Contains("B2C Total Completed") || item.ReportName.Contains("B2C Total Attempted") || item.ReportName.Contains("B2C Total Returned"))
                {                   
                    dailyReports[B2CTotalDispatchedIndex].TotalCount += item.TotalCount;                    
                }
            }

            foreach (var item in dailyReports)
            {
                if (item.ReportName.Contains("B2B Total Completed Percentage"))
                {
                    var B2BTotalCompletedIndex = dailyReports.FindLastIndex(x => x.ReportName == "B2B Total Completed");
                    item.TotalCount = percentageCalculate(dailyReports[B2BTotalCompletedIndex].TotalCount, dailyReports[B2BTotalDispatchedIndex].TotalCount);
                }
                if (item.ReportName.Contains("B2C Total Completed Percentage"))
                {
                    var B2CTotalCompletedIndex = dailyReports.FindLastIndex(x => x.ReportName == "B2C Total Completed");
                    item.TotalCount = percentageCalculate(dailyReports[B2CTotalCompletedIndex].TotalCount, dailyReports[B2CTotalDispatchedIndex].TotalCount);
                }

                if (item.ReportName.Contains("B2B Total Attempted Percentage"))
                {
                    var B2BTotalAttemptedIndex = dailyReports.FindLastIndex(x => x.ReportName == "B2B Total Attempted");
                    item.TotalCount = percentageCalculate(dailyReports[B2BTotalAttemptedIndex].TotalCount, dailyReports[B2BTotalDispatchedIndex].TotalCount);
                }
                if (item.ReportName.Contains("B2C Total Attempted Percentage"))
                {
                    var B2CTotalAttemptedIndex = dailyReports.FindLastIndex(x => x.ReportName == "B2C Total Attempted");
                    item.TotalCount = percentageCalculate(dailyReports[B2CTotalAttemptedIndex].TotalCount, dailyReports[B2CTotalDispatchedIndex].TotalCount);
                }

                reportText += item.ReportName + ", " + item.TotalCount + "\n";
                if (item.NewLine != null)
                {
                    reportText += item.NewLine;
                }
            }

            Console.WriteLine(reportText + "\n\n\n\n");
            return reportText;
        }
        
        public static string generateDailyReportCSV(string reportText)
        {
            //var reportText = DailyReportGenerator.generateDailyReport(dailyReports);
            var fileName = DateTime.UtcNow.AddHours(-6).ToShortDateString().Replace('/', '-');
            var filePath = URLs.ApplicationRootDirectory + "/CSV/" + fileName + ".csv";
            File.WriteAllText(filePath, reportText);
            return filePath;
        }

        private static double percentageCalculate(double occurance, double total)
        {    
            double percentage = (occurance / total) * 100;
            return Math.Round(percentage, 2);
        }
    }
}
