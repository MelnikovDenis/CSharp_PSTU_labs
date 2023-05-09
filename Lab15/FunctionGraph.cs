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
        /// <summary>
        /// Точность (шаг)
        /// </summary>
        public double Accuracy { get; set; } = 0.01d;

        /// <summary>
        /// Диапозон значений
        /// </summary>
        public double RangeTo { get; set; } = 100d;
        public object _sync = new object();
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
