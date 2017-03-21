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
        private DateTime nowUTCTime;

        public TimeString()
        {
            var todayDate = DateTime.UtcNow.Date.Day;
            var thisMonth = DateTime.UtcNow.Month;
            var thisYear = DateTime.UtcNow.Year;

            nowUTCTime = new DateTime(thisYear, thisMonth, todayDate, 23, 59, 00, DateTimeKind.Utc);
            nowUTCTime = nowUTCTime.AddHours(-6);
            this.EndTimeISO = nowUTCTime.ToString("yyyy-MM-ddTHH:mm:ssZ");
            this.StartTimeISO = nowUTCTime.AddHours(-24).ToString("yyyy-MM-ddTHH:mm:ssZ");
        }

        public string GetNewTimeString(int hourDifferenceFromStartTime)
        {
            var newTime = this.nowUTCTime.AddHours(hourDifferenceFromStartTime).ToString("yyyy-MM-ddTHH:mm:ssZ");
            return newTime;
        }

    }
}
