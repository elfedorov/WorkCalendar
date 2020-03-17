using System;
using System.Collections.Generic;
using System.Text;

namespace WorkCalendar
{
    public static class WCStatic
    {
        public static TimeSpan RoundToMinutes(this TimeSpan input)
        {
            return new TimeSpan(input.Hours, input.Minutes, 0);
        }
    }
}
