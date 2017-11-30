using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Threading;

namespace TestWindow
{
    public class MainWindowView
    {
        public DispatcherTimer timer = new DispatcherTimer();
        public MainWindowView()
        {
            var line1 = new LineGraph();
            line1.TitleDescription = "Line1";
            line1.PointData = new Point(0, 0);
            LineGraphs.Add(line1);

            var line2 = new LineGraph();
            line2.TitleDescription = "Line2";
            line2.PointData = new Point(0, 0);
            LineGraphs.Add(line2);

            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler(AnimatedPlot);
            timer.Start();
        }
        private ObservableCollection<LineGraph> lineGraphs;
        [System.ComponentModel.BrowsableAttribute(false)]
        public ObservableCollection<LineGraph> LineGraphs
        {
            get
            {
                if (this.lineGraphs == null)
                {
                    this.lineGraphs = new ObservableCollection<LineGraph>();
                }

                return this.lineGraphs;
            }
            set
            {
                lineGraphs = value;

            }
        }
        private double i = 0;
        private void AnimatedPlot(object sender, EventArgs e)
        {
            foreach (var item in LineGraphs)
            {
                var rd = new Random();
                double y = rd.Next(0,20);
                item.PointData = new Point(i, y);

            }
            i++;

        }
    }


    public class LineGraph : INotifyPropertyChanged
    {
        private string _titleDescription;
        public string TitleDescription
        {

            get
            {
                return this._titleDescription;
            }
            set
            {
                this._titleDescription = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("TitleDescription"));
            }
        }

        private Point _pointData;

        public event PropertyChangedEventHandler PropertyChanged;

        public Point PointData
        {
            get
            {
                return this._pointData;
            }
            set
            {
                this._pointData = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("PointData"));
            }
        }
    }
}
