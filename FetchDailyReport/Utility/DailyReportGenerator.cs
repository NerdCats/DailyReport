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
            var currentTime = DateTime.UtcNow.AddHours(6);
            string reportText = "Today is " + currentTime + "\n";
            foreach (var item in dailyReports)
            {
                reportText += item.ReportName + " : " + item.TotalCount + "\n";
            }
            Console.WriteLine(reportText + "\n\n\n\n");
            return reportText;
        }
        
        public static string generateDailyReportCSV(List<DailyReport> dailyReports)
        {
            var currentTime = DateTime.UtcNow.AddHours(6).ToShortDateString().Replace('/', '-');
            string reportText = "Today is " + currentTime + ",,\n";
            foreach (var item in dailyReports)
            {
                reportText += item.ReportName + "," + item.TotalCount + ",\n";
            }

            var filePath = URLs.ApplicationRootDirectory + "/CSV/" + currentTime + ".csv";
            File.WriteAllText(filePath, reportText);
            return filePath;
        }
    }
}
