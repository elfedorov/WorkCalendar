using System;
using System.Collections.Generic;
using System.Linq;

namespace WorkCalendar
{
    public class WCalendar
    {
        /// <summary>
        /// List of days, that are not default
        /// </summary>
        public List<WCDay> days { get; set; } = new List<WCDay>();
        /// <summary>
        /// Deafult time of start workday
        /// </summary>
        public TimeSpan defaulStart { get; set; } = new TimeSpan(8, 30, 0);
        /// <summary>
        /// Default time of end workday
        /// </summary>
        public TimeSpan defaultEnd { get; set; } = new TimeSpan(17, 30, 0);

        #region set defaults for weekdays 
        /// <summary>
        /// Is Monday working day
        /// </summary>
        public bool workMonday { get; set; } = true;
        /// <summary>
        /// Is Tuesday working day
        /// </summary>
        public bool workTuesday { get; set; } = true;
        /// <summary>
        /// Is Wednesday working day
        /// </summary>
        public bool workWednesday { get; set; } = true;
        /// <summary>
        /// Is Thursday working day
        /// </summary>
        public bool workThursday { get; set; } = true;
        /// <summary>
        /// Is Friday working day
        /// </summary>
        public bool workFriday { get; set; } = true;
        /// <summary>
        /// Is Saturday working day
        /// </summary>
        public bool workSaturday { get; set; } = false;
        /// <summary>
        /// Is Sunday working day
        /// </summary>
        public bool workSunday { get; set; } = false;
        /// <summary>
        /// Set all days in week as working
        /// </summary>
        public void SetAllDaysWorks()
        {
            workMonday = true;
            workTuesday = true;
            workWednesday = true;
            workThursday = true;
            workFriday = true;
            workSaturday = true;
            workSunday = true;
        }
        /// <summary>
        /// Set all days in week as no working
        /// </summary>
        public void SetAllDaysNotWorks()
        {
            workMonday = false;
            workTuesday = false;
            workWednesday = false;
            workThursday = false;
            workFriday = false;
            workSaturday = false;
            workSunday = false;
        }
        #endregion
        /// <summary>
        /// Calculate time in work hours between dates
        /// </summary>
        /// <param name="start">Start date</param>
        /// <param name="end">End date</param>
        public TimeSpan WorkTimeDiff(DateTime start, DateTime end)
        {
            return WorkTimeDiff(start, end, new TimeSpan(0));
        }
        /// <summary>
        /// Calculate time in work hours between dates
        /// </summary>
        /// <param name="start">Start date</param>
        /// <param name="end">End date</param>
        /// <param name="ts">TimeSpan for summarize</param>
        /// <returns></returns>
        public TimeSpan WorkTimeDiff(DateTime start, DateTime end, TimeSpan ts )
        {
            if (end < start)
            {
                var m = start;
                start = end;
                end = m;
            }
            if (start == end)
                return ts;
            var day = days.Where(x => x.date.Date == start.Date).FirstOrDefault();
            if (day == null)
            {
                day = new WCDay(start.Date, this);
            }
            //if no working time in this day go to next day
            var worktimes = day.workTimes.Where(x => x.end > start.TimeOfDay).OrderBy(x=>x.start).ToList();
            if (day.dayType != WCDayType.Workday || worktimes.Count() < 1)
                return WorkTimeDiff(start.Date.AddDays(1), end,ts);
            for (int i = 0; i < day.workTimes.Count; i++)
            {
                var workTime = worktimes[i];
                if (start.TimeOfDay < workTime.start)
                    start = start.Date.Add(workTime.start);
                //if end date bigger then just add worktime and go to next day
                if (end.Date>start.Date)
                {
                    ts = ts.Add(workTime.end - start.TimeOfDay);
                    start = start.Date.Add(workTime.end);
                    
                } else //if we have same date of start and end
                {
                    if (end <= start)
                        return ts;
                    if (end.TimeOfDay < workTime.end)
                    {
                        return ts.Add(end.TimeOfDay.Add(-start.TimeOfDay));
                    }
                    ts = ts.Add(workTime.end - workTime.start);
                    start = start.Date.Add(workTime.end);
                }
               
            }
            var d = new DateTime(2020, 1, 20, 12, 47, 0);
            return WorkTimeDiff(start, end,ts);
        }
        /// <summary>
        /// Add worktime to date
        /// </summary>
        /// <param name="start">Start date</param>
        /// <param name="addTime">How many time you need to add?</param>
        /// <returns></returns>
        public DateTime AddWorkTime(DateTime start, TimeSpan addTime)
        {
            //no need here
            //addTime = addTime.RoundToMinutes();
            var day = days.Where(x => x.date.Date == start.Date).FirstOrDefault();
            if (day == null)
            {
                day = new WCDay(start.Date,this);
            }
            var workTimes = day.workTimes.Where(x => x.end > start.TimeOfDay).OrderBy(x => x.start).ToList();
            //if no working time in this day go to next day
            if (day.dayType != WCDayType.Workday || workTimes.Count() < 1)
                return AddWorkTime(start.Date.AddDays(1), addTime);
            //start processing worktimes
            for (int i = 0; i < day.workTimes.Count; i++)
            {
                var workTime = workTimes.ToList()[i];
                if (workTime.start > start.TimeOfDay)
                    start = start.Date.Add(workTime.start);
                //time interval bigger then time need to add
                if (workTime.end -start.TimeOfDay > addTime)
                {
                    //we found that we needed
                    return start.Add(addTime);
                }
                else
                {
                    //decrease time to add
                    addTime = addTime.Add(-(workTime.end -start.TimeOfDay));
                    start=start.Date.Add(workTime.end);
                }
            }
            return AddWorkTime(start,addTime);
        }

    }
}
