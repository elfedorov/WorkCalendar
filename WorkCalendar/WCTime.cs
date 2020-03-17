using System;
using System.Collections.Generic;
using System.Text;

namespace WorkCalendar
{
    public class WCTime
    {
        public int day { get; set; }
        public int hour { get; set; }
        public int minute { get; set; }
        public int second { get; set; }
        public WCTime()
        {

        }
        /// <summary>
        /// Set time from values
        /// </summary>
        /// <param name="d">Day. Default 0.</param>
        /// <param name="h">Hour. Default 0.</param>
        /// <param name="m">Minute. Default 0.</param>
        /// <param name="s">Second. Default 0.</param>
        public WCTime(int d = 0, int h = 0, int m = 0, int s = 0)
        {
            if (h > 23 || m > 59 || s > 59
                || d < 0 || h < 0 || m < 0 || s < 0)
            {
                throw new ArgumentException("Invalid incoming values");
            }
            day = d;
            hour = h;
            minute = m;
            second = s;
        }
        /// <summary>
        /// Set time from datetime, date will be dropped
        /// </summary>
        /// <param name="dt"></param>
        public WCTime(DateTime dt)
        {
            day = 0;
            hour = dt.Hour;
            minute = dt.Minute;
            second = dt.Second;
        }
        public WCTime(TimeSpan ts)
        {
            day = ts.Days;
            hour = ts.Hours % 24;
            minute = ts.Minutes % 60;
            second = ts.Seconds % 60;
        }
        public TimeSpan ToTimeSpan()
        {
            return new TimeSpan(day, hour, minute, second);
                
        }
        public override string ToString()
        {
            return String.Format(
                "{0:00}:{1:00}:{2:00}",
                hour, minute, second);
        }
        
    }
}
