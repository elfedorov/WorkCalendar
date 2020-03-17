using NUnit.Framework;
using System;
using System.Globalization;
using WorkCalendar;

namespace NUnitWorkCalendar
{
    [TestFixture]
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }
        [TestCase("17.03.2020 12:00", 60, ExpectedResult = "17.03.2020 13:00")]
        [TestCase("17.03.2020 12:00", 360 , ExpectedResult = "18.03.2020 09:00")]
        [TestCase("17.03.2020 19:00", 1, ExpectedResult = "18.03.2020 08:31")]
        [TestCase("17.03.2020 12:00", 360, ExpectedResult = "18.03.2020 09:00")]
        [TestCase("20.03.2020 20:00", 1, ExpectedResult = "23.03.2020 08:31")]
        [TestCase("20.03.2020 20:00", 360, ExpectedResult = "23.03.2020 14:30")]
        [TestCase("20.03.2020 12:00", 360, ExpectedResult = "23.03.2020 09:00")]
        [TestCase("20.03.2020 12:00", 1440, ExpectedResult = "25.03.2020 09:00")]
        [Test]
        public string TestAdd(string date,int addTime)
        {
            CultureInfo provider = CultureInfo.InvariantCulture;
            var d = DateTime.ParseExact(date,"dd.MM.yyyy HH:mm", provider);
            
            var time = new TimeSpan(0, addTime, 0);
            WCalendar wc = new WCalendar();
            var result = wc.AddWorkTime(d, time);
            return result.ToString("dd.MM.yyyy HH:mm");
        }
        [TestCase("17.03.2020 12:00", "17.03.2020 13:00", ExpectedResult = 60)]
        [TestCase("17.03.2020 12:00", "18.03.2020 09:00", ExpectedResult = 360)]
        [TestCase("17.03.2020 19:00", "18.03.2020 08:31", ExpectedResult = 1)]
        [TestCase("17.03.2020 12:00", "18.03.2020 09:00", ExpectedResult = 360)]
        [TestCase("20.03.2020 20:00", "23.03.2020 08:31", ExpectedResult = 1)]
        [TestCase("20.03.2020 20:00", "23.03.2020 14:30", ExpectedResult = 360)]
        [TestCase("20.03.2020 12:00", "23.03.2020 09:00", ExpectedResult = 360)]
        [TestCase("20.03.2020 12:00", "25.03.2020 09:00", ExpectedResult = 1440)]
        [Test]
        public int TestDiff(string start, string end)
        {
            CultureInfo provider = CultureInfo.InvariantCulture;
            var s = DateTime.ParseExact(start, "dd.MM.yyyy HH:mm", provider);
            var e = DateTime.ParseExact(end, "dd.MM.yyyy HH:mm", provider);
            var ts = new TimeSpan(0);
            WCalendar wc = new WCalendar();
            var result = wc.DateDiff(s,e, ts);
            return (int)result.TotalMinutes;
        }
    }
}