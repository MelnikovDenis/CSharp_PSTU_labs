using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Lab15
{
    /// <summary>
    /// Конвертер для перевода просчитанных точек в фигуру.
    /// </summary>
    public class PointsToPathConverter : IMultiValueConverter
    {

        #region Implementation of IMultiValueConverter

        public object? Convert(object?[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var points = values[0] as IEnumerable<Point>; //получаем список точек
            if (points is null)
                return null;
            var w = (double)(values[1] ?? 0); //получаем ширину холста
            var h = (double)(values[2] ?? 0); //получаем высоту холста
            var pg = new PathGeometry(); //геометрия, которую будем возвращать
            var ps = new List<PathSegment>(); //набор сегментов пути
                                             
            var rangeX = points.Max(p => p.X) - points.Min(p => p.X); //размах значений по X
            
            var rangeY = points.Max(p => p.Y) - points.Min(p => p.Y); //размах значений по Y
            
            var scaleX = w / rangeX; //масштаб по X
            
            var scaleY = h / rangeY; //масштаб по Y
            scaleX = scaleY = 1;

            points = from point in points 
                     where (Math.Abs(point.X * scaleX) < (w / 2)) && (Math.Abs(point.Y * scaleY * -1) < (h / 2)) 
                     select new Point(point.X * scaleX, point.Y * scaleY * -1); //пересчёт точек
            
            ps.Add(new PolyLineSegment(points, true)); //по точкам добавляем сегменты пути
            
            var pf = new PathFigure(points.First(), ps, false); //из сегментов пути строим фигуру с первой точкой вначале
            
            pg.Figures.Add(pf); //добавляем фигуру в геометрию
          
            return pg; //возвращаем геометрию
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
