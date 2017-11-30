using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dynamic.Control
{
    public class DynamicChart : Microsoft.Research.DynamicDataDisplay.ChartPlotter
    {
        //double[] animatedX = new double[] { 0,1,2,3,4,5,6,7,8,9};
        //double[] animatedY = new double[10];
        //EnumerableDataSource<double> animatedDataSource = null;

        private bool isFirstRun = true;
        static DynamicChart()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DynamicChart), new FrameworkPropertyMetadata(typeof(DynamicChart)));

        }
        public DynamicChart()
        {
            this.Loaded += new RoutedEventHandler(DynamicChart_Loaded);
        }

        void DynamicChart_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.isFirstRun == true)
            {
                if (PointDataSource == null)
                    PointDataSource = new ObservableDataSource<Point>();
                               


                this.AddLineGraph(PointDataSource, 3.0, this.Description);
                this.Viewport.FitToView();
                this.isFirstRun = false;
            }

        }

        public Point NewPoint
        {
            get { return (Point)GetValue(NewPointProperty); }
            set { SetValue(NewPointProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NewPoint.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NewPointProperty =
            DependencyProperty.Register("NewPoint", typeof(Point), typeof(DynamicChart), new PropertyMetadata(new Point(0, 0), new PropertyChangedCallback(ChangePointDataSource)));




        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Description.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register("Description", typeof(string), typeof(DynamicChart), new PropertyMetadata("test", new PropertyChangedCallback(ChangeDescription)));




        public ObservableDataSource<Point> PointDataSource
        {
            get { return (ObservableDataSource<Point>)GetValue(PointDataSourceProperty); }
            set { SetValue(PointDataSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PointDataSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PointDataSourceProperty =
            DependencyProperty.Register("PointDataSource", typeof(ObservableDataSource<Point>), typeof(DynamicChart), new PropertyMetadata(null, new PropertyChangedCallback(ChangePointSource)));




        public double LineThickness
        {
            get { return (double)GetValue(LineThicknessProperty); }
            set { SetValue(LineThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LineThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LineThicknessProperty =
            DependencyProperty.Register("LineThickness", typeof(double), typeof(DynamicChart), new PropertyMetadata(1.0));

        public static void ChangeDescription(DependencyObject source, DependencyPropertyChangedEventArgs eventArgs)
        {
            (source as DynamicChart).UpdateDescription((string)eventArgs.NewValue);
        }
        public static void ChangePointSource(DependencyObject source, DependencyPropertyChangedEventArgs eventArgs)
        {
            (source as DynamicChart).UpdatePointDataSource((ObservableDataSource<Point>)eventArgs.NewValue);
        }
        private void UpdateDescription(string NewDescription)
        {

            this.Description = NewDescription;

        }
        private void UpdatePointDataSource(ObservableDataSource<Point> NewDescription)
        {

            this.PointDataSource = NewDescription;

        }

        public static void ChangePointDataSource(DependencyObject source, DependencyPropertyChangedEventArgs eventArgs)
        {
            (source as DynamicChart).UpdatePointData((Point)eventArgs.NewValue);
        }
        private void UpdatePointData(Point data)
        {
            if (PointDataSource == null)
                PointDataSource = new ObservableDataSource<Point>();
            //this.pointdatasource.appendasync(base.dispatcher, data);
            if (PointDataSource.Collection.Count > 60)
            {
                PointDataSource.Collection.RemoveAt(0);
                PointDataSource.ResumeUpdate();
            }
            this.PointDataSource.AppendMany(new List<Point>() { data });

            //int index = (int)(data.X % 10);
            //animatedY[index] = data.Y+1.8;
            //animatedDataSource.RaiseDataChanged();
        }
    }
}
