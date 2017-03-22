using FetchDailyReport.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FetchDailyReport.Utility
{
    class DailyReportGenerator
    {
        public static string generateDailyReport(List<DailyReport> dailyReports)
        {
            var currentTime = new TimeString();

            string reportText = "The following Report is calculated based on this timeline:\n From " + currentTime.StartBDTime + " to " + currentTime.EndBDTime + "\n\n";
            int b2bTotalDispatched = 0;
            int b2cTotalDispatched = 0;

            string b2bTotalCompletedPercent = "";
            string b2cTotalCompletedPercent = "";

            string b2bTotalAttemptedPercent = "";
            string b2cTotalAttemptedPercent = "";

            foreach (var item in dailyReports)
            {
                if (item.ReportName.Contains("B2B Total Completed") || item.ReportName.Contains("B2B Total Attempted") || item.ReportName.Contains("B2B Total Returned"))
                {
                    b2bTotalDispatched += item.TotalCount;
                }

                if (item.ReportName.Contains("B2C Total Completed") || item.ReportName.Contains("B2C Total Attempted") || item.ReportName.Contains("B2C Total Returned"))
                {
                    b2cTotalDispatched += item.TotalCount;
                }
            }
            reportText += "\nB2B Total Dispatched, " + b2bTotalDispatched;
            reportText += "\nB2C Total Dispatched, " + b2cTotalDispatched + "\n\n";

            foreach (var item in dailyReports)
            {
                if (item.ReportName.Contains("B2B Total Completed"))
                {
                    b2bTotalCompletedPercent = percentageCalculate(item.TotalCount, b2bTotalDispatched);
                }
                if (item.ReportName.Contains("B2C Total Completed"))
                {
                    b2cTotalCompletedPercent = percentageCalculate(item.TotalCount, b2cTotalDispatched);
                }

                if (item.ReportName.Contains("B2B Total Attempted"))
                {
                    b2bTotalAttemptedPercent = percentageCalculate(item.TotalCount, b2bTotalDispatched);
                }
                if (item.ReportName.Contains("B2C Total Attempted"))
                {
                    b2cTotalAttemptedPercent = percentageCalculate(item.TotalCount, b2cTotalDispatched);
                }

                reportText += item.ReportName + ", " + item.TotalCount + "\n";
                if (item.NewLine != null)
                {
                    reportText += item.NewLine;
                }
            }

            reportText += "\nB2B Total Completed Percentage, " + b2bTotalCompletedPercent + "%";
            reportText += "\nB2B Total Attempted Percentage, " + b2bTotalAttemptedPercent + "%\n\n";

            reportText += "\nB2C Total Completed Percentage, " + b2cTotalCompletedPercent + "%";
            reportText += "\nB2C Total Attempted Percentage, " + b2cTotalAttemptedPercent + "%";

            Console.WriteLine(reportText + "\n\n\n\n");
            return reportText;
        }
        
        public static string generateDailyReportCSV(List<DailyReport> dailyReports)
        {
            var reportText = DailyReportGenerator.generateDailyReport(dailyReports);
            var fileName = DateTime.UtcNow.AddHours(-6).ToShortDateString().Replace('/', '-');
            var filePath = URLs.ApplicationRootDirectory + "/CSV/" + fileName + ".csv";
            File.WriteAllText(filePath, reportText);
            return filePath;
        }

        private static string percentageCalculate(int occurance, int total)
        {
            double _occurance = Convert.ToDouble(occurance);
            double _total = Convert.ToDouble(total);
            double percentage = (_occurance / _total) * 100;
            return Math.Round(percentage, 2).ToString();
        }
    }
}
