using System;
using System.Collections.Generic;
using System.Text;

namespace WorkCalendar
{
    /// <summary>
    /// Work hours detailed to minute.
    /// </summary>
    public class WCWorkTime
    {
        /// <summary>
        /// Start of work period 
        /// </summary>
        public TimeSpan start { get; set; }
        /// <summary>
        /// End of work period
        /// </summary>
        public TimeSpan end { get; set; }
        public TimeSpan diff => end - start;
        /// <summary>
        /// I don't know what it's doing here...
        /// </summary>
        /// <param name="_startH"></param>
        /// <param name="_startMin"></param>
        /// <param name="_endH"></param>
        /// <param name="_endMin"></param>
        public WCWorkTime(int _startH,int _startMin,int _endH, int _endMin)
        {
            if (_startH < 0 || _startH>23
                || _endH < 0 || _endH>23
                || _startMin < 0 || _startMin>59
                || _endMin < 0  || _endMin > 59
                )
                throw new Exception("Incorrect params");
            if (_startH>_endH 
                || (_startH==_endH&&_startMin>=_endMin))
                throw new Exception("Start later or equal end");
            start = new TimeSpan(_startH, _startMin, 0);
            end = new TimeSpan(_endH, _endMin, 0);
        }
        /// <summary>
        /// Define period
        /// </summary>
        /// <param name="_start">start time</param>
        /// <param name="_end">end time</param>
        public WCWorkTime(TimeSpan _start,TimeSpan _end)
        {
            if (_start>=new TimeSpan(23,59,59)||_end>new TimeSpan(23,59,59))
                throw new Exception("So big time in this small day");
            start = _start.RoundToMinutes();
            end = _end.RoundToMinutes(); ;
            if (end<=start)
                throw new Exception("Start later or equal end");
            
        }
    }
}
