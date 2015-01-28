using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Practices.Prism.Commands;

namespace WorkingTime {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();

            DataContext = new ViewModel();


            Closing += MainWindow_Closing;
        }

        void MainWindow_Closing(object sender, CancelEventArgs e) {
            (DataContext as ViewModel).Save();
        }
    }

    public class ViewModel : INotifyPropertyChanged {
        public Month Month { get; set; }

        public ICommand AddWeek { get; set; }

        public double Average { get; set; }
        public double Surplus { get; set; }
        public ICommand Calculate { get; set; }


        public ViewModel() {
            Month = new Month();
            AddWeek = new DelegateCommand(Month.AddWeek);
            Calculate = new DelegateCommand(CalculateNow);
        }

        private void CalculateNow() {
            Average = 0;
            Surplus = 0;
            int workingDays = 0;
            foreach (Week week in Month.Weeks) {
                week.Calculate.Execute(null);

                Average = Average + week.Totalhours;
                Surplus = Surplus + week.Surplus;

                workingDays = workingDays + week.WorkingDays;
            }

            Average = Average / workingDays;

            OnPropertyChanged("Surplus");


            OnPropertyChanged("Average");
        }

        public void Save() {
            var dir = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var path = System.IO.Path.Combine(dir, "Mesec.xml");

            using (XmlWriter writer = XmlWriter.Create(path)) {
                writer.WriteStartDocument();
                writer.WriteStartElement("Mesec");

                foreach (Week week in Month.Weeks) {
                    writer.WriteStartElement("Teden");

                    // dan
                    writer.WriteStartElement("Dan");

                    writer.WriteElementString("Value", week.Monday.ToString());
                    writer.WriteElementString("UseIt", week.UseMonday ? "1" : "0");

                    writer.WriteEndElement();

                    writer.WriteStartElement("Dan");

                    writer.WriteElementString("Value", week.Tuesday.ToString());
                    writer.WriteElementString("UseIt", week.UseTuesday ? "1" : "0");

                    writer.WriteEndElement();

                    writer.WriteStartElement("Dan");

                    writer.WriteElementString("Value", week.Wednesday.ToString());
                    writer.WriteElementString("UseIt", week.UseWednesday ? "1" : "0");

                    writer.WriteEndElement();

                    writer.WriteStartElement("Dan");

                    writer.WriteElementString("Value", week.Thursday.ToString());
                    writer.WriteElementString("UseIt", week.UseThursday ? "1" : "0");

                    writer.WriteEndElement();

                    writer.WriteStartElement("Dan");

                    writer.WriteElementString("Value", week.Friday.ToString());
                    writer.WriteElementString("UseIt", week.UseFriday ? "1" : "0");

                    writer.WriteEndElement();

                    writer.WriteStartElement("Dan");

                    writer.WriteElementString("Value", week.Saturday.ToString());
                    writer.WriteElementString("UseIt", week.UseSaturday ? "1" : "0");

                    writer.WriteEndElement();

                    writer.WriteStartElement("Dan");

                    writer.WriteElementString("Value", week.Sunday.ToString());
                    writer.WriteElementString("UseIt", week.UseSunday ? "1" : "0");

                    writer.WriteEndElement();

                    //konec
                    writer.WriteEndElement();
                }

                writer.WriteEndDocument();
            }
        }


        /// <summary>
        /// PropertyChanged executing helper method.
        /// </summary>
        private void OnPropertyChanged(string propertyName) {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class Month : INotifyPropertyChanged {
        public ObservableCollection<Week> Weeks { get; set; }

        public Month() {
            Weeks = new ObservableCollection<Week>();


            var dir = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var path = System.IO.Path.Combine(dir, "Mesec.xml");

            if (File.Exists(path)) {

                XDocument data = XDocument.Load(path);


                foreach (XElement element in data.Element("Mesec").Elements()) {
                    Weeks.Add(new Week(element));
                }
            }

        }

        public void AddWeek() {
            Weeks.Add(new Week());
            OnPropertyChanged("Weeks");
        }



        /// <summary>
        /// PropertyChanged executing helper method.
        /// </summary>
        private void OnPropertyChanged(string propertyName) {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class Week : INotifyPropertyChanged {
        public double Monday { get; set; }
        public double Tuesday { get; set; }
        public double Wednesday { get; set; }
        public double Thursday { get; set; }
        public double Friday { get; set; }
        public double Saturday { get; set; }
        public double Sunday { get; set; }

        public bool UseMonday { get; set; }
        public bool UseTuesday { get; set; }
        public bool UseWednesday { get; set; }
        public bool UseThursday { get; set; }
        public bool UseFriday { get; set; }
        public bool UseSaturday { get; set; }
        public bool UseSunday { get; set; }


        public double Average { get; set; }
        public double Surplus { get; set; }
        public ICommand Calculate { get; set; }

        public int WorkingDays { get; set; }
        public double Totalhours { get; set; }


        public Week(XElement element) {
            int dayIndex = 0;
            foreach (XElement day in element.Elements()) {
                dayIndex++;

                XElement value = day.Element("Value");
                XElement useIt = day.Element("UseIt");

                switch (dayIndex) {
                    case 1:
                        Monday = Convert.ToDouble(value.Value);
                        UseMonday = Convert.ToBoolean(Convert.ToInt32(useIt.Value));
                        break;
                    case 2:
                        Tuesday = Convert.ToDouble(value.Value);
                        UseTuesday = Convert.ToBoolean(Convert.ToInt32(useIt.Value));
                        break;
                    case 3:
                        Wednesday = Convert.ToDouble(value.Value);
                        UseWednesday = Convert.ToBoolean(Convert.ToInt32(useIt.Value));
                        break;
                    case 4:
                        Thursday = Convert.ToDouble(value.Value);
                        UseThursday = Convert.ToBoolean(Convert.ToInt32(useIt.Value));
                        break;
                    case 5:
                        Friday = Convert.ToDouble(value.Value);
                        UseFriday = Convert.ToBoolean(Convert.ToInt32(useIt.Value));
                        break;
                    case 6:
                        Saturday = Convert.ToDouble(value.Value);
                        UseSaturday = Convert.ToBoolean(Convert.ToInt32(useIt.Value));
                        break;
                    case 7:
                        Sunday = Convert.ToDouble(value.Value);
                        UseSunday = Convert.ToBoolean(Convert.ToInt32(useIt.Value));
                        break;
                }
            }

            CalculateNow();

            Calculate = new DelegateCommand(CalculateNow);
        }
        public Week() {

            Calculate = new DelegateCommand(CalculateNow);
            UseMonday = UseTuesday = UseWednesday = UseThursday = UseFriday = true;

        }

        public void CalculateNow() {
            int useDaysCount = 0;
            if (UseMonday) useDaysCount++;
            if (UseTuesday) useDaysCount++;
            if (UseWednesday) useDaysCount++;
            if (UseThursday) useDaysCount++;
            if (UseFriday) useDaysCount++;
            if (UseSaturday) useDaysCount++;
            if (UseSunday) useDaysCount++;



            double totalTime = useDaysCount * 8;
            double realTime = Monday + Tuesday + Wednesday + Thursday + Friday + Saturday + Sunday;

            Surplus = realTime - totalTime;
            OnPropertyChanged("Surplus");

            Average = realTime / useDaysCount;
            OnPropertyChanged("Average");

            WorkingDays = useDaysCount;
            Totalhours = realTime;
        }

        /// <summary>
        /// PropertyChanged executing helper method.
        /// </summary>
        private void OnPropertyChanged(string propertyName) {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
