using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
	public static class AppData
	{
		/// <summary>
		/// This is the top most level of the application, and is set from the App.xaml.cs file at runtime. To set this,
		/// create an event handler for the "startup" event on the "application" markup of the app.xaml file. Then, in
		/// app.xaml.cs method that you just created, add these lines of code:
		/// string topFolder = "";
		/// string[] pathToDebug = AppDomain.CurrentDomain.BaseDirectory.Split('\\');
		/// for (int i = 0; i < pathToDebug.Length - 4; i++)
		/// {topFolder += pathToDebug[i] + '\\';}
		/// DataObjects.AppData.TopRuntimeDirectory = topFolder;
		/// </summary>
		public static string TopRuntimeDirectory { get; set; }
	}
}
