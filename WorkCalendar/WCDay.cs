using System;
using System.Collections.Generic;
using System.Text;

namespace WorkCalendar
{
    public class WCDay
    {
        /// <summary>
        /// Type of day. Used enum WCDayType <see cref="WCDayType"/>
        /// </summary>
        public WCDayType dayType { get; set; } 
        /// <summary>
        /// Date of day
        /// </summary>
        public DateTime date { get; set; }
        /// <summary>
        /// Title of day, can be used for naming date.
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// Working times of day. It can contains many periods of day <see cref="WCWorkTime"/>
        /// </summary>
        public List<WCWorkTime> workTimes { get; set; } = new List<WCWorkTime>();
        /// <summary>
        /// just simple see is it working
        /// </summary>
        public bool isWorkingDay => dayType == WCDayType.Workday || dayType == WCDayType.ShortDay ? true : false;
        public WCDay()
        {
            
        }
        /// <summary>
        /// Create base day day of calendar
        /// </summary>
        /// <param name="date">Date of day</param>
        /// <param name="calendar">Calendar with settings</param>
        public WCDay(DateTime date,WCalendar calendar)
        {
            date = date.Date;
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    dayType = calendar.monday;
                    break;
                case DayOfWeek.Tuesday:
                    dayType = calendar.tuesday;
                    break;
                case DayOfWeek.Wednesday:
                    dayType = calendar.wednesday;
                    break;
                case DayOfWeek.Thursday:
                    dayType = calendar.thursday;
                    break;
                case DayOfWeek.Friday:
                    dayType = calendar.friday;
                    break;
                case DayOfWeek.Saturday:
                    dayType = calendar.saturday;
                    break;
                case DayOfWeek.Sunday:
                    dayType = calendar.sunday;
                    break;
                default:
                    break;
            }
            if (dayType==WCDayType.Workday)
                workTimes.Add(new WCWorkTime ( calendar.defaulStart, calendar.defaultEnd ));
            if (dayType == WCDayType.ShortDay)
                workTimes.Add(new WCWorkTime(calendar.defaulStart, calendar.defaultShortEnd));
        }
    }
    
}
