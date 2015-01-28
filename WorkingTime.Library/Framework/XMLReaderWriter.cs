using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace WorkingTime.Library.Framework {
    /// <summary>
    /// Used to read XML from file on disk and save the XML to the file.
    /// </summary>
    public class XMLReaderWriter<T> where T : class {
        private string _fileName;
        private string _folderName;
        private string _path;



        /// <summary>
        /// Creates a new instance of the  <see cref="XMLReaderWriter"/> class.
        /// </summary>
        public XMLReaderWriter(string fileName, string folderName) {
            if (string.IsNullOrEmpty(fileName)) { throw new ArgumentNullException("fileName"); }
            if (string.IsNullOrEmpty(folderName)) { throw new ArgumentNullException("folderName"); }

            _fileName = fileName;
            _folderName = folderName;

            string folderPath = Path.Combine(GetLocationPath(), _folderName);
            _path = System.IO.Path.Combine(folderPath, _fileName);
        }



        /// <summary>
        /// Reads the XML and creates T object.
        /// </summary>
        public T Read() {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            if (File.Exists(_path)) {
                StreamReader reader = new StreamReader(_path);
                object deserialized = serializer.Deserialize(reader.BaseStream);

                return deserialized as T;
            }
            else { return null; }

        }

        /// <summary>
        /// Saves the T object into XML and XML is saved to file on disk.
        /// </summary>
        public void Save(T data) {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StreamWriter writer = new StreamWriter(_path)) {
                serializer.Serialize(writer.BaseStream, data);
            }
        }

        /// <summary>
        /// Gets the path to the file.
        /// </summary>
        private string GetLocationPath() {
            // TODO: chage this to use better location
            return System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }
    }
}
