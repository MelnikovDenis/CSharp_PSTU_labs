using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lab15
{
    /// <summary>
    /// График функции
    /// </summary>
    internal class FunctionGraph : DependencyObject, INotifyPropertyChanged
    {   
        public static readonly DependencyProperty PointsProperty = DependencyProperty.Register(
            "Points",
            typeof(ObservableCollection<Point>),
            typeof(MainWindow),
            new PropertyMetadata(
                default(ObservableCollection<Point>)
                )
            );
        /// <summary>
        /// Список точек
        /// </summary>
        public ObservableCollection<Point> Points
        {
            get { return (ObservableCollection<Point>)GetValue(PointsProperty); }
            set { 
                SetValue(PointsProperty, value); 
                OnPropertyChanged("Points");
            }
        }    
        public static readonly DependencyProperty AccuracyProperty = DependencyProperty.Register(
          "Accuracy", 
          typeof(double), 
          typeof(MainWindow), 
          new PropertyMetadata(0.01d)
            );
        /// <summary>
        /// Точность (шаг)
        /// </summary>
        public double Accuracy
        {
            get { return (double)GetValue(AccuracyProperty); }
            set { 
                SetValue(AccuracyProperty, value);
                OnPropertyChanged("Accuracy");
            }
        }    
        public static readonly DependencyProperty RangeToProperty = DependencyProperty.Register(
        "RangeTo", typeof(double), typeof(MainWindow), new PropertyMetadata(5d));
        /// <summary>
        /// Диапозон значений
        /// </summary>

        public double RangeTo
        {
            get { return (double)GetValue(RangeToProperty); }
            set { 
                SetValue(RangeToProperty, value);
                OnPropertyChanged("RangeTo");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
