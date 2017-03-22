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
            string reportText = "The following Report is calculated based on this timeline:\nFrom " + currentTime.StartBDTime + " to " + currentTime.EndBDTime + "\n\n";
            foreach (var item in dailyReports)
            {
                reportText += item.ReportName + " : " + item.TotalCount + "\n";
                if (item.NewLine != null)
                {
                    reportText += item.NewLine;
                }
            }
            Console.WriteLine(reportText + "\n\n\n\n");
            return reportText;
        }
        
        public static string generateDailyReportCSV(List<DailyReport> dailyReports)
        {
            var currentTime = new TimeString();
            
            string reportText = "The following Report is calculated based on this timeline:\n From " + currentTime.StartBDTime + " to " + currentTime.EndBDTime + "\n\n";
            foreach (var item in dailyReports)
            {
                reportText += item.ReportName + "," + item.TotalCount + ",\n";
                if (item.NewLine != null)
                {
                    reportText += item.NewLine;
                }
            }
            var fileName = DateTime.UtcNow.AddHours(-6).ToShortDateString().Replace('/', '-');
            var filePath = URLs.ApplicationRootDirectory + "/CSV/" + fileName + ".csv";
            File.WriteAllText(filePath, reportText);
            return filePath;
        }
    }
}
