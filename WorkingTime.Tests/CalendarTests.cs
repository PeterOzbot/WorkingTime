using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WorkingTime.Library.Calendar;

namespace WorkingTime.Tests {
    [TestClass]
    public class CalendarTests {

        [TestMethod]
        public void MonthIndexTest() {
            Calendar calendar = new Calendar();
            Year sampleYear = calendar[2015];

            int index = 1;
            foreach (Month month in sampleYear) {
                Assert.AreEqual(index, month.Index);
                index++;
            }
        }

        [TestMethod]
        public void MonthLabelTest() {
            Calendar calendar = new Calendar();
            Year sampleYear = calendar[2015];

            foreach (Month month in sampleYear) {
                Assert.IsNotNull(month.Label);
            }
        }

        [TestMethod]
        public void MonthDaysTest() {
            Calendar calendar = new Calendar();
            Year sampleYear = calendar[2015];

            foreach (Month month in sampleYear) {
                foreach (Day day in month) {
                    Assert.IsNotNull(day);
                    Assert.IsNotNull(day.Date);
                }
            }
        }

        [TestMethod]
        public void RandomDayTest() {
            Calendar calendar = new Calendar();
            Year sampleYear = calendar[2015];

            Assert.AreEqual(new DateTime(2015, 1, 1), sampleYear.January[1].Date);
            Assert.AreEqual(new DateTime(2015, 2, 20), sampleYear.February[20].Date);
            Assert.AreEqual(new DateTime(2015, 4, 8), sampleYear.April[8].Date);
            Assert.AreEqual(new DateTime(2015, 8, 12), sampleYear.August[12].Date);
            Assert.AreEqual(new DateTime(2015, 10, 20), sampleYear.October[20].Date);
        }
    }
}
