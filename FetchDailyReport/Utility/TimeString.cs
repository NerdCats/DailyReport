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
        private DateTime startUTCTime;
        private DateTime endUTCTime;

        public TimeString()
        {
            var todayDate = DateTime.UtcNow.Date.Day;
            var thisMonth = DateTime.UtcNow.Month;
            var thisYear = DateTime.UtcNow.Year;            
            
            startUTCTime = new DateTime(thisYear, thisMonth, todayDate, 2, 00, 00, DateTimeKind.Utc);  // Find out Next day 2AM in the night of BD time
            endUTCTime = DateTime.UtcNow;

            this.StartTimeISO = startUTCTime.ToString("yyyy-MM-ddTHH:mm:ssZ");
            this.EndTimeISO = endUTCTime.ToString("yyyy-MM-ddTHH:mm:ssZ");

            //var start = new DateTime(startUTCTime.Year, startUTCTime.Month, startUTCTime.Date.Day, startUTCTime.Hour, startUTCTime.Minute, startUTCTime.Second, DateTimeKind.Local).AddHours(6);
            //var end = new DateTime(endUTCTime.Year, endUTCTime.Month, endUTCTime.Date.Day, endUTCTime.Hour, endUTCTime.Minute, startUTCTime.Second, DateTimeKind.Local).AddHours(6);

        }

        public string GetNewTimeString(int hourDifferenceFromStartTime)
        {
            var newTime = this.startUTCTime.AddHours(hourDifferenceFromStartTime).ToString("yyyy-MM-ddTHH:mm:ssZ");
            return newTime;
        }

    }
}
