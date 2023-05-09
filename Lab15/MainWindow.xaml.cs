using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
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

namespace Lab15
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FunctionGraph graph = new FunctionGraph();
        public MainWindow()
        {
            InitializeComponent();           
            this.DataContext = graph;
        }
        private void Start_Button_Click(object sender, RoutedEventArgs e)
        {
            var thread = new Thread(UpdateGraph);
            thread.Start();             
        }
        private void UpdateGraph()
        {
            var counter = 0d;
            while(counter < graph.RangeTo) 
            {
                ++counter;
                List<Point> points = new List<Point>();
                for (double x = counter * -1d; x <= counter; x += graph.Accuracy)
                    points.Add(new Point(x, 23d * Math.Pow(x, 2) - 32));    
                Thread.Sleep(100);
                this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, () => {
                    graph.Points = new ObservableCollection<Point>(points);
                });
            }
            
        }
    }
}
