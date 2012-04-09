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
using VSCalm.Utility;

namespace VSCalm
{
	public class Calm
	{
		public void CalmDown()
		{
			foreach (DependencyObject window in Application.Current.Windows.OfType<DependencyObject>())
			{
				// Find hidden (docked) tool windows.
				var hiddenTabs = UIHelper.FindVisualChildren(window, "AutoHideTabItem");
				foreach (var tab in hiddenTabs)
				{
					var textBlock = UIHelper.FindVisualChildren<TextBlock>(tab).FirstOrDefault();
					if (textBlock != null)
					{
						RemoveOutrageousConverter(textBlock);
					}
				}

				// Find expanded tool windows.
				var expandedTabs = UIHelper.FindVisualChildren(window, "DragUndockHeader");
				foreach (var tab in expandedTabs)
				{
					var textBlock = UIHelper.FindVisualChildren<TextBlock>(tab).FirstOrDefault();
					if (textBlock != null)
					{
						RemoveOutrageousConverter(textBlock);
					}

					// :::: Hide the colon-like grippers :::::::
					var rectangles = UIHelper.FindVisualChildren<Rectangle>(tab);
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

}
