using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml.Linq;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WorkingTime.Library.Framework;

namespace WorkingTime.Tests {
    [TestClass]
    public class XMLReaderTests {
        private string _folderName = "TestData";

        /// <summary>
        /// Just some simple object used for testing
        /// </summary>
        public class TestObject {
            public string Label { get; set; }
            public DateTime Date { get; set; }
        }

        /// <summary>
        /// Tests if object is saved to file on disk in xml.
        /// </summary>
        [TestMethod]
        public void SaveTest() {
            // create sample data
            List<TestObject> data = new List<TestObject>();
            data.Add(new TestObject() { Label = "FIRST", Date = DateTime.UtcNow.Date });
            data.Add(new TestObject() { Label = "SECOND", Date = DateTime.UtcNow.AddDays(1).Date });
            data.Add(new TestObject() { Label = "THIRD", Date = DateTime.UtcNow.AddDays(2).Date });

            // initilaize reader
            string folderPath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            folderPath = Path.Combine(folderPath, _folderName);
            XMLReaderWriter<List<TestObject>> xmlReader = new XMLReaderWriter<List<TestObject>>("XMLReaderTestFileSave.xml", _folderName);

            // save
            xmlReader.Save(data);

            // file path + name
            string filePath = Path.Combine(folderPath, "XMLReaderTestFileSave.xml");
            // check if file exists
            Assert.IsTrue(File.Exists(filePath));
            // load
            XDocument document = XDocument.Load(filePath);
            XElement xElement = document.Elements().First();

            // check for data inside
            Assert.AreEqual(3, xElement.Elements().Count());
            for (int index = 0 ; index < 3 ; index++) {
                Assert.AreEqual(data[index].Date.Date, Convert.ToDateTime(xElement.Elements().ElementAt(index).Element("Date").Value).Date);
                Assert.AreEqual(data[index].Label, xElement.Elements().ElementAt(index).Element("Label").Value.ToString());
            }
        }


        /// <summary>
        /// Tests if object is loaded from xml.
        /// </summary>
        [TestMethod]
        public void ReadTest() {
            // create sample data
            List<TestObject> data = new List<TestObject>();
            data.Add(new TestObject() { Label = "FIRST", Date = DateTime.UtcNow.Date });
            data.Add(new TestObject() { Label = "SECOND", Date = DateTime.UtcNow.AddDays(1).Date });
            data.Add(new TestObject() { Label = "THIRD", Date = DateTime.UtcNow.AddDays(2).Date });

            // initilaize reader
            XMLReaderWriter<List<TestObject>> xmlReader = new XMLReaderWriter<List<TestObject>>("XMLReaderTestFileRead.xml", _folderName);

            // save
            xmlReader.Save(data);

            // now load the data
            List<TestObject> readData = xmlReader.Read();

            // check for data inside
            Assert.AreEqual(3, readData.Count());
            for (int index = 0 ; index < 3 ; index++) {
                Assert.AreEqual(data[index].Date.Date, readData[index].Date.Date);
                Assert.AreEqual(data[index].Label, readData[index].Label);
            }
        }
    }
}
