using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FetchDailyReport
{
    public class URLs
    {
        public static string apiServiceBaseURI = "http://fetchprod.gobd.co/";
        public static string authEndPoint = apiServiceBaseURI + "api/auth/token";

        public static string ApplicationRootDirectory = new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.FullName;
        public static string DailyReportConfigFilePath = ApplicationRootDirectory + "/Config/DailyReportConf.json";
        public static string EmailConfigFilePath = ApplicationRootDirectory + "/Config/MailConfig.json";
        public static string LoginCredentialsPath = ApplicationRootDirectory + "/Config/LoginCredential.json";
    }
}