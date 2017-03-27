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

        public string StartBDTime { get; set; }
        public string EndBDTime { get; set; }

        public TimeString()
        {
            var todayDate = DateTime.UtcNow.Date.Day;
            var thisMonth = DateTime.UtcNow.Month;
            var thisYear = DateTime.UtcNow.Year;            
                        

            endUTCTime = new DateTime(thisYear, thisMonth, todayDate, 18, 59, 59, DateTimeKind.Utc);  // Find out Next day 2AM in the night of BD time

            startUTCTime = endUTCTime.AddHours(-23).AddMinutes(-59).AddSeconds(-59);

            this.StartTimeISO = startUTCTime.ToString("yyyy-MM-ddTHH:mm:ssZ");
            this.EndTimeISO = endUTCTime.ToString("yyyy-MM-ddTHH:mm:ssZ");

            StartBDTime = new DateTime(startUTCTime.Year, startUTCTime.Month, startUTCTime.Date.Day, startUTCTime.Hour, startUTCTime.Minute, startUTCTime.Second, DateTimeKind.Local).AddHours(6).ToString("MMM d h:mm:ss tt");
            EndBDTime = new DateTime(endUTCTime.Year, endUTCTime.Month, endUTCTime.Date.Day, endUTCTime.Hour, endUTCTime.Minute, startUTCTime.Second, DateTimeKind.Local).AddHours(6).ToString("MMM d h:mm:ss tt");

        }

        public string GetNewTimeString(int hourDifferenceFromStartTime)
        {
            var newTime = this.endUTCTime.AddHours(hourDifferenceFromStartTime);

            var newTimeLocal = new DateTime(newTime.Year, newTime.Month, newTime.Date.Day, newTime.Hour, newTime.Minute, newTime.Second, DateTimeKind.Local).AddHours(6);

            string newTimeString = newTime.ToString("yyyy-MM-ddTHH:mm:ssZ");
            return newTimeString;
        }

    }
}
