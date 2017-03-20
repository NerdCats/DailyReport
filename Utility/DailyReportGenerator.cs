using FetchDailyReport.Model;
using System;
using System.Collections.Generic;
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
            return reportText;
        }
    }
}
