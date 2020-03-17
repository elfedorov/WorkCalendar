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

        public TimeSpan DateDiff(DateTime start, DateTime end)
        {
            if (end < start)
                throw new Exception("Start later then end");
            if (start == end)
                return new TimeSpan(0);
            return DateDiff(start, end);
        }
        public DateTime AddWorkTime(DateTime start, TimeSpan addTime)
        {
            //no need here
            //addTime = addTime.RoundToMinutes();
            var day = days.Where(x => x.date.Date == start.Date).FirstOrDefault();
            if (day == null)
            {
                day = new WCDay(start.Date,this);
            }
            //if no working time in this day go to next day
            if (day.dayType != WCDayType.Workday || day.workTimes.Where(x => x.end > start.TimeOfDay).Count() < 1)
                return AddWorkTime(start.Date.AddDays(1), addTime);
            //start processing worktimes
            for (int i = 0; i < day.workTimes.Count; i++)
            {
                var workTime = day.workTimes.OrderBy(x => x.start).ToList()[i];
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
                    Console.WriteLine("before");
                    Console.WriteLine(addTime);
                    Console.WriteLine(start.TimeOfDay);

                    addTime = addTime.Add(-(workTime.end -start.TimeOfDay));
                    start=start.Date.Add(workTime.end);
                    Console.WriteLine("after");
                    Console.WriteLine(addTime);
                    Console.WriteLine(start.TimeOfDay);
                }
            }
            return AddWorkTime(start,addTime);
        }

    }
}
