using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
            List<Point> points = new List<Point>();
            
            for(double x = graph.RangeTo * -1; x <= graph.RangeTo; x += graph.Accuracy) 
            {
                points.Add(new Point(x, 23d * Math.Pow(x, 2) - 32));
            }
            
            graph.Points = new ObservableCollection<Point>(points);
        }
    }
}
