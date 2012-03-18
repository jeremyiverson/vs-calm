using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace VSCalm.Utility
{
	public static class UIHelper
	{
		public static IEnumerable<DependencyObject> FindVisualChildren(DependencyObject depObj, string typeName)
		{
			if (depObj != null)
			{
				for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
				{
					DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
					if (child != null && child.GetType().Name.Contains(typeName))
					{
						yield return child;
					}

					foreach (var childOfChild in FindVisualChildren(child, typeName))
					{
						yield return childOfChild;
					}
				}
			}
		}

		public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
		{
			if (depObj != null)
			{
				for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
				{
					DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
					if (child != null && child is T)
					{
						yield return (T)child;
					}

					foreach (T childOfChild in FindVisualChildren<T>(child))
					{
						yield return childOfChild;
					}
				}
			}
		}

		public static string ToTitleCase(this string str)
		{
			// ToTitleCase leaves ALLCAPS text as all caps. So, convert it to lower case first.
			return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
		}
	}
}
