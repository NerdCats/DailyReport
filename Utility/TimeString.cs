using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FetchDailyReport.Utility
{
    class TimeString
    {
        public string StartTimeISO { get; set; }
        public string EndTimeISO { get; set; }

        public TimeString()
        {
            var nowUTCTime = new DateTime(2017, 03, 20, 23, 59, 00, DateTimeKind.Utc);

            this.EndTimeISO = nowUTCTime.ToString("yyyy-MM-ddTHH:mm:ssZ");
            this.StartTimeISO = nowUTCTime.AddHours(-24).ToString("yyyy-MM-ddTHH:mm:ssZ");

            Console.WriteLine(this.StartTimeISO);
            Console.WriteLine(this.EndTimeISO);
            //Console.ReadLine();
        }

    }
}
