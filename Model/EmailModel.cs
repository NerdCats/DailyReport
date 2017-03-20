using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FetchDailyReport.Model
{
    class EmailModel
    {
        public string FromName { get; set; }
        public string FromEmail { get; set; }
        public string FromLoginId { get; set; }
        public string FromLoginPassword { get; set; }
        public string ToName { get; set; }
        public string ToEmail { get; set; }
        public Dictionary<string, string> MailCcs { get; set; }
        public string EmailSubject { get; set; }
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
    }
}
