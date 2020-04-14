﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PiłkarzeMVVM.ModelView
{
    class DoubleToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null) return "";
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString() == "") return null;
            if (double.TryParse(value.ToString(), out _))
            {
                return System.Convert.ToDouble(value.ToString(), CultureInfo.CurrentCulture);
            }
            return null;
        }
    }
}
