using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;


namespace Algenta.VSCalm
{
	public class Calm
	{
		public void CalmDown()
		{
			var mainWindow = Application.Current.MainWindow;
			if (mainWindow != null)
			{
				// Find hidden (docked) tool windows.
				var hiddenTabs = Utility.FindVisualChildren(mainWindow, "AutoHideTabItem");
				foreach (var tab in hiddenTabs)
				{
					var textBlock = Utility.FindVisualChildren<TextBlock>(tab).FirstOrDefault();
					if (textBlock != null)
					{
						RemoveOutrageousConverter(textBlock);
					}
				}

				// Find expanded tool windows.
				var expandedTabs = Utility.FindVisualChildren(mainWindow, "DragUndockHeader");
				foreach (var tab in expandedTabs)
				{
					var textBlock = Utility.FindVisualChildren<TextBlock>(tab).FirstOrDefault();
					if (textBlock != null)
					{
						RemoveOutrageousConverter(textBlock);
					}

					// :::: Hide the colon-like grippers :::::::
					var rectangles = Utility.FindVisualChildren<Rectangle>(tab);
					foreach (var r in rectangles)
					{
						r.Visibility = Visibility.Collapsed;
					}
				}
			}
		}

		/// <summary>
		/// Removes the <c>Microsoft.VisualStudio.Platform.WindowManagement.StringUppercaseConverter</c> Converter
		/// from the binding.
		/// </summary>
		/// <remarks>
		/// Seriously.
		/// </remarks>
		/// <param name="textBlock"></param>
		private static void RemoveOutrageousConverter(TextBlock textBlock)
		{
			
			BindingExpression bindingExpression = textBlock.GetBindingExpression(TextBlock.TextProperty);
			if (bindingExpression != null)
			{
				Binding binding = bindingExpression.ParentBinding;
				if (binding != null)
				{
					// A binding's converter can't be changed after it has been used.
					// Create a new binding without a converter at all.
					Binding betterBinding = new Binding();
					betterBinding.Path = binding.Path;
					textBlock.SetBinding(TextBlock.TextProperty, betterBinding);
				}
			}

			
		}		
	}

	public static class Utility
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
			return new CultureInfo("en-US", false).TextInfo.ToTitleCase(str.ToLower());
		}
	}
}
