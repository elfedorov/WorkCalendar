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
        public string Test1(string date,int addTime)
        {
            CultureInfo provider = CultureInfo.InvariantCulture;
            var d = DateTime.ParseExact(date,"dd.MM.yyyy HH:mm", provider);
            Console.WriteLine(d.ToString("dd.MM.yyyy HH:mm"));
            var time = new TimeSpan(0, addTime, 0);
            WCalendar wc = new WCalendar();
            var result = wc.AddWorkTime(d, time);
            return result.ToString("dd.MM.yyyy HH:mm");
        }
    }
}