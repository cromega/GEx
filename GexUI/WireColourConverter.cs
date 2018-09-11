using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace GexUI {
    class WireColourConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var isWireSelected = (bool)value;
            var highlightedBrush = new SolidColorBrush(Colors.Yellow);
            var normalBrush = new SolidColorBrush(Colors.Black);

            return isWireSelected ? highlightedBrush : normalBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
