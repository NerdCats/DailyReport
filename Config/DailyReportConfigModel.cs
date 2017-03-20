using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FetchDailyReport.Config
{
    public class DailyReportConfigModel
    {
        public string ReportName { get; set; }
        public string ReportUrl { get; set; }

        public static List<DailyReportConfigModel> DailyReportConfigModelFactory()
        {
            var DailyReportConfigFileContent = File.ReadAllText(URLs.DailyReportConfigFilePath);
            var DailyReportConfig = JsonConvert.DeserializeObject<List<DailyReportConfigModel>>(DailyReportConfigFileContent);
            return DailyReportConfig;
        }
    }
}
