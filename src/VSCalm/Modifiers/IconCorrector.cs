using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using VSCalm.Utility;

namespace VSCalm.Modifiers
{
    public class IconCorrector
    {
        public static void FixIcons()
        {
            foreach (Image image in UIHelper.FindVisualChildren<Image>(Application.Current.MainWindow))
            {
                var test = image.Source;
            }
        }
    }
}
