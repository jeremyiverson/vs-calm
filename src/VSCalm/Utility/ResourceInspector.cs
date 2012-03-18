using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VSCalm.Utility
{
	public class ResourceInspector
	{
		public static void InspectResources()
		{
			var app = Application.Current;

			foreach (ResourceDictionary dict in app.Resources.MergedDictionaries)
			{
				foreach (object key in dict.Keys)
				{
					if (key.ToString().StartsWith("VsColor"))
					{
						Debug.WriteLine(string.Format("{0}: {1}", key, dict[key]));
					}
				}
			}
		}
	}
}
