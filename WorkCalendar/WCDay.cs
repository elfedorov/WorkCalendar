using System;
using System.Collections.Generic;
using System.Text;

namespace WorkCalendar
{
    public class WCDay
    {
        public WCDayType dayType { get; set; } 
        public DateTime date { get; set; }
        public List<WCWorkTime> workTimes { get; set; } = new List<WCWorkTime>();
        public bool isWorkingDay => dayType == WCDayType.Workday ? true : false;

        public WCDay(DateTime date,WCalendar calendar)
        {
            date = date.Date;
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    dayType = calendar.workMonday == true ? WCDayType.Workday : WCDayType.Weekend;
                    break;
                case DayOfWeek.Tuesday:
                    dayType = calendar.workTuesday == true ? WCDayType.Workday : WCDayType.Weekend;
                    break;
                case DayOfWeek.Wednesday:
                    dayType = calendar.workWednesday == true ? WCDayType.Workday : WCDayType.Weekend;
                    break;
                case DayOfWeek.Thursday:
                    dayType = calendar.workThursday == true ? WCDayType.Workday : WCDayType.Weekend;
                    break;
                case DayOfWeek.Friday:
                    dayType = calendar.workFriday == true ? WCDayType.Workday : WCDayType.Weekend;
                    break;
                case DayOfWeek.Saturday:
                    dayType = calendar.workSaturday == true ? WCDayType.Workday : WCDayType.Weekend;
                    break;
                case DayOfWeek.Sunday:
                    dayType = calendar.workSunday == true ? WCDayType.Workday : WCDayType.Weekend;
                    break;
                default:
                    break;
            }
            if (dayType==WCDayType.Workday)
                workTimes.Add(new WCWorkTime ( calendar.defaulStart, calendar.defaultEnd ));
        }
    }
    
}
