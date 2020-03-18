# WorkCalendar
It's simple model for calc time in work hours for 2 main methods 
1. AddWorkTime - add time in work hour. Calc end time of planned task.
2. DiffWorkTime - calc total time in work hours of task.

## Howto
Work calendar has default settings for work week, that can be overwitten.
For overwrite day, just add them into calendar and set type of day and worktime of it.
Day can contain many work periods.
### Configuring Start and End Time of work day
**TimeSpan defaulStart** -- Start of work day. Default 8-30
**TimeSpan dafaultEnd** -- End of work day. Default 17-30
### Configuring days of week
**bool workMonday** -- Default true.
**bool workTuesday** -- Default true.
**bool workWednesday** -- Default true.
**bool workThursday** -- Default true.
**bool workFriday** -- Default true.
**bool workSaturday** -- Default false.
**bool workSunday** -- Default false.
### Day types
 - Workday
 - Weekend
 - Holiday

### Adding exclusion days
```C#
WCalendar wc = new WCalendar();
//add an a holliday
//we can add a title for day for next using it in calendar
wc.days.Add(new WCDay{
  dayType = WCDayType.Holiday,
  title = "Happy new year",
  date = new DateTime(2020, 1, 1)
});
//set 2020/12/31 as short day
wc.days.Add(new WCDay{
  dayType = WCDayType.Workday,
  date = new DateTime(2020, 12,31),
  workTimes = new List<WCWorkTime>()
      {
           new WCWorkTime(new TimeSpan(8,30,0),new TimeSpan(12,30,0))
      ;
});
```
### Calc times 
```C#
\\calc en date
DateTime endDate = wc.AddWorkTime(new DateTime(2020, 1, 20, 12, 47, 0),new TimeSpan(16,0,0));
\\calc result time 
TimeSpan totalTime = wc.DiffWorkTime(new DateTime(2020, 1, 20, 12, 47, 0),new DateTime(2020, 3, 10, 18, 12, 0));
```
